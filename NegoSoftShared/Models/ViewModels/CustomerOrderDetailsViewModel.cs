using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.ViewModels
{
    public class CustomerOrderDetailsViewModel
    {
        public Guid CodOrderId { get; set; }
        public int CodQuantity { get; set; }
        public Guid CodProductId { get; set; }
        public float CodPrice { get; set; }
    }
}
