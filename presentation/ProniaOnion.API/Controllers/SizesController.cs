using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Application.DTOs.Sizes;
using ProniaOnion.Persistence.Contexts;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly ISizeService _sizeService;
        private readonly IValidator<CreateSizeDto> _validator;

        public SizesController(ISizeService sizeService, IValidator<CreateSizeDto> validator)
        {
            _sizeService = sizeService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult>Get(int page=1,int take = 3)
        {
            return Ok(await _sizeService.GetAllAsync(page, take));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>Get(int id)
        {
            if (id < 0) return BadRequest();
            GetSizeDto sizeDto=await _sizeService.GetByIdAsync(id);
            if (sizeDto == null) return NotFound();
            return StatusCode(StatusCodes.Status200OK, sizeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateSizeDto sizeDto)
        {
            await _sizeService.CreateAsync(sizeDto);    
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();
            await _sizeService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>Update(int id,UpdateSizeDto sizeDto)
        {
            if (id < 1) return BadRequest();
            await _sizeService.UpdateAsync(id, sizeDto);
            return NoContent();
        }
    }
}
