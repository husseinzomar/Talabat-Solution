using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Specifications;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification:BaseSpecification<Product>
    {
        /// This Constructor Use When Need to Get All Products
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
            :base(P => 
                    (string.IsNullOrEmpty(productParams.Search) || P.Name.ToLower().Contains(productParams.Search)) &&
                    (!productParams.BrandId.HasValue || P.ProductBrandId == productParams.BrandId.Value) &&
                    (!productParams.TypeId.HasValue || P.ProductTypeId == productParams.TypeId.Value))
        {
            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand);
            AddOrderBy(P => P.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
        }

        /// This Constructor Use When Need to Get a Specific Product With Id
        public ProductsWithTypesAndBrandsSpecification(int id):base(P => P.Id == id)
        {
            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand);
        }
    }
}
