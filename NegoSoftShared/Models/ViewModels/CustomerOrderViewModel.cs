using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.ViewModels
{
    public class CustomerOrderViewModel
    {
        public Guid CoCustomerId { get; set; }
        public Guid CoAddressId { get; set; }
        public float CoTotal { get; set; }
        public DateTime CoDate { get; set; }
        public string CoState { get; set; }
    }
}
