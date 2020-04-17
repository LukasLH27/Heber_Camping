using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heber_Camping.Models
{
    public enum BenutzerRolle
    {
        registrierterBenutzer, admin, gast
    }
    public class Benutzer
    {
        private int _id;

        public int ID
        {
            get { return this._id; }
            set
            {
                if (value >= this._id)
                {
                    this._id = value;
                }
            }
        }


        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }
        public BenutzerRolle Rolle { get; set; }


        public Benutzer() : this(0, "", "", "", "", "", BenutzerRolle.gast) { }

        public Benutzer(int id, string firstname, string lastname, string email, string password, string passwordConfirm, BenutzerRolle rolle)
        {
            this.ID = id;   
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Email = email;
            this.Password = password;
            this.PasswordConfirm = passwordConfirm;
            this.Rolle = rolle;
        }



    }
}