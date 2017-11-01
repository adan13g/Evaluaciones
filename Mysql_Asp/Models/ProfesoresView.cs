using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mysql_Asp.Models
{
    [NotMapped]
    public class ProfesoresView:tbl_profesores
    {

        [Display(Name = "Foto")]
        public HttpPostedFileBase LogoFile { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(20, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]

        public string Password { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas deben ser iguales")]
        [Display(Name = "Confirmar contraseña")]
        public string PasswordConfirmed { get; set; }
    }
}