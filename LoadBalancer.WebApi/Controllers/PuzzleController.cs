using LoadBalancer.WebApi.DTOs;
using LoadBalancer.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoadBalancer.WebApi.Controllers
{
    [Authorize]
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
        [Route("create/{port}")]
        public async Task<IActionResult> Create([FromBody] PuzzleRequestDto puzzle, int port)
        {
            try
            {
                return Ok(await _puzzleService.CreatePuzzleAsync(puzzle, port));
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
            return Ok(await _puzzleService.CancelPuzzleAsync(id));
        }

        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetPuzzlesByUser([FromRoute] string userId)
        {
            return Ok(_puzzleService.GetPuzzlesByUser(userId));
        }
    }
}
