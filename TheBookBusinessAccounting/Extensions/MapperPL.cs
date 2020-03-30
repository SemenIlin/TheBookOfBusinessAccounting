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
            return new ItemViewModel()
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
            return new ItemDto()
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
            return new ImageDto()
            {
                Id = imageViewModel.Id,
                Screen = imageViewModel.Screen,
                ScreenFormat = imageViewModel.ScreenFormat,
                ItemId = imageViewModel.ItemId
            };
        }

        public static ImageViewModel MapToViewModel(this ImageDto imageDto)
        {
            return new ImageViewModel()
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
            return new CategoryViewModel()
            {
                Id = categoryDto.Id,
                Title = categoryDto.Title
            };
        }

        public static CategoryDto MapToDtoModel(this CategoryViewModel categoryViewModel)
        {
            return new CategoryDto()
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
            return new StatusViewModel()
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
            return new UserDto()
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

            var imageDto = new List<ImageDto>();
            foreach (var imageViewModel in imageViewModels)
            {
                imageDto.Add(imageViewModel.MapToDtoModel());
            }

            return imageDto;
        }

        public static ICollection<ImageViewModel> MapToCollectionViewModels(this ICollection<ImageDto> imageDtos)
        {
            var imageViewModel = new List<ImageViewModel>();
            foreach (var imageDto in imageDtos)
            {
                imageViewModel.Add(imageDto.MapToViewModel());
            }

            return imageViewModel;
        }

        public static UserViewModel MapToViewModel(this UserDto userDto)
        {
            return new UserViewModel()
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
            return new UserDto()
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

            var roleDtos = new List<RoleDto>();
            foreach (var roleViewModel in roleViewModels)
            {
                roleDtos.Add(new RoleDto()
                {
                    Id = roleViewModel.Id,
                    RoleName = roleViewModel.RoleName
                });
            };

            return roleDtos;
        }

        public static ICollection<RoleViewModel> MapToCollectionViewModels(this ICollection<RoleDto> roleDtos)
        {
            if (roleDtos == null)
            {
                return null;
            }

            var roleViewModels = new List<RoleViewModel>();
            foreach (var roleDto in roleDtos)
            {
                roleViewModels.Add(new RoleViewModel()
                {
                    Id = roleDto.Id,
                    RoleName = roleDto.RoleName
                });
            };

            return roleViewModels;
        }   
    }
}