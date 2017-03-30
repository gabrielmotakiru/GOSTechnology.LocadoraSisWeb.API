using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocadoraSisWeb.Models
{
    public class Cliente : Base
    {
        public Int64 Cpf { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime DataNascimento { get; set; }
        public Int64 Telefone { get; set; }
    }
}