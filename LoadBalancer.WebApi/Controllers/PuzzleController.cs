using LoadBalancer.WebApi.DTOs;
using LoadBalancer.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoadBalancer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuzzleController : ControllerBase
    {

        protected readonly IPuzzleService _puzzleService;
        public PuzzleController(IPuzzleService puzzleService)
        {
            _puzzleService = puzzleService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] PuzzleRequestDto puzzle)
        {
            try
            {
                return Ok(await _puzzleService.CreatePuzzleAsync(puzzle));
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("cancel/{id}")]
        public async Task<IActionResult> Cancel([FromRoute] int id)
        {
            return Ok(await _puzzleService.CompletePuzzleAsync(id));
        }

        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetPuzzlesByUser([FromRoute] string userId)
        {
            return Ok(_puzzleService.GetPuzzlesByUser(userId));
        }
    }
}
