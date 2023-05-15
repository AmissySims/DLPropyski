using DLPropyski.DBConnect;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DLPropyski.MyPages
{
    /// <summary>
    /// Логика взаимодействия для PageReasonCancel.xaml
    /// </summary>
    public partial class PageReasonCancel : Page
    {
        Zayavka zayv;
        int z1;
        public PageReasonCancel(Zayavka zayavka, int z)
        {
            InitializeComponent();
            zayv = zayavka;
            z1 = z;
            Update();

        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (z1 == 2)
            {
                zayv.ResultBecouse = TbReason.Text;
                Connect.db.SaveChanges();
                MessageBox.Show("Спасибо", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ListGroupZayavkaPage());
            }
            else
            {
                zayv.ResultBecouse = TbReason.Text;
                Connect.db.SaveChanges();
                MessageBox.Show("Спасибо", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ListPersZayavkaPage());
            }

        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (z1 == 2)
            {

                NavigationService.Navigate(new ListGroupZayavkaPage());
            }
            else
            {

                NavigationService.Navigate(new ListPersZayavkaPage());
            }
        }
        void Update()
        {
            BtnSave.IsEnabled = false;

            if (TbReason.Text.Length > 0)
            {
                BtnSave.IsEnabled = true;
            }
        }
        private void TbReason_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
    }

}


