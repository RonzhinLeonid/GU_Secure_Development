using System.Collections.Generic;
using System.Threading.Tasks;

namespace Les1.Interface
{
    public interface ICRUDReposotory<TEntity>
    {
        Task Add(TEntity debetCard);
        Task<IList<TEntity>> Get();
        Task Delete(int id);
        Task Update(TEntity debetCard);
    }
}
