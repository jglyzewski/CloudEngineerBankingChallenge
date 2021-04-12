using CloudEngineerBankingChallenge.Infrastructure.DTO;
using CloudEngineerBankingChallenge.Infrastructure.Services;
using CloudEngineerBankingChallenge.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CloudEngineerBankingChallenge.Tests.Services
{
    [TestClass]
    public class LoanServiceTests
    {
        private readonly Mock<IOptions<GeneralSettings>> _generalSettingsMock;

        public LoanServiceTests()
        {
            var generalSettings = new GeneralSettings()
            {
                AdministrationFeeMinAmount = 10000,
                AdministrationFeeMinPercent = 1,
                AnnualInterestRatePercent = 5.0
            };

            _generalSettingsMock = new Mock<IOptions<GeneralSettings>>();
            _generalSettingsMock.Setup(x => x.Value).Returns(generalSettings);
        }

        [TestMethod]
        public void Generate_should_calculate_loan_overview()
        {
            var amount = 500000;
            var duration = 10;

            var expectedResult = new LoanDto()
            {
                MonthlyCost = 5303.28m,
                TotalAmountPaidAsInterestRate = 136393.09m,
                TotalAmountPaidAsAdministrationFee = 5000.00m
            };

            var loanManager = new LoanManager(_generalSettingsMock.Object);

            var sut = new LoanService(loanManager);

            var result = sut.Generate(amount, duration);

            Assert.AreEqual(expectedResult.MonthlyCost, result.MonthlyCost);
            Assert.AreEqual(expectedResult.TotalAmountPaidAsInterestRate, result.TotalAmountPaidAsInterestRate);
            Assert.AreEqual(expectedResult.TotalAmountPaidAsAdministrationFee, result.TotalAmountPaidAsAdministrationFee);
        }
    }
}
