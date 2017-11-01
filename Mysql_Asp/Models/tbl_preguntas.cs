namespace Mysql_Asp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tbl_preguntas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_preguntas()
        {
            tbl_respuestas = new HashSet<tbl_respuestas>();
            tbl_respuestas = new List<tbl_respuestas>();
        }

        [Key]
        public int id_pregunta { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string pregunta { get; set; }

        [Required]
        [StringLength(20)]
        public string id_profesor { get; set; }

        public int id_resultado { get; set; }

        public int id_status { get; set; }

        public virtual tbl_profesores tbl_profesores { get; set; }

        public virtual tbl_subunidad tbl_subunidad { get; set; }

        public virtual tbl_status tbl_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_respuestas> tbl_respuestas { get; set; }

        public string RespuestaSeleccionada { set; get; }

        public List<tbl_preguntas> Listar()
        {
            var alumnos = new List<tbl_preguntas>();
            try
            {
                using (var context = new ModeloDatos())
                {
                    alumnos = context.tbl_preguntas.Include(t=>t.tbl_respuestas).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return alumnos;
        }
    }
}
