using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion

            #region Menu
            CreateMap<Menu, DTOMenu>().ReverseMap();
            #endregion

            #region Usuario
            CreateMap<Usuario, DTOUsuario>()
                .ForMember(destino =>
                destino.RolDescripcion,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                )
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );
            CreateMap<Usuario, SesionDTO>()
                .ForMember(destino =>
                destino.RolDescripcion,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                );
            CreateMap<DTOUsuario, Usuario>()
                .ForMember(destino =>
                destino.IdRolNavigation,
                opt => opt.Ignore())
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion

            #region Producto
            CreateMap<Producto, ProductoDTO>()
                .ForMember(destino =>
                destino.DescripcionCategoria,
                opt => opt.MapFrom(origen => origen.TipoNavigation.Nombre)
                )
                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );

            CreateMap<ProductoDTO, Producto>()
                .ForMember(destino =>
                destino.Tipo,
                opt => opt.Ignore()
                )
                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion Producto

            #region Pedido
            CreateMap<Pedido, PedidoDTO>()
                .ForMember(destino =>
                destino.TotalTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                destino.DescripcionEstado,
                opt => opt.MapFrom(origen => origen.IdEstadoNavigation.Descripcion)
                )
                .ForMember(destino =>
                destino.VendedorNombre,
                opt => opt.MapFrom(origen => origen.IdVendedorNavigation.NombreCompleto)
                )
                .ForMember(destino =>
                destino.RepartidorNombre,
                opt => opt.MapFrom(origen => origen.IdRepartidorNavigation.NombreCompleto)
                )
            .ForMember(destino =>
                destino.FechaPedido,
                opt => opt.MapFrom(origen => origen.FechaPedido.Value.ToString("dd/MM/yyyy"))
                )
            .ForMember(destino =>
                destino.FechaRecepcion,
                opt => opt.MapFrom(origen => origen.FechaRecepcion.Value.ToString("dd/MM/yyyy"))
                )
            .ForMember(destino =>
                destino.FechaDespacho,
                opt => opt.MapFrom(origen => origen.FechaDespacho.Value.ToString("dd/MM/yyyy"))
                )
            .ForMember(destino =>
                destino.FechaEntrega,
                opt => opt.MapFrom(origen => origen.FechaEntrega.Value.ToString("dd/MM/yyyy"))
                );
            CreateMap<PedidoDTO, Pedido>()
                .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-PE")))
                )
            .ForMember(destino =>
                destino.FechaPedido,
                opt => opt.MapFrom(origen => Convert.ToDateTime(origen.FechaPedido)
                ))
            .ForMember(destino =>
                destino.FechaRecepcion,
                opt => opt.MapFrom(origen => Convert.ToDateTime(origen.FechaRecepcion)
                ))
            .ForMember(destino =>
                destino.FechaDespacho,
                opt => opt.MapFrom(origen => Convert.ToDateTime(origen.FechaDespacho)
                ))
            .ForMember(destino =>
                destino.FechaEntrega,
                opt => opt.MapFrom(origen => Convert.ToDateTime(origen.FechaEntrega)
                ));
            #endregion Pedido

            #region DetallePedido
            CreateMap<DetallePedido, DetallePedidoDTO>()
                .ForMember(destino =>
                destino.DescripcionProducto,
                opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre))
                .ForMember(destino =>
                destino.PrecioTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                destino.TotalTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
                );

            CreateMap<DetallePedidoDTO, DetallePedido>()

                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-PE")))
                );
            #endregion DetallePedido
            
        }
    }
}
