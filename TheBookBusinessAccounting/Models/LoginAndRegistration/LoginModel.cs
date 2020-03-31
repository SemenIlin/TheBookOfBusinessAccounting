using System.ComponentModel.DataAnnotations;

namespace TheBookBusinessAccounting.Models.LoginAndRegistration
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

    }
}