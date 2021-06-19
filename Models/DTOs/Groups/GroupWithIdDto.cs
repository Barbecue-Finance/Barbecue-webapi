using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Db;
using Models.DTOs.Users;

namespace Models.DTOs.Groups
{
    public class GroupWithIdDto
    {
        public long Id { get; set; }

        public string Title { get; set; }
        
        public GroupType Type { get; set; }

        public ICollection<UserWithIdDto> Users { get; set; }
    }
}