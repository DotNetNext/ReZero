using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ReZero.Configuration
{
    public class ReZeroJson
    {
        public ReZeroUiBasicdatabase? BasicDatabase { get; set; }
        public ReZeroJwt? Jwt { get; set; }
        public ReZeroUi? Ui { get; set; } 

        public ReZeroCorsOptions? Cors { get; set; }
    }

}
