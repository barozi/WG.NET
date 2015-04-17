using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class InjectionController : Controller
    {
        //
        // GET: /Injection/Kategorie
        public ActionResult Kategorie()
        {
            IEnumerable<Kategoria> model = new List<Kategoria>();
            using (var db = new SecDbContext())
            {
                model = db.Kategorie.ToList();
            }

            return View(model);
        }

        // przykładowe wstrzyknięcie 'or 1=1'
        // GET: /Injection/Ksiazki?kategoriaId={id}
        public ActionResult Ksiazki(string kategoriaId)
        {
            var ksiazki = new List<Ksiazka>();
            var connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var query = "SELECT Id, Tytuł, ISBN, Cena FROM Ksiazki WHERE Kategoria_Id = " + kategoriaId;
            // porawka - dodanie parametru
            //var query = "SELECT Id, Tytuł, ISBN, Cena FROM Ksiazki WHERE Kategoria_Id = @kategoriaId";
            
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    // poprawka w postaci dodania parametru
                    //cmd.Parameters.Add("@kategoriaId", SqlDbType.VarChar).Value = kategoriaId;
                    cmd.Connection.Open();
                    var ds = cmd.ExecuteReader();
                    while (ds.Read())
                    {
                        var item = new Ksiazka()
                        {
                            Id = ds.GetInt32(0),
                            Tytuł = ds.GetString(1),
                            ISBN = ds.GetString(2),
                            Cena = ds.GetInt32(3)
                        };
                        ksiazki.Add(item);
                    }
                }
            }
            return View(ksiazki);


        }

        public ActionResult Wyszukiwanie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KsiazkiSearchByTitle(string searchText)
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

            return View("Ksiazki", ksiazki);
        }
    }
}

//cmd.Parameters.Add("@kategoriaId", SqlDbType.VarChar).Value = kategoriaId ;
