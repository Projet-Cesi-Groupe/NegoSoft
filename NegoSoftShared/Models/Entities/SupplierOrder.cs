using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.Entities
{
    public class SupplierOrder
    {
        // Propriétés de la classe SupplierOrder
        [Key]
        [Column("so_id")]
        public Guid SoId { get; set; }

        [Required]
        [Column("so_total")]
        public float SoTotal { get; set; }

        [Required]
        [ForeignKey("Supplier")]
        [Column("so_supplier_id")]
        public Guid SoSupplierId { get; set; }

        [Required]
        [ForeignKey("Address")]
        [Column("so_address_id")]
        public Guid SoAddressId { get; set; }

        [Required]
        [Column("so_state")]
        public bool SoState { get; set; } = false;

        [Required]
        [Column("so_date")]
        public DateTime SoDate { get; set; } = DateTime.Now;

        // Propriétés de navigation de la classe permettant de naviguer entre les différentes classes (voir cours sur les relations entre les classes)
        public Address Address { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<SupplierOrderDetails> SupplierOrderDetails { get; set; }
    }

}
