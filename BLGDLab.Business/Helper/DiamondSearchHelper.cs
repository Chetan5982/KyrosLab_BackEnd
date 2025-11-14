using BLGDLab.Data.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLGDLab.Business.Helper
{
    public static class DiamondSearchHelper
    {
        public static IEnumerable<object> CreateFilterList(IEnumerable<dynamic> source, string fieldType, string keyForId, bool isShape, CustomModel.ManageFile photoPaths, bool isShowInSearch = false)
        {
            return source
     .Where(x =>
     {
         var dict = (IDictionary<string, object>)x;
         bool matchesFieldType = dict["FieldType"].ToString() == fieldType;
         bool hasName = !string.IsNullOrEmpty(dict["Name"]?.ToString());
         bool matchesShowInSearch = !isShowInSearch
                                    || (Convert.ToBoolean(dict["IsShowInSearch"]) == isShowInSearch);

         return matchesFieldType && hasName && matchesShowInSearch;
     })
     .Select((s, index) =>
     {
         var dict = (IDictionary<string, object>)s;
         return new
         {
             Code = dict["Code"],
             Value = dict["Name"],
             KeyForId = keyForId,
             IsShape = isShape,
             PhotoPaths = photoPaths,
         };
     });
        }

    }
}
