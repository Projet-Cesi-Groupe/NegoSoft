namespace NegoSoftWeb.Models.ViewModels
{
    public class ProductViewModel
    {
        public string ProName { get; set; }
        public string ProDescription { get; set; }
        public float ProPrice { get; set; }
        public int ProStock { get; set; }
        public string ProPictureName { get; set; }
        public Guid ProTypeId { get; set; }
        public Guid ProSupplierId { get; set; }
        public float ProBoxPrice { get; set; }
    }
}
