using NetChat.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Models
{
    public class Tag : AuditEntity
    {
        public Tag() { }
        public string Name { get; set; }
    }
}
