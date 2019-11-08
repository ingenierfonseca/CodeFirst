using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalData.Class
{
    [Table("Carrito")]
    public class Carrito
    {
        [Key]
        public int Id { get; set; }
        public int IdArticulo { get; set; }
        public string Identificacion { get; set; }
        public int Cantidad { get; set; }
    }
}
