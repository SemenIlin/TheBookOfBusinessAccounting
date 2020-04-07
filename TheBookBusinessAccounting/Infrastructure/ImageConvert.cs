using System.IO;
using System.Web;

namespace TheBookBusinessAccounting.Infrastructure
{
    public static class ImageConvert
    {
        public static byte[] ImageToByteArray(HttpPostedFileBase uploadImage)
        {
            using (var binaryReader = new BinaryReader(uploadImage.InputStream))
            {
                return binaryReader.ReadBytes(uploadImage.ContentLength);
            }
        }

        public static string GetImageExtension(HttpPostedFileBase uploadImage)
        {
            return Path.GetExtension(uploadImage.FileName);
        }
    }
}