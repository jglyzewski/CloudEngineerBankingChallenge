using CloudEngineerBankingChallenge.Infrastructure.DTO;

namespace CloudEngineerBankingChallenge.Infrastructure.Services
{
    public interface ILoanService
    {
        LoanDto Generate(decimal amount, int duration);
    }
}
