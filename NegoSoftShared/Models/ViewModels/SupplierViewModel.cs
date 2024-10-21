using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.ViewModels
{
    public class SupplierViewModel
    {
        public string SupName { get; set; }
        public string SupEmail { get; set; }
        public string SupPhone { get; set; }
        public Guid SupDefaultAddressId { get; set; }
    }
}
