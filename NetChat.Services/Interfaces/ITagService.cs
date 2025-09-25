using NetChat.Services.Models.CreateDto;
using NetChat.Services.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Services.Interfaces
{
    public interface ITagService
    {
        public Task<TagViewModel> UpdateAsync(Guid id, UpdateTagDto dto);
        public Task DeleteTag(Guid id);
        public Task<TagListViewModel> GetTagsAsync(int page, int page_size);
        public Task<TagViewModel> CreateAsync(CreateTagDto dto);
        public Task<TagViewModel> GetTag(Guid id);
    }
}
