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
            methodInfo.GetParameters().ToList().ForEach((p) =>
            {
                object? value = dataModel?.DefaultParameters?.FirstOrDefault(it => it.Name!.EqualsCase(p.Name)).Value;
                value= UtilMethods.ChangeType2(value, p.ParameterType);
                parameters[p.Position] = value!;
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
    }
}
