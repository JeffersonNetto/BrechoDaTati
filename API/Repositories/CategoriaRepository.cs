using API.Data;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class CategoriaRepository : RepositoryBase<Categoria>
    {
        public CategoriaRepository(Context context) : base(context)
        {

        }
    }
}
