using System.Windows;
using WPF.Services;


namespace WPF.Windows
{
    public partial class LoginWindow : Window
    {


        public LoginWindow()
        {
            InitializeComponent();
        }


        private void ValiderConnexionAdmin()
        {
            if (txtMotDePasse.Password == "admin")
            {
                LogService.EcrireLog( "Connexion administrateur réussie" );
                DialogResult = true;
                Close();
            }
            else
            {
                LogService.EcrireLog( "Echec connexion administrateur" );
                MessageBox.Show( "Mot de passe incorrect" );
            }
        }

        private void Connexion(object sender, RoutedEventArgs e)
        {
            ValiderConnexionAdmin();
        }

        private void txtMotDePasse_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                ValiderConnexionAdmin();
                e.Handled = true;
            }
        }

        private void Annuler(
            object sender,
            RoutedEventArgs e)
        {

            Close();

        }
    }
}