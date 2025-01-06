﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories.Generic;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Tags;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository,IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateTagDto tagDto)
        {
            if (await _tagRepository.AnyAsync(t => t.Name == tagDto.Name)) throw new Exception("Already exists");
            var tag=_mapper.Map<Tag>(tagDto);
            tag.ModifiedAt = DateTime.Now;
            tag.CreatedAt=DateTime.Now;
            await _tagRepository.AddAsync(tag);
            await _tagRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {     
            Tag tag=await _tagRepository.GetByIdAsync(id);
            if(tag==null) throw new Exception("Not found");
            _tagRepository.Delete(tag);
            await _tagRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TagItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Tag> tags=await _tagRepository
                .GetAll(skip:(page-1)*take, take:take)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TagItemDto>>(tags);
        }

        public async Task<GetTagDto> GetByIdAsync(int id)
        {
            Tag tag = await _tagRepository.GetByIdAsync(id);
            if (tag == null) return null;
            GetTagDto tagDto=_mapper.Map<GetTagDto>(tag);
            return tagDto;


        }

        public async Task UpdateAsync(int id, UpdateTagDto tagDto)
        {
            Tag tag=await _tagRepository.GetByIdAsync(id);
            if (tag == null) throw new Exception("Not found");
            if (await _tagRepository.AnyAsync(t => t.Name == tag.Name && t.Id != id)) throw new Exception("Already exists");

            tag=_mapper.Map(tagDto,tag);

            tag.Name= tagDto.Name;
            tag.ModifiedAt = DateTime.Now;
             _tagRepository.Update(tag);
            await _tagRepository.SaveChangesAsync();

        }
    }
}
