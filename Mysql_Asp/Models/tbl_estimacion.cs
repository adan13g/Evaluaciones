namespace Mysql_Asp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_estimacion
    {
        [Key]
        public int id_estimacion { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        public int tiempo { get; set; }

        public int id_resultado { get; set; }

        public int id_grupos { get; set; }

        [Required]
        [StringLength(20)]
        public string id_profesor { get; set; }

        public virtual tbl_grupos tbl_grupos { get; set; }

        public virtual tbl_profesores tbl_profesores { get; set; }

        public virtual tbl_subunidad tbl_subunidad { get; set; }
    }
}
