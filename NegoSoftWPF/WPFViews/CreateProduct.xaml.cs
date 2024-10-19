using Microsoft.Win32;
using NegoSoftShared.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Net;
using System.Xml.Linq;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Net.Http.Headers;
using NegoSoftWeb.Models.ViewModels;
using NegoSoftShared.Models.ViewModels;

namespace NegoSoftWPF.WPFViews
{
    /// <summary>
    /// Logique d'interaction pour CreateProduct.xaml
    /// </summary>
    public partial class CreateProduct : Window
    {
        //attributes
        private string _selectedPicPath;
        private static readonly HttpClient client = new HttpClient();
        public CreateProduct()
        {
            InitializeComponent();
            LoadSuppliersAsync();
            LoadTypesAsync();

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
                        suppliersBox.ItemsSource = suppliers;
                        suppliersBox.DisplayMemberPath = "SupName";
                        suppliersBox.SelectedValuePath = "SupId";
                        suppliersBox.SelectedIndex = 0;
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
                        typeBox.ItemsSource = types;
                        typeBox.DisplayMemberPath = "TypLibelle";
                        typeBox.SelectedValuePath = "TypId";
                        typeBox.SelectedIndex = 0;
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
        

        private void BrowseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                _selectedPicPath = openFileDialog.FileName;
                //txtSelectedImage.Text = System.IO.Path.GetFileName(_selectedPicPath);
            }
        }

        private async void CreateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameBox.Text) 
                || string.IsNullOrEmpty(DescriptionBox.Text) 
                || string.IsNullOrEmpty(PriceBox.Text) 
                || string.IsNullOrEmpty(StockBox.Text) 
                || string.IsNullOrEmpty(YearBox.Text)
                || string.IsNullOrEmpty(BoxPriceBox.Text))

            {
                MessageBox.Show("Veuillez remplir tous les champs.");
            }
            else
            {
                var productViewModel = new ProductAddViewModel
                {
                    ProId = Guid.NewGuid(),
                    ProName = NameBox.Text,
                    ProDescription = DescriptionBox.Text,
                    ProSupplierId = (Guid)suppliersBox.SelectedValue,
                    ProPrice = float.Parse(PriceBox.Text),
                    ProBoxPrice = float.TryParse(BoxPriceBox.Text, out var boxPrice) ? (float?)boxPrice : null,
                    ProTypeId = (Guid)typeBox.SelectedValue,
                    ProStock = int.Parse(StockBox.Text),
                    ProYear = int.Parse(YearBox.Text),
                    ProAlcoholVolume = float.Parse(AlcoholVolumeBox.Text)
                };

                string imagePath = _selectedPicPath;

                if (System.IO.File.Exists(imagePath))
                {
                    await UploadProductAsync(productViewModel, imagePath);
                }
                else
                {
                    MessageBox.Show("Image non trouvée.");
                }
            }
                
        }

        private async Task UploadProductAsync(ProductAddViewModel productViewModel, string imagePath)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7101/"); 

                using (var content = new MultipartFormDataContent())
                {
                    var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                    var streamContent = new StreamContent(fileStream);
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    content.Add(streamContent, "image", System.IO.Path.GetFileName(imagePath));

                    content.Add(new StringContent(productViewModel.ProName), "ProName");
                    content.Add(new StringContent(productViewModel.ProDescription ?? ""), "ProDescription");
                    content.Add(new StringContent(productViewModel.ProSupplierId.ToString()), "ProSupplierId");
                    content.Add(new StringContent(productViewModel.ProPrice.ToString()), "ProPrice");
                    content.Add(new StringContent(productViewModel.ProBoxPrice?.ToString() ?? ""), "ProBoxPrice");
                    content.Add(new StringContent(productViewModel.ProTypeId.ToString()), "ProTypeId");
                    content.Add(new StringContent(productViewModel.ProStock.ToString()), "ProStock");
                    content.Add(new StringContent(productViewModel.ProYear.ToString()), "ProYear");
                    content.Add(new StringContent(productViewModel.ProAlcoholVolume.ToString()), "ProAlcoholVolume");

                    var response = await client.PostAsync("api/product", content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Produit crée avec succès");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"Erreur lors de la création du produit : {response.ReasonPhrase}");
                        this.Close();
                    }
                }
            }
        }


        private void DecimalTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextDecimal((sender as TextBox).Text + e.Text);
        }

        private bool IsTextDecimal(string text)
        {
            return decimal.TryParse(text, out _);
        }
        private void NumericTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));

                if (!IsTextDecimal(pastedText))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
