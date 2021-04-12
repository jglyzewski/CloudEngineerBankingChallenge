using System;

namespace CloudEngineerBankingChallenge.Infrastructure.DTO
{
    public class LoanDto
    {
        public Guid Id { get; set; }
        public decimal MonthlyCost { get; set; }
        public decimal TotalAmountPaidAsAdministrationFee { get; set; }
        public decimal TotalAmountPaidAsInterestRate { get; set; }
    }
}
