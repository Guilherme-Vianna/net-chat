using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Models.Base
{
    public class AuditEntity : Audit
    {
        public AuditEntity() { }
        public Guid Id { get; set; } 
    }
}
