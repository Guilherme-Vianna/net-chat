using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Services.Models.Dto
{
    public class CreateJwtTokenDto
    {
        public CreateJwtTokenDto(string email, Guid id)
        {
            Email = email;
            Id = id.ToString();
        }

        public readonly string Email;
        public readonly string Id;
    }
}
