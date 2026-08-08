using System;
using System.IO;
using System.Windows;
using DataEF;
using Microsoft.EntityFrameworkCore;
using PdfGen;


namespace WPF.Windows
{
    public partial class FicheSalarieWindow : Window
    {
        private Salarie salarie;

        // =========================
        // CONSTRUCTEUR
        // =========================

        public FicheSalarieWindow(
            //int idSalarie
            Salarie s)
        {
            InitializeComponent();

            this.salarie = s;

            AfficherSalarie();

            btnPdf.Click += GenererPdf;
        }

        // =========================
        // AFFICHAGE
        // =========================

        private void AfficherSalarie()
        {
            txtNom.Text = "Nom : " + salarie.Nom;

            txtPrenom.Text = "Prénom : " + salarie.Prenom;

            txtTel.Text = "Téléphone : " + salarie.TelFixe;

            txtPortable.Text = "Portable : " + salarie.TelPortable;

            txtEmail.Text = "Email : " + salarie.Email;

            txtService.Text = "Service : " + salarie.Service.Nom;

            txtSite.Text = "Site : " + salarie.Site.Ville;
        }


        // =========================
        // CREATION PDF
        // =========================

        private void GenererPdf(
            object sender,
            RoutedEventArgs e)
        {

            string dossier = @"C:\Users\theod\source\repos\Annuaire\pdf";

            if (!Directory.Exists(dossier))
            {
                Directory.CreateDirectory(dossier);
            }

            string chemin = $@"{dossier}\FicheSalarie_{salarie.Id}.pdf";

            PdfService.GenererPdf(
                chemin,
                salarie.Nom,
                salarie.Prenom,
                salarie.TelFixe,
                salarie.TelPortable,
                salarie.Email,
                salarie.Service.Nom,
                salarie.Site.Ville
            );

            MessageBox.Show( "PDF créé !" );
        }

        // =========================
        // FERMETURE
        // =========================
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }
    }
}
