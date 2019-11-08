using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalData.Class
{
    [Table("Articulo")]
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength =3)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(120, MinimumLength =3)]
        public string Descripcion { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public float Precio { get; set; }

        public string Imagen { get; set; }
    }
}
