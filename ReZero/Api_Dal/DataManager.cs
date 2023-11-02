using SqlSugar.Extensions;
using System;
using System.Threading.Tasks;

namespace ReZero 
{
    public class DataManager : IDataManager
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var actionTypeName = $"ReZero.{dataModel.ActionType}";
            var actionType = Type.GetType(actionTypeName);
            CheckActionType(dataModel, actionType); 
            var actionInstance = (IDataManager)Activator.CreateInstance(actionType);
            var result = await actionInstance.ExecuteAction(dataModel);
            return result;
        }

        private static void CheckActionType(DataModel dataModel, Type actionType)
        {
            if (actionType == null)
            {
                throw new ArgumentException($"Invalid ActionType: {dataModel.ActionType}");
            }
        }
    }
}