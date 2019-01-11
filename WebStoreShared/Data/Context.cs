using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreShared.Models;

namespace WebStoreShared.Data
{
    public class Context:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public Context(): base("ContextConnection")
        {
            //call to the SetInitializer method
            //which holds the database's seed data
            Database.SetInitializer(new DataBaseInitializer());
        }
    }
}
