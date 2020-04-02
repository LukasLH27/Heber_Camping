using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heber_Camping.Models;
using Heber_Camping.Models.db;


namespace Heber_Camping.Controllers
{
    public class ReservierungController : Controller
    {
        private IRepositoryDB rep;

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Anfrage()
        {
            return View(new Request());
        }


        [HttpPost]
        public ActionResult Anfrage(Request request)
        { 

            //1. Parameter überprüfen
            if (request == null)
            {
                return RedirectToAction("Anfrage");
            }

            CheckRequests(request);

   
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            else
            {
                rep = new RepositoryDB();

                rep.Open();
 
                if (rep.Insert(request))
                {
 
                    rep.Close();
                    //erfolgreich
                    return View("Message", new Message("Anfrage", "Ihre Anfrage wurden erfolgreich gesendet"));
                }
                else
                {
                    rep.Close();
                    //Fehler
                    return View("Message", new Message("Anfrage", "Ihre Anfrage konnte nicht gesendet werden!"));
                }

            }
        }

        private void CheckRequests(Request request)
        {
            if (request == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(request.Firstname.Trim()))
            {
                ModelState.AddModelError("Firstname", "Vorname ist ein Pflichtfeld.");
            }

            if (string.IsNullOrEmpty(request.Lastname.Trim()))
            {
                ModelState.AddModelError("Lastname", "Nachname ist ein Pflichtfeld.");
            }

            if (string.IsNullOrEmpty(request.Email.Trim()))
            {
                ModelState.AddModelError("Email", "Email ist ein Pflichtfeld.");
            }

            if (string.IsNullOrEmpty(request.Telnum.Trim()))
            {
                ModelState.AddModelError("Telnum", "Telefonnummer ist ein Pflichtfeld.");
            }

            if (request.DateArrival == null)
            {
                ModelState.AddModelError("DateArrival", "Bitte tragen sie ihr Datum der Ankunft ein.");
            }

            if (request.DateDeparture == null)
            {
                ModelState.AddModelError("DateDeparture", "Bitte tragen sie ihr Datum der Abreise ein.");
            }

            if (request.CountOfPeople == null)
            {
                ModelState.AddModelError("CountOfPeople", "Bitte tragen sie die Anzahl der Personen ein.");
            }

            if (!EmailContainsAddSign(request.Email))
            {
                ModelState.AddModelError("EMail", "Ihre Email muss ein [@] Zeichen enthalten.");
            }
            if (!EmailContainsAddSign(request.Email))
            {
                ModelState.AddModelError("EMail", "Ihre Email muss einen [.] enthalten.");
            }

        }

        private bool EmailContainsAddSign(string email)
        {
            string allowedChars = "@";
            int count = 0;
            int minCount = 1;

            foreach (char c in email)
            {
                if (allowedChars.Contains(c))
                {
                    count++;
                }
            }

            return count >= minCount;
        }

        private bool EmailContainsDot(string email)
        {
            string allowedChars = ".";
            int count = 0;
            int minCount = 1;

            foreach (char c in email)
            {

                if (allowedChars.Contains(c))
                {
                    count++;
                }
            }
            return count >= minCount;
        }
    }
}