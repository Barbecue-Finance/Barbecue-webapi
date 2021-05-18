using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Account;
using Models.Db.Common;

namespace Models.Db.Sessions
{
    public class TokenSession : IdEntity
    {
        public string Token { get; set; }

        [ForeignKey(nameof(User))]
        public long UserAccountId { get; set; }

        public virtual User User { get; set; }

        public DateTime StartDate { get; set; }

        // Not null, because token has an expiration date
        public DateTime EndDate { get; set; }
    }
}