using Dominio.Clases;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class PortLogContext: DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Importacion> Importaciones { get; set; }

        

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public PortLogContext() : base("con")
        {

        }

    }
}
