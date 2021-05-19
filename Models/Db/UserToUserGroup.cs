using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Account;

namespace Models.Db
{
    public class UserToUserGroup
    {
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }

        public User User { get; set; }

        [ForeignKey(nameof(UserGroup))]
        public long UserGroupId { get; set; }
        
        public UserGroup UserGroup { get; set; }
    }
}