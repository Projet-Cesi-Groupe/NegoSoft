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
        public bool ProIsActive { get; set; }
        public int ProYear { get; set; }
        public float ProAlcoholVolume { get; set; }
    }
}
