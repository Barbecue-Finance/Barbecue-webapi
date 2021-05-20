using System.ComponentModel.DataAnnotations;
using Models.Db.Account;

namespace Models.DTOs.Groups
{
    public class CreateGroupDto
    {
        [Required(AllowEmptyStrings = true)]
        [String(1, 256)]
        public string Title { get; set; }
        
        [Id(typeof(User))]
        public long CreatorId { get; set; }
    }
}