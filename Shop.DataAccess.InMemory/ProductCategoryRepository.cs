using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory pCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);
            if (pCategoryToUpdate != null)
            {
                pCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public ProductCategory FindById(int id)
        {
            ProductCategory p = productCategories.Find(p1 => p1.Id == id);
            if (p != null)
            {
                return p;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(int id)
        {
            ProductCategory pCategoryToDelete = productCategories.Find(p => p.Id == id);
            if (pCategoryToDelete != null)
            {
                productCategories.Remove(pCategoryToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
