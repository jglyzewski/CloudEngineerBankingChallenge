using CloudEngineerBankingChallenge.Core.Domain;
using CloudEngineerBankingChallenge.Infrastructure.DTO;
using System;

namespace CloudEngineerBankingChallenge.Infrastructure.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanManager _loanManager;

        public LoanService(ILoanManager loanManager)
        {
            _loanManager = loanManager;
        }

        public LoanDto Generate(decimal amount, int duration)
        {
            if (amount <= 0 || duration <= 0)
            {
                throw new ArgumentException("Amount and duration must be greater than zero.");
            }

            var loan = new Loan(amount, duration);

            var montlyCost = _loanManager.CalculateMonthlyCost(loan);
            var totalInterestRate = _loanManager.CalculateTotalAmountPaidAsInterestRate(loan, montlyCost);
            var totalAdministrationFee = _loanManager.CalculateTotalAmountPaidAsAdministrationFee(loan);

            return new LoanDto()
            {
                Id = loan.Id,
                MonthlyCost = Math.Round(montlyCost, 2),
                TotalAmountPaidAsInterestRate = Math.Round(totalInterestRate, 2),
                TotalAmountPaidAsAdministrationFee = Math.Round(totalAdministrationFee, 2)
            };
        }
    }
}
