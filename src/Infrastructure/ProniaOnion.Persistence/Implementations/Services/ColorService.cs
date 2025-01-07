using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Implementations.Repostories;
using ProniaOnion.Persistence.Implementations.Repostories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository colorRepository,IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateColorDto colorDto)
        {

            if (await _colorRepository.AnyAsync(c => c.Name == colorDto.Name)) throw new Exception("Already exists");

            var color = _mapper.Map<Color>(colorDto);

          
            await _colorRepository.AddAsync(color);
            await _colorRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Color color = await _colorRepository.GetByIdAsync(id);
            if (color == null) throw new Exception("Not found");
            _colorRepository.Delete(color);
            await _colorRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ColorItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Color> colors = await _colorRepository
                 .GetAll(skip: (page - 1) * take, take: take)
                 .ToListAsync();

            return _mapper.Map<IEnumerable<ColorItemDto>>(colors);
        }

        public async Task<GetColorDto> GetByIdAsync(int id)
        {
            Color color = await _colorRepository.GetByIdAsync(id);
            if (color == null) return null;
            GetColorDto colorDto = _mapper.Map<GetColorDto>(color);               
            return colorDto;
        }

        public async Task UpdateAsync(int id, UpdateColorDto colorDto)
        {
            Color color = await _colorRepository.GetByIdAsync(id);
            if (color == null) throw new Exception("Not found");
            if (await _colorRepository.AnyAsync(c => c.Name == colorDto.Name && c.Id != id)) throw new Exception("Already exists");

            color = _mapper.Map(colorDto,color);
          //  color.Id = id;

            color.Name = colorDto.Name;
           
            _colorRepository.Update(color);
            await _colorRepository.SaveChangesAsync();
        }

        public async Task SoftDelete(int id)
        {
            Color color = await _colorRepository.GetByIdAsync(id);
            if (color == null) throw new Exception("Not found");
            color.IsDeleted = true;
            _colorRepository.Update(color);
            await _colorRepository.SaveChangesAsync();
        }
    }
}
