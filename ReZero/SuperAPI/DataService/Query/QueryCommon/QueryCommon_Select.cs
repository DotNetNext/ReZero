using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Primitives;
using System.Xml.Linq;
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
                else if (IsSelectSubqueryName(item)) 
                {
                    selectLists.Add(item.SubquerySQL!);
                    resultTypeInfos.Add(new ResultTypeInfo() { PropertyName = item.AsName, Type = GetColumnInfo(type, item)?.PropertyInfo?.PropertyType ?? typeof(object) });
                }
                else if (IsSelectJoinName(item))
                {
                    var propertyName = _sqlBuilder!.GetTranslationColumnName(item.AsName);
                    var tableInfo = dataModel!.JoinParameters![item.TableIndex - 1];
                    var name = $"{_sqlBuilder!.GetTranslationColumnName(PubConst.Orm_TableDefaultPreName + item.TableIndex)}.{_sqlBuilder!.GetTranslationColumnName(item.Name)} AS {propertyName} ";
                    selectLists.Add(name);
                    resultTypeInfos.Add(new ResultTypeInfo() { PropertyName = item.AsName, Type = typeof(string) });
                }
                else if (!string.IsNullOrEmpty(item.Name))
                {
                    if (string.IsNullOrEmpty(item.AsName))
                        item.AsName = item.Name;
                    var name = $"{_sqlBuilder!.GetTranslationColumnName(GetSelectFieldName(queryObject, item))} AS {_sqlBuilder!.GetTranslationColumnName(item.AsName)} ";
                    selectLists.Add(name);
                    resultTypeInfos.Add(new ResultTypeInfo() { PropertyName = item.AsName, Type = GetColumnInfo(type, item)?.PropertyInfo?.PropertyType ?? typeof(object) });
                }
            }
            var  resultType=new DynamicTypeBuilder(_sqlSugarClient!,"ViewModel_"+dataModel.ApiId, resultTypeInfos).BuildDynamicType();
            queryObject = queryObject.Select(string.Join(",", selectLists), resultType);
            return queryObject;
        }

        private string GetSelectFieldName(QueryMethodInfo queryObject, DataModelSelectParameters item)
        {
            var name = _sqlSugarClient!.EntityMaintenance.GetDbColumnName(item.Name, queryObject.EntityType);
            return PubConst.Orm_TableDefaultPreName + item.TableIndex + "." + name;
        }
        private EntityColumnInfo GetColumnInfo(Type type, DataModelSelectParameters item)
        {
            var collumnInfo = _sqlSugarClient!.EntityMaintenance.GetEntityInfo(type).Columns.FirstOrDefault(it=>it.PropertyName.EqualsCase(item.AsName!));
            return collumnInfo;
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
          return _sqlBuilder!.GetTranslationColumnName(PubConst.Orm_TableDefaultMasterTableShortName) +"."+ _sqlBuilder!.GetTranslationColumnName(it.DbColumnName) + " AS " + _sqlBuilder!.GetTranslationColumnName(it.PropertyName);
        }
    }
}
