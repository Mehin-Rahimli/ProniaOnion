using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Authors;
using ProniaOnion.Application.DTOs.Categories;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService service)
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
            var authorDto = await _service.GetByIdAsync(id);
            if (authorDto == null) return NotFound();
            return StatusCode(StatusCodes.Status200OK, authorDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAuthorDto authorDto)
        {
            await _service.CreateAsync(authorDto);

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
        public async Task<IActionResult> Update(int id, UpdateAuthorDto authorDto)
        {
            if (id < 1) return BadRequest();

            await _service.UpdateAsync(id, authorDto);

            return NoContent();
        }
    }
}
