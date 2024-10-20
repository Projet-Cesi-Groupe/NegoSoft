using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Data;
using NegoSoftWeb.Models.Entities;
using NegoSoftWeb.Services.AddressService;
using NegoSoftWeb.Services.CartService;
using NegoSoftWeb.Services.CustomerService;
using NegoSoftWeb.Services.ProductService;
using NegoSoftWeb.Services.SupplierService;

namespace NegoSoftWeb.Services.SupplierOrderService
{
    public class SupplierOrderService : ISupplierOrderService
    {
        private readonly NegoSoftContext _context;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;

        public SupplierOrderService(NegoSoftContext context, ICartService cartService, IProductService productService, ISupplierService supplierService)
        {
            _context = context;
            _cartService = cartService;
            _productService = productService;
            _supplierService = supplierService;
        }

        public async Task CreateSupplierOrderAsync()
        {
            var cart = await _cartService.GetCartAsync();

            if (cart == null)
            {
                throw new InvalidOperationException("Informations missing for the creation of the Order");
            }

            //On recupère les articles du panier par fournisseur (trié par fournisseur)

            var cartWithProducts = new List<(CartItem item, Product product)>();

            foreach (var item in cart)
            {
                var product = await _productService.GetProductByIdAsync(item.ProId);
                cartWithProducts.Add((item, product));
            }

            var filteredCart = cartWithProducts
                .Where(cp => cp.item.ProQuantity > cp.product.ProStock)
                .ToList();

            var productsGroupedBySupplier = filteredCart
                .GroupBy(cp => cp.product.ProSupplierId);



            //parcourir chaque fournisseur du groupe pour créer des commandes
            foreach (var supplierGroup in productsGroupedBySupplier)
            {
                var supplierId = supplierGroup.Key;
                var supplier = await _supplierService.GetSupplierByIdAsync(supplierId);

                var order = new SupplierOrder
                {
                    SoId = Guid.NewGuid(),
                    SoSupplierId = supplierId,
                    SoAddressId = supplier.SupDefaultAddressId,
                    SoDate = DateTime.Now,
                    SoTotal = 0
                };

                float total = 0;
                var orderDetails = new List<SupplierOrderDetails>();

                foreach (var cartProduct in supplierGroup)
                {
                    var item = cartProduct.item;
                    var product = cartProduct.product;

                    var orderDetail = new SupplierOrderDetails
                    {
                        SodId = Guid.NewGuid(),
                        SodProductId = item.ProId,
                        SodQuantity = item.ProQuantity - product.ProStock + 5,
                        SodOrderId = order.SoId,
                        SodPrice = item.ProPrice * (item.ProQuantity - product.ProStock + 5)
                    };

                    orderDetails.Add(orderDetail);
                    total += item.ProPrice * orderDetail.SodQuantity;
                }

                order.SoTotal = total;

                await _context.SupplierOrders.AddAsync(order);
                await _context.SupplierOrderDetails.AddRangeAsync(orderDetails);
            }
            await _context.SaveChangesAsync();
        }
    }
}
