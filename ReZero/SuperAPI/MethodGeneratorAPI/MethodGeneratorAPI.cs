using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var methodInfo = classType.GetMyMethod(dataModel?.MyMethodInfo?.MethodName, dataModel!.MyMethodInfo!.MethodArgsCount);
            var classObj = ReZero.DependencyInjection.ActivatorHelper.CreateInstance(classType, nonPublic: true);
            object[] parameters = new object[methodInfo.GetParameters().Length];
            var argsTypes = dataModel.MyMethodInfo.ArgsTypes;
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
            var result = methodInfo.Invoke(classObj, parameters);
            if (result is Task)
            {
                throw new NotSupportedException();
            }
            else
            {
                return await Task.FromResult(result);
            }
        }

        private static object[] FillSingleModelParameters(DataModel dataModel, MethodInfo methodInfo)
        {
            object[] parameters;
            var type = methodInfo.GetParameters().First().ParameterType;
            var parameterOjb = Activator.CreateInstance(type, nonPublic: true);
            foreach (var item in type!.GetProperties())
            {
                var p = dataModel.DefaultParameters.First(it => it.Name == item.Name);
                item.SetValue(parameterOjb, UtilMethods.ChangeType2(p.Value, item.PropertyType));
            }
            parameters = new object[] { parameterOjb };
            return parameters;
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

        private static bool IsObject(object? value, Type type)
        {
            return (type.IsArray || type.FullName.StartsWith("System.Collections.Generic.List")) && value != null;
        }
    }
}
