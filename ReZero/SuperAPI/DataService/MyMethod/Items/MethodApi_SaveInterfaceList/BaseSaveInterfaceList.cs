using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class BaseSaveInterfaceList
    {

        protected   void Check(EntityColumnInfo pk)
        {
            if (pk == null)
            {
                throw new Exception(TextHandler.GetCommonText("创建失败实体没有主键", "The failed entity does not have a primary key"));

            }
        }
        protected void SetCommonProperties(ZeroInterfaceList zeroInterfaceList, SaveInterfaceListModel saveInterfaceListModel)
        {
            // Set default values for ZeroInterfaceList
            zeroInterfaceList.IsInitialized = false;
            zeroInterfaceList.IsDeleted = false;
            zeroInterfaceList.Name = saveInterfaceListModel.Name;
            zeroInterfaceList.Url = GetUrl(saveInterfaceListModel);
            zeroInterfaceList.GroupName = !string.IsNullOrEmpty(saveInterfaceListModel?.GroupName)? saveInterfaceListModel?.GroupName!:saveInterfaceListModel?.TableId!;
            zeroInterfaceList.InterfaceCategoryId = Convert.ToInt64(saveInterfaceListModel?.InterfaceCategoryId);

            // Set creator information
            var options = SuperAPIModule._apiOptions;
            var userInfo = options?.DatabaseOptions!.GetCurrentUserCallback();
            zeroInterfaceList.Creator = userInfo?.UserName;
            zeroInterfaceList.CreateTime = DateTime.Now;

            // Set default HttpMethod if not specified
            if (zeroInterfaceList.HttpMethod == null)
            {
                zeroInterfaceList.HttpMethod = HttpRequestMethod.All.ToString();
            }

            // Set DataModel for ZeroInterfaceList
            zeroInterfaceList.DataModel = new DataModel()
            {
                ActionType = saveInterfaceListModel!.ActionType!.Value,
                TableId = GetTableId(saveInterfaceListModel.TableId)
            };
             
        }
        protected EntityInfo GetEntityInfo(long tableId) 
        { 
            var type = EntityGeneratorManager.GetTypeAsync(tableId).GetAwaiter().GetResult();
            var entityInfo = App.Db.EntityMaintenance.GetEntityInfo(type);
            return entityInfo;
        }

        protected   object InsertData(ZeroInterfaceList zeroInterfaceList)
        {
            App.Db.Insertable(zeroInterfaceList).ExecuteReturnSnowflakeId();
            return true;
        }
        private long GetTableId(string? tableId)
        {
            var db = App.Db;
            var entityInfo= db.Queryable<ZeroEntityInfo>()
                       .Includes(x=>x.ZeroEntityColumnInfos)
                       .Where(it => it.ClassName == tableId).ToList();
            if (entityInfo.Count > 1) 
            {
                throw new Exception("表名重复");
            }
            else if (entityInfo.Count == 0)
            {
                throw new Exception("表名不存在");
            }
            else if (!entityInfo.First().ZeroEntityColumnInfos.Any())
            {
                throw new Exception("实体没有配置列");
            }
            else
            {
                return entityInfo.First().Id;
            } 
        }

        private static string GetUrl(SaveInterfaceListModel? saveInterfaceListModel)
        {
            if (string.IsNullOrEmpty(saveInterfaceListModel?.Url)) 
            {
                saveInterfaceListModel!.Url = $"/{saveInterfaceListModel.InterfaceCategoryId}/{saveInterfaceListModel.ActionType.ToString().ToLower()}/{saveInterfaceListModel.TableId?.ToLower()}/{SqlSugar.SnowFlakeSingle.Instance.NextId()}";
            }
            if (saveInterfaceListModel?.Url?.StartsWith(@"/") != true) 
            {
                saveInterfaceListModel!.Url = $@"/{saveInterfaceListModel?.Url}";
            }
            return saveInterfaceListModel?.Url!;
        }
    }
}
