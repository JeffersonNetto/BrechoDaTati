using API.Data;
using API.Models;

namespace API.Repositories
{
    public class TecidoRepository : RepositoryBase<Tecido>
    {
        public TecidoRepository(Context context) : base (context)
        {

        }
    }
}
