namespace Mysql_Asp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class tbl_profesores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_profesores()
        {
            tbl_asignacion = new HashSet<tbl_asignacion>();
            tbl_estimacion = new HashSet<tbl_estimacion>();
            tbl_preguntas = new HashSet<tbl_preguntas>();
        }

        [Key]
        [StringLength(20)]
        public string id_profesor { get; set; }


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


        [StringLength(50)]
        public string image { get; set; }

        [StringLength(200)]
        public string estate { get; set; }

        [NotMapped]
        [Display(Name = "Profesor")]
        public string Profesor { get { return string.Format("{0} {1} {2}", nombre, ape_pat,ape_mat); } }

        public int id_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_asignacion> tbl_asignacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_estimacion> tbl_estimacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_preguntas> tbl_preguntas { get; set; }

        public virtual tbl_status tbl_status { get; set; }


      
    }
}
