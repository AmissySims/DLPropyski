using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DLPropyski.DBConnect;

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
            if(user == null)
            {
                MessageBox.Show("Проверьте введенные данные", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else {
                DLPropyski.Classesss.UserClass.AuthUser = user;
                NavigationService.Navigate(new FirstPage());
}
            
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }
    }
}
