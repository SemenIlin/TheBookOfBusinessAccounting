using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.ModelsDto
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public ICollection<UserDto> UserDtos { get; set; }
    }
}
