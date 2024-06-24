using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepositorio;
        private readonly IGenericRepository<DetallePedido> _detallePedidoRepositorio;
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository pedidoRepositorio, IGenericRepository<DetallePedido> detallePedidoRepositorio, IMapper mapper)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _detallePedidoRepositorio = detallePedidoRepositorio;
            _mapper = mapper;
        }

        public async Task<PedidoDTO> Registrar(PedidoDTO modelo)
        {
            try
            {
                var pedidoGenerado = await _pedidoRepositorio.Registrar(_mapper.Map<Pedido>(modelo));

                if (pedidoGenerado.IdPedido == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _mapper.Map<PedidoDTO>(pedidoGenerado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<PedidoDTO>> Historial(string buscarPor, string numeroDocumento, string fechaInicio, string fechaFin)
        {
            IQueryable<Pedido> query = await _pedidoRepositorio.Consultar();
            var ListaResultado = new List<Pedido>();
            try
            {
                if (buscarPor == "fecha")
                {
                    DateTime fecha_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                    DateTime fecha_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                    ListaResultado = await query.Where(v=>
                    v.FechaPedido.Value.Date>= fecha_Inicio &&
                    v.FechaPedido.Value.Date<=  fecha_Fin)
                        .Include(dv=>dv.DetallePedidos)
                        .ThenInclude(p=>p.IdProductoNavigation)
                        .ToListAsync();
                }
                else
                {
                    ListaResultado = await query.Where(v=>
                    v.NumeroDocumento==numeroDocumento)
                        .Include(u=>u.IdVendedorNavigation)
                        .Include(r=>r.IdRepartidorNavigation)
                        .Include(e=>e.IdEstadoNavigation)
                        .Include(dv=>dv.DetallePedidos)
                        .ThenInclude(p=>p.IdProductoNavigation)
                        .ToListAsync();

                    
                    
                }
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<PedidoDTO>>(ListaResultado);
        }

        public async Task<bool> Editar(PedidoDTO modelo)
        {
            try
            {
                //var pedidoModelo = _mapper.Map<Pedido>(modelo);
                var pedidoEncontrado = await _pedidoRepositorio.Obtener(u => u.IdPedido == modelo.IdPedido);
                if (pedidoEncontrado.IdPedido == 0)
                    throw new TaskCanceledException("El pedido no existe");

                if(modelo.IdEstado > 4 || modelo.IdEstado < 1)
                    throw new TaskCanceledException("Estado no valido");

                if (pedidoEncontrado.IdEstado>modelo.IdEstado)
                    throw new TaskCanceledException("No se puede actualizar a un estado anterior al actual");
                if (modelo.IdEstado == pedidoEncontrado.IdEstado)
                    throw new TaskCanceledException("El pedido ya se encuentra en este estado");

                pedidoEncontrado.IdEstado = modelo.IdEstado;
                
                if (modelo.IdEstado == 2)
                {
                    pedidoEncontrado.FechaRecepcion=DateTime.Now;
                }
                if (modelo.IdEstado == 3)
                {
                    pedidoEncontrado.FechaRecepcion = pedidoEncontrado.FechaRecepcion == null ? DateTime.Now : pedidoEncontrado.FechaRecepcion;
                    pedidoEncontrado.FechaDespacho = DateTime.Now;
                }
                if (modelo.IdEstado == 4)
                {
                    pedidoEncontrado.FechaRecepcion = pedidoEncontrado.FechaRecepcion == null ? DateTime.Now : pedidoEncontrado.FechaRecepcion;
                    pedidoEncontrado.FechaDespacho = pedidoEncontrado.FechaDespacho == null ? DateTime.Now : pedidoEncontrado.FechaDespacho;
                    pedidoEncontrado.FechaEntrega = DateTime.Now;
                }

                bool respuesta = await _pedidoRepositorio.Editar(pedidoEncontrado);
                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar");

                return respuesta;
            }
            catch
            {
                throw;
            }

        }
    }
}
