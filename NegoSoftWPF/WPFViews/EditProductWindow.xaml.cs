using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using Newtonsoft.Json;
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

namespace NegoSoftWPF.WPFViews
{
    public partial class EditProductWindow : Window
    {
        private readonly Guid _productId;
        private readonly HttpClient _httpClient;

        public EditProductWindow(Guid productId)
        {
            InitializeComponent();
            _productId = productId;
            _httpClient = new HttpClient();
            LoadProductData();
        }

        private async void LoadProductData()
        {
            try
            {
                // Appel à l'API pour obtenir les données du produit à éditer
                var product = await _httpClient.GetFromJsonAsync<ProductEditViewModel>($"https://localhost:7101/api/Product/{_productId}");
                await LoadSuppliersAsync();
                await LoadTypesAsync();

                if (product != null)
                {
                    txtProductName.Text = product.ProName;
                    txtProductDescription.Text = product.ProDescription;
                    txtPrice.Text = product.ProPrice.ToString();
                    txtStock.Text = product.ProStock.ToString();
                    cmbSupplierId.SelectedValue = product.ProSupplierId;
                    cmbTypeId.SelectedValue = product.ProTypeId;
                    txtBoxPrice.Text = product.ProBoxPrice.ToString();
                    txtYear.Text = product.ProYear.ToString();
                    txtAlcoholVolume.Text = product.ProAlcoholVolume.ToString();
                    chkIsActive.IsChecked = product.ProIsActive;
                }
                else
                {
                    MessageBox.Show("Product not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Création du modèle à partir des champs de la fenêtre
                var updatedProduct = new ProductEditViewModel
                {
                    ProName = txtProductName.Text,
                    ProDescription = txtProductDescription.Text,
                    ProPrice = float.Parse(txtPrice.Text),
                    ProStock = int.Parse(txtStock.Text),
                    ProSupplierId = (Guid)cmbSupplierId.SelectedValue,
                    ProTypeId = (Guid)cmbTypeId.SelectedValue,
                    ProBoxPrice = float.Parse(txtBoxPrice.Text),
                    ProYear = int.Parse(txtYear.Text),
                    ProAlcoholVolume = float.Parse(txtAlcoholVolume.Text),
                    ProIsActive = chkIsActive.IsChecked ?? false
                };

                // Appel PUT vers l'API pour mettre à jour le produit
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7101/api/Product/{_productId}", updatedProduct);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Product updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("Failed to update product", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                        cmbSupplierId.ItemsSource = suppliers;
                        cmbSupplierId.DisplayMemberPath = "SupName";
                        cmbSupplierId.SelectedValuePath = "SupId";
                        cmbSupplierId.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la récupération des données des fournisseurs.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}");
                }
            }
        }
        public async Task LoadTypesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://localhost:7101/api/Type";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<NegoSoftShared.Models.Entities.Type> types = JsonConvert.DeserializeObject<List<NegoSoftShared.Models.Entities.Type>>(jsonResponse);
                        cmbTypeId.ItemsSource = types;
                        cmbTypeId.DisplayMemberPath = "TypLibelle";
                        cmbTypeId.SelectedValuePath = "TypId";
                        cmbTypeId.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la récupération des données des types.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}");
                }
            }
        }
    }
}
