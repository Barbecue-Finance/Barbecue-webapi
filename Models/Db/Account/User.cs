using System.Collections.Generic;
using Models.Db.Common;
using Models.Db.MoneyOperations;
using Models.Db.Sessions;

namespace Models.Db.Account
{
    public class User : IdEntity
    {
        public string Username { get; set; }

        public string Login { get; set; }
        
        public string Password { get; set; }
        
        public virtual ICollection<TokenSession> TokenSessions { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<UserToGroup> GroupsRelation { get; set; }
        
        public virtual ICollection<Invite> IssuedInvites { get; set; }
        public virtual ICollection<Invite> ReceivedInvites { get; set; }
        
        public virtual ICollection<IncomeMoneyOperation> IncomeMoneyOperations { get; set; }
        public virtual ICollection<OutComeMoneyOperation> OutComeMoneyOperations { get; set; }
    }
}