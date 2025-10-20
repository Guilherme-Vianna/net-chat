using NetChat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Repository.Interfaces
{
    public interface ITagRepository
    {
        public Task<Tag> CreateTag(Tag tag);
        public Task<bool> ExistName(string name);
        public Task<List<Tag>> GetTagsAsync(int page, int page_size);
        public Task<int> GetTagCount();
        public Task UpdateTag(Tag tag);
        public Task<Tag> GetTagById(Guid tagId);
        public Task DeleteTag(Guid tagId);
        public Task<List<Tag>> GetMostRecent(int count);
        public Task<Tag> CreateIfNotExistOrReturnIfExist(Tag tag);
    }
}
