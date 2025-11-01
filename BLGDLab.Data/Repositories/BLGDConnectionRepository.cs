using BLGDLab.Data.IRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLGDLab.Data.Repository
{
    public class BLGDConnectionRepository 
    {
        private readonly string _connectionString;

        public BLGDConnectionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConnectionAsync()
        {
            return new SqlConnection(_connectionString);
        }

    }
}
