using FluentValidation;
using ProniaOnion.Application.Abstractions.Repositories.Generic;
using ProniaOnion.Application.DTOs.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Validators
{
    public class CreateAuthorDtoValidator:AbstractValidator<CreateAuthorDto>
    {
        private readonly IAuthorRepository _repository;

        public CreateAuthorDtoValidator(IAuthorRepository repository)
        {
            _repository = repository;

            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("Data required")
               .MaximumLength(100).WithMessage("Characters should be less than 100")
               .MinimumLength(3)
               .Matches(@"[A-Zaz-z\s0-9]*$")
               .MustAsync(CheckNameExsistence);
        }

        public async Task<bool>CheckNameExsistence(string name,CancellationToken token)
        {
            return !await _repository.AnyAsync(c=>c.Name==name);
        }
    }
}
