using Microsoft.AspNetCore.Mvc;
using CloudEngineerBankingChallenge.Infrastructure.Services;
using CloudEngineerBankingChallenge.Infrastructure.Commands;

namespace CloudEngineerBankingChallenge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost]
        public IActionResult GenerateLoad([FromBody] GenerateLoan request)
        {
            var loanOverwie = _loanService.Generate(request.Amount, request.Duration);

            return Ok(loanOverwie);
        }
    }
}
