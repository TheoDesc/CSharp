using System.Windows;


namespace WPF.Windows
{
    public partial class AdministrationWindow : Window
    {

        public AdministrationWindow()
        {
            InitializeComponent();
        }



        private void OuvrirSalaries(
            object sender,
            RoutedEventArgs e)
        {
            SalarieWindow fenetre = new SalarieWindow();
            fenetre.Show();

            // Prochaine étape :
            // ouverture SalarieWindow
        }



        private void OuvrirSites(
            object sender,
            RoutedEventArgs e)
        {
            SiteWindow fenetre =
    new SiteWindow();

            fenetre.Show();

            // Prochaine étape :
            // ouverture SiteWindow
        }



        private void OuvrirServices(
            object sender,
            RoutedEventArgs e)
        {
            ServiceWindow fenetre =
     new ServiceWindow();

            fenetre.Show();

            // Prochaine étape :
            // ouverture ServiceWindow
        }



        private void Fermer(
            object sender,
            RoutedEventArgs e)
        {
            Close();
        }

    }
}