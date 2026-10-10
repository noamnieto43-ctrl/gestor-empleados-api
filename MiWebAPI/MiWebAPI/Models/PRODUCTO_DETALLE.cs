namespace MiWebAPI.Models
{
    public class PRODUCTO_DETALLE

    {

        public long Id { get; set; }
        public string? NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public string? Descripcion { get; set; }
    } 
}
