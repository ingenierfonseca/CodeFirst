using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalData.Class
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Required]
        public string Identificacion { get; set; }

        [Required]
        [StringLength(25, MinimumLength =3)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50, MinimumLength =3)]
        public string Apellidos { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, MinimumLength =3)]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength =3)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(20, MinimumLength =3)]
        [Display(Name = "Contraseña")]
        public string Contrasenya { get; set; }
    }
}
