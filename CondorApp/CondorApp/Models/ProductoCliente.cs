using System;

namespace CondorApp.Models
{
    public class ProductoCliente
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdMercaderia { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal ValorCompra { get; set; }
        public string Anotaciones { get; set; }
        public string EstadoPago { get; set; }
        public string Retirado { get; set; }

        public string NombreProducto { get; set; }

        public decimal ValorCuota { get; set; }
    }
}
