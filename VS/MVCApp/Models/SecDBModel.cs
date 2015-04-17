using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class SecDbContext : DbContext
    {
        public SecDbContext()
            : base("DefaultConnection")
        {
            
        }

        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Ksiazka> Ksiazki { get; set; }

        //public ObjectResult<Ksiazka> KsiazkiByTitle(string title)
        //{
        //    var titleSearch = new ObjectParameter("TitleSearch", title);
        //    return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Ksiazka>("GetKsiazkiByTitle",
        //        titleSearch);
        //}
    }

    [Table("Ksiazki")]
    public class Ksiazka
    {
        [Key]
        public int Id { get; set; }
        public string Tytuł { get; set; }
        public string ISBN { get; set; }
        public int Cena { get; set; }
        public Kategoria Kategoria { get; set; }
    }

    [Table("Kategorie")]
    public class Kategoria
    {
        [Key]
        public int Id { get; set; }
        public string Nazwa { get; set; }

        public virtual List<Ksiazka> Ksiazki { get; set; }
    }
}