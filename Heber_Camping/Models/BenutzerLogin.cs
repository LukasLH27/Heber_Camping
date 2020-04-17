using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heber_Camping.Models
{
    public class BenutzerLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public BenutzerLogin() : this("", "") { }
        public BenutzerLogin(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }


    }
}