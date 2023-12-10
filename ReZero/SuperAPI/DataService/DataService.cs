using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using SqlSugar;
using SqlSugar.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    public partial class DataService : IDataService
    {
        public BindHttpParameters BindHttpParameters => new BindHttpParameters();
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            try
            {
                var actionTypeName = GetActionTypeName(dataModel);
                var errorParameters = ValidateParameters.Check(dataModel);
                object? errorData = await ErrorParameterHelper.GetErrorParameters(errorParameters);
                if (ErrorParameterHelper.IsError(errorData))
                {
                    return errorData;
                }
                else
                {
                    var actionType = Type.GetType(actionTypeName);
                    CheckActionType(dataModel, actionType);
                    var actionInstance = (IDataService)Activator.CreateInstance(actionType);
                    var result = await actionInstance.ExecuteAction(dataModel);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
         
        private static string GetActionTypeName(DataModel dataModel)
        {
            return $"ReZero.SuperAPI.{dataModel.ActionType}";
        }

        private static void CheckActionType(DataModel dataModel, Type actionType)
        {
            if (actionType == null)
            {
                throw new ArgumentException($"Invalid ActionType: {dataModel.ActionType}");
            }
        } 
    }
}