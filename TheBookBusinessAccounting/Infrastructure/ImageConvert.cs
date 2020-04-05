using System.IO;
using System.Web;

namespace TheBookBusinessAccounting.Infrastructure
{
    public static class ImageConvert
    {
        public static byte[] ImageToByteArray(HttpPostedFileBase uploadImage)
        {
            byte[] imageData = null;
            using (var binaryReader = new BinaryReader(uploadImage.InputStream))
            {
                imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
            }

            return imageData;
        }

        public static string GetImageExtension(HttpPostedFileBase uploadImage)
        {
            return Path.GetExtension(uploadImage.FileName);
        }
    }
}