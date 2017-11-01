namespace Mysql_Asp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_carreras
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_carreras()
        {
            tbl_asignacion = new HashSet<tbl_asignacion>();
        }

        [Key]
        public int id_carrera { get; set; }

        [Required]
        [StringLength(50)]
        public string carrera { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_asignacion> tbl_asignacion { get; set; }
    }
}
