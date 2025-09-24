using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Models.Base
{
    public class Audit
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
