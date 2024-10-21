using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.ViewModels
{
    public class SupplierOrderViewModel
    {
        public Guid SoSupplierId { get; set; }
        public Guid SoAddressId { get; set; }
        public float SoTotal { get; set; }
        public DateTime SoDate { get; set; }
        public bool SoState { get; set; }
    }
}
