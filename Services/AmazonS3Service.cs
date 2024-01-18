using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace Api_S3_AWS.Services
{
    public class AmazonS3Service
    {
        public string AwsKeyID { get; private set; }
        public string AwsPrivateKey { get; private set; }
        public BasicAWSCredentials CredentialsAws { get; private set; }

        private readonly IAmazonS3 _s3Client;

        public AmazonS3Service()
        {
            AwsKeyID = "";
            AwsPrivateKey = "";
            CredentialsAws = new BasicAWSCredentials(AwsKeyID, AwsPrivateKey);

            var config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.USEast1
            };

            _s3Client = new AmazonS3Client(CredentialsAws, config);
        }

        public async Task<bool> UploadFileAsync(string bucket, string key, IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);

            var fileTransfer = new TransferUtility(_s3Client);

            await fileTransfer.UploadAsync(new TransferUtilityUploadRequest
            {
                InputStream = memoryStream,
                Key = key,
                BucketName = bucket,
                ContentType = file.ContentType
            });

            return true;
        }
    }
}
