using LogicalData.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicalData.Repository.DTO
{
    public class DtoCar
    {
        public int Id { get; set; }
        public int IdArticulo { get; set; }
        public string Identificacion { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
        public float SubTotal { get; set; }
        public string Articulo { get; set; }

        public Carrito ToCar()
        {
            Carrito car = new Carrito();
            car.Id = Id;
            car.IdArticulo = IdArticulo;
            car.Identificacion = Identificacion;
            car.Cantidad = Cantidad;
            return car;
        }

    }
}