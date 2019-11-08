using LogicalData.Class;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalData.Repository
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext():base("name=LogicalData")
        {
            Database.SetInitializer<ApplicationDbContext>(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
    }
}
