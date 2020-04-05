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

        public void Add(UserDto userDto,out int id)
        {
            _userRepository.Create(userDto.MapToDbModel(), out id);
        }

        public void AddRoleForUser(int userId, int roleId)
        {
            _userRepository.AddRole(userId, roleId);
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public void DeleteRoleFromUser(int userId, int roleId)
        {
            _userRepository.DeleteRole(userId, roleId);
        }

        public UserDto Find(string login, string password)
        {
            var userDto = _userRepository.FindUserIsLogin(login, password).MapToDtoModel();
            if (userDto == null)
            {
                return userDto;            
            }

            userDto.RoleDtos = _roleRepository.GetAllRolesOfUser(userDto.UserLogin).MapToCollectionDtoModels();

            return userDto;
        }

        public UserDto Find(string login)
        {
            var userDto = _userRepository.FindUser(login).MapToDtoModel();
            if (userDto == null)
            {
                throw new NotFoundException("Данный пользователь не найден.", "");
            }

            userDto.RoleDtos = _roleRepository.GetAllRolesOfUser(userDto.UserLogin).MapToCollectionDtoModels();

            return userDto;
        }

        public IEnumerable<UserDto> Find(string userLogin, int pageSize, int skip)
        {
            return _userRepository.Find(userLogin, pageSize, skip).MapToListDtoModels();
        }

        public IEnumerable<UserDto> FindAll(string userLogin)
        {
            return _userRepository.Find(userLogin).MapToListDtoModels();
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
            var users = _userRepository.GetAll();
            if(users == null)
            {
                throw new NotFoundException("Пользователей не найдено.", "");
            }

            var userDtos = users.MapToListDtoModels();
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
