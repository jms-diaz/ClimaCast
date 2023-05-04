using ClimaCast.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClimaCast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyService _currencyService;

        public CurrencyController(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrencyAsync()
        {
            var data = await _currencyService.GetCurrencyDataAsync();

            if (data != null) 
            {
                return Ok(data);
            } else { 
                return BadRequest(); 
            }
        }
    }
}
