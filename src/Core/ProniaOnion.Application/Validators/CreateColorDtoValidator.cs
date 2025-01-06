using FluentValidation;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.DTOs.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Validators
{
    internal class CreateColorDtoValidator:AbstractValidator<CreateColorDto>
    {
        private readonly IColorRepository _repository;

        public CreateColorDtoValidator(IColorRepository repository)
        {
            _repository = repository;

            RuleFor(c=>c.Name)
                .NotEmpty()
                .MaximumLength(100)
                .MinimumLength(2)
                .Matches(@"[A-Za-z\s0-9]*$")
                ;
            
        }
    }
}
