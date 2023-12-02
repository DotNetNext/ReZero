using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    internal interface IResultService
    {
        object GetResult(object data, ResultModel result);
    }
}
