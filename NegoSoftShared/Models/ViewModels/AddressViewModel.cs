using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.ViewModels
{
    public class AddressViewModel
    {
        public string AddDeliveryStreet { get; set; }
        public string AddDeliveryCity { get; set; }
        public string AddDeliveryZipCode { get; set; }
        public string AddDeliveryCountry { get; set; }
        public string AddBillingStreet { get; set; }
        public string AddBillingCity { get; set; }
        public string AddBillingZipCode { get; set; }
        public string AddBillingCountry { get; set; }
    }
}
