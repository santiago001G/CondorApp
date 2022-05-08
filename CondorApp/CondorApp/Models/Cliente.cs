namespace CondorApp.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public int IdCartera { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
        public decimal ValorCuota { get; set; }
        public string EstadoPagos { get; set; }
        public string PeridicidadCobro { get; set; }
        public string Latitud { get; set; }
        public string Logitud { get; set; }
        public bool PagadoHoy { get; set; }
        public string Ubicacion { get; set; }
    }
}
