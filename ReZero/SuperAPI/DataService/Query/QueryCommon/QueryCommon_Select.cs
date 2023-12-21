using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Primitives;
namespace ReZero.SuperAPI
{
    /// <summary>
    /// Select
    /// </summary>
    public partial class QueryCommon : IDataService
    {
        public List<ResultTypeInfo> resultTypeInfos = new List<ResultTypeInfo>(); 
        private QueryMethodInfo Select(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            if (IsAnySelect(dataModel))
            {
                queryObject = GetSelectByParameters(type,dataModel, queryObject);
            }
            else if (IsAnyJoin(dataModel))
            {
                queryObject = GetDefaultSelect(type, queryObject);
            }
            return queryObject;
        }

        private  QueryMethodInfo GetSelectByParameters(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            List<string> selectLists = new List<string>();
            foreach (var item in dataModel.SelectParameters ?? new List<DataModelSelectParameters>())
            {
                if (IsSelectMasterAll(item))
                {
                    selectLists.Add(GetMasterSelectAll(type));
                }
                else if (IsSelectJoinName(item))
                {
                    var propertyName = _sqlBuilder!.GetTranslationColumnName(item.AsName);
                    var tableInfo = dataModel!.JoinParameters![item.TableIndex-1];
                    var name = $"{_sqlBuilder!.GetTranslationColumnName(PubConst.TableDefaultPreName+item.TableIndex)}.{_sqlBuilder!.GetTranslationColumnName(item.Name)} AS {propertyName} ";
                    selectLists.Add(name);
                    resultTypeInfos.Add(new ResultTypeInfo() { PropertyName = item.AsName, Type = typeof(string) });
                }
            }
            var  resultType=new DynamicTypeBuilder(_sqlSugarClient!,"ViewModel_"+dataModel.ApiId, resultTypeInfos).BuildDynamicType();
            queryObject = queryObject.Select(string.Join(",", selectLists), resultType);
            return queryObject;
        }

        private QueryMethodInfo GetDefaultSelect(Type type, QueryMethodInfo queryObject)
        {
            string selectString = GetMasterSelectAll(type);
            queryObject = queryObject.Select(selectString);
            return queryObject;
        }

        private string GetMasterSelectAll(Type type)
        {
            var columns = _sqlSugarClient!.EntityMaintenance.GetEntityInfo(type).Columns.Where(it => !it.IsIgnore)
                  .Select(it => GetEntityColumns(it)).ToList();
            var selectString = String.Join(",", columns);
            return selectString;
        }

        private object GetEntityColumns(EntityColumnInfo it)
        {
          resultTypeInfos.Add(new ResultTypeInfo() { PropertyName=it.PropertyName,Type=it.PropertyInfo.PropertyType });
          return _sqlBuilder!.GetTranslationColumnName(PubConst.TableDefaultMasterTableShortName) +"."+ _sqlBuilder!.GetTranslationColumnName(it.DbColumnName) + " AS " + _sqlBuilder!.GetTranslationColumnName(it.PropertyName);
        }
    }
}
