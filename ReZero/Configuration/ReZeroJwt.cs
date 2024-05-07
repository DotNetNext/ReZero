using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.Configuration
{
    public class ReZeroJwt
    {
        public string? Secret { get; set; }
        public string? UserTableName { get; set; }
        public string? UserNameFieldName { get; set; }
        public string? PasswordFieldName { get; set; }
        public long? Expires { get; set; }
        public List<ClaimItem>? Claim { get; set; }
    }

    public class ClaimItem
    {
        public string? Key { get; set; }
        public string? FieldName { get; set; }
    }
}
