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
            var methodInfo = classType.GetMyMethod(dataModel?.MyMethodInfo?.MethodName, dataModel!.MyMethodInfo!.MethodArgsCount);
            var classObj = Activator.CreateInstance(classType, nonPublic: true);
            object[] parameters = new object[methodInfo.GetParameters().Length];
            var argsTypes = dataModel.MyMethodInfo.ArgsTypes;
            if (IsJObject(dataModel, parameters))
            {
                FillJObjectParameters(dataModel, methodInfo, parameters, argsTypes);
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
                value = UtilMethods.ChangeType2(value, p.ParameterType);
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
