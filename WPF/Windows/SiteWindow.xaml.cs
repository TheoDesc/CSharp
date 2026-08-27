using System;
using System.Windows;
using DataEF;
using Microsoft.EntityFrameworkCore;

namespace WPF.Windows
{
    public partial class SiteWindow : Window
    {
        private AnnuaireContext ctx;
        private int idSelectionne = 0;

        public SiteWindow()
        {
            InitializeComponent();

            var options =
                new DbContextOptionsBuilder<AnnuaireContext>()
                .UseSqlite("Data Source=annuaire.db")
                .Options;

            ctx = new AnnuaireContext(options);

            ChargerSites();
        }

        private void ChargerSites()
        {
            dgSites.ItemsSource = DbEF.SiteLireTous(ctx);
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
                DbEF.SiteAjouter(ctx, ville);

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
                    "Impossible d'ajouter le site.\n\n" + ex.Message,
                    "Erreur",
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
                DbEF.SiteModifier(
                    ctx,
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
                    "Impossible de modifier le site.\n\n" + ex.Message,
                    "Erreur",
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
                DbEF.SiteSupprimer(
                    ctx,
                    idSelectionne);

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
            Site site =
                dgSites.SelectedItem as Site;

            if (site == null)
                return;

            idSelectionne = site.Id;

            txtVille.Text = site.Ville;
        }

        protected override void OnClosed(EventArgs e)
        {
            ctx.Dispose();

            base.OnClosed(e);
        }
    }
}