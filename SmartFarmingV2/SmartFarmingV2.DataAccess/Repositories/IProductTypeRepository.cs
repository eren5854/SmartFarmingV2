using SmartFarmingV2.Entities.Models;
using System.Linq.Expressions;

namespace SmartFarmingV2.DataAccess.Repositories;
public interface IProductTypeRepository
{
    void Create(ProductType productType);
    void Update(ProductType productType);
    void DeleteById(Guid id);
    IQueryable<ProductType> GetAll();
    ProductType? GetProductTypeById(Guid productTypeId);
    bool Any(Expression<Func<ProductType, bool>> predicate);
}
