using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.Configuration
{
    public class RezeroLicense
    {
        public bool? Enable { get; set; }
        public Func<DateTime>? LicenseValidateFunc { get; set; }
        public bool LicenseFilePath { get; set; }
    }
}
