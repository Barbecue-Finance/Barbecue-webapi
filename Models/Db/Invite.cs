using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Account;
using Models.Db.Common;

namespace Models.Db
{
    public class Invite : IdEntity
    {
        [ForeignKey(nameof(Issuer))]
        public long IssuerId { get; set; }

        public virtual User Issuer { get; set; }

        [ForeignKey(nameof(Recipient))]
        public long RecipientId { get; set; }

        public virtual User Recipient { get; set; }

        [ForeignKey(nameof(Group))]
        public long GroupId { get; set; }

        public virtual Group Group { get; set; }

        public InviteState State { get; set; }
        
        public DateTime IssuedAt { get; set; }
    }
}