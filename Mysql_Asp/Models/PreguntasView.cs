using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mysql_Asp.Models
{
    [NotMapped]
    public class PreguntasView: tbl_preguntas
    {
        [Display(Name = "Materia")]
        [StringLength(50)]
        public string siglema { get; set; }

        [Display(Name = "Unidad")]
        public int id_unidad { get; set; }

        [Display(Name = "Respuesta")]
        public int id_respuesta { get; set; }

        public List<tbl_respuestas> respuesta { get; set; }
    }
}