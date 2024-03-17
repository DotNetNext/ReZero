using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class SaveInterfaceListQueryCommon : BaseSaveInterfaceList, ISaveInterfaceList
    {
        private void SetWhere(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
            if (saveInterfaceListModel.Json!.Where?.Any() != true)
            {
                return;
            }
            var json = saveInterfaceListModel.Json;
            switch (json.WhereRelation)
            {
                case WhereRelation.And:

                    break;
                case WhereRelation.AndAll:

                    break;
                case WhereRelation.Or:

                    break;
                case WhereRelation.OrAll:

                    break;
                case WhereRelation.Custom:

                    break;

                case WhereRelation.CustomAll:

                    break;
                default:
                    break;
            }
        }
    }
}