using CMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace CMS.DAL
{
    public class CMSContext : DbContext
    {
        //Database save - for debug
        /*public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }

                throw;  // You can also choose to handle the exception here...
            }
        }*/

        public CMSContext() : base("MVC")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Always drop database on launch
            //Database.SetInitializer<CMSContext>(new DropCreateDatabaseAlways<CMSContext>());
            Database.SetInitializer<CMSContext>(new DropCreateDatabaseIfModelChanges<CMSContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}