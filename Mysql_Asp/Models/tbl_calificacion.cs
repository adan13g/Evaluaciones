namespace Mysql_Asp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_calificacion
    {
        [Key]
        public int id_calificacion { get; set; }

        public int calificacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(20)]
        public string matricula { get; set; }

        public int id_asignacion { get; set; }

        public int id_resultado { get; set; }

        public virtual tbl_alumnos tbl_alumnos { get; set; }

        public virtual tbl_asignacion tbl_asignacion { get; set; }

        public virtual tbl_subunidad tbl_subunidad { get; set; }
    }
}
