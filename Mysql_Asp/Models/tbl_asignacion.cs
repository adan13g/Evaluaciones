namespace Mysql_Asp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_asignacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_asignacion()
        {
            tbl_calificacion = new HashSet<tbl_calificacion>();
        }

        [Key]
        public int id_asignacion { get; set; }

        [Display(Name = "Carrera")]
        public int id_carrera { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Materia")]
        public string siglema { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Profesor")]
        public string id_profesor { get; set; }

        [Display(Name = "Grupo")]
        public int id_grupos { get; set; }

        public virtual tbl_carreras tbl_carreras { get; set; }

        public virtual tbl_grupos tbl_grupos { get; set; }

        public virtual tbl_profesores tbl_profesores { get; set; }

        public virtual tbl_materias tbl_materias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_calificacion> tbl_calificacion { get; set; }
    }
}
