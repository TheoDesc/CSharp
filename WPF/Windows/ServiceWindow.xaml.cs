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

            DbEF.ServiceAjouter(
                ctx,
                txtNom.Text
            );


            ChargerServices();


            txtNom.Text = "";


            MessageBox.Show(
                "Service ajouté"
            );

        }





        private void Modifier(
            object sender,
            RoutedEventArgs e)
        {

            if (idSelectionne == 0)
            {
                MessageBox.Show(
                    "Sélectionnez un service"
                );

                return;
            }



            DbEF.ServiceModifier(
                ctx,
                idSelectionne,
                txtNom.Text
            );


            ChargerServices();


            MessageBox.Show(
                "Service modifié"
            );

        }





        private void Supprimer(
            object sender,
            RoutedEventArgs e)
        {

            if (idSelectionne == 0)
            {
                MessageBox.Show(
                    "Sélectionnez un service"
                );

                return;
            }



            DbEF.ServiceSupprimer(
                ctx,
                idSelectionne
            );


            ChargerServices();


            txtNom.Text = "";

            idSelectionne = 0;


            MessageBox.Show(
                "Service supprimé"
            );

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


            txtNom.Text =
                service.Nom;

        }





        protected override void OnClosed(EventArgs e)
        {
            ctx.Dispose();

            base.OnClosed(e);
        }

    }
}