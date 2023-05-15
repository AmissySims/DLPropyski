using DLPropyski.DBConnect;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DLPropyski.MyPages
{
    /// <summary>
    /// Логика взаимодействия для ListGroupZayavkaPage.xaml
    /// </summary>
    public partial class ListGroupZayavkaPage : Page
    {
        bool isAdmin = (bool)Classess.UserClass.AuthUser.IsAdmin;
        public ListGroupZayavkaPage()
        {
            InitializeComponent();
            Update();
        }

        void Update()
        {
            if (isAdmin == false)
            {
                List<Zayavka> zayavkasss = Connect.db.Zayavka.Where(x => x.UserID == Classess.UserClass.AuthUser.id && x.TypeZayavkaID == 2).ToList();
                if (zayavkasss.Count > 0)
                {
                    ListZayavka.ItemsSource = zayavkasss;
                }

                else
                {

                    MessageBox.Show("Нет заявок на групповое посещение", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                List<Zayavka> zayavkasss = Connect.db.Zayavka.Where(x => x.TypeZayavkaID == 2).ToList();
                if (zayavkasss.Count > 0)
                {
                    ListZayavka.ItemsSource = zayavkasss;
                }

                else
                {
                    MessageBox.Show("Нет заявок на групповое посещение", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }


        }



        private void LichZayav_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListPersZayavkaPage());
        }

        private void AddGroupZayavka_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ZayavkaGroupPageAdd());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin == false)
            {
                if (MessageBox.Show("Хотите отменить эту заявку? ", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Zayavka sel = (sender as Button).DataContext as Zayavka;
                    sel.StatusID = 4;
                    Connect.db.SaveChanges();
                    Update();

                }
            }
            else
            {
                if (MessageBox.Show("Хотите отменить эту заявку?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    Zayavka sel = (sender as Button).DataContext as Zayavka;
                    sel.StatusID = 3;
                    Connect.db.SaveChanges();
                    Update();


                    if (MessageBox.Show("Хотите указать причину отмены?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        NavigationService.Navigate(new PageReasonCancel(sel, 2));
                    }
                }
            }
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            Zayavka sel = (sender as Button).DataContext as Zayavka;
            sel.StatusID = 2;
            Connect.db.SaveChanges();
            Update();
        }
    }
}
