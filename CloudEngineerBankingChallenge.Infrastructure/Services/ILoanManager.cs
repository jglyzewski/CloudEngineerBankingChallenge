using CloudEngineerBankingChallenge.Core.Domain;

namespace CloudEngineerBankingChallenge.Infrastructure.Services
{
    public interface ILoanManager
    {
        decimal CalculateMonthlyCost(Loan loan);
        decimal CalculateTotalAmountPaidAsAdministrationFee(Loan loan);
        decimal CalculateTotalAmountPaidAsInterestRate(Loan loan, decimal montlyCost);
    }
}
