using NetChat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Services.Models.ViewModels
{
    public struct UserViewModel
    {
        public UserViewModel(Guid id, string email, string name, List<UserTag> userTags)
        {
            this.id = id;
            this.email = email;
            this.name = name;
            foreach(var tag in userTags)
            {
                tags.Add(new UserTagViewModel(tag.Tag.Name, tag.TagId)); 
            }
        }
        public Guid id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public List<UserTagViewModel> tags { get; set; } = [];
    }
}
