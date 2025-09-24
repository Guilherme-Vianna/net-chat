using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Services.Models.CreateDto
{
    public class CreateUserDto
    {
        public string email { get;set; }
        public string name { get; set; }
        public string password { get; set; }
        public List<Guid> tags_ids { get; set; }
    }
}
