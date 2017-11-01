namespace Mysql_Asp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_subunidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_subunidad()
        {
            tbl_calificacion = new HashSet<tbl_calificacion>();
            tbl_estimacion = new HashSet<tbl_estimacion>();
            tbl_preguntas = new HashSet<tbl_preguntas>();
        }

        [Key]
        public int id_resultado { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre { get; set; }

        public int id_unidad { get; set; }

        public int id_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_calificacion> tbl_calificacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_estimacion> tbl_estimacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_preguntas> tbl_preguntas { get; set; }

        public virtual tbl_status tbl_status { get; set; }

        public virtual tbl_unidad tbl_unidad { get; set; }
    }
}
