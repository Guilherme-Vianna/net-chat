using NetChat.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Models
{
    public class UserTag : Audit
    {
        public UserTag()
        {

        }

        public Tag Tag { get; set; }
        public Guid TagId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
