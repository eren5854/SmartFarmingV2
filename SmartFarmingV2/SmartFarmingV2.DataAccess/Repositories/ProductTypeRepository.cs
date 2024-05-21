using SmartFarmingV2.DataAccess.Context;
using SmartFarmingV2.Entities.Models;
using System.Linq.Expressions;

namespace SmartFarmingV2.DataAccess.Repositories;
public sealed class ProductTypeRepository(ApplicationDbContext context) : IProductTypeRepository
{
    public bool Any(Expression<Func<ProductType, bool>> predicate)
    {
        return context.ProductTypes.Any(predicate);
    }

    public void Create(ProductType productType)
    {
        context.Add(productType);
        context.SaveChanges();
    }

    public void DeleteById(Guid id)
    {
        ProductType? productType = GetProductTypeById(id);
        if(productType is not null)
        {
            productType.IsDeleted = true;
            context.SaveChanges();
        }
    }

    public IQueryable<ProductType> GetAll()
    {
        return context.ProductTypes.AsQueryable();
    }

    public ProductType? GetProductTypeById(Guid productTypeId)
    {
        return context.ProductTypes.Where(p => p.Id == productTypeId).FirstOrDefault();
    }

    public void Update(ProductType productType)
    {
        context.Update(productType);
        context.SaveChanges();
    }
}
