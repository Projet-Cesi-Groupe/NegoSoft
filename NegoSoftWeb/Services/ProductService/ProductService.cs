using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Data;
using NegoSoftWeb.Models.Entities;
using NegoSoftWeb.Models.ViewModels;


namespace NegoSoftWeb.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly NegoSoftContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(NegoSoftContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Type)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Type)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProId == id);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<bool> ProductExistsAsync(Guid id)
        {
            return await _context.Products.AnyAsync(e => e.ProId == id);
        }

        public async Task<IEnumerable<int>> GetYearsAsync()
        {
            return await _context.Products
                .Select(p => p.ProYear)
                .Distinct()
                .ToListAsync();
        }

        public async Task<ProductSearchViewModel> SearchAsync(string searchString, Guid? typeId, Guid? supplierId, int? productYear, SortOrder sortOrder)
        {
            var products = await GetAllProductsAsync();
            Console.WriteLine(products.Count());

            //controle de la recherche
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            //on filtre par type de produit si un type est selectionné
            if (typeId.HasValue)
            {
                products = products.Where(p => p.ProTypeId == typeId.Value).ToList();
                Console.WriteLine(typeId.Value);
                Console.WriteLine(products.Count());
            }

            //on filtre par fournisseur si un fournisseur est selectionné
            if (supplierId.HasValue)
            {
                products = products.Where(p => p.ProSupplierId == supplierId.Value).ToList();
            }


            //on filtre par année si une année est selectionnée
            if (productYear.HasValue)
            {
                products = products.Where(p => p.ProYear == productYear.Value).ToList();
            }

            //on trie les produits
            switch (sortOrder)
            {
                case SortOrder.NameAsc:
                    products = products.OrderBy(p => p.ProName).ToList();
                    break;
                case SortOrder.NameDesc:
                    products = products.OrderByDescending(p => p.ProName).ToList();
                    break;
                case SortOrder.PriceAsc:
                    products = products.OrderBy(p => p.ProPrice).ToList();
                    break;
                case SortOrder.PriceDesc:
                    products = products.OrderByDescending(p => p.ProPrice).ToList();
                    break;
            }

            //instanciation du model à passer à la vue contenant les produits filtrés et les types de produits
            var model = new ProductSearchViewModel
            {
                Products = products,
                ProductTypes = await _context.Types.ToListAsync(),
                SelectedTypeId = typeId,
                ProductSuppliers = await _context.Suppliers.ToListAsync(),
                SelectedSupplierId = supplierId,
                ProductYears = await GetYearsAsync(),
                SelectedYear = productYear ?? 0,
                SelectedSortOrder = sortOrder
            };

            return model;
        }
    }
}
