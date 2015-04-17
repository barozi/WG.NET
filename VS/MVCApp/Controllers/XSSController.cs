using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Security.AntiXss;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class XSSController : Controller
    {
        //
        // GET: /XSS/

        public ActionResult Index()
        {
            return View();
        }

        // Możemy wprowadzać różne skrypty do parametru, np.: <script>alert('Hello!!!')</script>

        [HttpGet]
        // dodane specjalnie aby MVC nie walidował defaultowo
        [ValidateInputAttribute(false)]
        public ActionResult Search(string searchText)
        {
            List<Ksiazka> ksiazki;
            using (var db = new SecDbContext())
            {
                //ksiazki = db.KsiazkiByTitle(searchText).ToList();
                var param = new SqlParameter
                {
                    ParameterName = "titleSearch",
                    Value = searchText
                };
                ksiazki = db.Database.SqlQuery<Ksiazka>("GetKsiazkiByTitle @titleSearch", param).ToList();
            }

            ViewData["SearchText"] = searchText;
            // wykorzystanie Encodera
            //ViewData["SearchText"] = AntiXssEncoder.HtmlEncode(searchText, true);

            return View("Search", ksiazki);
        }

    }
}



//AntiXssEncoder.HtmlEncode(searchText, true);
//%3C%73%63%72%69%70%74%3E%61%6C%65%72%74%28%22%5A%6F%73%74%61%6C%65%73%20%7A%68%61%63%6B%6F%77%61%6E%79%22%29%3C%2F%73%63%72%69%70%74%3E