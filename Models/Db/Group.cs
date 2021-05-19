using System.Collections.Generic;
using Models.Db.Account;
using Models.Db.Common;

namespace Models.Db
{
    public class Group : IdEntity
    {
        public string Title { get; set; }
        
        public virtual Purse Purse { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<UserToGroup> UsersRelation { get; set; }

        public virtual ICollection<Invite> Invites { get; set; }
    }
}