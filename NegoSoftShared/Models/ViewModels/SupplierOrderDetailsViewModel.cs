using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.ViewModels
{
    public class SupplierOrderDetailsViewModel
    {
        public Guid SodOrderId { get; set; }
        public int SodQuantity { get; set; }
        public Guid SodProductId { get; set; }
        public float SodPrice { get; set; }
    }
}
