using NegoSoftShared.Models.ViewModels;
using NegoSoftWeb.Models.Entities;
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
    /// <summary>
    /// Logique d'interaction pour EditCustomerWindow.xaml
    /// </summary>
    public partial class EditTypeWindow : Window
    {
        private readonly Guid _typeId;
        private readonly HttpClient _httpClient;

        public EditTypeWindow(Guid typeId)
        {
            InitializeComponent();
            _typeId = typeId;
            _httpClient = new HttpClient();
            LoadTypeData();
        }

        private async void LoadTypeData()
        {
            try
            {
                // Appel à l'API pour obtenir les données du type à éditer
                var type = await _httpClient.GetFromJsonAsync<TypeViewModel>($"https://localhost:7101/api/Type/{_typeId}");

                if (type != null)
                {
                    TypeLibelleTextBox.Text = type.TypLibelle;
                }
                else
                {
                    MessageBox.Show("Type not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Type data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Création du modèle à partir des champs de la fenêtre
                var updatedType = new TypeViewModel
                {
                    TypLibelle = TypeLibelleTextBox.Text
                };

                // Appel PUT vers l'API pour mettre à jour le type
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7101/api/Type/{_typeId}", updatedType);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Famille produit mis à jour avec succès", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Echec de la modification", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating type: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
