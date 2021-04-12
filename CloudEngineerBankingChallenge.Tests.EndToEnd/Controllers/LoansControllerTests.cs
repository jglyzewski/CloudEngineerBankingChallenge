using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CloudEngineerBankingChallenge.Api;
using CloudEngineerBankingChallenge.Infrastructure.Commands;
using CloudEngineerBankingChallenge.Infrastructure.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CloudEngineerBankingChallenge.Tests.EndToEnd.Controllers
{
    [TestClass]
    public class LoanControllerTests
    {
        protected readonly TestServer _server;
        protected readonly HttpClient _client;

        public LoanControllerTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [TestMethod]
        public async Task Given_grater_than_zero_duration_and_amount_load_should_be_generated()
        {
            var command = new GenerateLoan
            {
                Amount = 500000,
                Duration = 10
            };

            var expectedResult = new LoanDto()
            {
                MonthlyCost = 5303.28m,
                TotalAmountPaidAsInterestRate = 136393.09m,
                TotalAmountPaidAsAdministrationFee = 5000.00m
            };

            var payload = GetPayload(command);
            var response = await _client.PostAsync("api/loans", payload);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<LoanDto>(responseString);

            Assert.AreEqual(expectedResult.MonthlyCost, result.MonthlyCost);
            Assert.AreEqual(expectedResult.TotalAmountPaidAsInterestRate, result.TotalAmountPaidAsInterestRate);
            Assert.AreEqual(expectedResult.TotalAmountPaidAsAdministrationFee, result.TotalAmountPaidAsAdministrationFee);
        }

        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
