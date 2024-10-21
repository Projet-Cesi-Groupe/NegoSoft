using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace NegoSoftShared.Models.Entities
{
    public class Customer
    {
        // Propriétés de la classe Customer
        [Key]
        [Column("cus_id")]
        public Guid CusId { get; set; }

        [Required]
        [Column("cus_first_name")]
        public string CusFirstName { get; set; }

        [Required]
        [Column("cus_last_name")]
        public string CusLastName { get; set; }

        [Required]
        [Column("cus_email")]
        public string CusEmail { get; set; }

        [Required]
        [Column("cus_phone")]
        public string CusPhone { get; set; }
        
        [Required]
        [Column("cus_user_id")]
        public string CusUserId { get; set; }

        // Propriétés de navigation de la classe permettant de naviguer entre les différentes classes (voir cours sur les relations entre les classes)
        public ICollection<CustomerOrder> CustomerOrders { get; set; }
    }

}
