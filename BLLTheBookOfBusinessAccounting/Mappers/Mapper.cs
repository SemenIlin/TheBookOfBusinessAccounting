using BLLTheBookOfBusinessAccounting.ModelsDto;
using DALTheBookBusinessAccounting.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BLLTheBookOfBusinessAccounting.Mappers
{
    public static class Mapper
    {
        public static Item MapToDbModel(this ItemDto itemDto)
        {
            return new Item()
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
            return new ItemDto()
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
            return new Category()
            {
                Id = categoryDto.Id,
                Title = categoryDto.Title
            };
        }

        public static CategoryDto MapToDtoModel(this Category category)
        {
            return new CategoryDto()
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
            return new StatusDto()
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
            return new ImageDto()
            {
                Id = image.Id,
                Screen = image.Screen,
                ScreenFormat = image.ScreenFormat,
                ItemId = image.ItemId
            };
        }

        public static Image MapToDbModel(this ImageDto imageDto)
        {
            return new Image()
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

        private static ICollection<ImageDto> MapToCollectionDtoModels(this ICollection<Image> images)
        {
            var collectionByImage = new List<ImageDto>();
            foreach (var image in images)
            {
                collectionByImage.Add(image.MapToDtoModel());
            }

            return collectionByImage;
        }

        private static ICollection<Image> MapToCollectionDBModels(this ICollection<ImageDto> itemDtos)
        {
            var collectionByImage = new List<Image>();
            foreach (var itemDto in itemDtos)
            {
                collectionByImage.Add(itemDto.MapToDbModel());
            }

            return collectionByImage;
        }
    }
}
