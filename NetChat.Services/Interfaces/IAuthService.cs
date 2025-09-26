using NetChat.Services.Models.Dto;
using NetChat.Services.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResponseViewModel> Login(LoginDto dto);
    }
}
