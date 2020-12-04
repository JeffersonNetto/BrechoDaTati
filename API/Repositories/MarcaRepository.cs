using API.Data;
using API.Models;

namespace API.Repositories
{
    public class MarcaRepository : RepositoryBase<Marca>
    {
        public MarcaRepository(Context context) : base(context)
        {
        }
    }
}
