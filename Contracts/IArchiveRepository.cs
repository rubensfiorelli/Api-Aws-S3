using Api_S3_AWS.Domain;

namespace Api_S3_AWS.Contracts
{
    public interface IArchiveRepository
    {
        Archive AddArchive(Archive archive);
        Task<IEnumerable<Archive>> GetArchivesAsync();
    }
}
