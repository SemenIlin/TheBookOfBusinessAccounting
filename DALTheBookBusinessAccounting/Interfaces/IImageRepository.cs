using DALTheBookBusinessAccounting.Entities;
using System.Collections.Generic;

namespace DALTheBookBusinessAccounting.Interfaces
{
    public interface  IImageRepository
    {
        IEnumerable<Image> GetAll();
        Image Get(int id);

        void Create(Image image, out int id);
        void Update(Image Image);
        void Delete(int id);
    }
}
