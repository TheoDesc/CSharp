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





        private void Connexion(
            object sender,
            RoutedEventArgs e)
        {


            if (txtMotDePasse.Password == "admin")
            {

                LogService.EcrireLog(
                    "Connexion administrateur réussie"
                );


                DialogResult = true;

                Close();

            }
            else
            {

                LogService.EcrireLog(
                    "Echec connexion administrateur"
                );


                MessageBox.Show(
                    "Mot de passe incorrect"
                );

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