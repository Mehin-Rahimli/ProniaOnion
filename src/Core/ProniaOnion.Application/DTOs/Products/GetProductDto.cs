using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Application.DTOs.Sizes;
using ProniaOnion.Application.DTOs.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.DTOs.Products
{
    public record GetProductDto(
        int Id, 
        decimal Price, 
        string Name,
        string SKU,
        string Description,
        CategoryItemDto Category,
        IEnumerable<ColorItemDto> Colors,
        IEnumerable<TagItemDto> Tags,
        IEnumerable<SizeItemDto>Sizes 
        


        );
    
}
