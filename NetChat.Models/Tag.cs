using NetChat.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Models
{
    public class Tag : AuditEntity
    {
        public Tag() { }

        public Tag(string name)
        {
            Name = name;
        }

        public Tag Update(string name)
        {
            Name = name;
            return this;
        }

        public string Name { get; private set; }
    }
}
