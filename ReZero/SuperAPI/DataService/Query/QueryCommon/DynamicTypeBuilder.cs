using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class DynamicTypeBuilder
    {
        private readonly ISqlSugarClient db;
        private readonly string tableName;
        private readonly List<ResultTypeInfo> propertyInfos;

        public DynamicTypeBuilder(ISqlSugarClient db, string tableName, List<ResultTypeInfo> propertyInfos)
        {
            this.db = db;
            this.tableName = tableName;
            this.propertyInfos = propertyInfos;
        }

        public Type BuildDynamicType()
        {
            var typeBuilder = db.DynamicBuilder().CreateClass(tableName, new SugarTable() { });

            foreach (var propInfo in propertyInfos)
            {
                typeBuilder.CreateProperty(propInfo.PropertyName, propInfo.Type, new SugarColumn() { });
            }

            typeBuilder.WithCache();

            return typeBuilder.BuilderType();
        }
    }
}
