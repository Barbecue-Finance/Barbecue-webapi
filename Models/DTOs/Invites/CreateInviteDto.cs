using Models.Db;
using Models.Db.Account;

namespace Models.DTOs.Invites
{
    public class CreateInviteDto
    {
        [Id(typeof(User))]
        public long IssuerId { get; set; }

        [Id(typeof(User))]
        public long RecipientId { get; set; }

        [Id(typeof(Group))]
        public long GroupId { get; set; }
    }
}