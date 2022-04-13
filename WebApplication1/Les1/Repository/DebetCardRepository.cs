using DataLayer;
using Les1.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Les1.Repository
{
    public class DebetCardRepository : IDebetCardRepository
    {
        private readonly ApplicationDataContext _context;

        public DebetCardRepository(ApplicationDataContext context) => _context = context;

        public async Task Add(DebetCard debetCard)
        {
            _context.DebetCards.Add(debetCard);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var catDelete = await _context.DebetCards.SingleOrDefaultAsync(t => t.Id == id);
            if (catDelete is null) return;
            _context.DebetCards.Remove(catDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<DebetCard>> Get()
        {
            return await _context.DebetCards.ToListAsync();
        }

        public async Task Update(DebetCard debetCard)
        {
            _context.DebetCards.Update(debetCard);
            await _context.SaveChangesAsync();
        }
    }
}
