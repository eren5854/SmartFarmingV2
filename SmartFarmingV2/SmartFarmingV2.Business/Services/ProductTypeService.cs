using AutoMapper;
using FluentValidation.Results;
using SmartFarmingV2.Business.Validator;
using SmartFarmingV2.DataAccess.Repositories;
using SmartFarmingV2.Entities.DTOs;
using SmartFarmingV2.Entities.Models;

namespace SmartFarmingV2.Business.Services;

public sealed class ProductTypeService(
    IProductTypeRepository productTypeRepository, 
    IMapper mapper) : IProductTypeService
{
    public string Create(CreateProductTypeDto request)
    {
        CreateProductTypeDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if (!result.IsValid)
        {
            throw new ArgumentException(string.Join(", ", result.Errors.Select(s => s.ErrorMessage).ToList()));
        }

        bool isProductTypeNameExists = productTypeRepository.Any(p => p.Name == request.Name);

        if (isProductTypeNameExists)
        {
            throw new ArgumentException("Bu ürün tipi kategorisi zaten mevcut");
        }

        ProductType productType = mapper.Map<ProductType>(request);
        productType.CreatedBy = "Admin";
        productType.CreatedDate = DateTime.Now;

        productTypeRepository.Create(productType);
        return "Ürün kategorisi oluşturuldu";
    }

    public string Update(UpdateProductTypeDto request)
    {
        ProductType? productType = productTypeRepository.GetProductTypeById(request.Id);
        if (productType is null)
        {
            throw new ArgumentException("Ürün kategorisi bulunamadı.");
        }

        if(productType.Name != request.Name)
        {
            bool isProductTypeNameExists = productTypeRepository.Any(p => p.Name == request.Name);
            if (isProductTypeNameExists)
            {
                throw new ArgumentException("Bu Ürün kategorisi zaten mevcut");
            }
        }

        mapper.Map(request, productType);
        productType.UpdatedBy = "Admin";
        productType.UpdatedDate = DateTime.Now;

        productTypeRepository.Update(productType);
        return "Ürün kategorisi güncellendi";
    }

    public string DeleteById(Guid id)
    {
        productTypeRepository.DeleteById(id);
        return "Ürün kategorisi silindi";
    }

    public List<ProductType> GetAll()
    {
        List<ProductType> productTypes = productTypeRepository.GetAll().OrderBy(p => p.Name).ToList();
        return productTypes;
    }
}
