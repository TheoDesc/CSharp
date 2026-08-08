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
            dgSites.ItemsSource =
                DbEF.SiteLireTous(ctx);
        }





        private void Ajouter(
            object sender,
            RoutedEventArgs e)
        {

            DbEF.SiteAjouter(
                ctx,
                txtVille.Text
            );


            ChargerSites();


            txtVille.Text = "";


            MessageBox.Show(
                "Site ajouté"
            );
        }





        private void Modifier(
            object sender,
            RoutedEventArgs e)
        {

            if (idSelectionne == 0)
            {
                MessageBox.Show(
                    "Sélectionnez un site"
                );

                return;
            }



            DbEF.SiteModifier(
                ctx,
                idSelectionne,
                txtVille.Text
            );


            ChargerSites();


            MessageBox.Show(
                "Site modifié"
            );
        }





        private void Supprimer(
            object sender,
            RoutedEventArgs e)
        {

            if (idSelectionne == 0)
            {
                MessageBox.Show(
                    "Sélectionnez un site"
                );

                return;
            }



            DbEF.SiteSupprimer(
                ctx,
                idSelectionne
            );


            ChargerSites();


            txtVille.Text = "";

            idSelectionne = 0;


            MessageBox.Show(
                "Site supprimé"
            );
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


            txtVille.Text =
                site.Ville;

        }





        protected override void OnClosed(EventArgs e)
        {
            ctx.Dispose();

            base.OnClosed(e);
        }

    }
}