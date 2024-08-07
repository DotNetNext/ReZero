﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;

namespace ReZero.SuperAPI 
{
    public class BaseSaveInterfaceList
    {

        protected void ApplyDefaultAndClearIfNotEmpty(ZeroInterfaceList zeroInterfaceList)
        {
            foreach (var item in zeroInterfaceList.DataModel?.DefaultValueColumns ?? new List<DataModelDefaultValueColumnParameter>())
            {
                var paramter = zeroInterfaceList.DataModel?.DefaultParameters?.FirstOrDefault(it => it.Name == item.PropertyName);
                if (paramter != null)
                {
                    if (paramter?.ParameterValidate?.IsRequired == true && item?.Type != DefaultValueType.None)
                    {
                        paramter.ParameterValidate = null;
                    }
                }
            }
        }
        protected void Check(EntityColumnInfo pk)
        {
            if (pk == null)
            {
                throw new Exception(TextHandler.GetCommonText("创建失败实体没有主键", "The failed entity does not have a primary key"));

            }
        }
        protected virtual void SetCommonProperties(ZeroInterfaceList zeroInterfaceList, SaveInterfaceListModel saveInterfaceListModel)
        {
            // Set default values for ZeroInterfaceList
            zeroInterfaceList.IsInitialized = false;
            zeroInterfaceList.IsDeleted = false;
            zeroInterfaceList.IsAttributeMethod = false;
            zeroInterfaceList.Name = saveInterfaceListModel.Name;
            zeroInterfaceList.Url = GetUrl(saveInterfaceListModel);
            zeroInterfaceList.DatabaseId = saveInterfaceListModel?.Json?.DataBaseId;
            zeroInterfaceList.GroupName = !string.IsNullOrEmpty(saveInterfaceListModel?.GroupName)? saveInterfaceListModel?.GroupName!:saveInterfaceListModel?.TableId!;
            zeroInterfaceList.InterfaceCategoryId = Convert.ToInt64(saveInterfaceListModel?.InterfaceCategoryId);

            // Set creator information
            var options = SuperAPIModule._apiOptions;
            var userInfo = options?.DatabaseOptions!.GetCurrentUserCallback();
            zeroInterfaceList.Creator = userInfo?.UserName;
            zeroInterfaceList.CreateTime = DateTime.Now;

            // Set default HttpMethod if not specified
            if (string.IsNullOrEmpty(zeroInterfaceList.HttpMethod))
            {
                zeroInterfaceList.HttpMethod = HttpRequestMethod.All.ToString();
            }
            if (!string.IsNullOrEmpty(saveInterfaceListModel?.HttpMethod)) 
            {
                zeroInterfaceList.HttpMethod = saveInterfaceListModel.HttpMethod;
            }

            // Set DataModel for ZeroInterfaceList
            zeroInterfaceList.DataModel = new DataModel()
            {
                ActionType = saveInterfaceListModel!.ActionType!.Value,
                TableId = GetTableId(saveInterfaceListModel.TableId)
            };

            //primary key
            zeroInterfaceList.Id= saveInterfaceListModel.Json?.Id??0;

            //update info
            SetCurrentData(zeroInterfaceList.DataModel,saveInterfaceListModel);

        }

        protected  void SetCurrentData(DataModel dataModel, SaveInterfaceListModel saveInterfaceListModel)
        {
            dataModel.CurrentDataString = saveInterfaceListModel?.Json?.CurrentDataString;
        }
        protected EntityInfo GetEntityInfo(long tableId) 
        { 
            var type = EntityGeneratorManager.GetTypeAsync(tableId).GetAwaiter().GetResult();
            var entityInfo = App.Db.EntityMaintenance.GetEntityInfo(type);
            return entityInfo;
        }
         
        protected  object SaveData(ZeroInterfaceList zeroInterfaceList)
        {
            if (zeroInterfaceList.Id == 0) 
            {
                zeroInterfaceList.Id = SnowFlakeSingle.Instance.NextId();
            } 
            zeroInterfaceList.IsDeleted = false;
            var url = zeroInterfaceList.Url?.ToLower();
            var urlCount = App.Db.Queryable<ZeroInterfaceList>()
                           .Where(it => it.Id != zeroInterfaceList!.Id)
                           .Where(it => it.Url!.ToLower() == url)
                           .Count();
            if (urlCount > 0) throw new Exception(TextHandler.GetCommonText("接口地址已存在", "The interface address already exists."));
            var x= App.Db.Storageable(zeroInterfaceList).ToStorage();
            x.AsInsertable.ExecuteCommand();
            if(x.UpdateList.Any())
                App.Db.Updateable(x.UpdateList.Select(it=>it.Item).First()).ExecuteCommand();
            return true;
        }
        public long GetTableId(string? tableId)
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

        public  string GetUrl(SaveInterfaceListModel? saveInterfaceListModel)
        {
            if (string.IsNullOrEmpty(saveInterfaceListModel?.Url))
            {
                var data = App.Db.Queryable<ZeroInterfaceList>().InSingle(saveInterfaceListModel?.Json?.Id ?? 0);
                if (data != null)
                {
                    saveInterfaceListModel!.Url = data.Url;
                }
                else if (Regex.IsMatch(saveInterfaceListModel?.TableId?.ToLower()+"", @"[\u4e00-\u9fa5]")) 
                {
                    saveInterfaceListModel!.Url = $"/{saveInterfaceListModel.InterfaceCategoryId}/{saveInterfaceListModel.ActionType.ToString().ToLower()}/{SqlSugar.SnowFlakeSingle.Instance.NextId()}";
                }
                else
                {
                    saveInterfaceListModel!.Url = $"/{saveInterfaceListModel.InterfaceCategoryId}/{saveInterfaceListModel.ActionType.ToString().ToLower()}/{saveInterfaceListModel.TableId?.ToLower()}/{SqlSugar.SnowFlakeSingle.Instance.NextId()}";
                }
            }
            if (saveInterfaceListModel?.Url?.StartsWith(@"/") != true) 
            {
                saveInterfaceListModel!.Url = $@"/{saveInterfaceListModel?.Url}";
            }
            return saveInterfaceListModel?.Url!;
        }
    }
}
