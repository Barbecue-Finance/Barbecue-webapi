using System.Collections.Generic;
using Models.Db.Account;
using Models.Db.Common;

namespace Models.Db
{
    public class UserGroup : IdEntity
    {
        public virtual Purse Purse { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<UserToUserGroup> UsersRelation { get; set; }
    }
}