using System;
using System.Windows;
using Data;

namespace WPF.Windows
{
    public partial class ServiceWindow : Window
    {
        private int idSelectionne = 0;

        public ServiceWindow()
        {
            InitializeComponent();

            ChargerServices();
        }

        private void ChargerServices()
        {
            try
            {
                dgServices.ItemsSource =
                    DB.ServiceLireTous();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Impossible de charger les services",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Ajouter(
            object sender,
            RoutedEventArgs e)
        {
            string nom = txtNom.Text.Trim();

            if (string.IsNullOrWhiteSpace(nom))
            {
                MessageBox.Show(
                    "Impossible d'ajouter un service vide.",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            try
            {
                DB.ServiceAjouter(nom);

                ChargerServices();

                txtNom.Text = "";
                idSelectionne = 0;

                MessageBox.Show(
                    "Service ajouté.",
                    "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Impossible d'ajouter le service",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Modifier(
            object sender,
            RoutedEventArgs e)
        {
            if (idSelectionne == 0)
            {
                MessageBox.Show(
                    "Sélectionnez un service.",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            string nom = txtNom.Text.Trim();

            if (string.IsNullOrWhiteSpace(nom))
            {
                MessageBox.Show(
                    "Impossible de modifier le service avec un nom vide.",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            try
            {
                DB.ServiceModifier(
                    idSelectionne,
                    nom);

                ChargerServices();

                MessageBox.Show(
                    "Service modifié.",
                    "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Impossible de modifier le service",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Supprimer(
            object sender,
            RoutedEventArgs e)
        {
            if (idSelectionne == 0)
            {
                MessageBox.Show(
                    "Sélectionnez un service.",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            MessageBoxResult resultat = MessageBox.Show(
                "Voulez-vous vraiment supprimer ce service ?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultat != MessageBoxResult.Yes)
                return;

            try
            {
                DB.ServiceSupprimer(idSelectionne);

                ChargerServices();

                txtNom.Text = "";
                idSelectionne = 0;

                MessageBox.Show(
                    "Service supprimé.",
                    "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Impossible de supprimer ce service.\n\n" +
                    "Il est peut-être utilisé par un salarié.\n\n" +
                    ex.Message,
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void SelectionChange(
            object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Data.Service service =
                dgServices.SelectedItem as Data.Service;

            if (service == null)
                return;

            idSelectionne = service.Id;

            txtNom.Text = service.Nom;
        }
    }
}