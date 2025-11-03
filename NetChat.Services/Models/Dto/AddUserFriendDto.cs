using System;

namespace NetChat.Services.Models.Dto;

public class AddUserFriendDto
{
    public Guid user_id { get; private set; }
    public void InsertUserId(Guid userId)
    {
        user_id = userId;
    }
    public Guid friend_id { get; set;}
}
