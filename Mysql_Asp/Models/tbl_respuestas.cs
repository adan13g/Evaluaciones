namespace Mysql_Asp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_respuestas
    {
        [Key]
        public int id_respuesta { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string respuesta { get; set; }

        public int puntos { get; set; }

        public int id_pregunta { get; set; }

        public virtual tbl_preguntas tbl_preguntas { get; set; }
    }
}
