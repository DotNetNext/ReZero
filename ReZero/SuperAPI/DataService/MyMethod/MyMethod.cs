using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI 
{
    internal class MyMethod : IDataService
    {
        private ISqlSugarClient db;
        public MyMethod()
        {
            db = App.Db;
        }
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            if (dataModel.MyMethodInfo == null) return null;

            var classType= Type.GetType(dataModel.MyMethodInfo?.MethodClassFullName);
            var methodInfo=classType.GetMyMethod(dataModel?.MyMethodInfo?.MethodName, dataModel!.MyMethodInfo!.MethodArgsCount);
            var classObj =  Activator.CreateInstance(classType, nonPublic: true);
            object [] parameters = new object[methodInfo.GetParameters().Length];
            var argsTypes=dataModel.MyMethodInfo.ArgsTypes;
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

        private static bool IsObject(object? value, Type type)
        {
            return (type.IsArray || type.FullName.StartsWith("System.Collections.Generic.List")) && value != null;
        }
    }
}
