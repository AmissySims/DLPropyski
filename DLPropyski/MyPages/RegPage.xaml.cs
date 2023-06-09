﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
            
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            User user = Connect.db.User.Where(x => x.Login == TbLogin.Text).FirstOrDefault();
            if (user == null)
            {
                TbLogUnickle.Visibility = Visibility.Collapsed;
            }
            else
            {
                TbLogUnickle.Visibility = Visibility.Visible;
            }

            if (Connect.db.User.ToList().Find(x => x.Login == TbLogin.Text) != null)
            {
                MessageBox.Show("Такой логин уже существует", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            string email = TbMail.Text;
            if (TbLogin.Text.Length > 0 && TbPassword.Text.Length > 0 && (!Regex.IsMatch(TbPassword.Text, @"\s")))
            {

                if (Regex.IsMatch(TbPassword.Text, @"\S{8,}") && Regex.IsMatch(TbPassword.Text, @"\d") && Regex.IsMatch(TbPassword.Text, @"[A-ZА-Я]") && Regex.IsMatch(TbPassword.Text, @"[!@#$%^]"))
                {
                    if (Regex.IsMatch(email, @"^[\w.-]+@\w+\.\w+$"))
                    {
                        User newUse = new User();
                        newUse.IsAdmin = false;
                        newUse.Login = TbLogin.Text;
                        newUse.Password = TbPassword.Text;
                        newUse.Mail = TbMail.Text;
                        Connect.db.User.Add(newUse);
                        Connect.db.SaveChanges();
                        MessageBox.Show("Регистрация завершена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Электронная почта введена не верно", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Пароль должен соответствовать условиям \r\nМинимум 8 символов.\r\nМинимум 1 прописная буква.\r\nМинимум 1 цифра.\r\nМинимум один символ из набора: ! @ # $ % ^.\r\n", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните поля", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnAuth_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());

        }


        private void TbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}
