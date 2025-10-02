using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Services.Models.ViewModels
{
    public class LoginResponseViewModel
    {
        public LoginResponseViewModel(string token)
        {
            Token = token;
        }

        public  string Token { init; get; }
    }
}
