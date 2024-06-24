using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public string? NumeroDocumento { get; set; }

    public DateTime? FechaPedido { get; set; }

    public DateTime? FechaRecepcion { get; set; }

    public DateTime? FechaDespacho { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public int? IdVendedor { get; set; }

    public int? IdRepartidor { get; set; }

    public int? IdEstado { get; set; }

    public decimal? Total { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual Usuario? IdRepartidorNavigation { get; set; }

    public virtual Usuario? IdVendedorNavigation { get; set; }
}
