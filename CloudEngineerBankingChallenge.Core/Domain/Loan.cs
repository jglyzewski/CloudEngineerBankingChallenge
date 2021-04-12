using System;

namespace CloudEngineerBankingChallenge.Core.Domain
{
    public class Loan
    {
        public const int MonthsPerYear = 12;

        public Guid Id { get; protected set; }
        public decimal Amount { get; protected set; }
        public int Duration { get; protected set; }
        public int NumberOfPayments { get => Duration * MonthsPerYear; }

        protected Loan() { }

        public Loan(decimal amount, int duration)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            Duration = duration;
        }
    }
}
