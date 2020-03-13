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
                Title =itemDto.Title,
                InventoryNumber = itemDto.InventoryNumber,
                LocationOfItem = itemDto.LocationOfItem,
                About = itemDto.About,
                Images = itemDto.Images,
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
                Images =item.Images
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


    }
}
