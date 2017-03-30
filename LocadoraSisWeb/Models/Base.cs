using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocadoraSisWeb.Models
{
    public abstract class Base
    {
        public Int64 Id { get; set; }
        public String Nome { get; set; }
    }
}