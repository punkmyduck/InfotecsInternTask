using InfotecsInternTask.ApplicationLayer.Interfaces;
using InfotecsInternTask.ApplicationLayer.Services.QueryServices;
using InfotecsInternTask.DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InfotecsInternTask.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private IValuesQueryService _valuesQueryService;
        public ValuesController(IValuesQueryService valuesQueryService)
        {
            _valuesQueryService = valuesQueryService;
        }


        [HttpGet("Last10ByFile")]
        public async Task<ActionResult<IEnumerable<Value>>> GetLast10ByFile(string fileName)
        {
            var results = await _valuesQueryService.GetLast10ByFileAsync(fileName);

            if (!results.Any())
                return NotFound("No results match the given filters.");

            return Ok(results);
        }
    }
}
