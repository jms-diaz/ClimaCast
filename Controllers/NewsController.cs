using ClimaCast.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClimaCast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNewsAsync()
        {
            var data = await _newsService.GetNewsDataAsync();

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
