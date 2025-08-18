using CardInsight.API.Models;

namespace CardInsight.API.Repositories
{
    public interface ICreditCard
    {
        Task AddCreditCardAsync(CreditCard creditCard);
        Task UpdateCreditCardAsync(CreditCard creditCard);
        Task DeleteCreditCardAsync(int Id);
        Task<List<CreditCard>> GetCreditCard(string? search, string? category);
        Task<List<CreditCard>> GetAllCreditCards();
        Task<CreditCard> GetCreditCardByID(int id);

    }
}
