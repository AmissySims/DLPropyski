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
        Zayavka zayv1;
        int v1;
        public PageReasonCancel(Zayavka zayavka, int v)
        {
            InitializeComponent();
            zayv1 = zayavka;
            v1 = v;
            Update();

        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (v1 == 2)
            {
                zayv1.ResultBecouse = TbBecause.Text;
                ConnectClass.db.SaveChanges();
                MessageBox.Show("Спасибо", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ListGroupZayavka());
            }
            else
            {
                zayv1.ResultBecouse = TbBecause.Text;
                ConnectClass.db.SaveChanges();
                MessageBox.Show("Спасибо", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ListPersZayavkaPage());
            }

        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (v1 == 2)
            {

                NavigationService.Navigate(new ListGroupZayavka());
            }
            else
            {

                NavigationService.Navigate(new ListPersZayavkaPage());
            }
        }
        void Update()
        {
            BtnSave.IsEnabled = false;

            if (TbBecause.Text.Length > 0)
            {
                BtnSave.IsEnabled = true;
            }
        }
        private void TbBecause_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
    }

}


