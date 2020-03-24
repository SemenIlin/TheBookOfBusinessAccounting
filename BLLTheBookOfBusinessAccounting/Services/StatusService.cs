using System.Collections.Generic;
using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.Mappers;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using Common.Exceptions;
using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;

namespace BLLTheBookOfBusinessAccounting.Services
{
    public class StatusService : IReadService<StatusDto>
    {
        private readonly IReadRepository<Status> _readRepository;

        public StatusService(IReadRepository<Status> readRepository)
        {
            _readRepository = readRepository;
        }

        public StatusDto Get(int id)
        {
            var status = _readRepository.Get(id);
            if(status == null)
            {
                throw new NotFoundException("Статус не найден.", "");
            }

            return status.MapToDtoModel();
        }

        public IEnumerable<StatusDto> GetAll()
        {
            return _readRepository.GetAll().MapToListDtoModels();
        }
    }
}
