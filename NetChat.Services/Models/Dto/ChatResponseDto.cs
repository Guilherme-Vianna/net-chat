using NetChat.Models;

namespace NetChat.Services.Models.Dto;

public class ChatResponseDto
{
    public ChatResponseDto(Guid userId, string name, List<Tag> search_tags, List<Tag> user_tags)
    {
        user_id = userId;
        this.name = name;
        var tagsView = new List<TagView>();
        var userTags = new List<TagView>();
        foreach (var tag in search_tags)
        {
            var tagView = new TagView(tag.Name, tag.Id);
            tagsView.Add(tagView);
        }
        
        foreach (var tag in user_tags)
        {
            var tagView = new TagView(tag.Name, tag.Id);
            userTags.Add(tagView);
        }
        
        tags_search = tagsView;
        this.user_tags = userTags;
    }
    public Guid user_id { get; set; }
    public string name { get; set; }
    public List<TagView> tags_search { get; set; }
    public List<TagView> user_tags { get; set; }
}

public class TagView
{
    public TagView(string name, Guid id)
    {
        this.name = name;
        this.id = id;
    }
    public string name { get; set; }
    public Guid id { get; set; }
}