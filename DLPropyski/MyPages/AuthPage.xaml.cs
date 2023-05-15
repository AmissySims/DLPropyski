using DLPropyski.DBConnect;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DLPropyski.MyPages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void BtnAuth_Click(object sender, RoutedEventArgs e)
        {

            User user = ConnectClass.db.User.Where(x => x.Login == TbLogin.Text && x.Password == TbPassword.Text).FirstOrDefault();
            if (user == null)
            {
                MessageBox.Show("Проверьте введенные данные", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                Classesss.UserClass.AuthUser = user;
                NavigationService.Navigate(new MainPage());
            }

        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }
    }
}
