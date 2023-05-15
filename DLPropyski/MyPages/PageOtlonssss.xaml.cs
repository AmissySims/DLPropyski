using DLPropyski.DBConnect;
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

namespace DLPropyski.MyPages
{
    /// <summary>
    /// Логика взаимодействия для PageOtlonssss.xaml
    /// </summary>
    public partial class PageOtlonssss : Page
    {
        Zayavka zayavka1;
        int v1;
        public PageOtlonssss( Zayavka zayavka, int v )
        {
            InitializeComponent();
            zayavka1 = zayavka;
            v1 = v;
            Up();

        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (v1 == 2)
            {
                zayavka1.ResultBecouse = TbBecause.Text;
                ConnectClass.db.SaveChanges();
                MessageBox.Show("Спасибо", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ListGroupZayavka());
            }
            else
            {
                zayavka1.ResultBecouse = TbBecause.Text;
               ConnectClass.db.SaveChanges();
                MessageBox.Show("Спасибо", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ListLichnZayavkaPage());
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
               
                NavigationService.Navigate(new ListLichnZayavkaPage());
            }
        }
        void Up()
        {
            BtnSave.IsEnabled = false;

            if (TbBecause.Text.Length > 0)
            {
                BtnSave.IsEnabled = true;
            }
        }
        private void TbBecause_TextChanged(object sender, TextChangedEventArgs e)
        {
            Up();
        }
    }

}


