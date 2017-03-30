using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LocadoraSisWeb.Models
{
    public class LocadoraSisWebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public LocadoraSisWebContext() : base("name=LocadoraSisWebContext")
        {
        }

        public System.Data.Entity.DbSet<LocadoraSisWeb.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<LocadoraSisWeb.Models.Marca> Marcas { get; set; }

        public System.Data.Entity.DbSet<LocadoraSisWeb.Models.Opcional> Opcionais { get; set; }

        public System.Data.Entity.DbSet<LocadoraSisWeb.Models.Veiculo> Veiculos { get; set; }

        public System.Data.Entity.DbSet<LocadoraSisWeb.Models.Locacao> Locacoes { get; set; }
    }
}
