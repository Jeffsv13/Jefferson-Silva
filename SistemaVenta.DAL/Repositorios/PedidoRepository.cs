using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.Model;
namespace SistemaVenta.DAL.Repositorios
{
    public class PedidoRepository:GenericRepository<Pedido>,IPedidoRepository
    {
        private readonly DbventaContext _dbContext;

        public PedidoRepository(DbventaContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Pedido> Registrar(Pedido modelo)
        {
            Pedido pedidoGenerado = new Pedido();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try {
                    foreach (DetallePedido dp in modelo.DetallePedidos)
                    {
                        Producto producto_encontrado = _dbContext.Productos.Where(p=>p.IdProducto == dp.IdProducto).First();
                        
                    }
                    NumeroDocumento correlativo = _dbContext.NumeroDocumentos.First();
                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro= DateTime.Now;

                    _dbContext.NumeroDocumentos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();

                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroPedido = ceros + correlativo.UltimoNumero.ToString();

                    numeroPedido = numeroPedido.Substring(numeroPedido.Length - CantidadDigitos, CantidadDigitos);

                    modelo.NumeroDocumento = numeroPedido;

                    await _dbContext.Pedidos.AddAsync(modelo);
                    await _dbContext.SaveChangesAsync();

                    pedidoGenerado = modelo;

                    transaction.Commit();

                } catch {
                
                    transaction.Rollback();
                    throw;
                }
                return pedidoGenerado;

            }
        }
    }
}
