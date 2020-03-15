using BLLTheBookOfBusinessAccounting.ModelsDto;
using System.Collections.Generic;
using System.Linq;
using TheBookBusinessAccounting.Models;

namespace TheBookBusinessAccounting.Mappers
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

        private static ICollection<ImageDto> MapToCollectionDtoModels(this ICollection<ImageViewModel> imageViewModels)
        {
            var imageDto = new List<ImageDto>();
            foreach (var imageViewModel in imageViewModels)
            {
                imageDto.Add(imageViewModel.MapToDtoModel());
            }

            return imageDto;
        }

        private static ICollection<ImageViewModel> MapToCollectionViewModels(this ICollection<ImageDto> imageDtos)
        {
            var imageViewModel = new List<ImageViewModel>();
            foreach (var imageDto in imageDtos)
            {
                imageViewModel.Add(imageDto.MapToViewModel());
            }

            return imageViewModel;
        }
    }
}