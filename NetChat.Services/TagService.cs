using NetChat.Models;
using NetChat.Repository.Interfaces;
using NetChat.Services.Interfaces;
using NetChat.Services.Models.CreateDto;
using NetChat.Services.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace NetChat.Services
{
    public class TagService(ITagRepository repository) : ITagService
    {
        public async Task<TagViewModel> CreateAsync(CreateTagDto dto)
        {
            var exist = await repository.ExistName(dto.name);
            if (exist) throw new Exception("Tag already exist");
            var newTag = new Tag(dto.name);
            var register = await repository.CreateTag(newTag);
            var result = new TagViewModel(register);
            return result;
        }

        public async Task<TagViewModel> UpdateAsync(Guid id, UpdateTagDto dto)
        {
            var exist = await repository.ExistName(dto.name);
            if (exist) throw new Exception("Tag already exist");
            var tag = await repository.GetTagById(id);
            tag.Update(dto.name); 
            await repository.UpdateTag(tag);
            var result = new TagViewModel(tag);
            return result;
        }

        public async Task DeleteTag(Guid id)
        {
            await repository.DeleteTag(id);
        }

        public async Task<TagListViewModel> GetTagsAsync(int page, int page_size)
        {
            var totalCount = await repository.GetTagCount();
            var tags = await repository.GetTagsAsync(page, page_size); 
            var tagsView = tags.Select(x => new TagViewModel(x)).ToList();
            return new TagListViewModel(totalCount, tagsView);
        }

        public async Task<TagViewModel> GetTag(Guid id)
        {
            var tag = await repository.GetTagById(id);
            return new TagViewModel(tag);
        }
    }
}
