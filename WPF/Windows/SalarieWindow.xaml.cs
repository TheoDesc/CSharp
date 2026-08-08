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
            dgSalaries.ItemsSource =
                DbEF.SalarieLireTous(ctx);
        }





        // =========================
        // CHARGEMENT SERVICES
        // =========================

        private void ChargerServices()
        {
            cbService.ItemsSource =
                DbEF.ServiceLireTous(ctx);

            cbService.DisplayMemberPath = "Nom";

            cbService.SelectedValuePath = "Id";
        }





        // =========================
        // CHARGEMENT SITES
        // =========================

        private void ChargerSites()
        {
            cbSite.ItemsSource =
                DbEF.SiteLireTous(ctx);

            cbSite.DisplayMemberPath = "Ville";

            cbSite.SelectedValuePath = "Id";
        }





        // =========================
        // AJOUTER
        // =========================

        private void Ajouter(
            object sender,
            RoutedEventArgs e)
        {

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
                "Salarié ajouté"
            );

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
                    "Sélectionnez un salarié"
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
                "Salarié modifié"
            );

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
                    "Sélectionnez un salarié"
                );

                return;
            }



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
                "Salarié supprimé"
            );

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





        protected override void OnClosed(EventArgs e)
        {

            ctx.Dispose();

            base.OnClosed(e);

        }

    }
}