using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Products;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,ICategoryRepository categoryRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductItemDto>> GetAllAsync(int page, int take)
        {
            var products = _mapper.Map<IEnumerable<ProductItemDto>>(await _productRepository
                    .GetAll(skip: (page - 1) * take, take: take)
                    .ToListAsync());

            return products;
        }

        public async Task<GetProductDto> GetByIdAsync(int id)
        {

            var product = _mapper.Map<GetProductDto>(await _productRepository.GetByIdAsync(id, "Category", "ProductColors.Color","ProductTags.Tag","ProductSizes.Size"));
            if (product == null) throw new Exception("Product does not exists");
            return product;
        }

        public async Task CreateAsync(CreateProductDto productDto)
        {
            if (!await _categoryRepository.AnyAsync(c => c.Id == productDto.CategoryId))
                throw new Exception("Category does not exists");


            var colorEntities = await _productRepository.GetManyToManyEntities<Color>(productDto.ColorIds);

            if (colorEntities.Count() != productDto.ColorIds.Distinct().Count())
                throw new Exception("Color id is wrong");



            var tagEntities = await _productRepository.GetManyToManyEntities<Tag>(productDto.TagIds);

            if (tagEntities.Count() != productDto.TagIds.Distinct().Count())
                throw new Exception("Tag id is wrong");


            var sizeEntities = await _productRepository.GetManyToManyEntities<Size>(productDto.SizeIds);

            if (sizeEntities.Count() != productDto.SizeIds.Distinct().Count())
                throw new Exception("Size id is wrong");


            await _productRepository.AddAsync(_mapper.Map<Product>(productDto));
            await _productRepository.SaveChangesAsync();


        }


        public async Task UpdateAsync(int id,UpdateProductDto productDto)
        {
            Product product=await _productRepository.GetByIdAsync(id,"ProductColors","ProductTags","ProductSizes");
            if (productDto.CategoryId != product.CategoryId)
                if (!await _categoryRepository.AnyAsync(c => c.Id == productDto.CategoryId))
                    throw new Exception("Category does not exists");
            
           // product.ProductColors=product.ProductColors.Where(pc=>productDto.ColorIds.Contains(pc.ColorId)).ToList();
          
            ICollection<int> createItems=productDto.ColorIds.Where(cId => !product.ProductColors.Any(pc => pc.ColorId == cId)).ToList();
            var colorEntities = await _productRepository.GetManyToManyEntities<Color>(createItems);
            if (colorEntities.Count() != createItems.Distinct().Count())
                throw new Exception("One or more color id is wrong");

            
            ICollection<int> createItems2 = productDto.TagIds.Where(cId => !product.ProductTags.Any(pc => pc.TagId == cId)).ToList();
            var tagEntities = await _productRepository.GetManyToManyEntities<Tag>(createItems2);
            if (tagEntities.Count() != createItems.Distinct().Count())
                throw new Exception("One or more tag id is wrong"); 
            
            
            ICollection<int> createItems3 = productDto.SizeIds.Where(cId => !product.ProductSizes.Any(pc => pc.SizeId == cId)).ToList();
            var sizeEntities = await _productRepository.GetManyToManyEntities<Size>(createItems3);
            if (sizeEntities.Count() != createItems.Distinct().Count())
                throw new Exception("One or more size id is wrong");

            _productRepository.Update(_mapper.Map(productDto, product));
            await _productRepository.SaveChangesAsync();    
        }
    }
}
