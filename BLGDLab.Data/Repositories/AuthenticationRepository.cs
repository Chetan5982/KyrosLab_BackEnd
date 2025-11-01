using BLGDLab.Data.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Reflection.Metadata;

namespace BLGDLab.Data.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly SqlConnectionRepository _connectionFactoryRepository;
       

        public AuthenticationRepository(SqlConnectionRepository dbConnectionFactoryRepository)
        {
            _connectionFactoryRepository = dbConnectionFactoryRepository;
        }

       

        public async Task<dynamic> Login(string userName, string password)
        {
            dynamic user;
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", userName);
            parameters.Add("@Password", password);
            
             var conn =(SqlConnection)_connectionFactoryRepository._blgdContext;
             user = await conn.QueryFirstOrDefaultAsync("dbo.sp_LoginUser", parameters, commandType: CommandType.StoredProcedure);
            return user;
        }

        
    }
}
