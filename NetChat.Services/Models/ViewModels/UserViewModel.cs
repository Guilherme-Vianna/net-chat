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
            Id = id;
            Email = email;
            Name = name;
            foreach(var tag in userTags)
            {
                tags.Add(new UserTagViewModel(tag.Tag.Name, tag.TagId)); 
            }
        }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public List<UserTagViewModel> tags { get; set; } = [];
    }
}
