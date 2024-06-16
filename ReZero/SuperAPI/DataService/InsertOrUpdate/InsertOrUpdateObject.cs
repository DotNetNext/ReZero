using ClosedXML;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks; 

namespace ReZero.SuperAPI
{
    internal class InsertOrUpdateObject : CommonDataService, IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            base.InitDb(type, db);
            base.InitData(type, db, dataModel);
            CheckSystemData(db, dataModel, type, db.EntityMaintenance.GetEntityInfo(type));
            this.SetDefaultValue(dataModel, db, type);
            int result =await InsertOrUpdateAsync(dataModel, db);
            base.ClearAll(dataModel);
            return GetResult(dataModel, result);
        } 

        private static async Task<int> InsertOrUpdateAsync(DataModel dataModel, ISqlSugarClient db)
        {
            int result = 0;
            var context = ((SqlSugarClient)db).Context;
            var storageableByObject = db.StorageableByObject(dataModel.Data);
            var methodino = (MethodInfo)storageableByObject.GetType().GetProperty("MethodInfo", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(storageableByObject);
            var value = storageableByObject.GetType().GetProperty("objectValue", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(storageableByObject);
            var groupObject = methodino.Invoke(context, new object[] { value });
            var task=(Task)groupObject.GetType().GetMethod("ToStorageAsync").Invoke(groupObject,new object[] { });
            var groupValues= await GetTask(task);
            var insertList= GetSelectItemList(groupValues.GetType().GetProperty("InsertList").GetValue(groupValues));
            var updateList = GetSelectItemList(groupValues.GetType().GetProperty("UpdateList").GetValue(groupValues));
            var updatecolumns = GetUpdateableColumns(dataModel); 
            if(updateList.Any())
              result+=db.UpdateableByObject(updateList).UpdateColumns(updatecolumns).ExecuteCommand();
            if(insertList.Any())
              result += db.InsertableByObject(insertList).ExecuteCommand();
            return result;
        }
        public static List<object> GetSelectItemList(object objectValue) 
        {
            var list= ((IList)objectValue).Cast<object>().ToList();
            list = list.Select(it => it.GetType().GetProperty("Item").GetValue(it)).ToList();
            return list;
        }

        private static async Task<object> GetTask(Task task)
        {
            await task.ConfigureAwait(false); // 等待任务完成
            var resultProperty = task.GetType().GetProperty("Result");
            var result = resultProperty.GetValue(task);
            return result;
        }
        private static string[] GetUpdateableColumns(DataModel dataModel)
        {
            string[] result = null!;
            if (!string.IsNullOrEmpty(dataModel.TableColumns))
            {
                result=dataModel.TableColumns.Split(",");
            }
            else
            {
                result=dataModel.DefaultParameters.Select(it => it.Name).ToArray()!;
            }
            return result;
        }

        private static object GetResult(DataModel dataModel, int result)
        {
            if (dataModel.ResultType == SqlResultType.AffectedRows)
            {
                return result;
            }
            else
            {
                return true;
            }
        }

        private void SetDefaultValue(DataModel dataModel, ISqlSugarClient db, Type type)
        {
            if (EntityMappingService.IsAnyDefaultValue(dataModel))
            {
                dataModel.Data = EntityMappingService.GetDataByDefaultValueParameters(type, db, dataModel);
            }
        }
    }
}
