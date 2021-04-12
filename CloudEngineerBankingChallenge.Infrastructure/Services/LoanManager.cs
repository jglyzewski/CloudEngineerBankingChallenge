using System;
using Microsoft.Extensions.Options;
using CloudEngineerBankingChallenge.Core.Domain;
using CloudEngineerBankingChallenge.Infrastructure.Settings;

namespace CloudEngineerBankingChallenge.Infrastructure.Services
{
    public class LoanManager : ILoanManager
    {
        private readonly IOptions<GeneralSettings> _generalSettings;

        public LoanManager(IOptions<GeneralSettings> generalSettings)
        {
            _generalSettings = generalSettings;
        }

        public decimal CalculateMonthlyCost(Loan loan)
        {
            var interest = _generalSettings.Value.AnnualInterestRatePercent;
            var rateOfInterest = interest / (Loan.MonthsPerYear * 100);

            // formula: loan amount = (interest rate * loan amount) / (1 - (1 + interest rate)^(number of payments * -1))
            return Math.Round((decimal)rateOfInterest * loan.Amount / (1 - (decimal)Math.Pow(1 + rateOfInterest, loan.NumberOfPayments * -1)), 2);
        }

        public decimal CalculateTotalAmountPaidAsAdministrationFee(Loan loan)
        {
            return Math.Round(LowestFee(_generalSettings.Value.AdministrationFeeMinPercent / 100 * loan.Amount), 2);

            decimal LowestFee(decimal adminFeePercentAmount)
                => adminFeePercentAmount < _generalSettings.Value.AdministrationFeeMinAmount
                    ? adminFeePercentAmount
                    : _generalSettings.Value.AdministrationFeeMinAmount;
        }

        public decimal CalculateTotalAmountPaidAsInterestRate(Loan loan, decimal montlyCost)
            => Math.Round(montlyCost * loan.NumberOfPayments - loan.Amount, 2);
    }
}
