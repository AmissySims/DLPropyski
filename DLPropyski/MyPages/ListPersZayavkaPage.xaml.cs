using DLPropyski.DBConnect;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DLPropyski.MyPages
{
    /// <summary>
    /// Логика взаимодействия для ListPersZayavkaPage.xaml
    /// </summary>
    public partial class ListPersZayavkaPage : Page
    {
        bool isAdmin = (bool)Classess.UserClass.AuthUser.IsAdmin;
        public ListPersZayavkaPage()
        {
            InitializeComponent();

            Update();

        }

        void Update()
        {
            if (isAdmin == false)
            {
                List<Zayavka> zayavkasss = Connect.db.Zayavka.Where(x => x.UserID ==   Classess.UserClass.AuthUser.id && x.TypeZayavkaID == 1).ToList();
                if (zayavkasss.Count > 0)
                {
                    ListZayavka.ItemsSource = zayavkasss;
                }

                else
                {
                    MessageBox.Show("Нет заявок на личное посещение", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                List<Zayavka> zayavkasss = Connect.db.Zayavka.Where(x => x.TypeZayavkaID == 1).ToList();
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
            NavigationService.Navigate(new ListGroupZayavkaPage());
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


                    if (MessageBox.Show("Хотите ввести причину отклонения?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        NavigationService.Navigate(new PageReasonCancel(sel, 4));
                    }
                }
            }
        }

        private void LichZayavkaAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PagePersZayavka());
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            Zayavka sel = (sender as Button).DataContext as Zayavka;
            sel.StatusID = 2;
            Connect.db.SaveChanges();
            Update();
        }

        private void AddGroupZayavka_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PagePersZayavka());
        }
    }
}
