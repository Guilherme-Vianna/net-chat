using NetChat.Services.Models.CreateDto;
using NetChat.Services.Models.UpdateDto;
using NetChat.Services.Models.ViewModels;

namespace NetChat.Services.Interfaces
{
    public interface ITagService
    {
        public Task<TagViewModel> UpdateAsync(Guid id, UpdateTagDto dto);
        public Task DeleteTag(Guid id);
        public Task<TagListViewModel> GetTagsAsync(int page, int page_size);
        public Task<TagListViewModel> GetMostRecent();
        public Task<TagViewModel> CreateAsync(CreateTagDto dto);
        public Task<TagViewModel> GetTag(Guid id);
    }
}
