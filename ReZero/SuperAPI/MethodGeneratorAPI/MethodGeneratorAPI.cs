using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReZero.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI 
{
    internal class MethodGeneratorAPI : IDataService
    {
        private ISqlSugarClient db;
        public MethodGeneratorAPI()
        {
            db = App.Db;
        }
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            if (dataModel.MyMethodInfo == null) return null;

            var classType = Type.GetType(dataModel.MyMethodInfo?.MethodClassFullName);
            classType = GetTypeByAttribute(dataModel, classType);
            if (IsDynamicApiEntry(dataModel, classType))
            {
                classType = GetDynamicApiEntryType(dataModel);
            }
            var methodInfo = classType.GetMyMethod(dataModel?.MyMethodInfo?.MethodName, dataModel!.MyMethodInfo!.MethodArgsCount);
            var classObj = ReZero.DependencyInjection.ActivatorHelper.CreateInstance(classType!, nonPublic: true, (ServiceProvider)dataModel.ServiceProvider!);
            object[] parameters = new object[methodInfo.GetParameters().Length];
            var argsTypes = dataModel.MyMethodInfo.ArgsTypes;
            parameters = GetParameters(dataModel, methodInfo, parameters, argsTypes);
            var unitType = methodInfo.GetCustomAttributes().FirstOrDefault(it => it.GetType().GetInterfaces().Any(s => s == typeof(IUnitOfWork)));
            if (unitType != null)
            {
                var unitObj = (IUnitOfWork)ReZero.DependencyInjection.ActivatorHelper.CreateInstance(unitType!.GetType(), nonPublic: true, (ServiceProvider)dataModel.ServiceProvider!);
                unitObj.db = ((ServiceProvider)dataModel.ServiceProvider!).GetRequiredService<ISqlSugarClient>();
                try
                {
                    unitObj.BeginTran();
                    object result = new object();
                    result = await ExecuteMethodAsync(methodInfo, classObj, parameters);
                    unitObj.CommitTran();
                    return result;
                }
                catch (Exception)
                {
                    unitObj.RollbackTran();
                    throw;
                }
            }
            else
            {
                object result = await ExecuteMethodAsync(methodInfo, classObj, parameters);
                return result;
            }
        }

        private Type? GetDynamicApiEntryType(DataModel dataModel)
        {
            // Add new assembly to ApplicationPartManager
            var partManager = DependencyResolver.GetRequiredService<ApplicationPartManager>();
            // Find and remove the corresponding AssemblyPart
            var partToRemove = FindAssemblyPartToRemove(dataModel, partManager);
            if (partToRemove == null)
            {
                var loader = new CodeAnalysisControllerLoader();
                var id =Convert.ToInt64(dataModel!.AssemblyName!.Replace(nameof(CodeAnalysisControllerLoader),string.Empty));
                var zeroInterfaceList=App.Db.Queryable<ZeroInterfaceList>().Where(it => it.Id == id).First();
                loader.LoadController(zeroInterfaceList);
                partToRemove= FindAssemblyPartToRemove(dataModel, partManager);
            }
            var result = partToRemove.Assembly.GetType(PubConst.Common_DynamicApiEntry);
            return result;
        }

        private static AssemblyPart FindAssemblyPartToRemove(DataModel dataModel, ApplicationPartManager partManager)
        {
            return partManager.ApplicationParts
                            .OfType<AssemblyPart>()
                            .FirstOrDefault(p => p.Assembly.GetName().Name == dataModel.AssemblyName);
        }

        private static bool IsDynamicApiEntry(DataModel dataModel, Type? classType)
        {
            return classType == null && dataModel?.MyMethodInfo?.MethodClassFullName == PubConst.Common_DynamicApiEntry;
        }

        private object[] GetParameters(DataModel dataModel, MethodInfo methodInfo, object[] parameters, Type[]? argsTypes)
        {
            if (IsJObject(dataModel, parameters))
            {
                FillJObjectParameters(dataModel, methodInfo, parameters, argsTypes);
            }
            else if (IsSingleModel(dataModel))
            {
                parameters = FillSingleModelParameters(dataModel, methodInfo);
            }
            else
            {
                FillDefaultParameters(dataModel, methodInfo, parameters, argsTypes);
            }

            return parameters;
        }

        private static async Task<object> ExecuteMethodAsync(MethodInfo methodInfo, object classObj, object[] parameters)
        {
            var result = methodInfo.Invoke(classObj, parameters);
            if (result is Task)
            {
                result = await GetTask((Task)result);
            }
            else
            {
                result = await Task.FromResult(result);
            }

            return result;
        }

        private static object[] FillSingleModelParameters(DataModel dataModel, MethodInfo methodInfo)
        {
            object[] parameters;
            var type = methodInfo.GetParameters().First().ParameterType;
            var parameterOjb = Activator.CreateInstance(type, nonPublic: true);
            foreach (var item in type!.GetProperties())
            {
                var p = dataModel.DefaultParameters.First(it => it.Name == item.Name);
                p.Value = ConvetEmptyValue(item.PropertyType, p.Value);
                if (IsJson(item, p))
                {
                    item.SetValue(parameterOjb,JsonConvert.DeserializeObject(p.Value + "",item.PropertyType));
                }
                else
                {
                    item.SetValue(parameterOjb, UtilMethods.ChangeType2(p.Value, item.PropertyType));
                }
            }
            parameters = new object[] { parameterOjb };
            return parameters;
        }

        private static bool IsJson(PropertyInfo item, DataModelDefaultParameter p)
        {
            if (item.PropertyType?.FullName?.StartsWith("System.Collections.Generic.List")==true) 
            {
                var value2 = p.Value?.ToString()?.Trim();
                return value2?.StartsWith("[") == true && value2?.EndsWith("]") == true;
            }
            if (item.PropertyType?.FullName?.StartsWith("System.")==true) 
            {
                return false;
            }
            var value = p.Value?.ToString()?.Trim();
            return value?.StartsWith("{") == true && value?.EndsWith("}") == true;
        }

        private static bool IsSingleModel(DataModel dataModel)
        {
            return dataModel.MyMethodInfo?.ArgsTypes?.Any(it => typeof(SingleModel) == it) == true;
        }

        private static Type? GetTypeByAttribute(DataModel dataModel, Type? classType)
        {
            if (classType == null)
            {
                var ass = SuperAPIModule._apiOptions?.DependencyInjectionOptions?.Assemblies;
                if (ass?.Any() == true)
                {
                    classType = ass.Select(it => it.GetType(dataModel.MyMethodInfo?.MethodClassFullName)).Where(it => it != null).FirstOrDefault();
                }
            } 
            return classType;
        }

        private void FillJObjectParameters(DataModel dataModel, MethodInfo methodInfo, object[] parameters, Type[]? argsTypes)
        {
            var value = dataModel?.DefaultParameters?.FirstOrDefault()?.Value! + "";
            var type = methodInfo.GetParameters().First().ParameterType;
            if (!string.IsNullOrEmpty(value))
            {
                parameters[0] = JsonConvert.DeserializeObject(value, type)!;
                if (parameters[0] is SaveInterfaceListModel saveInterfaceListModel) 
                {
                    saveInterfaceListModel.InterfaceCategoryId =Convert.ToInt64( Convert.ToDouble(saveInterfaceListModel.InterfaceCategoryId)) + "";
                }
            }
        }

        private static bool IsJObject(DataModel dataModel, object[] parameters)
        {
            return parameters.Count() == 1 && dataModel.DefaultParameters.First().ValueType == nameof(JObject)&& dataModel.DefaultParameters.First().IsSingleParameter==true;
        }

        private static int FillDefaultParameters(DataModel dataModel, MethodInfo methodInfo, object[] parameters, Type[]? argsTypes)
        {
            var index = 0;
            methodInfo.GetParameters().ToList<System.Reflection.ParameterInfo>().ForEach((p) =>
            {
                object? value = dataModel?.DefaultParameters?.FirstOrDefault(it => it.Name!.EqualsCase(p.Name)).Value;
                if (argsTypes?.Length - 1 >= index)
                {
                    var type = argsTypes![index];
                    if (IsObject(value, type))
                    {
                        value = JsonConvert.DeserializeObject(value + "", type);
                    }
                }
                try
                {
                    value = ConvetEmptyValue(p.ParameterType, value);
                    value = UtilMethods.ChangeType2(value, p.ParameterType);
                }
                catch (Exception)
                {
                    throw new Exception(TextHandler.GetCommonText(p.Name+"参数类型不匹配 "+value, p.Name + " Parameter type does not match " + value));
                }
                parameters[p.Position] = value!;
                index++;
            });
            return index;
        }

        private static object? ConvetEmptyValue(Type type, object? value)
        {
            if (value?.Equals("") == true && type != typeof(string))
            {
                value = null;
            }

            return value;
        }
        private static async Task<object> GetTask(Task task)
        {
            await task.ConfigureAwait(false); // 等待任务完成
            var resultProperty = task.GetType().GetProperty("Result");
            var result = resultProperty.GetValue(task);
            return result;
        }
        private static bool IsObject(object? value, Type type)
        {
            return (type.IsArray || type.FullName.StartsWith("System.Collections.Generic.List")) && value != null;
        }
    }
}
