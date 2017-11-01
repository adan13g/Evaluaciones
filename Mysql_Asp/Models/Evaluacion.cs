using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mysql_Asp.Models
{
    public class Evaluacion
    {
        public List<tbl_preguntas> Preguntas { get; set; }
        public Evaluacion()
        {
            Preguntas = new List<tbl_preguntas>();
        }
    }
}