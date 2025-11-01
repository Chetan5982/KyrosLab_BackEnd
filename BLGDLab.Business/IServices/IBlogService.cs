using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLGDLab.Business.IServices
{
    public interface IBlogService
    {
        Task<IEnumerable<dynamic>> GetAllBlogsAsync(string jsonString);
    }
}
