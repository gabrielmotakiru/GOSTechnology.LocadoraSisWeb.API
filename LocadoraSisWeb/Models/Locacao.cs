using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocadoraSisWeb.Models
{
    public class Locacao
    {
        public Int64 Id { get; set; }
        public Veiculo Veiculo { get; set; }
        [ForeignKey("Veiculo")]
        public Int64 VeiculoId { get; set; }
        public Cliente Cliente { get; set; }
        [ForeignKey("Cliente")]
        public Int64 ClienteId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime PeriodoInicial { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime PeriodoFinal { get; set; }
        public Decimal Valor { get; set; }
    }
}