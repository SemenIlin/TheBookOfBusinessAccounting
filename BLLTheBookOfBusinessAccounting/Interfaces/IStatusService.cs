using BLLTheBookOfBusinessAccounting.ModelsDto;
using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Interfaces
{
    public interface IStatusService
    {
        StatusDto Get(int id);
        IEnumerable<StatusDto> GetAll();
    }
}
