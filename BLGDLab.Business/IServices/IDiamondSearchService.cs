using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLGDLab.Business.IServices
{
    public  interface IDiamondSearchService
    {
        Task<Dictionary<string, IEnumerable<dynamic>>> GetDiamondFilter(bool IsForDataSet, int userId, bool IsIncludeOnlyInstockCriteria = false);
    }
}
