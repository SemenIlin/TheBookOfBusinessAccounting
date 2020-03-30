using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheBookBusinessAccounting.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserLogin { get; set; }

        [Required]
        [Display(Name ="Имя")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string UserPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Повторить пароль")]
        [Compare("UserPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public ICollection<RoleViewModel> Roles { get; set; }
    }
}