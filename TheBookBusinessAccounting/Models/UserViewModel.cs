using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheBookBusinessAccounting.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Логин")]
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

        public int RoleId { get; set; }
        public Dictionary<int, string> RoleNames { get; } = new Dictionary<int, string>()
        {
            {1, "User" },
            {2, "Editor" },
            {3, "Administrator" }
        };

        public ICollection<RoleViewModel> Roles { get; set; }
    }
}