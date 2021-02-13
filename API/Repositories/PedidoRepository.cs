using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {

    }
    public class PedidoRepository : IPedidoRepository
    {
        private readonly Context _context;

        public PedidoRepository(Context context) =>
            _context = context;

        public async Task Add(Pedido obj)
        {
            //obj.Cliente = null;
            //obj.Cupom = null;

            //foreach(var item in obj.PedidoItem)
            //{
            //    item.Produto = null;               
            //}

            await _context.Pedido.AddAsync(obj);
        }
            

        public async Task<bool> Exists<T>(T id) =>
            await _context.Pedido
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id)) != null;

        public async Task<List<Pedido>> GetAll() =>
            await _context.Pedido
            .Include(_ => _.Cliente)
            .Include(_ => _.Cupom)
            .Include(_ => _.Status)
            .Include(_ => _.PedidoItem)
            .AsNoTracking()
            .ToListAsync();


        public async Task<Pedido> GetById<T>(T id) =>
            await _context.Pedido
            .Include(_ => _.Cliente)
            .Include(_ => _.Cupom)
            .Include(_ => _.Status)
            .Include(_ => _.PedidoItem)
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id));

        public void Remove(Pedido obj) =>
            _context.Pedido.Remove(obj);

        public void Update(Pedido obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property("DataCriacao").IsModified = false;
        }
    }
}
