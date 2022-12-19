using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;
using PetKingdomFN.BusEntities.ConfigOptions;
using PetKingdomFN.Interfaces;

namespace PetKingdomFN.Services
{
    public class CloudStorageService:ICloudStorageService
    {
        private readonly GCSConfigOptions _options;
        private readonly GoogleCredential _googleCredential;

        public CloudStorageService(IOptions<GCSConfigOptions> options
)
        {
            _options = options.Value;

            try
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (environment == Environments.Production)
                {
                    _googleCredential = GoogleCredential.FromJson(_options.GCPStorageAuthFile);
                }
                else
                {
                    _googleCredential = GoogleCredential.FromFile(_options.GCPStorageAuthFile);
                }
            }
            catch (Exception ex)
            {
    
                throw;
            }
        }
        public async Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await fileToUpload.CopyToAsync(memoryStream);
                    using (var storageClient = StorageClient.Create(_googleCredential))
                    {
                        var uploadedFile = await storageClient.UploadObjectAsync(_options.GoogleCloudStorageBucketName,  fileNameToSave, fileToUpload.ContentType, memoryStream);
                        return uploadedFile.MediaLink;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task DeleteFileAsync(string fileNameToDelete)
        {
            try
            {
                using (var storageClient = StorageClient.Create(_googleCredential))
                {
                    await storageClient.DeleteObjectAsync(_options.GoogleCloudStorageBucketName, fileNameToDelete);
                }
            }
            catch (Exception ex)
            {
              
            }
        }
        public async Task<string> GetSignedUrlAsync(string fileNameToRead, int timeOutInMinutes = 30)
        {
            try
            {
                var sac = _googleCredential.UnderlyingCredential as ServiceAccountCredential;
                var urlSigner = UrlSigner.FromServiceAccountCredential(sac);
                var signedUrl = await urlSigner.SignAsync(_options.GoogleCloudStorageBucketName, fileNameToRead, TimeSpan.FromMinutes(timeOutInMinutes));
                return signedUrl.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
