using InfotecsInternTask.ApplicationLayer.Interfaces;
using InfotecsInternTask.DomainLayer.DTO;
using InfotecsInternTask.DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InfotecsInternTask.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResultsController : ControllerBase
    {
        private IResultsQueryService _resultsQueryService;
        public ResultsController(IResultsQueryService resultsQueryService)
        {
            _resultsQueryService = resultsQueryService;
        }

        [HttpGet("FilterResults")]
        public async Task<ActionResult<IEnumerable<Result>>> FilterResultsAsync([FromQuery] ResultFilterDto filterParams)
        {
            var results = await _resultsQueryService.FilterResultsAsync(filterParams);

            if (!results.Any())
                return NotFound("No results match the given filters.");

            return Ok(results);
        }
    }
}
