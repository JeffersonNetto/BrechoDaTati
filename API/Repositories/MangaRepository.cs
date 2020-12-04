using API.Data;
using API.Models;

namespace API.Repositories
{
    public class MangaRepository : RepositoryBase<Manga>
    {
        public MangaRepository(Context context) : base(context)
        {
        }
    }
}
