using BLLTheBookOfBusinessAccounting.ModelsDto;
using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Interfaces
{
    public interface IImageService
    {
        ImageDto Get(int id);
        IEnumerable<ImageDto> GetAll();

        void Add(ImageDto imageDto, out int id);
        void Update(ImageDto imageDto);
        void Delete(int id);
    }
}
