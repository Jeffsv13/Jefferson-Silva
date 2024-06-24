using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? Sku { get; set; }

    public string? Nombre { get; set; }

    public int? Tipo { get; set; }

    public string? Etiquetas { get; set; }

    public decimal? Precio { get; set; }

    public string? UnidadMedida { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Categoria? TipoNavigation { get; set; }
}
