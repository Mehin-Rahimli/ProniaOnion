﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Blogs;


namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _service;
        private readonly IValidator<CreateBlogDto> _validator;
     

        public BlogsController(IBlogService service, IValidator<CreateBlogDto> validator)
        {
            _service = service;
            _validator = validator;
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
            GetBlogDto blogDto = await _service.GetByIdAsync(id);
            if (blogDto == null) return NotFound();
            return StatusCode(StatusCodes.Status200OK, blogDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBlogDto blogDto)
        {
            await _service.CreateAsync(blogDto);
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
        public async Task<IActionResult> Update(int id, UpdateBlogDto blogDto)
        {
            if (id < 1) return BadRequest();
            await _service.UpdateAsync(id, blogDto);
            return NoContent();
        }
    }
}
