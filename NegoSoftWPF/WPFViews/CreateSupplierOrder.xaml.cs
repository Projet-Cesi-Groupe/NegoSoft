using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Customer = NegoSoftShared.Models.Entities.Customer;
using Product = NegoSoftShared.Models.Entities.Product;

namespace NegoSoftWPF.WPFViews
{
    /// <summary>
    /// Logique d'interaction pour CreateCustomerOrder.xaml
    /// </summary>
    public partial class CreateSupplierOrder : Window
    {
        private readonly HttpClient _httpClient;
        private Guid _addressId;
        private List<SupplierOrderDetailsViewModel> _orderDetails;

        public CreateSupplierOrder()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _orderDetails = new List<SupplierOrderDetailsViewModel>();
            LoadSuppliersAsync();
            LoadProductsAsync();
        }

        public async Task LoadSuppliersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://localhost:7101/api/Supplier";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(jsonResponse);

                        SupplierIdComboBox.ItemsSource = suppliers;
                        SupplierIdComboBox.DisplayMemberPath = "SupName";
                        SupplierIdComboBox.SelectedValuePath = "SupId";
                        SupplierIdComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la récupération des données des Fournisseurs.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}");
                }
            }
        }

        public async Task LoadProductsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://localhost:7101/api/Product";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<Product> products = JsonConvert.DeserializeObject<List<Product>>(jsonResponse);

                        ProductComboBox.ItemsSource = products;
                        ProductComboBox.DisplayMemberPath = "ProName";
                        ProductComboBox.SelectedValuePath = "ProId";
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la récupération des produits.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}");
                }
            }
        }

        private async Task CreateAdress()
        {
            using (HttpClient client = new HttpClient())
            {
                if (DeliveryStreetTextBox.Text != "" &&
                DeliveryCityTextBox.Text != "" &&
                DeliveryZipCodeTextBox.Text != "" &&
                DeliveryCountryTextBox.Text != "" &&
                BillingStreetTextBox.Text != "" &&
                BillingCityTextBox.Text != "" &&
                BillingZipCodeTextBox.Text != "" &&
                BillingCountryTextBox.Text != "")
                {
                    string request = "https://localhost:7101/api/Address";
                    var address = new AddressViewModel
                    {
                        AddDeliveryStreet = DeliveryStreetTextBox.Text,
                        AddDeliveryCity = DeliveryCityTextBox.Text,
                        AddDeliveryZipCode = DeliveryZipCodeTextBox.Text,
                        AddDeliveryCountry = DeliveryCountryTextBox.Text,
                        AddBillingStreet = BillingStreetTextBox.Text,
                        AddBillingCity = BillingCityTextBox.Text,
                        AddBillingZipCode = BillingZipCodeTextBox.Text,
                        AddBillingCountry = BillingCountryTextBox.Text
                    };
                    var rnr = JsonContent.Create(address);

                    HttpResponseMessage response = await client.PostAsync(request, rnr);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    NegoSoftShared.Models.Entities.Address createdAdress = JsonConvert.DeserializeObject<NegoSoftShared.Models.Entities.Address>(responseBody);
                    _addressId = createdAdress.AddId;
                }
                else
                {
                    MessageBox.Show("Champs adresse manquant");
                }
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (_orderDetails.Count == 0)
            {
                MessageBox.Show("Veuillez ajouter au moins une ligne de commande.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SupplierIdComboBox.SelectedItem == null || BillingStreetTextBox == null
                || DeliveryStreetTextBox == null || BillingCountryTextBox == null
                || DeliveryCountryTextBox == null || BillingCityTextBox == null
                || DeliveryCityTextBox == null || BillingZipCodeTextBox == null
                || DeliveryZipCodeTextBox == null)
            {
                MessageBox.Show("Veuillez sélectionner un fournisseur et vos informations de livraison et facturation", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                await CreateAdress();

                var newOrder = new SupplierOrderViewModel
                {
                    SoSupplierId = (Guid)SupplierIdComboBox.SelectedValue,
                    SoAddressId = _addressId,
                    SoState = OrderStateTextBox.Text,
                    SoTotal = float.Parse(OrderTotalTextBox.Text),
                    SoDate = DateTime.Now
                };

                var response = await _httpClient.PostAsJsonAsync("https://localhost:7101/api/SupplierOrder", newOrder);
                if (response.IsSuccessStatusCode)
                {
                    var createdOrder = JsonConvert.DeserializeObject<SupplierOrder>(await response.Content.ReadAsStringAsync());
                    foreach (var detail in _orderDetails)
                    {
                        detail.SodOrderId = createdOrder.SoId;
                        await _httpClient.PostAsJsonAsync("https://localhost:7101/api/SupplierOrderDetails", detail);
                    }
                    MessageBox.Show("Commande créée avec succès", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la création de la commande", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SupplierIdSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SupplierIdComboBox.SelectedItem is Supplier selectedSupplier)
            {
                SupplierNameTextBlock.Text = selectedSupplier.SupName;
                SupplierEmailTextBlock.Text = selectedSupplier.SupEmail;
                SupplierPhoneTextBlock.Text = selectedSupplier.SupPhone;
            }
        }

        private void AddOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            if (ProductComboBox.SelectedItem is Product selectedProduct &&
                int.TryParse(QuantityTextBox.Text, out int quantity) &&
                float.TryParse(PriceTextBox.Text, out float price))
            {
                var orderDetail = new SupplierOrderDetailsViewModel
                {
                    SodProductId = selectedProduct.ProId,
                    SodQuantity = quantity,
                    SodPrice = price
                };

                _orderDetails.Add(orderDetail);
                OrderDetailsDataGrid.ItemsSource = null;
                OrderDetailsDataGrid.ItemsSource = _orderDetails;
            }
            else
            {
                MessageBox.Show("Veuillez vérifier les détails du produit.");
            }
        }
    }
}
