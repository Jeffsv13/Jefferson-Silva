using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }

        public string? Sku { get; set; }

        public string? Nombre { get; set; }

        public int? Tipo { get; set; }
        public string? DescripcionCategoria {  get; set; }

        public string? Etiquetas { get; set; }

        public string? Precio { get; set; }

        public string? UnidadMedida { get; set; }

        public int? EsActivo { get; set; }
    }
}
