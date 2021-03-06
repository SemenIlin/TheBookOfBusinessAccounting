﻿using BLLTheBookOfBusinessAccounting.ModelsDto;
using DALTheBookBusinessAccounting.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BLLTheBookOfBusinessAccounting.Mappers
{
    public static class Mapper
    {
        public static Item MapToDbModel(this ItemDto itemDto)
        {
            return itemDto == null ?
                null :
                new Item()
                {
                    Id = itemDto.Id,
                    Title = itemDto.Title,
                    InventoryNumber = itemDto.InventoryNumber,
                    LocationOfItem = itemDto.LocationOfItem,
                    About = itemDto.About,
                    Images = itemDto.ImageDtos.MapToCollectionDBModels(),
                    StatusId = itemDto.StatusId,
                    CategoryId = itemDto.CategoryId
                };
        }

        public static ItemDto MapToDtoModel(this Item item)
        {
            return item == null ?
                null : 
                new ItemDto()
                {
                    Id = item.Id,
                    Title = item.Title,
                    InventoryNumber = item.InventoryNumber,
                    About = item.About,
                    LocationOfItem = item.LocationOfItem,
                    CategoryId = item.CategoryId,
                    StatusId = item.StatusId,
                    Status = item.StatusName,
                    ImageDtos = item.Images.MapToCollectionDtoModels(),
                    Category = item.CategoryName
                };
        }

        public static IEnumerable<ItemDto> MapToListDtoModels(this IEnumerable<Item> items)
        {
            return items.Select(item => item.MapToDtoModel());
        }

        public static Category MapToDbModel(this CategoryDto categoryDto)
        {
            return categoryDto == null ? 
                null : 
                new Category()
                {
                    Id = categoryDto.Id,
                    Title = categoryDto.Title
                };
        }

        public static CategoryDto MapToDtoModel(this Category category)
        {
            return category == null ? 
                null : 
                new CategoryDto()
                {
                    Id = category.Id,
                    Title = category.Title
                };
        }

        public static IEnumerable<CategoryDto> MapToListDtoModels(this IEnumerable<Category> categories)
        {
            return categories.Select(category => category.MapToDtoModel());
        }

        public static StatusDto MapToDtoModel(this Status status)
        {
            return status == null ? 
                null : 
                new StatusDto()
                {
                    Id = status.Id,
                    Title = status.Title,
                };
        }

        public static IEnumerable<StatusDto> MapToListDtoModels(this IEnumerable<Status> statuses)
        {
            return statuses.Select(status => status.MapToDtoModel());
        }

        public static ImageDto MapToDtoModel(this Image image)
        {
            return image == null ?
                null : 
                new ImageDto()
                {
                    Id = image.Id,
                    Screen = image.Screen,
                    ScreenFormat = image.ScreenFormat,
                    ItemId = image.ItemId
                };
        }

        public static Image MapToDbModel(this ImageDto imageDto)
        {
            return imageDto == null ?
                null :
                new Image()
                {
                    Id = imageDto.Id,
                    Screen = imageDto.Screen,
                    ScreenFormat = imageDto.ScreenFormat,
                    ItemId = imageDto.ItemId
                };
        }

        public static IEnumerable<ImageDto> MapToListDtoModels(this IEnumerable<Image> images)
        {
            return images.Select(image => image.MapToDtoModel());
        }

        public static UserDto MapToDtoModel(this User user)
        {
            return user == null ? 
                null : 
                new UserDto()
                {
                    Id = user.Id,
                    UserLogin = user.UserLogin,
                    UserPassword = user.UserPassword,
                    UserName = user.UserName,
                    Email = user.Email
                };
        }

        public static User MapToDbModel(this UserDto userDto)
        {
            return userDto == null ? 
                null : 
                new User()
                { 
                    Id = userDto.Id,
                    UserLogin = userDto.UserLogin,
                    UserName = userDto.UserName,
                    UserPassword = userDto.UserPassword,
                    Email = userDto.Email,
                    UsersRoles = userDto.RoleDtos.MapToCollectionDbModels(userDto.Id)
                };
        }

        public static IEnumerable<UserDto> MapToListDtoModels(this IEnumerable<User> users)
        {
            return users.Select(user => user.MapToDtoModel());
        }

        public static RoleDto MapToDtoModel(this Role role)
        {
            return role == null ? 
                null : 
                new RoleDto()
                { 
                    Id = role.Id,
                    RoleName = role.RoleName
                };
        }

        public static IEnumerable<RoleDto> MapToListDtoModels(this IEnumerable<Role> roles)
        {
            return roles.Select(role => role.MapToDtoModel());
        }

        public static ICollection<RoleDto> MapToCollectionDtoModels(this ICollection<Role> roles)
        {
            if(roles == null)
            {
                return null;
            }

            return roles.Select(role => role.MapToDtoModel()).ToList();
        }

        public static ICollection<ImageDto> MapToCollectionDtoModels(this ICollection<Image> images)
        {
            if(images == null)
            {
                return null;
            }

            return images.Select(image => image.MapToDtoModel()).ToList();          
        }

        public static ICollection<Image> MapToCollectionDBModels(this ICollection<ImageDto> itemDtos)
        {
            if(itemDtos == null)
            {
                return null;
            }

            return itemDtos.Select(item => item.MapToDbModel()).ToList();
        }

        private static ICollection<UsersRole> MapToCollectionDbModels(this ICollection<RoleDto> roleDtos, int userId)
        {
            if(roleDtos == null)
            {
                return null;
            }

            var collectionRoles = new List<UsersRole>();
            foreach(var role in roleDtos)
            {
                collectionRoles.Add(new UsersRole()
                {
                    RoleId = role.Id,
                    UserId = userId                    
                });
            }

            return collectionRoles;
        }        
    }
}
