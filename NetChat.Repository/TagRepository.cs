using Microsoft.EntityFrameworkCore;
using NetChat.Database;
using NetChat.Models;
using NetChat.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Repository
{
    public class TagRepository(NetChatContext context) : ITagRepository
    {
        public async Task<Tag> CreateTag(Tag tag)
        {
            var newTag = await context.Tags.AddAsync(tag);
            await context.SaveChangesAsync();
            return newTag.Entity;
        }

        public async Task UpdateTag(Tag tag)
        {
            context.Tags.Update(tag);
            await context.SaveChangesAsync();
        }

        public async Task<Tag> GetTagById(Guid tagId)
        {
            var tag = await context.Tags.AsNoTracking().FirstOrDefaultAsync(x => x.Id == tagId);
            if (tag == null) throw new Exception("Tag not found");
            return tag;
        }

        public async Task DeleteTag(Guid tagId)
        {
            var tag = await context.Tags.FirstOrDefaultAsync(x =>  x.Id == tagId);
            if(tag != null)
            {
                context.Tags.Remove(tag);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistName(string name)
        {
            return await context.Tags.AnyAsync(x => x.Name == name); 
        }

        public async Task<int> GetTagCount()
        {
            return await context.Tags.CountAsync();
        }

        public async Task<List<Tag>> GetTagsAsync(int page, int page_size)
        {
            if (page < 1) page = 1;
            if (page_size < 1) page_size = 10; 

            return await context.Tags
                .AsNoTracking()
                .Skip((page - 1) * page_size)
                .Take(page_size)
                .ToListAsync();
        }

        public async Task<List<Tag>> GetMostRecent(int count)
        {
            return await context.Tags
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<Tag> CreateIfNotExistOrReturnIfExist(Tag tag)
        {
            var existingTag = await context.Tags.FirstOrDefaultAsync(x => x.Name == tag.Name);
            if (existingTag != null) return existingTag;
    
            var newTag = await context.Tags.AddAsync(tag);
            await context.SaveChangesAsync();
            return newTag.Entity;
        }

        public async Task<Guid?> GetTagIdByName(string name)
        {
            var tag = await context.Tags.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
            return tag?.Id;
        }
    }
}
