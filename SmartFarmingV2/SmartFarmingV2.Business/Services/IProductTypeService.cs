using SmartFarmingV2.Entities.DTOs;
using SmartFarmingV2.Entities.Models;

namespace SmartFarmingV2.Business.Services;

public interface IProductTypeService
{
    string Create(CreateProductTypeDto request);
    string Update(UpdateProductTypeDto request);
    string DeleteById(Guid id);
    List<ProductType> GetAll();
}
