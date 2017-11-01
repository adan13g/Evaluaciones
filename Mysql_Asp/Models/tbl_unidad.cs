namespace Mysql_Asp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_unidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_unidad()
        {
            tbl_subunidad = new HashSet<tbl_subunidad>();
        }

        [Key]
        public int id_unidad { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string siglema { get; set; }

        public int id_status { get; set; }

        public virtual tbl_materias tbl_materias { get; set; }

        public virtual tbl_status tbl_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_subunidad> tbl_subunidad { get; set; }
    }
}
