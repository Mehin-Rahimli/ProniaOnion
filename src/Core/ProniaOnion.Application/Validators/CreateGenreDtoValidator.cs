using FluentValidation;
using ProniaOnion.Application.Abstractions.Repositories.Generic;
using ProniaOnion.Application.DTOs.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Validators
{
    public class CreateGenreDtoValidator:AbstractValidator<CreateGenreDto>
    {
        private readonly IGenreRepository _repository;

        public CreateGenreDtoValidator(IGenreRepository repository)
        {
            _repository = repository;
            RuleFor(c => c.Name)
               .NotEmpty()
               .MaximumLength(100)
               .MinimumLength(2)
               .Matches(@"[A-Za-z\s0-9]*$");
        }
        //        .MustAsync(CheckNameExsistence);
        //}

        //public async Task<bool> CheckNameExsistence(string name, CancellationToken token)
        //{
        //    return !await _repository.AnyAsync(c => c.Name == name);
        //}
    }
}
