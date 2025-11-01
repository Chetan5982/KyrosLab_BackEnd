using BLGDLab.Business.IServices;
using BLGDLab.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLGDLab.Business.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository; 

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IEnumerable<dynamic>> GetAllBlogsAsync(string jsonString)
        {
            return  await _blogRepository.GetAllBlogsAsync(jsonString);
        }
    }
}
