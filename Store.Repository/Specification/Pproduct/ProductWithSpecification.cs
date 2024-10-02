using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Data.Entities;

namespace Store.Repository.Specification.Pproduct
{
    public class ProductWithSpecification : BaseSpecification<Data.Entities.Product>
    {
        public ProductWithSpecification(ProductSpecification Spec) : base(Product => (!Spec.BrandId.HasValue || Product.BrandId == Spec.BrandId.Value)
        && (!Spec.TypeId.HasValue || Product.TypeId == Spec.TypeId.Value)
            &&(string.IsNullOrEmpty(Spec.Search) || Product.Name.Trim().ToLower().Contains(Spec.Search)))
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
            AddOrderBy(x => x.Name);
            ApplyPaginted(Spec.PageSize * (Spec.PageIndex - 1) ,  Spec.PageSize);
            if(!string.IsNullOrEmpty(Spec.Sort))
            {
                switch(Spec.Sort) 
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

      

        public ProductWithSpecification(int ? id) : base(product =>product.Id == id )
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
        }
    }
}
