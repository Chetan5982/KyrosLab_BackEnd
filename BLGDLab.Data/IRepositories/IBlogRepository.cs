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
        Task<IEnumerable<dynamic>> GetBlogByCategoryUrlAsync(string jsonString);
        Task<IEnumerable<dynamic>> GetBlogByTitleAsync(string jsonString);
        Task<IEnumerable<dynamic>> DeleteBlogAsync(int Id);
        Task EditBlogAsync(int Id);
    }
}
