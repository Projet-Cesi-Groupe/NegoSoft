using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.ViewModels
{
    public class CustomerViewModel
    {
        public string CusFirstName { get; set; }
        public string CusLastName { get; set; }
        public string CusEmail { get; set; }
        public string CusPhone { get; set; }
        public string CusUserId { get; set; }
    }
}
