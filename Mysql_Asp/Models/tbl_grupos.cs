namespace Mysql_Asp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_grupos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_grupos()
        {
            tbl_alumnos = new HashSet<tbl_alumnos>();
            tbl_asignacion = new HashSet<tbl_asignacion>();
            tbl_estimacion = new HashSet<tbl_estimacion>();
        }

        [Key]
        public int id_grupos { get; set; }

        [Required]
        [StringLength(20)]
        public string grupos { get; set; }

        public int id_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_alumnos> tbl_alumnos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_asignacion> tbl_asignacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_estimacion> tbl_estimacion { get; set; }

        public virtual tbl_status tbl_status { get; set; }
    }
}
