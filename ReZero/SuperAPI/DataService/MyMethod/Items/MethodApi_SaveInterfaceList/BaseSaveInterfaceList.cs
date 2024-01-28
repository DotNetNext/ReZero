using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class BaseSaveInterfaceList
    {
        internal void SetCommonProperties(ZeroInterfaceList zeroInterfaceList, SaveInterfaceListModel saveInterfaceListModel)
        {
            zeroInterfaceList.IsInitialized = false;
            zeroInterfaceList.IsDeleted = false;
            zeroInterfaceList.Name = saveInterfaceListModel.Name;
            zeroInterfaceList.Url = saveInterfaceListModel.Url;
            zeroInterfaceList.GroupName = saveInterfaceListModel?.GroupName ?? saveInterfaceListModel?.TableId!;
            zeroInterfaceList.Url = GetUrl(saveInterfaceListModel);
            zeroInterfaceList.InterfaceCategoryId =Convert.ToInt64(saveInterfaceListModel?.InterfaceCategoryId);
            var options = SuperAPIModule._apiOptions;
            var userInfo = options?.DatabaseOptions!.GetCurrentUserCallback();
            zeroInterfaceList.Creator = userInfo?.UserName;
            zeroInterfaceList.CreateTime =DateTime.Now;
            if (zeroInterfaceList.HttpMethod == null) 
            {
                zeroInterfaceList.HttpMethod = HttpRequestMethod.All+"";
            }
            zeroInterfaceList.DataModel = new DataModel()
            {
                 ActionType = saveInterfaceListModel!.ActionType!.Value,
                 TableId=GetTableId(saveInterfaceListModel.TableId)
            };
            var type = EntityGeneratorManager.GetTypeAsync(zeroInterfaceList.DataModel.TableId)
                .GetAwaiter().GetResult();
            var entityInfo = App.Db.EntityMaintenance.GetEntityInfo(type); 
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
            return saveInterfaceListModel?.Url!;
        }
    }
}
