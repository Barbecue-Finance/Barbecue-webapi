using System.Collections.Generic;
using Models.DTOs.Users;

namespace Models.DTOs.Groups
{
    public class GroupWithIdDto
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public ICollection<UserWithIdDto> Users { get; set; }
    }
}