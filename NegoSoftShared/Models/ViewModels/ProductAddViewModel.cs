using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.ViewModels
{
    public class ProductAddViewModel
    {
        public Guid ProId { get; set; }
        public string ProName { get; set; }
        public string? ProDescription { get; set; }
        public Guid ProSupplierId { get; set; }
        public float ProPrice { get; set; }
        public float? ProBoxPrice { get; set; }
        public Guid ProTypeId { get; set; }
        public int ProStock { get; set; }
        public int ProYear { get; set; }
        public float ProAlcoholVolume { get; set; }
    }
}
