using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    internal class InstanceManager
    {
        public static string GetActionTypeName(ActionType actionType)
        {
            return $"ReZero.SuperAPI.{actionType}";
        }
        public static string GetActionTypeName(DataModel dataModel)
        {
            return $"ReZero.SuperAPI.{dataModel.ActionType}";
        }
        public static string GetActionTypeElementName(ActionType actionType)
        {
            return $"ReZero.SuperAPI.Element{actionType}";
        }

        public static string GetSaveInterfaceType(ActionType actionType)
        {
            return $"ReZero.SuperAPI.SaveInterfaceList{actionType}";
        }
        public static void CheckActionType(DataModel dataModel, Type actionType)
        {
            if (actionType == null)
            {
                throw new ArgumentException($"Invalid ActionType: {dataModel.ActionType}");
            }
        }
    }
}
