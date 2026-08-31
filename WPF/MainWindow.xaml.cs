using System.Linq;
using System.Windows;
using System.Windows.Input;
using DataEF;
using Microsoft.EntityFrameworkCore;


namespace WPF
{

    public partial class MainWindow : Window
    {

        private AnnuaireContext ctx;



        public MainWindow()
        {
            InitializeComponent();

            string NomFichierSQLite = @"C:\Users\theod\source\repos\CSharp\annuaire.db";

            // initialisation Data
            Data.DB.NomFichierSQLite = NomFichierSQLite;

            var options =
                new DbContextOptionsBuilder<AnnuaireContext>()
                .UseSqlite(
                    "Data Source=" + NomFichierSQLite)
                .Options;

            ctx = new AnnuaireContext(options);

            ChargerSites();

            ChargerServices();
        }





        // =========================
        // SITES
        // =========================

        private void ChargerSites()
        {

            var sites =
                DbEF.SiteLireTous(ctx);


            sites.Insert(
                0,
                new Site
                {
                    Id = 0,
                    Ville = "Tous"
                });


            cbSite.ItemsSource = sites;

            cbSite.DisplayMemberPath = "Ville";

            cbSite.SelectedValuePath = "Id";

            cbSite.SelectedIndex = 0;

        }





        // =========================
        // SERVICES
        // =========================

        private void ChargerServices()
        {

            var services =
                DbEF.ServiceLireTous(ctx);



            services.Insert(
                0,
                new Service
                {
                    Id = 0,
                    Nom = "Tous"
                });


            cbService.ItemsSource = services;

            cbService.DisplayMemberPath = "Nom";

            cbService.SelectedValuePath = "Id";

            cbService.SelectedIndex = 0;

        }





        // =========================
        // RECHERCHE
        // =========================

        private void Rechercher(
            object sender,
            RoutedEventArgs e)
        {


            int? siteId = null;

            int? serviceId = null;



            if (cbSite.SelectedValue != null &&
               (int)cbSite.SelectedValue != 0)
            {
                siteId =
                    (int)cbSite.SelectedValue;
            }



            if (cbService.SelectedValue != null &&
               (int)cbService.SelectedValue != 0)
            {
                serviceId =
                    (int)cbService.SelectedValue;
            }



            var salaries =
                DbEF.RechercherSalaries(
                    ctx,
                    txtRecherche.Text,
                    siteId,
                    serviceId
                );



            dgSalaries.ItemsSource =
                salaries;



            if (salaries.Count == 0)
            {
                MessageBox.Show(
                    "Aucun salarié trouvé");
            }

        }





        // =========================
        // FICHE SALARIE
        // =========================

        private void OuvrirFiche(
            object sender,
            MouseButtonEventArgs e)
        {

            Salarie salarie =
                dgSalaries.SelectedItem as Salarie;


            if (salarie != null)
            {

                Windows.FicheSalarieWindow fiche =
                    new Windows.FicheSalarieWindow(
                        //salarie.Id
                        salarie
                     );


                fiche.Show();

            }

        }





        // =========================
        // ADMIN
        // =========================

        private void OuvrirAdmin(
            object sender,
            RoutedEventArgs e)
        {

            Windows.LoginWindow login =
                new Windows.LoginWindow();


            if (login.ShowDialog() == true)
            {

                Windows.AdministrationWindow win =
                    new Windows.AdministrationWindow();


                win.Show();

            }

        }



        protected override void OnClosed(
            System.EventArgs e)
        {

            ctx.Dispose();

            base.OnClosed(e);

        }

    }

}