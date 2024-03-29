﻿using Microsoft.AspNetCore.Http;
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
                var actionTypeName = InstanceManager.GetActionTypeName(dataModel);
                var errorParameters =await ValidateParameters.CheckAsync(dataModel);
                object? errorData = await ErrorParameterHelper.GetErrorParameters(errorParameters);
                if (ErrorParameterHelper.IsError(errorData))
                {
                    return errorData;
                }
                else
                {
                    var actionType = Type.GetType(actionTypeName);
                    InstanceManager.CheckActionType(dataModel, actionType);
                    var actionInstance = (IDataService)Activator.CreateInstance(actionType);
                    var result = await actionInstance.ExecuteAction(dataModel);
                    return result;
                }
            }
            catch (Exception ex)
            { 
                if (ex.InnerException != null) 
                {
                    Console.WriteLine(ex.InnerException.Message);
                    throw ex.InnerException;
                } 
                else
                {
                    Console.WriteLine(ex.Message);
                    throw;
                } 
            }
        }

    }
}