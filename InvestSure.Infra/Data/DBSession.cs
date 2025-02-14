
using System.Data;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;


namespace InvestSure.Infra.Data
{
    public class DBSession : IDisposable
    {

        public IDbConnection DbConnection { get; }
        public IDbTransaction Transaction { get; private set; }


        public DBSession(IConfiguration configuration)
        {
            DbConnection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            DbConnection.Open();
        }
        
        public void BeginTransaction()
        {
            Transaction = DbConnection.BeginTransaction();
        }
        public void Commit()
        {
            Transaction.Commit();
        }
        public void Rollback()
        {
            Transaction?.Rollback();
            Dispose();
        }
        public void Dispose()
        {
            Transaction?.Dispose();
            DbConnection?.Dispose();
        }
    }
}
