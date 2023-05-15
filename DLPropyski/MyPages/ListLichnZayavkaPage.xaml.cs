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
    /// Логика взаимодействия для ListLichnZayavkaPage.xaml
    /// </summary>
    public partial class ListLichnZayavkaPage : Page
    {
        bool isAdmines = (bool)Classesss.UserClass.AuthUser.IsAdmin;
        public ListLichnZayavkaPage()
        {
            InitializeComponent();

            Up();

        }

        void Up()
        {
          if(isAdmines == false)
            {
                List<Zayavka> zayavkasss = ConnectClass.db.Zayavka.Where(x => x.UserID == Classesss.UserClass.AuthUser.id && x.TypeZayavkaID == 1).ToList();
                if (zayavkasss.Count > 0)
                {
                    ListZayavka.ItemsSource = zayavkasss;
                }

                else
                {
                    MessageBox.Show("У вас нет заявок на личное посещение", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                List<Zayavka> zayavkasss = ConnectClass.db.Zayavka.Where(x => x.TypeZayavkaID == 1).ToList();
                if (zayavkasss.Count > 0)
                {
                    ListZayavka.ItemsSource = zayavkasss;
                }

                else
                {
                    MessageBox.Show("Нет заявок на личное посещение", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            

        }

        private void GrouppZayavka_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListGroupZayavka());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmines == false)
            {
                if (MessageBox.Show("Вы уверены, что желаете отменить эту заявку? Отменить нельзя", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Zayavka sel = (sender as Button).DataContext as Zayavka;
                    sel.StatusID = 4;
                   ConnectClass.db.SaveChanges();
                    Up();

                }
            }
            else
            {
                if (MessageBox.Show("Вы уверены, что желаете отменить эту заявку? Отменить нельзя", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    Zayavka sel = (sender as Button).DataContext as Zayavka;
                    sel.StatusID = 3;
                    ConnectClass.db.SaveChanges();
                    Up();


                    if (MessageBox.Show("Желаете ввести причина отклонения?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        NavigationService.Navigate(new PageOtlonssss(sel, 1));
                    }
                }
            }
        }

        private void LichZayavkaAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageGroupZayavka());
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            Zayavka sel = (sender as Button).DataContext as Zayavka;
            sel.StatusID = 2;
            ConnectClass.db.SaveChanges();
            Up();
        }

        private void AddGroupZayavka_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageGroupZayavka());
        }
    }
}
