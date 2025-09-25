using NetChat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Services.Models.ViewModels
{
    public struct TagViewModel
    {
        public TagViewModel(Tag tag)
        {
            id = tag.Id;
            name = tag.Name;
        }

        public Guid id { get; set; }
        public string name { get; set; }
    }
}
