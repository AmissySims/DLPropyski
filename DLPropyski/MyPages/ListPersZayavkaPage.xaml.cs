﻿using DLPropyski.DBConnect;
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
        bool isAdmines = (bool)Classesss.UserClass.AuthUser.IsAdmin;
        public ListPersZayavkaPage()
        {
            InitializeComponent();

            Update();

        }

        void Update()
        {
            if (isAdmines == false)
            {
                List<Zayavka> zayavkasss = ConnectClass.db.Zayavka.Where(x => x.UserID == Classesss.UserClass.AuthUser.id && x.TypeZayavkaID == 1).ToList();
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
                if (MessageBox.Show("Вы уверены, что хотите отменить эту заявку? ", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Zayavka sel = (sender as Button).DataContext as Zayavka;
                    sel.StatusID = 4;
                    ConnectClass.db.SaveChanges();
                    Update();

                }
            }
            else
            {
                if (MessageBox.Show("Вы уверены, что хотите отменить эту заявку?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    Zayavka sel = (sender as Button).DataContext as Zayavka;
                    sel.StatusID = 3;
                    ConnectClass.db.SaveChanges();
                    Update();


                    if (MessageBox.Show("Хотите ввести причину отклонения?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        NavigationService.Navigate(new PageReasonCancel(sel, 1));
                    }
                }
            }
        }

        private void LichZayavkaAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageGroupZayavka());
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            Zayavka sel = (sender as Button).DataContext as Zayavka;
            sel.StatusID = 2;
            ConnectClass.db.SaveChanges();
            Update();
        }

        private void AddGroupZayavka_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageGroupZayavka());
        }
    }
}