using ReZero.Options;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero
{
    public class ORM
    {
        public ISqlSugarClient SugarClient { get; private set; }
        public ORM(ConnectionConfig connectionConfig)
        {
            SugarClient = new SqlSugarClient(connectionConfig);
        } 
    }
}
