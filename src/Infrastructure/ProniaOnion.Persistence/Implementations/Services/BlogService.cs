using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories.Generic;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Blogs;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateBlogDto blogDto)
        {
            if (await _repository.AnyAsync(c => c.Name == blogDto.Name)) throw new Exception("Already exists");
           
            var blog = _mapper.Map<Blog>(blogDto);

            blog.CreatedAt = DateTime.Now;
            blog.ModifiedAt = DateTime.Now;

            await _repository.AddAsync(blog);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Blog blog = await _repository.GetByIdAsync(id);
            if (blog == null) throw new Exception("Not found");
            _repository.Delete(blog);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Blog> blogs = await _repository
                 .GetAll(skip: (page - 1) * take, take: take)
                 .ToListAsync();

            return _mapper.Map<IEnumerable<BlogItemDto>>(blogs);
        }

        public async Task<GetBlogDto> GetByIdAsync(int id)
        {
            Blog blog = await _repository.GetByIdAsync(id);
            if (blog == null) return null;
            GetBlogDto blogDto = _mapper.Map<GetBlogDto>(blog);
            return blogDto;
        }

        public async Task UpdateAsync(int id, UpdateBlogDto blogDto)
        {
            Blog blog = await _repository.GetByIdAsync(id);
            if (blog == null) throw new Exception("Not found");
            if (await _repository.AnyAsync(c => c.Name == blogDto.Name && c.Id != id)) throw new Exception("Already exists");

            blog = _mapper.Map(blogDto, blog);

            blog.Name = blogDto.Name;
            blog.Article = blogDto.Article;
            blog.Image = blogDto.Image;
            blog.AuthorId=blogDto.AuthorId;
            blog.GenreId = blogDto.GenreId;


            blog.ModifiedAt = DateTime.Now;

            _repository.Update(blog);
            await _repository.SaveChangesAsync();
        }
    }
}
