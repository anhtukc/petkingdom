namespace PetKingdomFN.Interfaces
{
    public interface ICloudStorageService
    {
        Task<string> GetSignedUrlAsync(string fileNameToRead, int timeOutInMinutes = 30);
        Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave, string folder);
        Task DeleteFileAsync(string fileNameToDelete);

    }
}
