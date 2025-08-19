using CardInsight.API.Data;
using CardInsight.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CardInsight.API.Repositories
{
    public class CreditCardRepository : ICreditCard
    {
        private readonly ApplicationDbContext dbContext;

        public CreditCardRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddCreditCardAsync(CreditCard creditCard)
        {
            await dbContext.CreditCards.AddAsync(creditCard);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCreditCardAsync(int Id)
        {
            var find = await dbContext.CreditCards.FindAsync(Id);
            dbContext.CreditCards.Remove(find);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<CreditCard>> GetAllCreditCards()
        {
            var creditcards = await dbContext.CreditCards.ToListAsync();
            return creditcards;
        }

        public async Task<List<CreditCard>> GetCreditCard(string? search, string? category)
        {
            if (string.IsNullOrEmpty(search) && string.IsNullOrEmpty(category))
            {
                return new List<CreditCard>();
            }
            var creditard = dbContext.CreditCards.AsQueryable();
            if(!string.IsNullOrEmpty(search)) 
                creditard = creditard.Where(c => c.Name.ToLower().Contains(search.ToLower()));
            if(!string.IsNullOrEmpty(category))
                creditard = creditard.Where( c => c.Category.ToLower() == category.ToLower());
            return await creditard.ToListAsync();
        }

        public async Task<CreditCard> GetCreditCardByID(int id)
        {
            var cc = await dbContext.CreditCards.FirstOrDefaultAsync(x => x.Id == id);
            return cc;
        }

        public async Task UpdateCreditCardAsync(CreditCard creditCard)
        {
            dbContext.CreditCards.Update(creditCard);
            await dbContext.SaveChangesAsync();
        }
    }
}
