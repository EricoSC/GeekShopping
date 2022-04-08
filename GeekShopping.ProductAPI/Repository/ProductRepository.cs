using AutoMapper;
using GeekShopping.ProductAPI.DTOs;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySQLContext _context;
        private readonly IMapper _map;

        public ProductRepository(MySQLContext context, IMapper map)
        {
            _map = map;
            _context = context;
        }
        public async Task<ProductDTO> Create(ProductDTO product)
        {
            await _context.Products.AddAsync(_map.Map<Product>(product));
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var productdto = await FindById(id);

                if (productdto == null)
                {
                    return false;
                }
                else
                {
                    var product = _map.Map<Product>(productdto);
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    return true;
                }

            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductDTO>> FindAll()
        {
            var products = await _context.Products.ToListAsync();
            return _map.Map<List<ProductDTO>>(products);
        }

        public async Task<ProductDTO> FindById(long id)
        {
            var products = await _context.Products.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return _map.Map<ProductDTO>(products);
        }

        public async Task<ProductDTO> Update(ProductDTO product)
        {
            _context.Products.Update(_map.Map<Product>(product));
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
