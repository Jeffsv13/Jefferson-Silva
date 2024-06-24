using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<List<DTOUsuario>> Lista();
        Task<SesionDTO> ValidarCredenciales(string correo,string clave);
        Task<DTOUsuario> Crear(DTOUsuario modelo);
        Task<bool> Editar(DTOUsuario modelo);
        Task<bool> Eliminar(int id);
    }
}
