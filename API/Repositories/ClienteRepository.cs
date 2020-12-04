using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>
    {
        public ClienteRepository(Data.Context context) : base(context)
        {

        }

        public async Task<Cliente> GetByEmailSenha(string email, string senha) => 
            await _context.Cliente.FirstOrDefaultAsync(_ => _.Email == email && _.Senha == senha);
    }
}
