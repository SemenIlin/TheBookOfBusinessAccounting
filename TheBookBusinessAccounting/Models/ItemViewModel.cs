using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheBookBusinessAccounting.Models
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 100 символов")]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов")]
        [Display(Name = "Инвентарный номер")]
        public string InventoryNumber { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 200 символов")]
        [Display(Name = "Месторасположение")]
        public string LocationOfItem { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Описание")]
        public string About { get; set; }

        [Display(Name = "Категория")]
        public string Category { get; set; }

        [Display(Name = "Статус")]
        public string Status { get; set; }

        [Display(Name = "Картинки")]
        public ICollection<ImageViewModel> ImageViewModels { get; set; }
        public byte[] Screen { get; set; }
        public string ScreenFormat { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public int StatusId { get; set; }        
    }
}