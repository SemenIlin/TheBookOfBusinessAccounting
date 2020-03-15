using System.IO;
using System.Drawing;

namespace DALTheBookOfBusinessAccounting.Images
{
    public static class ConvertImage
    {
        public static (byte[] Screen, string ImageExtension) PutImageBinaryInDb(string iFile)
        {
            // конвертация изображения в байты
            byte[] imageData;
            FileInfo fInfo = new FileInfo(iFile);
            long numBytes = fInfo.Length;
            using (FileStream fStream = new FileStream(iFile, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fStream))
                {
                    imageData = br.ReadBytes((int)numBytes);
                }
            }
            // получение расширения файла изображения не забыв удалить точку перед расширением
            string iImageExtension = (Path.GetExtension(iFile)).Replace(".", "").ToLower();

            return (imageData, iImageExtension);           
        }

        public static Image GetImage(byte[] bytesOfImage, string imageExtension)
        {
            // конвертируем бинарные данные в изображение
            byte[] imageData = bytesOfImage; // возвращает массив байт из БД. Так как у нас SQL вернёт одну запись и в ней хранится нужное нам изображение, то из листа берём единственное значение с индексом '0'
            MemoryStream ms = new MemoryStream(imageData);
            Image newImage = Image.FromStream(ms);

            // сохраняем изоражение на диск
            string iImageExtension = imageExtension; // получаем расширение текущего изображения хранящееся в БД
            string iImageName = System.DateTime.Now + "." + iImageExtension; // задаём путь сохранения и имя нового изображения
            if (iImageExtension == "png") 
            { 
                newImage.Save(iImageName, System.Drawing.Imaging.ImageFormat.Png);
            }
            else if (iImageExtension == "jpg" || iImageExtension == "jpeg") 
            { 
                newImage.Save(iImageName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else if (iImageExtension == "png") 
            {
                newImage.Save(iImageName, System.Drawing.Imaging.ImageFormat.Png); 
            }

            return newImage;
        }

    }
}
