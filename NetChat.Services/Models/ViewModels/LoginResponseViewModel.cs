using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Services.Models.ViewModels
{
    public class LoginResponseViewModel
    {
        public LoginResponseViewModel(string token)
        {
            this.token = token;
        }

        public  string token { init; get; }
    }
}
