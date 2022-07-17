using Microsoft.EntityFrameworkCore;
using SampleDB.Entities.Orders;
using SampleDB.Entities.Products;
using SampleDB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDB.Contexts
{
    public class SampleContext : DbContext, IContext
    {
        public SampleContext() : base()
        { }
        public SampleContext(DbContextOptions<SampleContext> options)
            :base(options)
        { }

        // 
        // KUN LUOT TIETOKANNAN 1. KERRAN TAI TEET PÄIVITYKSEN
        // VALITSE PACKAGE MANAGER CONSOLESTA DEFAULT PROJEKTIKSI SampleDB
        // JA POISTA ALLA OLEVASTA OnConfiguring KOODISTA KOMMENTOINTI. 
        // ADD-MIGRATION tai UPDATE-DATABASE ei löydä oikeaa dbcontextia
        // jos default project on ExceleraSample (ts. winforms projekti)
        //
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var conn = "Server=.\\SQLExpress;Database=SampleDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        //    optionsBuilder.UseSqlServer(conn);
        //}
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ks. esim. OrderConfiguration kuinka sekvensit konfiguroidaan.
            modelBuilder.HasSequence<int>("OrderNumbers");
            modelBuilder.HasSequence<int>("ProductNumbers");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SampleContext).Assembly);
        }

        //
        // Full text search db function 
        //
        [DbFunction("CHARINDEX", IsBuiltIn = true)]
        public static long CHARINDEX(string substring, string str)
        {
            throw new NotImplementedException();
        }
    }
}
