using System.Web;

namespace TheBookBusinessAccounting.Infrastructure
{
    public static class ImageConvert
    {
        public static byte[] ImageToByteArray(HttpPostedFileBase uploadImage)
        {
            byte[] imageData = null;
            // считываем переданный файл в массив байтов
            using (var binaryReader = new System.IO.BinaryReader(uploadImage.InputStream))
            {
                imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
            }

            return imageData;
        }

        public static string GetImageExtension(HttpPostedFileBase uploadImage)
        {
            string mirrorExtensionImage = System.String.Empty;
            string extensionImage = System.String.Empty;

            for (int i = uploadImage.FileName.Length - 1; i > 0; --i)
            {                
                if (uploadImage.FileName[i] == '.')
                {
                    break;
                }

                mirrorExtensionImage += uploadImage.FileName[i];
            }

            for (int i = mirrorExtensionImage.Length - 1; i >= 0; --i)
            {
                extensionImage += mirrorExtensionImage[i];               
            }

            return extensionImage;
        }
    }
}