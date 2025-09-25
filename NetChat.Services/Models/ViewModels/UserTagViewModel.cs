using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Services.Models.ViewModels
{
    public struct UserTagViewModel
    {
        public UserTagViewModel(string name, Guid tag_id)
        {
            this.name = name;
            this.tag_id = tag_id;
        }

        public string name { get; set; }
        public Guid tag_id { get; set; }
    }
}
