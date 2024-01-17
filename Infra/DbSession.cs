using Microsoft.Data.SqlClient;

namespace Api_S3_AWS.Infra
{
    public class DbSession : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public DbSession(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration.GetConnectionString("SQLConnection"));
            Connection.Open();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
