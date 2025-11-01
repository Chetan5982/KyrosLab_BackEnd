using BLGDLab.Data.IRepository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLGDLab.Data.Repository
{
    public class BlogRepository : IBlogRepository
    {
        SqlConnectionRepository _sqlConnectionRepository;
        public BlogRepository(SqlConnectionRepository sqlConnectionRepository)
        {
            _sqlConnectionRepository = sqlConnectionRepository;
        }

        public async Task<IEnumerable<dynamic>> GetAllBlogsAsync(string jsonString)
        {
            var con= _sqlConnectionRepository._blgdContext;
            DynamicParameters param = new DynamicParameters();
            param.Add("@JsonData", "");
            IEnumerable<dynamic> result = await con.QueryAsync("SP_GetStaticContents",param,commandType:System.Data.CommandType.StoredProcedure);

            return result;
        }


    }
}
