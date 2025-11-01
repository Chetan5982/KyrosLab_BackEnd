using Microsoft.Data.SqlClient;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLGDLab.Data.Repository
{
    public class SqlConnectionRepository:IDisposable
    {
        public  SqlConnection _blgdContext;
        
        private bool _disposed;

        public SqlConnectionRepository(string blgdConnectionString)
        {
            _blgdContext = new SqlConnection(blgdConnectionString);
        }

        public void Dispose()
        {
            if (_disposed) return;

            
            if (_blgdContext.State != System.Data.ConnectionState.Closed) _blgdContext.Close();
                _blgdContext.Dispose();

            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
