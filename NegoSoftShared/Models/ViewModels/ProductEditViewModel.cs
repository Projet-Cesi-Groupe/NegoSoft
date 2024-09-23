﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.ViewModels
{
    public class ProductEditViewModel
    {
        public string ProName { get; set; }
        public string ProDescription { get; set; }
        public float ProPrice { get; set; }
        public int ProStock { get; set; }
        public Guid ProTypeId { get; set; }
        public Guid ProSupplierId { get; set; } 
        public float ProBoxPrice { get; set; }
    }
}
