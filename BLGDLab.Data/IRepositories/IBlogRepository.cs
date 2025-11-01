using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLGDLab.Data.IRepository
{
    public interface IBlogRepository
    {
        Task<IEnumerable<dynamic>> GetAllBlogsAsync(string jsonString);
    }
}
