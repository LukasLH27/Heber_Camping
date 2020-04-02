using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heber_Camping.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
	    public string Telnum { get; set; }
        public DateTime? DateArrival { get; set; }
        public DateTime? DateDeparture { get; set; }
        public int? CountOfPeople { get; set; }
        public string Comments { get; set; }

        public Request() : this(0, "", "","","",null,null,0,"") { }

        public Request(int id, string firstname, string lastname, string email,
            string telnum,DateTime? dateArrival,DateTime? dateDeparture, int countOfPeople,
            string comments)
        {
            this.Id = id;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Email = email;
            this.Telnum = telnum;
            this.DateArrival = dateArrival;
            this.DateDeparture = dateDeparture;
            this.CountOfPeople = countOfPeople;
            this.Comments = comments;
        }

        //ToString()


    }
}