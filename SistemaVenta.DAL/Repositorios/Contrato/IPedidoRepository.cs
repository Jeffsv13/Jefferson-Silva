﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.Model;

namespace SistemaVenta.DAL.Repositorios.Contrato
{
    public interface IPedidoRepository:IGenericRepository<Pedido>
    {
        Task<Pedido> Registrar(Pedido modelo);
    }
}
