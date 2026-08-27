using System;
using System.Windows;
using DataEF;
using Microsoft.EntityFrameworkCore;
using WPF.Services;

namespace WPF.Windows
{
    public partial class SalarieWindow : Window
    {
        private AnnuaireContext ctx;

        private int idSelectionne = 0;


        public SalarieWindow()
        {
            InitializeComponent();

            var options =
                new DbContextOptionsBuilder<AnnuaireContext>()
                .UseSqlite("Data Source=annuaire.db")
                .Options;

            ctx = new AnnuaireContext(options);

            ChargerServices();
            ChargerSites();
            ChargerSalaries();
        }


        // =========================
        // CHARGEMENT SALARIES
        // =========================

        private void ChargerSalaries()
        {
            try
            {
                dgSalaries.ItemsSource =
                    DbEF.SalarieLireTous(ctx);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Impossible de charger les salariés",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }


        // =========================
        // CHARGEMENT SERVICES
        // =========================

        private void ChargerServices()
        {
            try
            {
                cbService.ItemsSource =
                    DbEF.ServiceLireTous(ctx);

                cbService.DisplayMemberPath = "Nom";
                cbService.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Impossible de charger les services",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }


        // =========================
        // CHARGEMENT SITES
        // =========================

        private void ChargerSites()
        {
            try
            {
                cbSite.ItemsSource =
                    DbEF.SiteLireTous(ctx);

                cbSite.DisplayMemberPath = "Ville";
                cbSite.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Impossible de charger les sites",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }


        // =========================
        // AJOUTER
        // =========================

        private void Ajouter(
            object sender,
            RoutedEventArgs e)
        {
            try
            {
                // Vérification du site
                if (cbSite.SelectedValue == null)
                {
                    MessageBox.Show(
                        "Vous devez sélectionner un site.",
                        "Ajout impossible",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );

                    return;
                }


                // Vérification du service
                if (cbService.SelectedValue == null)
                {
                    MessageBox.Show(
                        "Vous devez sélectionner un service.",
                        "Ajout impossible",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );

                    return;
                }


                Salarie salarie = new Salarie
                {
                    Nom = txtNom.Text,
                    Prenom = txtPrenom.Text,
                    TelFixe = txtTel.Text,
                    TelPortable = txtPortable.Text,
                    Email = txtEmail.Text,

                    ServiceId =
                        (int)cbService.SelectedValue,

                    SiteId =
                        (int)cbSite.SelectedValue
                };


                DbEF.SalarieAjouter(
                    ctx,
                    salarie
                );


                LogService.EcrireLog(
                    "Ajout salarié : "
                    + salarie.Nom
                    + " "
                    + salarie.Prenom
                );


                ChargerSalaries();

                ViderFormulaire();


                MessageBox.Show(
                    "Salarié ajouté.",
                    "Succès",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Impossible d'ajouter le salarié",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }
        }


        // =========================
        // MODIFIER
        // =========================

        private void Modifier(
            object sender,
            RoutedEventArgs e)
        {
            if (idSelectionne == 0)
            {
                MessageBox.Show(
                    "Sélectionnez un salarié.",
                    "Modification impossible",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );

                return;
            }


            try
            {
                // Vérification du site
                if (cbSite.SelectedValue == null)
                {
                    MessageBox.Show(
                        "Vous devez sélectionner un site.",
                        "Modification impossible",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );

                    return;
                }


                // Vérification du service
                if (cbService.SelectedValue == null)
                {
                    MessageBox.Show(
                        "Vous devez sélectionner un service.",
                        "Modification impossible",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );

                    return;
                }


                Salarie salarie = new Salarie
                {
                    Nom = txtNom.Text,
                    Prenom = txtPrenom.Text,
                    TelFixe = txtTel.Text,
                    TelPortable = txtPortable.Text,
                    Email = txtEmail.Text,

                    ServiceId =
                        (int)cbService.SelectedValue,

                    SiteId =
                        (int)cbSite.SelectedValue
                };


                DbEF.SalarieModifier(
                    ctx,
                    idSelectionne,
                    salarie
                );


                LogService.EcrireLog(
                    "Modification salarié : "
                    + salarie.Nom
                    + " "
                    + salarie.Prenom
                );


                ChargerSalaries();


                MessageBox.Show(
                    "Salarié modifié.",
                    "Succès",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Impossible de modifier le salarié",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }
        }


        // =========================
        // SUPPRIMER
        // =========================

        private void Supprimer(
            object sender,
            RoutedEventArgs e)
        {
            if (idSelectionne == 0)
            {
                MessageBox.Show(
                    "Sélectionnez un salarié.",
                    "Suppression impossible",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );

                return;
            }


            // Confirmation avant suppression
            MessageBoxResult resultat =
                MessageBox.Show(
                    "Voulez-vous vraiment supprimer ce salarié ?",
                    "Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );


            if (resultat != MessageBoxResult.Yes)
                return;


            try
            {
                DbEF.SalarieSupprimer(
                    ctx,
                    idSelectionne
                );


                LogService.EcrireLog(
                    "Suppression salarié ID : "
                    + idSelectionne
                );


                ChargerSalaries();

                ViderFormulaire();


                MessageBox.Show(
                    "Salarié supprimé.",
                    "Succès",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Impossible de supprimer le salarié",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }
        }


        // =========================
        // SELECTION SALARIE
        // =========================

        private void SelectionChange(
            object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Salarie salarie =
                dgSalaries.SelectedItem as Salarie;


            if (salarie == null)
                return;


            idSelectionne =
                salarie.Id;


            txtNom.Text =
                salarie.Nom;


            txtPrenom.Text =
                salarie.Prenom;


            txtTel.Text =
                salarie.TelFixe;


            txtPortable.Text =
                salarie.TelPortable;


            txtEmail.Text =
                salarie.Email;


            cbService.SelectedValue =
                salarie.ServiceId;


            cbSite.SelectedValue =
                salarie.SiteId;
        }


        // =========================
        // VIDER FORMULAIRE
        // =========================

        private void ViderFormulaire()
        {
            idSelectionne = 0;

            txtNom.Text = "";

            txtPrenom.Text = "";

            txtTel.Text = "";

            txtPortable.Text = "";

            txtEmail.Text = "";

            cbService.SelectedIndex = -1;

            cbSite.SelectedIndex = -1;
        }


        // =========================
        // FERMETURE
        // =========================

        protected override void OnClosed(EventArgs e)
        {
            ctx.Dispose();

            base.OnClosed(e);
        }
    }
}