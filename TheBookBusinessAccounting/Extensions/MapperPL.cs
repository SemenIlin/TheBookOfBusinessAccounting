using BLLTheBookOfBusinessAccounting.ModelsDto;
using System.Collections.Generic;
using System.Linq;
using TheBookBusinessAccounting.Models;
using TheBookBusinessAccounting.Models.LoginAndRegistration;

namespace TheBookBusinessAccounting.Extensions
{
    public static class MapperPL
    {
        public static ItemViewModel MapToViewModel(this ItemDto itemDto)
        {
            return itemDto == null ?
                null : 
                new ItemViewModel()
                {
                    Id = itemDto.Id,
                    Title = itemDto.Title,
                    InventoryNumber = itemDto.InventoryNumber,
                    LocationOfItem = itemDto.LocationOfItem,
                    About = itemDto.About,
                    Category = itemDto.Category,
                    CategoryId = itemDto.CategoryId,
                    Status = itemDto.Status,
                    StatusId = itemDto.StatusId,
                    ImageViewModels = itemDto.ImageDtos.MapToCollectionViewModels()
                };
        }

        public static ItemDto MapToDtoModel(this ItemViewModel itemViewModel)
        {
            return itemViewModel == null ?
                null : 
                new ItemDto()
                {
                    Id = itemViewModel.Id,
                    InventoryNumber = itemViewModel.InventoryNumber,
                    Title = itemViewModel.Title,
                    LocationOfItem = itemViewModel.LocationOfItem,
                    About = itemViewModel.About,
                    Category = itemViewModel.Category,
                    CategoryId = itemViewModel.CategoryId,
                    Status = itemViewModel.Status,
                    StatusId = itemViewModel.StatusId,
                    ImageDtos = itemViewModel.ImageViewModels.MapToCollectionDtoModels()
                };
        }

        public static IEnumerable<ItemViewModel> MapToListViewModels(this IEnumerable<ItemDto> itemDtos)
        {
            return itemDtos.Select(itemDto => itemDto.MapToViewModel());
        }

        public static ImageDto MapToDtoModel(this ImageViewModel imageViewModel)
        {
            return imageViewModel == null ? 
                null : 
                new ImageDto()
                {
                    Id = imageViewModel.Id,
                    Screen = imageViewModel.Screen,
                    ScreenFormat = imageViewModel.ScreenFormat,
                    ItemId = imageViewModel.ItemId
                };
        }

        public static ImageViewModel MapToViewModel(this ImageDto imageDto)
        {
            return imageDto == null ?
                null : 
                new ImageViewModel()
                {
                    Id = imageDto.Id,
                    Screen = imageDto.Screen,
                    ScreenFormat = imageDto.ScreenFormat,
                    ItemId = imageDto.ItemId
                };
        }

        public static IEnumerable<ImageViewModel> MapToListViewModels(this IEnumerable<ImageDto> imageDtos)
        {
            return imageDtos.Select(imageDto => imageDto.MapToViewModel());
        }

        public static CategoryViewModel MapToViewModel(this CategoryDto categoryDto)
        {
            return categoryDto == null ? 
                null : 
                new CategoryViewModel()
                {
                    Id = categoryDto.Id,
                    Title = categoryDto.Title
                };
        }

        public static CategoryDto MapToDtoModel(this CategoryViewModel categoryViewModel)
        {
            return categoryViewModel == null ?
                null :
                new CategoryDto()
                {
                    Id = categoryViewModel.Id,
                    Title = categoryViewModel.Title
                };
        }

        public static IEnumerable<CategoryViewModel> MapToListViewModels(this IEnumerable<CategoryDto> categoryDtos)
        {
            return categoryDtos.Select(categoryDto => categoryDto.MapToViewModel());
        }

        public static StatusViewModel MapToViewModel(this StatusDto statusDto)
        {
            return statusDto == null ? 
                null : 
                new StatusViewModel()
                {
                    Id = statusDto.Id,
                    Title = statusDto.Title
                };
        }

        public static IEnumerable<StatusViewModel> MapToListViewModels(this IEnumerable<StatusDto> statusDtos)
        {
            return statusDtos.Select(statusDto => statusDto.MapToViewModel());
        }

        public static UserDto MapToDtoModel(this RegisterModel registerModel) 
        {
            return registerModel == null ? 
                null :
                new UserDto()
                {
                     UserLogin = registerModel.Login,
                     UserPassword = registerModel.Password,
                     Email = registerModel.Email
                };
        }

        public static ICollection<ImageDto> MapToCollectionDtoModels(this ICollection<ImageViewModel> imageViewModels)
        {
            if(imageViewModels == null)
            {
                return null;
            }

            return imageViewModels.Select(image => image.MapToDtoModel()).ToList();
        }

        public static ICollection<ImageViewModel> MapToCollectionViewModels(this ICollection<ImageDto> imageDtos)
        {
            if(imageDtos == null)
            { 
                return null;
            }

            return imageDtos.Select(image => image.MapToViewModel()).ToList();
        }

        public static UserViewModel MapToViewModel(this UserDto userDto)
        {
            return userDto == null ?
                null :
                new UserViewModel()
                {
                    Id = userDto.Id,
                    UserLogin = userDto.UserLogin,
                    UserName = userDto.UserName,
                    UserPassword = userDto.UserPassword,
                    Email = userDto.Email,
                    Roles = userDto.RoleDtos.MapToCollectionViewModels()
                };
        }

        public static UserDto MapToDtoModel(this UserViewModel userViewModel)
        {
            return userViewModel == null ?
                null : 
                new UserDto()
                {
                    Id = userViewModel.Id,
                    UserLogin = userViewModel.UserLogin,
                    UserPassword = userViewModel.UserPassword,
                    UserName = userViewModel.UserName,
                    Email = userViewModel.Email,
                    RoleDtos = userViewModel.Roles.MapToCollectionDtoModels()
                };
        }

        public static IEnumerable<UserViewModel> MapToListViewModels(this IEnumerable<UserDto> userDtos)
        {
            return userDtos.Select(userDto => userDto.MapToViewModel());
        }
        
        public static ICollection<RoleDto> MapToCollectionDtoModels(this ICollection<RoleViewModel> roleViewModels )
        {
            if (roleViewModels == null)
            {
                return null;
            }

            return roleViewModels.Select(roleViewModel => roleViewModel.MapToDtoModel()).ToList();
        }

        public static ICollection<RoleViewModel> MapToCollectionViewModels(this ICollection<RoleDto> roleDtos)
        {
            if (roleDtos == null)
            {
                return null;
            }

            return roleDtos.Select(roleDto => roleDto.MapToViewModel()).ToList();
        }

        private static RoleViewModel MapToViewModel(this RoleDto roleDto)
        {
            return new RoleViewModel()
            {
                Id = roleDto.Id,
                RoleName = roleDto.RoleName
            };
        }

        private static RoleDto MapToDtoModel(this RoleViewModel roleViewModel)
        {
            return new RoleDto()
            {
                Id = roleViewModel.Id,
                RoleName = roleViewModel.RoleName
            };
        }
    }   
}