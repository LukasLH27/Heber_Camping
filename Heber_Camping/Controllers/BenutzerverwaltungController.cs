using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heber_Camping.Models;
using Heber_Camping.Models.db;

namespace Heber_Camping.Controllers
{
    public class BenutzerverwaltungController : Controller
    {
        private IRepositoryBenutzer rep;
        private IRepositoryDB repDB;

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View(new BenutzerLogin());
            
        }
        [HttpPost]
        public ActionResult Login(BenutzerLogin benutzerLogin)
        {
            Benutzer benutzer = new Benutzer();
            rep = new RepositoryBenutzer();
            rep.Open();
            benutzer = rep.Login(benutzerLogin);
            rep.Close();

            if(benutzer == null)
            {
                ModelState.AddModelError("Email", "EMail oder Passwort stimmen nicht!");
                return View(benutzerLogin);
            }
            else
            {
                Session["loggedIn"] = benutzer;
                if(benutzer.Rolle == BenutzerRolle.admin)
                {
                    Session["AdminSession"] = true;
                }
                else
                {
                    Session["AdminSession"] = false;
                }


                return RedirectToAction("index", "home");
            }

        }

        public ActionResult Registrierung()
        {
            return View(new Benutzer());
        }
        [HttpPost]
        public ActionResult Registrierung(Benutzer benutzer)
        {
            if(benutzer == null)
            {
                return View(benutzer);
            }

            CheckUserData(benutzer);

            if (!ModelState.IsValid)
            {
                return View(benutzer);
            }
            else
            {
                benutzer.Rolle = BenutzerRolle.registrierterBenutzer;

                rep = new RepositoryBenutzer();

                rep.Open();
                if (rep.Insert(benutzer))
                {
                    rep.Close();
                    return RedirectToAction("Login");
                }
                else
                {
                    rep.Close();
                    return View(benutzer);
                }
            }


        }

        public ActionResult Reservierungen()
        {
            List<Request> requests;
            repDB = new RepositoryDB();
            repDB.Open();
            requests = repDB.GetRequests();
            repDB.Close();

            List<Request> newRequests = new List<Request>();

            foreach(var r in requests)
            {
                if (!r.RequestEdited)
                {
                    newRequests.Add(r);
                }
            }


            return View(newRequests);
        }

        public ActionResult RegistrierteBenutzer()
        {
            return View();
        }
        public ActionResult ChangeStatus(int id)
        {
            repDB = new RepositoryDB();
            repDB.Open();
            if (repDB.Edit(id))
            {
                repDB.Close();
                return RedirectToAction("Reservierungen");
            }
            else
            {
                repDB.Close();
                return View("Message", new Message("Anfrage wurde nicht bearbeitet", "Sie haben die Anfrage nicht bearbeitet"));
            }
        }

        public ActionResult Delete(int id)
        {
            repDB = new RepositoryDB();
            repDB.Open();
            if (repDB.Delete(id))
            {
                repDB.Close();
                return RedirectToAction("Reservierungen");
            }
            else
            {
                repDB.Close();
                return View("Message", new Message("Anfrage wurde nicht gelöscht", "Sie haben die Anfrage nicht gelöscht"));
            }
        }


        //Methoden:
        private void CheckUserData(Benutzer user)
        {
            if (user == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(user.Firstname.Trim()))
            {
                ModelState.AddModelError("Firstname", "Vorname muss eingetragen werden");
            }
            if (string.IsNullOrEmpty(user.Lastname.Trim()))
            {
                ModelState.AddModelError("Lastname", "Nachname muss eingetragen werden");
            }
            if (!CheckPassword(user.Password))
            {
                ModelState.AddModelError("Password", "Passwort muss mindestens 8 Zeichen lang sein und mindestens einen Großbuchstaben und mindstens eine Zahl enthalten.");
            }

            if (user.Password != user.PasswordConfirm)
            {
                ModelState.AddModelError("PasswordConfirm", "Die Passwörter stimmen nicht überein!");
            }

            if (!EmailContainsAddSign(user.Email, 1))
            {
                ModelState.AddModelError("Email", "Email muss ein @ Zeichen enthalten.");
            }

            if (!EmailContainsAddSign(user.Email, 1))
            {
                ModelState.AddModelError("Email", "Email muss  einen . enthalten.");
            }

        }

        private bool CheckPassword(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }

            if (!PWContainsNumber(password, 1))
            {
                return false;
            }
            if (!PWContainsUppercaseCharacter(password, 1))
            {
                return false;
            }

            return true;
        }

        private bool PWContainsUppercaseCharacter(string text, int minCount)
        {
            int count = 0;
            foreach (char c in text)
            {
                if (char.IsUpper(c))
                {
                    count++;
                }
            }

            return count >= minCount;
        }

        private bool PWContainsNumber(string text, int minCount)
        {
            int count = 0;
            foreach (char c in text)
            {
                if (char.IsNumber(c))
                {
                    count++;
                }
            }
            return count >= minCount;
        }

        private bool EmailContainsAddSign(string text, int minCount)
        {
            string allowedChars = "@";
            int count = 0;
            foreach (char c in text)
            {
                if (allowedChars.Contains(c))
                {
                    count++;
                }
            }

            return count >= minCount;
        }

        private bool EmailContainsDot(string text, int minCount)
        {
            string allowedChars = ".";
            int count = 0;
            foreach (char c in text)
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