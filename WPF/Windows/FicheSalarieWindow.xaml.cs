using System.Windows;
using Data;
using PdfGen;


namespace WPF.Windows
{
    public partial class FicheSalarieWindow : Window
    {

        private Salarie salarie;


        public FicheSalarieWindow(Salarie s)
        {
            InitializeComponent();

            salarie = s;


            AfficherSalarie();


            btnPdf.Click += GenererPdf;
        }



        private void AfficherSalarie()
        {
            txtNom.Text = "Nom : " + salarie.Nom;

            txtPrenom.Text = "Prénom : " + salarie.Prenom;

            txtTel.Text = "Téléphone : " + salarie.TelFixe;

            txtPortable.Text = "Portable : " + salarie.TelPortable;

            txtEmail.Text = "Email : " + salarie.Email;

            txtService.Text = "Service : " + salarie.Service;

            txtSite.Text = "Site : " + salarie.Site;
        }



        private void GenererPdf(object sender, RoutedEventArgs e)
        {
            string chemin =
                $@"C:\Users\theod\source\repos\Annuaire\FicheSalarie_{salarie.Id}.pdf";


            PdfService.GenererPdf(
                chemin,
                salarie.Nom,
                salarie.Prenom,
                salarie.TelFixe,
                salarie.TelPortable,
                salarie.Email,
                salarie.Service,
                salarie.Site
            );


            MessageBox.Show("PDF créé !");
        }
    }
}