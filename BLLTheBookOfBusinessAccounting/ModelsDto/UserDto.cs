using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.ModelsDto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public ICollection<RoleDto> RoleDtos { get; set; }
    }
}
