using System.Collections.Generic;
using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.Mappers;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using Common.Exceptions;
using DALTheBookBusinessAccounting.Interfaces;

namespace BLLTheBookOfBusinessAccounting.Services
{
    public class StatusService : IReadService<StatusDto>
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public StatusDto Get(int id)
        {
            var status = _statusRepository.Get(id);
            if(status == null)
            {
                throw new NotFoundException("Статус не найден.", "");
            }

            return status.MapToDtoModel();
        }

        public IEnumerable<StatusDto> GetAll()
        {
            return _statusRepository.GetAll().MapToListDtoModels();
        }
    }
}
