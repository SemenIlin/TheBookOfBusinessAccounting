using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.Mappers;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using Common.Exceptions;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public void Add(UserDto userDto)
        {
            _userRepository.Create(userDto.MapToDbModel());
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public UserDto Find(string login, string password)
        {
            var userDto = _userRepository.FindUserIsLogin(login, password).MapToDtoModel();
            userDto.RoleDtos = _roleRepository.GetAllRolesOfUser(userDto.UserLogin).MapToCollectionDtoModels();

            return userDto;
        }

        public UserDto Get(int id)
        {
            var user = _userRepository.Get(id);
            if(user == null)
            {
                throw new NotFoundException("Данный пользователь не найден.","");
            }
            var userDto = user.MapToDtoModel();
            userDto.RoleDtos = _roleRepository.GetAllRolesOfUser(userDto.UserLogin).MapToCollectionDtoModels();

            return user.MapToDtoModel();
        }

        public IEnumerable<UserDto> GetAll()
        {
            var userDtos = _userRepository.GetAll().MapToListDtoModels();
            foreach(var userDto in userDtos)
            {
                userDto.RoleDtos = _roleRepository.GetAllRolesOfUser(userDto.UserLogin).MapToCollectionDtoModels();
            }

            return userDtos;
        }

        public void Update(UserDto userDto)
        {
            _userRepository.Update(userDto.MapToDbModel());
        } 
    }
}
