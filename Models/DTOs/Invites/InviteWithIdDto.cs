using System;

namespace Models.DTOs.Invites
{
    public class InviteWithIdDto
    {
        public long Id { get; set; }

        public long IssuerId { get; set; }
        public string IssuerUsername { get; set; }
        
        public long RecipientId { get; set; }
        public string RecipientUsername { get; set; }

        public long GroupId { get; set; }
        public string GroupTitle { get; set; }

        public DateTime IssuedAt { get; set; }
    }
}