namespace NetChat.Services.Models.Dto;

public class UpdatePasswordDto
{
    public Guid id { get; set; }
    public string old_password { get; set; }
    public string new_password { get; set; }
}