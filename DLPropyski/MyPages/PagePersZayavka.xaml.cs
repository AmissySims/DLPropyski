using DLPropyski.Classess;
using DLPropyski.DBConnect;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace DLPropyski.MyPages
{
    /// <summary>
    /// Логика взаимодействия для PagePersZayavka.xaml
    /// </summary>
    public partial class PagePersZayavka : Page

    {
        Zayavka zayavka = new Zayavka();
        Client client = new Client();


        public PagePersZayavka()
        {
            InitializeComponent();
            StopDate.IsEnabled = false;

            BtnAdd.IsEnabled = false;
            CBPoseshenie.ItemsSource = Connect.db.VisitPurpose.ToList();
            CBPoseshenie.DisplayMemberPath = "Name";

            CBPodrazdel.ItemsSource = Connect.db.Podrazdel.ToList();
            CBPodrazdel.DisplayMemberPath = "Name";



        }

        public void Update()
        {
            if (StartDate.SelectedDate != null && StopDate.SelectedDate != null && CBPoseshenie.SelectedIndex > -1 && CBPodrazdel.SelectedIndex > -1 && CBEmployes.SelectedIndex > -1
                  && TbFamiliya.Text.Length > 0 && TbName.Text.Length > 0 && TbOtchestvo.Text.Length > 0 && TbPassportSeriya.Text.Length > 0 && TbPassportNumber.Text.Length > 0)
            {
                BtnAdd.IsEnabled = true;
            }

        }
        private void BtnClearForms_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Хотите очистить эту форму?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new PagePersZayavka());
            }
        }

        private void BtnAddimage_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };


            if (openFile.ShowDialog().GetValueOrDefault())
            {
                client.Images = File.ReadAllBytes(openFile.FileName);
                imageProfile.Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }

        private void CBPodrazdel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBEmployes.IsEnabled = false;

            Podrazdel podrazdel = (Podrazdel)CBPodrazdel.SelectedItem;
            List<Employee> employees = Connect.db.Employee.Where(x => x.PodrazdelID == podrazdel.id).ToList();
            CBEmployes.ItemsSource = employees;
            CBEmployes.DisplayMemberPath = "FIO";

            if (CBPodrazdel.SelectedIndex > -1)
            {
                CBEmployes.IsEnabled = true;
            }
            Update();
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            StopDate.IsEnabled = false;
            if (StartDate.SelectedDate != null && StartDate.SelectedDate < DateTime.Today.AddDays(1))
            {
                MessageBox.Show("Можно выбрать дату, начиная с завтрашнего дня", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                StartDate.SelectedDate = null;
            }
            else if (StartDate.SelectedDate == null)
            {
                StopDate.IsEnabled = false;
            }
            else
            {
                StopDate.IsEnabled = true;
            }

            Update();
        }

        private void StopDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = (DateTime)StartDate.SelectedDate;

            if (StopDate.SelectedDate != null && StopDate.SelectedDate > date.AddDays(15))
            {
                MessageBox.Show("Нельзя выбрать дату окончания, если промежуток с началом больше 15 дней", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                StopDate.SelectedDate = null;
            }
            try
            {
                DateTime date2 = (DateTime)StopDate.SelectedDate;
                if (StartDate.SelectedDate == StopDate.SelectedDate || date > date2)
                {
                    MessageBox.Show("Дата окончания должна быть позже от даты начала и отличаться от нее действия пропуска", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    StopDate.SelectedDate = null;
                }
            }
            catch
            {

            }


            Update();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string num = TbPhone.Text;
            string email = TbMail.Text;
            if (Regex.IsMatch(num, @"^((\+?7|8)[ -]?)?([(]?\d[- ]?[()]?[- ]?){10}$") && Regex.IsMatch(num, @"^(\+?7|8)?[\s(-]?[(-]?\d{3,4}[)-]?[ )-]?\d{2,7}[ -]?\d{2,4}[ -]?\d{0,2}$"))
            {

                if (Regex.IsMatch(email, @"^[\w.-]+@\w+\.\w+$"))
                {
                    zayavka.DateStart = StartDate.SelectedDate;
                    zayavka.DateStop = StopDate.SelectedDate;

                    VisitPurpose visit = (VisitPurpose)CBPoseshenie.SelectedItem;
                    Employee employee = (Employee)CBEmployes.SelectedItem;
                    Podrazdel podrazdel = (Podrazdel)CBPodrazdel.SelectedItem;

                    zayavka.TypeZayavkaID = 1;
                    zayavka.VisitID = visit.id;
                    zayavka.StatusID = 1;
                    zayavka.PodrazdelEmplID = employee.id;
                    zayavka.UserID = UserClass.AuthUser.id;

                    Connect.db.Zayavka.Add(zayavka);
                    Connect.db.SaveChanges();
                    Zayavka zay = Connect.db.Zayavka.Where(x => x.DateStart == StartDate.SelectedDate && x.DateStop == StopDate.SelectedDate && x.PodrazdelEmplID == employee.id && x.StatusID == 1
                    && x.TypeZayavkaID == 1 && x.VisitID == visit.id && x.UserID == (UserClass.AuthUser.id)).FirstOrDefault();

                    client.FName = TbFamiliya.Text.Trim();
                    client.LName = TbOtchestvo.Text.Trim();
                    client.Name = TbName.Text.Trim();
                    client.Mail = TbMail.Text.Trim();
                    client.NameOrganizatciya = TbOrganizaciya.Text.Trim();
                    client.Phone = TbPhone.Text.Trim();
                    client.Primechanie = TbPrim.Text;
                    client.DateBirthfay = DPDateBirthday.SelectedDate;
                    client.PassportSeries = int.Parse(TbPassportSeriya.Text.Trim());
                    client.PassportNumber = int.Parse(TbPassportNumber.Text.Trim());

                    Connect.db.Client.Add(client);
                    Connect.db.SaveChanges();


                    Client clientt = Connect.db.Client.Where(x => x.FName == TbFamiliya.Text.Trim() && x.LName == TbOtchestvo.Text.Trim() &&
                    x.Name == TbName.Text.Trim() && x.Mail == TbMail.Text.Trim() && x.PassportNumber == client.PassportNumber && x.PassportSeries == client.PassportSeries
                    && x.Phone == TbPhone.Text && x.Primechanie == TbPrim.Text && x.DateBirthfay == DPDateBirthday.SelectedDate).FirstOrDefault();

                    ZayavkaClient zayClient = new ZayavkaClient();
                    zayClient.ClientID = clientt.id;
                    zayClient.ZayavkaID = zay.id;

                    Connect.db.ZayavkaClient.Add(zayClient);

                    Connect.db.SaveChanges();

                    MessageBox.Show("Заявка создана", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new ListPersZayavkaPage());
                }
                else
                {
                    MessageBox.Show("Электронная почта введена не верно", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Номер введен не верно", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CBPoseshenie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void CBEmployes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void TbFamiliya_TextChanged(object sender, TextChangedEventArgs e)
        {

            Update();
        }

        private void TbName_TextChanged(object sender, TextChangedEventArgs e)
        {

            Update();
        }

        private void TbOtchestvo_TextChanged(object sender, TextChangedEventArgs e)
        {

            Update();
        }

        private void DPDateBirthday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime date = (DateTime)DPDateBirthday.SelectedDate;
                var tod = DateTime.Today;



                var mind = new DateTime((tod.Year - 16), tod.Month, tod.Day);
                if (date >= mind)
                {
                    MessageBox.Show("Возраст должен быть более 16 лет", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    DPDateBirthday.SelectedDate = null;
                }

                Update();
            }
            catch { }
        }

        private void TbPassportSeriya_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void TbPassportNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

            Update();
        }

        private void TbPhone_TextChanged(object sender, TextChangedEventArgs e)
        {

            Update();
        }

        private void TbFamiliya_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void TbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void TbOtchestvo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void TbPassportNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void TbPassportSeriya_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void TbMail_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
