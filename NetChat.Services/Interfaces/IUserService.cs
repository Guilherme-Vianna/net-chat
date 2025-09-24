using NetChat.Services.Models.CreateDto;
using NetChat.Services.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserViewModel> CreateAsync(CreateUserDto userViewModel);
    }
}
