using FluentValidation;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Repositories.Generic;
using ProniaOnion.Application.DTOs.Tags;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Validators
{
    public class CreateTagDtoValidator:AbstractValidator<CreateTagDto>
    {
        private readonly ITagRepository _tagRepository;

        public CreateTagDtoValidator(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;

            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(100)
                .MinimumLength(2)
                .Matches(@"[A-Za-z\s0-9]*$");
        }
        //.MustAsync(CheckNameExsistence);
        //}

        //public async Task<bool> CheckNameExsistence(string name, CancellationToken token)
        //{
        //    return !await _tagRepository.AnyAsync(c => c.Name == name);
        //}
    }
}
