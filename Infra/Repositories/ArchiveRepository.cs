using Api_S3_AWS.Contracts;
using Api_S3_AWS.Domain;
using Dapper;

namespace Api_S3_AWS.Infra.Repositories
{
    public class ArchiveRepository : IArchiveRepository
    {
        private readonly DbSession _session;

        public ArchiveRepository(DbSession session)
        {
            _session = session;
        }

        public Archive AddArchive(Archive archive)
        {
            
            using var conn = _session.Connection;

            string sqlQuery = @"INSERT INTO [dbo].[archives] ([title], [path]) VALUES(@title, @path)";

            var rowsAffected = conn.Execute(sqlQuery, new
            {
                archive.Title,
                archive.Path

            });

            if (rowsAffected == 0)
                throw new Exception("Fail in add file");

            return archive;
        }

        public async Task<IEnumerable<Archive>> GetArchivesAsync()
        {
            using var conn = _session.Connection;
            string query = @"SELECT * FROM archives;";

            var archives = await conn.QueryAsync<Archive>(sql: query);

            return archives;
        }
    }
}
