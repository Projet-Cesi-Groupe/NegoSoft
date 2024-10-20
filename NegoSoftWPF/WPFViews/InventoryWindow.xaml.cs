using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using NegoSoftShared.Models.ViewModels;
using NegoSoftShared.Models.Entities;

namespace NegoSoftWPF.WPFViews
{
    public partial class InventoryWindow : Window
    {
        private List<ProductEditViewModelWithID> _products;
        private string apiUrl;
        private HttpClient _httpClient = new HttpClient();

        public InventoryWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        public class ProductEditViewModelWithID : ProductEditViewModel
        {
            public Guid ProId { get; set; }
            public int NewQuantity { get; set; }
        }

        private async void LoadProducts()
        {
            try
            {
                apiUrl = "https://localhost:7101/api/product";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<ProductEditViewModelWithID> data = JsonConvert.DeserializeObject<List<ProductEditViewModelWithID>>(jsonResponse);
                        ProductDataGrid.ItemsSource = data;
                        _products = data;
                    }
                    else
                    {
                        MessageBox.Show("Le chargement des produits a rencontré un problème.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void RegularizeStockButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var product in _products)
                {
                    if (product.ProStock != product.NewQuantity)
                    {
                        ProductEditViewModel newProduct = new ProductEditViewModel
                        {
                            ProName = product.ProName,
                            ProDescription = product.ProDescription,
                            ProPrice = product.ProPrice,
                            ProStock = product.NewQuantity,
                            ProTypeId = product.ProTypeId,
                            ProSupplierId = product.ProSupplierId,
                            ProBoxPrice = product.ProBoxPrice,
                            ProIsActive = product.ProIsActive,
                            ProYear = product.ProYear,
                            ProAlcoholVolume = product.ProAlcoholVolume
                        };

                        product.ProStock = product.NewQuantity;

                        var response = await _httpClient.PutAsJsonAsync($"https://localhost:7101/api/Product/{product.ProId}", newProduct);
                        if (!response.IsSuccessStatusCode)
                        {
                            MessageBox.Show($"Erreur lors de la mise à jour du produit {product.ProName}.");
                        }
                    }
                }

                MessageBox.Show("Le stock a été régularisé.");
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue : " + ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var product in _products)
                {
                    product.NewQuantity = 0;
                }

                ProductDataGrid.ItemsSource = null;
                ProductDataGrid.ItemsSource = _products;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue : " + ex.Message);
            }
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Vérifie si l'entrée est un chiffre
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true; // Empêche l'entrée si ce n'est pas un chiffre
                MessageBox.Show("Veuillez entrer uniquement des chiffres positifs");
            }
        }

    }
}
