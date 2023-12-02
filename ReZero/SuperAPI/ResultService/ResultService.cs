using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class ResultService: IResultService
    {
        public object GetResult(object data, ResultModel result)
        {
            if (result == null||result?.ResultType==null)
            {
                return data;
            }
            else
            {
                return GetResultProvider(data, result);
            }
        }

        private static object GetResultProvider(object data, ResultModel model)
        {
            var actionType = Type.GetType("ReZero.SuperAPI.Items." + model?.ResultType);
            var actionInstance = (IResultService)Activator.CreateInstance(actionType);
            var result = actionInstance.GetResult(data, model!);
            return result;
        }
    }
}
