using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories.Generic;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Authors;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateAuthorDto authorDto)
        {
            if (await _repository.AnyAsync(c => c.Name == authorDto.Name)) throw new Exception("Already exists");

            var author = _mapper.Map<Author>(authorDto);

            author.CreatedAt = DateTime.Now;
            author.ModifiedAt = DateTime.Now;

            await _repository.AddAsync(author);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Author author = await _repository.GetByIdAsync(id);
            if (author == null) throw new Exception("Not found");
            _repository.Delete(author);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuthorItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Author> authors = await _repository
                 .GetAll(skip: (page - 1) * take, take: take)
                 .ToListAsync();

            return _mapper.Map<IEnumerable<AuthorItemDto>>(authors);
        }

        public async Task<GetAuthorDto> GetByIdAsync(int id)
        {
            Author author = await _repository.GetByIdAsync(id, nameof(Author.Blogs));


            if (author == null) return null;

            GetAuthorDto authorDto = _mapper.Map<GetAuthorDto>(author);

            return authorDto;
        }

        public async Task UpdateAsync(int id, UpdateAuthorDto authorDto)
        {
            Author author = await _repository.GetByIdAsync(id);
            if (author == null) throw new Exception("Not found");
            if (await _repository.AnyAsync(c => c.Name == authorDto.Name && c.Id != id)) throw new Exception("Already exists");

            author = _mapper.Map(authorDto, author);

            author.Name = authorDto.Name;
            author.Surname = authorDto.Surname;

            author.ModifiedAt = DateTime.Now;

            _repository.Update(author);
            await _repository.SaveChangesAsync();
        }
    }
}
