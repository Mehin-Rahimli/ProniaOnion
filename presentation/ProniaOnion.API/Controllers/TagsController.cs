﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Application.DTOs.Tags;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _service;
        private readonly IValidator<CreateTagDto> _validator;

        public TagsController(ITagService service, IValidator<CreateTagDto> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult>Get(int page=1,int take = 3)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult>Get(int id)
        {
            if(id<1) return BadRequest();
            GetTagDto tagDto=await _service.GetByIdAsync(id);
            if(tagDto == null) return NotFound();
            return StatusCode(StatusCodes.Status200OK,tagDto);
        }

        [HttpPost]
        public async Task<IActionResult>Create([FromForm]CreateTagDto tagDto)
        {
            await _service.CreateAsync(tagDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            if(id<1) return BadRequest();
            await _service.DeleteAsync(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>Update(int id,UpdateTagDto tagDto)
        {
            if(id<1) return BadRequest();
            await _service.UpdateAsync(id,tagDto);
            return NoContent();
        }
    }
}
