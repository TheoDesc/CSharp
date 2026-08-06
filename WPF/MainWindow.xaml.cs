using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Data;
using WPF.Windows;

namespace WPF
{
    public partial class MainWindow : Window
    {
        // Garde la liste complète des salariés
        private List<Salarie> tousLesSalaries;


        public MainWindow()
        {
            InitializeComponent();


            // Chemin de la base SQLite
            DB.NomFichierSQLite = @"C:\Users\theod\source\repos\Annuaire\annuaire.db";


            // Chargement des données au démarrage
            ChargerSites();
            ChargerServices();
            ChargerSalaries();


            // Bouton rechercher
            btnRechercher.Click += Rechercher;
        }



        // =========================
        // CHARGER LES SITES
        // =========================

        private void ChargerSites()
        {
            var sites = DB.SiteLireTous();


            // Ajoute une option sans filtre
            sites.Insert(0, new Site
            {
                Ville = "Tous"
            });


            cbSite.ItemsSource = sites;

            cbSite.DisplayMemberPath = "Ville";

            cbSite.SelectedIndex = 0;
        }



        // =========================
        // CHARGER LES SERVICES
        // =========================

        private void ChargerServices()
        {
            var services = DB.ServiceLireTous();


            // Ajoute une option sans filtre
            services.Insert(0, new Service
            {
                Nom = "Tous"
            });


            cbService.ItemsSource = services;

            cbService.DisplayMemberPath = "Nom";

            cbService.SelectedIndex = 0;
        }



        // =========================
        // CHARGER LES SALARIES
        // =========================

        private void ChargerSalaries()
        {
            // Récupère les salariés depuis la base
            tousLesSalaries = DB.SalarieLireTous();


            // Affiche dans le tableau
            dgSalaries.ItemsSource = tousLesSalaries;
        }



        // =========================
        // RECHERCHE
        // =========================

        private void Rechercher(object sender, RoutedEventArgs e)
        {
            var resultat = tousLesSalaries;


            // Filtre par nom ou prénom
            if (txtRecherche.Text != "")
            {
                string recherche = txtRecherche.Text.ToLower();


                resultat = resultat
                    .Where(s =>
                        s.Nom.ToLower().Contains(recherche)
                        ||
                        s.Prenom.ToLower().Contains(recherche))
                    .ToList();
            }



            // Filtre par site
            if (cbSite.SelectedIndex > 0)
            {
                Site site = cbSite.SelectedItem as Site;


                resultat = resultat
                    .Where(s => s.Site == site.Ville)
                    .ToList();
            }



            // Filtre par service
            if (cbService.SelectedIndex > 0)
            {
                Service service = cbService.SelectedItem as Service;


                resultat = resultat
                    .Where(s => s.Service == service.Nom)
                    .ToList();
            }


            // Affiche les résultats
            dgSalaries.ItemsSource = resultat;
        }



        // =========================
        // OUVRIR LA FICHE SALARIE
        // =========================

        private void dgSalaries_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Récupère la ligne sélectionnée
            Salarie salarie = dgSalaries.SelectedItem as Salarie;


            // Vérifie qu'un salarié est sélectionné
            if (salarie != null)
            {
                // Ouvre la fenêtre fiche salarié
                FicheSalarieWindow fiche = new FicheSalarieWindow(salarie);

                fiche.Show();
            }
        }

        private void dgSalaries_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}