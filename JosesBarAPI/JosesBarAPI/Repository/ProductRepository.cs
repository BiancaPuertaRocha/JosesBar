using JosesBarAPI.Exceptions;
using JosesBarAPI.DataContext;
using JosesBarAPI.Dtos;
using JosesBarAPI.Entities;
using System;
using Microsoft.EntityFrameworkCore;

namespace JosesBarAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<bool> DeleteProduct(int productID)
        {
            var _products = _context.Products;
            if (_products == null)
                throw new InternalServerError();
            var prod = await _products.FirstOrDefaultAsync(x => x.Id == productID);
            if (prod == null)
                return false;
            try
            {
                _products.Remove(prod);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new InternalServerError();
            }
        }


        public async Task<Product?> GetProductByID(int productId)
        {
            var _products = _context.Products;
            if (_products != null)
                return await _products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == productId);
            else
                throw new InternalServerError();
        }

        public async Task<List<Product>> GetProductByDescription(string descriprion)
        {
            var _products = _context.Products;
            if (_products != null)
                return await _products.AsNoTracking().Where(x => x.Description.Contains(descriprion)).ToListAsync();
            else
                throw new InternalServerError();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var _products = _context.Products;
            if (_products != null)
                return await _products.AsNoTracking().ToListAsync();
            else
                throw new InternalServerError();
        }

        public async Task<Product> InsertProduct(CreateProduct product)
        {
            var _products = _context.Products;

            if (_products == null)
                throw new InternalServerError();

            
            var prod = new Product
            {
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                Create_at = DateTimeOffset.Now
            };
            try
            {
                await _products.AddAsync(prod);
                await _context.SaveChangesAsync();
                return prod;
            }
            catch (Exception)
            {
                throw new InternalServerError();
            }
        }

        public async Task<Product?> UpdateProduct(UpdateProduct product, int id)
        {
            var _products = _context.Products;
            if (_products == null)
                throw new InternalServerError();

            var prod = await _products.FirstOrDefaultAsync(x => x.Id == id);
            if (prod == null)
                return null;

            try
            {
                prod.Description = product.Description;
                prod.Price = product.Price;
                prod.Quantity = product.Quantity;

                _products.Update(prod);
                await _context.SaveChangesAsync();
                return prod;
            }
            catch
            {
                throw new InternalServerError();
            }

        }

   
    }
}
