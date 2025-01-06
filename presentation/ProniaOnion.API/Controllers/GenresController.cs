using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Genres;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _service;

        public GenresController(IGenreService service)
        {

            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();
            var genreDto = await _service.GetByIdAsync(id);
            if (genreDto == null) return NotFound();
            return StatusCode(StatusCodes.Status200OK, genreDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateGenreDto genreDto)
        {
            await _service.CreateAsync(genreDto);

            return StatusCode(StatusCodes.Status201Created);


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();

            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateGenreDto genreDto)
        {
            if (id < 1) return BadRequest();

            await _service.UpdateAsync(id, genreDto);

            return NoContent();
        }
    }
}
