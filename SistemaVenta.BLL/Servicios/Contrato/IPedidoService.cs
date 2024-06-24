using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IPedidoService
    {
        Task<PedidoDTO> Registrar(PedidoDTO modelo);

        Task<List<PedidoDTO>> Historial(string buscarPor, string numeroPedido, string fechaInicio, string fechaFin);
        Task<bool> Editar(PedidoDTO modelo);
    }
}
