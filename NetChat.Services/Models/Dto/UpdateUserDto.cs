namespace NetChat.Services.Models.Dto;

public class UpdateUserDto
{
    public Guid id { get; set; }
    public string email { get; set; }
    public string name { get; set; }
    public List<Guid> tags_ids { get; set; }
}