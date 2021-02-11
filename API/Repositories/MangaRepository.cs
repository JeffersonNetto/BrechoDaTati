using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class MangaRepository : IRepositoryBase<Manga>
    {
        private readonly Context _context;

        public MangaRepository(Context context) =>
            _context = context;

        public async Task Add(Manga obj) =>
            await _context.Manga.AddAsync(obj);

        public async Task<bool> Exists<T>(T id) =>
            await _context.Manga
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id)) != null;

        public async Task<List<Manga>> GetAll() =>
            await _context.Manga
            .AsNoTracking()
            .ToListAsync();

        public async Task<Manga> GetById<T>(T id) =>
            await _context.Manga
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id));

        public void Remove(Manga obj) =>
            _context.Manga.Remove(obj);

        public void Update(Manga obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property("DataCriacao").IsModified = false;
        }
    }
}
