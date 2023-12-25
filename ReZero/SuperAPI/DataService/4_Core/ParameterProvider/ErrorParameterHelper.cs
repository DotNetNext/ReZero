using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI 
{
    internal class ErrorParameterHelper
    {
        public static bool IsError(object? errorData)
        {
            return errorData != null;
        }

        public static async Task<object?> GetErrorParameters(List<ErrorParameter> errorParameters)
        {
            object? errorData = null;
            if (errorParameters.Any())
            {
                var data = await Task.FromResult(new
                {
                    ErrorParameters = errorParameters
                });
                errorData = data;
            }

            return errorData;
        }
    }
}
