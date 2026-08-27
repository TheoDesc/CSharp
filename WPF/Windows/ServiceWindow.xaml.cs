using System;
using System.Windows;
using DataEF;
using Microsoft.EntityFrameworkCore;

namespace WPF.Windows
{
    public partial class ServiceWindow : Window
    {
        private AnnuaireContext ctx;
        private int idSelectionne = 0;

        public ServiceWindow()
        {
            InitializeComponent();

            var options =
                new DbContextOptionsBuilder<AnnuaireContext>()
                .UseSqlite("Data Source=annuaire.db")
                .Options;

            ctx = new AnnuaireContext(options);

            ChargerServices();
        }

        private void ChargerServices()
        {
            dgServices.ItemsSource =
                DbEF.ServiceLireTous(ctx);
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
                DbEF.ServiceAjouter(
                    ctx,
                    nom);

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
                    "Impossible d'ajouter le service.\n\n" +
                    ex.Message,
                    "Erreur",
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
                DbEF.ServiceModifier(
                    ctx,
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
                    "Impossible de modifier le service.\n\n" +
                    ex.Message,
                    "Erreur",
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
                DbEF.ServiceSupprimer(
                    ctx,
                    idSelectionne);

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
            Service service =
                dgServices.SelectedItem as Service;

            if (service == null)
                return;

            idSelectionne = service.Id;

            txtNom.Text = service.Nom;
        }

        protected override void OnClosed(EventArgs e)
        {
            ctx.Dispose();

            base.OnClosed(e);
        }
    }
}