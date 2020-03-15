using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.Mappers;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using Common.Exceptions;
using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Services
{
    public class ImageService : IReadAndEditService<ImageDto>
    {
        private readonly IReadAndEditRepository<Image> _readAndEditRepository;

        public ImageService(IReadAndEditRepository<Image> readAndEditRepository)
        {
            _readAndEditRepository = readAndEditRepository;
        }

        public void Add(ImageDto imageDto)
        {
            _readAndEditRepository.Create(imageDto.MapToDbModel());
        }

        public void Delete(int id)
        {
            _readAndEditRepository.Delete(id);
        }

        public ImageDto Get(int id)
        {
            var image = _readAndEditRepository.Get(id);
            if(image == null)
            {
                throw new NotFoundException("Изображения нет.", "");
            }

            return image.MapToDtoModel();
        }

        public IEnumerable<ImageDto> GetAll()
        {
            return _readAndEditRepository.GetAll().MapToListDtoModels();
        }

        public void Update(ImageDto imageDto)
        {
            _readAndEditRepository.Update(imageDto.MapToDbModel());
        }
    }
}
