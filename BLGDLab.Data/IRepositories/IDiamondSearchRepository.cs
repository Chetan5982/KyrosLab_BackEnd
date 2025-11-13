using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLGDLab.Data.IRepositories
{
    public interface IDiamondSearchRepository
    {
        Task<IEnumerable<IEnumerable<dynamic>>> GetDiamondFilter(bool IsForDataSet, int userId, bool IsIncludeOnlyInstockCriteria = false);
    }
}
