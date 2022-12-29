using Microsoft.AspNetCore.Http;

namespace PetKingdomFN.Helpers
{
    public class ImageConvert
    {
        public static IFormFile ConvertBase64ToIFormFile(string base64, string fileName)
        {
            base64 = base64.Split(',')[1];
            var imageBytes = Convert.FromBase64String(base64);

            using (var ms = new MemoryStream(imageBytes))
            {
                // Create a new MemoryStream object and pass it to the FormFile constructor
                using (var formFileStream = new MemoryStream())
                {
                    ms.CopyTo(formFileStream);
                    formFileStream.Seek(0, SeekOrigin.Begin);

                    var formFile = new FormFile(formFileStream, 0, formFileStream.Length, "file", fileName);
                    ms.Close();
                    formFileStream.Close();
                    return formFile;
                }
            }
        }
    }
}
