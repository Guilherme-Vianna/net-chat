using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Services.Models.ViewModels
{
    public record FriendViewModel(string? last_message, DateTime? last_messsage_date, string friend_name, Guid friend_id);
}
