using API.Data;
using API.Models;

namespace API.Repositories
{
    public class CondicaoRepository : RepositoryBase<Condicao>
    {
        public CondicaoRepository(Context context) : base(context)
        {

        }
    }
}
