namespace Mysql_Asp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_semestres
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_semestres()
        {
            tbl_materias = new HashSet<tbl_materias>();
        }

        [Key]
        public int id_semestre { get; set; }

        [Required]
        [StringLength(20)]
        public string semestre { get; set; }

        public int id_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_materias> tbl_materias { get; set; }

        public virtual tbl_status tbl_status { get; set; }
    }
}
