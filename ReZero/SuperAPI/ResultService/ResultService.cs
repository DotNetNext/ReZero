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

        private static object GetResultProvider(object data, ResultModel result)
        {
            var actionType = Type.GetType("ReZero.SuperAPI.Items." + result?.ResultType);
            var actionInstance = (IResultService)Activator.CreateInstance(actionType);
            var result2 = actionInstance.GetResult(data, result ?? new ResultModel());
            return data;
        }
    }
}
