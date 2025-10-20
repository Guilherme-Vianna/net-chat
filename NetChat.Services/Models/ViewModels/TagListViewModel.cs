using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Services.Models.ViewModels
{
    public struct TagListViewModel
    {
        public TagListViewModel(List<TagViewModel> tagViewModels, int? totalCount = null)
        {
            data = tagViewModels;
            total_count = totalCount;
        }
        public List<TagViewModel> data { get; set; }
        public int? total_count { get; set; }
    }
}
