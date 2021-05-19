using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Account;

namespace Models.Db
{
    public class UserToGroup
    {
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }

        public User User { get; set; }

        [ForeignKey(nameof(Group))]
        public long GroupId { get; set; }
        
        public Group Group { get; set; }
    }
}