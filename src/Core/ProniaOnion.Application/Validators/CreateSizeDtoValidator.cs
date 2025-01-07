using FluentValidation;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Repositories.Generic;
using ProniaOnion.Application.DTOs.Sizes;
using ProniaOnion.Application.DTOs.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Validators
{
    public class CreateSizeDtoValidator:AbstractValidator<CreateSizeDto>
    {
        private readonly ISizeRepository _sizeRepository;

        public CreateSizeDtoValidator(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;

            RuleFor(s=>s.Name)
                .NotEmpty()
                .MaximumLength(100)
                .MinimumLength(2)
                .Matches(@"[A-Za-z\s0-9]*$").MustAsync(CheckNameExsistence);
        }

        public async Task<bool> CheckNameExsistence(string name, CancellationToken token)
        {
            return !await _sizeRepository.AnyAsync(c => c.Name == name);
        }
    }
}
