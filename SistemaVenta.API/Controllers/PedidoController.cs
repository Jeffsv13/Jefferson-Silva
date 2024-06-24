using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;
using Microsoft.AspNetCore.Authorization;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoServicio;

        public PedidoController(IPedidoService pedidoServicio)
        {
            _pedidoServicio = pedidoServicio;
        }
        [Authorize]
        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] PedidoDTO pedido)
        {
            var rsp = new Response<PedidoDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _pedidoServicio.Registrar(pedido);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
        [Authorize]
        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string buscarPor,string? numeroPedido, string? fechaInicio, string? fechaFin)
        {
            var rsp = new Response<List<PedidoDTO>>();
            numeroPedido = numeroPedido is null ? "" : numeroPedido;
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;

            try
            {
                rsp.status = true;
                rsp.value = await _pedidoServicio.Historial(buscarPor,numeroPedido,fechaInicio,fechaFin);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
        [Authorize]
        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] PedidoDTO pedido)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _pedidoServicio.Editar(pedido);
                rsp.msg = "Se actualizo correctamente el estado del pedido";
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }


    }
}
