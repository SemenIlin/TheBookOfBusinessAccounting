using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.Mappers;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using Common.Exceptions;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public RoleDto Get(int id)
        {
            var role = _roleRepository.Get(id);
            if(role == null)
            {
                throw new NotFoundException("Такой роли не существует.", "");
            }

            return role.MapToDtoModel();
        }

        public IEnumerable<RoleDto> GetAll()
        {
            return _roleRepository.GetAll().MapToListDtoModels();
        }

        public ICollection<RoleDto> GetAllRolesOfUser(string login)
        {
            return _roleRepository.GetAllRolesOfUser(login).MapToCollectionDtoModels();
        }
    }
}
