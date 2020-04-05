using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.Mappers;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using Common.Exceptions;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public void Add(ImageDto imageDto, out int id)
        {
            _imageRepository.Create(imageDto.MapToDbModel(), out id);
        }

        public void Delete(int id)
        {
            _imageRepository.Delete(id);
        }

        public ImageDto Get(int id)
        {
            var image = _imageRepository.Get(id);
            if(image == null)
            {
                throw new NotFoundException("Изображения нет.", "");
            }

            return image.MapToDtoModel();
        }

        public IEnumerable<ImageDto> GetAll()
        {
            return _imageRepository.GetAll().MapToListDtoModels();
        }

        public void Update(ImageDto imageDto)
        {
            _imageRepository.Update(imageDto.MapToDbModel());
        }
    }
}
