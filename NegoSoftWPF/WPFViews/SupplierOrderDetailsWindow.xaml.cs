using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using NegoSoftShared.Models.ViewModels;
using NegoSoftShared.Models.Entities;

namespace NegoSoftWPF.WPFViews
{
    public partial class SupplierOrderDetailsWindow : Window
    {
        private readonly Guid _orderId;
        private readonly HttpClient _httpClient;

        public SupplierOrderDetailsWindow(Guid orderId)
        {
            InitializeComponent();
            _orderId = orderId;
            _httpClient = new HttpClient();
            LoadOrderData();
        }

        private async void LoadOrderData()
        {
            try
            {
                // Charger les détails de la commande
                string apiUrl = $"https://localhost:7101/api/SupplierOrder/{_orderId}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                string jsonResponse = await response.Content.ReadAsStringAsync();
                SupplierOrder order = JsonConvert.DeserializeObject<SupplierOrder>(jsonResponse);
                await LoadOrderDetailsAsync();

                if (order != null)
                {
                    SupplierNameTextBlock.Text = order.Supplier.SupName;
                    SupplierEmailTextBlock.Text = order.Supplier.SupEmail;
                    SupplierPhoneTextBlock.Text = order.Supplier.SupPhone;

                    string deliveryAddress = $"{order.Address.AddDeliveryStreet}, {order.Address.AddDeliveryCity}, {order.Address.AddDeliveryZipCode}, {order.Address.AddDeliveryCountry}";
                    DeliveryAddressTextBlock.Text = deliveryAddress;

                    string billingAddress = $"{order.Address.AddBillingStreet}, {order.Address.AddBillingCity}, {order.Address.AddBillingZipCode}, {order.Address.AddBillingCountry}";
                    BillingAddressTextBlock.Text = billingAddress;

                    OrderStateTextBlock.Text = order.SoState;
                    OrderDateTextBlock.Text = order.SoDate.ToString();
                    OrderTotalTextBlock.Text = order.SoTotal.ToString();
                }
                else
                {
                    MessageBox.Show("Commande introuvable", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des données: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task LoadOrderDetailsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"https://localhost:7101/api/SupplierOrderDetails/supplierorder/{_orderId}";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<SupplierOrderDetails> orderDetails = JsonConvert.DeserializeObject<List<SupplierOrderDetails>>(jsonResponse);
                        OrderDetailsDataGrid.ItemsSource = orderDetails;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la récupération des détails de la commande.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}");
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
