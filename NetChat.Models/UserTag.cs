using NetChat.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Models
{
    public class UserTag : AuditEntity
    {
        public UserTag()
        {

        }

        public UserTag(Guid tagId, Guid userId)
        {
            TagId = tagId;
            UserId = userId;
        }

        public Tag Tag { get; set; }
        public Guid TagId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
