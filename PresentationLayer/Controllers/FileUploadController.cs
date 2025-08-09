using InfotecsInternTask.ApplicationLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfotecsInternTask.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CsvController : ControllerBase
    {
        private readonly CsvProcessingService _csvService;
        public CsvController(CsvProcessingService csvService)
        {
            _csvService = csvService;
        }

        [HttpPost("csv")]
        public async Task<IActionResult> PostFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty or not posted");

            try
            {
                await _csvService.Process(file);
                return Ok("File loaded successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}