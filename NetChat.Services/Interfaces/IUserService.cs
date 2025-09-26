using NetChat.Services.Models.UpdateDto;
using NetChat.Services.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using NetChat.Services.Models.Dto;

namespace NetChat.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> CreateAsync(CreateUserDto userViewModel);
        Task<UserViewModel> UpdateAsync(UpdateUserDto dto);
        Task<Guid> GetUserId(string email);
        Task<string> GetUserPassword(string email);
        Task<bool> Exist(string email);
    }
}
