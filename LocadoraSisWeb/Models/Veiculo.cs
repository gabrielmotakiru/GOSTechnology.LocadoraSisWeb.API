using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocadoraSisWeb.Models
{
    public class Veiculo : Base
    {
        public virtual Marca Marca { get; set; }
        [ForeignKey("Marca")]
        public Int64 MarcaId { get; set; }
        public Int32 Modelo { get; set; }
        public IEnumerable<Opcional> Opcionais { get; set; }
        public Decimal Preco { get; set; }
        public String Cor { get; set; }
        public String Placa { get; set; }
        public Boolean Alugado { get; set; }
    }
}