using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class PedidoDTO
    {
        public int IdPedido { get; set; }

        public string? NumeroDocumento { get; set; }

        public string? FechaPedido { get; set; }

        public string? FechaRecepcion { get; set; }

        public string? FechaDespacho { get; set; }

        public string? FechaEntrega { get; set; }

        public int? IdVendedor { get; set; }
        public string? VendedorNombre { get; set; }

        public int? IdRepartidor { get; set; }
        public string? RepartidorNombre { get; set; }

        public int? IdEstado { get; set; }
        public string? DescripcionEstado { get; set; }

        public string? TotalTexto { get; set; }

        public virtual ICollection<DetallePedidoDTO> DetallePedidos { get; set; }



    }
}
