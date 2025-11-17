using BLGDLab.Data.IRepositories;
using BLGDLab.Data.Repository;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLGDLab.Data.Repositories
{
    public class DiamondSearchRepository : IDiamondSearchRepository
    {
        private readonly SqlConnectionRepository _connectionFactoryRepository;
        private readonly SqlConnection _sqlConnection;
        public DiamondSearchRepository(SqlConnectionRepository connectionFactoryRepository)
        {
            this._connectionFactoryRepository = connectionFactoryRepository;
            _sqlConnection = this._connectionFactoryRepository._blgdContext;
        }

        public Task<IEnumerable<dynamic>> DimaondSearchData(string json)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@JsonRequest", json);

            var data = _sqlConnection.QueryAsync("safedb.SpDiamondSearch_NEW", parameters, commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }

        public async Task<IEnumerable<IEnumerable<dynamic>>> GetDiamondFilter(bool IsForDataSet,int userId, bool IsIncludeOnlyInstockCriteria = false)
        {
            DynamicParameters  parameters = new DynamicParameters();
            parameters.Add("@IsIncludeOnlyInstockCriteria", IsIncludeOnlyInstockCriteria == true ? 1 : 0);
            parameters.Add("@UserId", userId);
            parameters.Add("@IsForDiamondSearchNew", 1);

            var data = await _sqlConnection.QueryMultipleAsync("safedb.SPGetIsInStockFilterCriteria", parameters, commandType: System.Data.CommandType.StoredProcedure);

            var resultSet = new List<IEnumerable<dynamic>>();

            while (!data.IsConsumed)
            {
                var obj = data.Read();
                resultSet.Add(obj);
            }

            return resultSet; 
        }
    }
}
