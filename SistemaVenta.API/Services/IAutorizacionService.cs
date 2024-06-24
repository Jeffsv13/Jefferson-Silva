using SistemaVenta.API.Custom;

namespace SistemaVenta.API.Services
{
    public interface IAutorizacionService
    {
        Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion);

    }
}
