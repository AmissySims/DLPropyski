using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DLPropyski.MyPages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnPers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListPersZayavkaPage());
        }

        private void BtnGroupp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListGroupZayavkaPage());

        }
    }
}
