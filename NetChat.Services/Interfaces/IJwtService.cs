using Microsoft.Extensions.Configuration;
using NetChat.Services.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(CreateJwtTokenDto dto);
    }
}
