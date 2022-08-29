using API_Aplication.Data;
using API_Aplication.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Aplication.Repository
{
    public interface IProductsRepository
    {
        Task<Products> CreateProduct(Products products);
        Task<Products> GetProductById(string ProductId);
        Task<List<Products>> GetProducts();
    }

    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Create or insert Product
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public async Task<Products> CreateProduct(Products products)
        {
            _dbContext.Products.Add(products);
            int res = await _dbContext.SaveChangesAsync();
            if (res > 0)
            {
                return products;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Read or Get All Products
        /// </summary>
        /// <returns></returns>
        public async Task<List<Products>> GetProducts()
        {
            List<Products> products = await _dbContext.Products.ToListAsync();
            return products;
        }

        /// <summary>
        /// Get a single Product
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public async Task<Products> GetProductById(string ProductId)
        {
            Products product = await _dbContext.Products.FirstOrDefaultAsync(x => x.id == ProductId);
            return product;
        }

    }
}
