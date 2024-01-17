using Api_S3_AWS.Contracts;
using Api_S3_AWS.Domain;
using Api_S3_AWS.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api_S3_AWS.Controllers
{
    [Route("api/archives")]
    [ApiController]
    public class ArchivesController(IArchiveRepository archiveRepository) : ControllerBase
    {
        private readonly IArchiveRepository _archiveRepository = archiveRepository ?? throw new ArgumentNullException(nameof(archiveRepository));

        [HttpPost]
        [ProducesResponseType(typeof(Archive), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> AddArchive([FromForm] ArchiveViewModel archive)
        {
            try
            {
                var amazon = new AmazonS3Service();
                var key = "uploads/" + Guid.NewGuid();
                var upload = await amazon.UploadFileAsync("bkt-rubensfiorelli-s3", key, archive.Path);

                if (!upload)
                    throw new Exception("Fail in add archive in course");

                var archiveDomain = new Archive(archive.Title, key);
                var result = _archiveRepository.AddArchive(archiveDomain);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(Archive), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetAllArchives()
        {
            var result = await _archiveRepository.GetArchivesAsync();

            if (result is null)
                return BadRequest("User does not exist");

            return Ok(result);
        }



    }
}
