using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mysql_Asp.Models
{

    [NotMapped]
    public class AsignacionPreguntas:tbl_asignacion
    {

        [Display(Name = "Unidad")]
        public int id_unidad { get; set; }


        [Display(Name = "Resultado")]
        public int id_resultado { get; set; }
    }
}