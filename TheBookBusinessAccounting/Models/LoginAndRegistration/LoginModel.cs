using System.ComponentModel.DataAnnotations;

namespace TheBookBusinessAccounting.Models.LoginAndRegistration
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}