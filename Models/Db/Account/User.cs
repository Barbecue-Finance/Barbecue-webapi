using System.Collections.Generic;
using Models.Db.Common;
using Models.Db.Sessions;

namespace Models.Db.Account
{
    public class User : IdEntity
    {
        public string Username { get; set; }

        public string Login { get; set; }
        
        public string Password { get; set; }
        
        public virtual ICollection<TokenSession> TokenSessions { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<UserToUserGroup> UserGroupsRelation { get; set; }
    }
}