namespace Mysql_Asp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_materias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_materias()
        {
            tbl_asignacion = new HashSet<tbl_asignacion>();
            tbl_unidad = new HashSet<tbl_unidad>();
        }

        [Key]
        [StringLength(50)]
        public string siglema { get; set; }

        [Required]
        [StringLength(150)]
        public string nombre { get; set; }

        public int id_semestre { get; set; }

        public int id_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_asignacion> tbl_asignacion { get; set; }

        public virtual tbl_semestres tbl_semestres { get; set; }

        public virtual tbl_status tbl_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_unidad> tbl_unidad { get; set; }
    }
}
