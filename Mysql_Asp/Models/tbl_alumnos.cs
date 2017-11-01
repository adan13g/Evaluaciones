namespace Mysql_Asp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class tbl_alumnos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_alumnos()
        {
            tbl_calificacion = new HashSet<tbl_calificacion>();
        }

        [Key]
        [StringLength(20)]
        public string matricula { get; set; }

        [Required(ErrorMessage = "the field {0} is requiered")]
        [MaxLength(256, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        [Index("UserName", IsUnique = true)]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string nombre { get; set; }

        [Required]
        [StringLength(20)]
        public string ape_pat { get; set; }

        [Required]
        [StringLength(20)]
        public string ape_mat { get; set; }

        [NotMapped]
        [Display(Name = "Alumno")]
        public string Alumno { get { return string.Format("{0} {1} {2}", nombre, ape_pat, ape_mat); } }

        [StringLength(100)]
        public string image { get; set; }

        [StringLength(200)]
        public string estate { get; set; }

        public int id_grupos { get; set; }

        public int id_status { get; set; }

        public virtual tbl_grupos tbl_grupos { get; set; }

        public virtual tbl_status tbl_status { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_calificacion> tbl_calificacion { get; set; }

      
    }
}
