using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{

    internal class ResultPageGrid
    {
        public object? Data { get; set; }
        public IEnumerable<ResultGridColumn>? Columns { get; set; }
        public ResultPage? Page { get; set; }
    }
}
