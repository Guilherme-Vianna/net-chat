using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Services.Models.Dto
{
    public record SearchChatDto(List<string> tags)
    {
    }
}
