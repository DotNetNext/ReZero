using SqlSugar.Extensions;
using System;
using System.Threading.Tasks;

namespace ReZero 
{
    public class DataService : IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var actionTypeName = GetActionTypeName(dataModel);
            var actionType = Type.GetType(actionTypeName);
            CheckActionType(dataModel, actionType);
            var actionInstance = (IDataService)Activator.CreateInstance(actionType);
            var result = await actionInstance.ExecuteAction(dataModel);
            return result;
        }

        private static string GetActionTypeName(DataModel dataModel)
        {
            return $"ReZero.{dataModel.ActionType}";
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