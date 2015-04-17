using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApp.Models;
using Newtonsoft.Json;

namespace MVCApp.Controllers
{
    public class DirectController : Controller
    {
        //
        // GET: /Direct/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult KartyKredytowe(string userName)
        // 2. zmiana nazwy parametru
        //public ActionResult KartyKredytowe(string id)
        {
            using (var db = new UsersContext())
            {
                // 2. przetworzenie id na normalną nazwę
                //var userName = id.GetDirectRef();

                // 1. dodanie autoryzacji
                //if (userName != User.Identity.Name)
                //{
                //    throw new ApplicationException("Nie jesteś uprawnionym użytkownikiem do oglądania tych danych.");
                //}

                int userId;
                try
                {
                    userId = db.UserProfiles.Single(u => u.UserName == userName).UserId;
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Nie ma takiego profilu");
                }
                var karty = db.KartyKredytowe.Where(m => m.WlascicielId == userId).ToList();
                var retval = Json(new {CreditCards = karty}, JsonRequestBehavior.AllowGet);
                return retval;
            }
        }

    }
}
