using System;
using System.Windows;
using Data;

namespace WPF.Windows
{
    public partial class SiteWindow : Window
    {
        private int idSelectionne = 0;

        public SiteWindow()
        {
            InitializeComponent();

            ChargerSites();
        }

        private void ChargerSites()
        {
            try
            {
                dgSites.ItemsSource = DB.SiteLireTous();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Impossible de charger les sites",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Ajouter(object sender, RoutedEventArgs e)
        {
            string ville = txtVille.Text.Trim();

            if (string.IsNullOrWhiteSpace(ville))
            {
                MessageBox.Show(
                    "Impossible d'ajouter un site vide.",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            try
            {
                DB.SiteAjouter(ville);

                ChargerSites();

                txtVille.Text = "";
                idSelectionne = 0;

                MessageBox.Show(
                    "Site ajouté.",
                    "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Impossible d'ajouter le site",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Modifier(object sender, RoutedEventArgs e)
        {
            if (idSelectionne == 0)
            {
                MessageBox.Show(
                    "Sélectionnez un site.",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            string ville = txtVille.Text.Trim();

            if (string.IsNullOrWhiteSpace(ville))
            {
                MessageBox.Show(
                    "Impossible de modifier le site avec un nom vide.",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            try
            {
                DB.SiteModifier(
                    idSelectionne,
                    ville);

                ChargerSites();

                MessageBox.Show(
                    "Site modifié.",
                    "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Impossible de modifier le site",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Supprimer(object sender, RoutedEventArgs e)
        {
            if (idSelectionne == 0)
            {
                MessageBox.Show(
                    "Sélectionnez un site.",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            MessageBoxResult resultat = MessageBox.Show(
                "Voulez-vous vraiment supprimer ce site ?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultat != MessageBoxResult.Yes)
                return;

            try
            {
                DB.SiteSupprimer(idSelectionne);

                ChargerSites();

                txtVille.Text = "";
                idSelectionne = 0;

                MessageBox.Show(
                    "Site supprimé.",
                    "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Impossible de supprimer ce site.\n\n" +
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
            Data.Site site =
                dgSites.SelectedItem as Data.Site;

            if (site == null)
                return;

            idSelectionne = site.Id;

            txtVille.Text = site.Ville;
        }
    }
}