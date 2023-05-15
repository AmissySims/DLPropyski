using DLPropyski.DBConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace DLPropyski.MyPages
{
    /// <summary>
    /// Логика взаимодействия для ZayavkaGroupPageAdd.xaml
    /// </summary>
    public partial class ZayavkaGroupPageAdd : Page
    {
        Zayavka zayav = new Zayavka();


        int count = 0;
        List<Client> clients = new List<Client>();
        public ZayavkaGroupPageAdd()
        {
            InitializeComponent();
            MessageBox.Show("Заявка может быть оформлена только при кол-ве участников от 5 человек и более.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            StopDate.IsEnabled = false;

            BtnAdd.IsEnabled = false;
            CBPoseshenie.ItemsSource = Connect.db.VisitPurpose.ToList();
            CBPoseshenie.DisplayMemberPath = "Name";

            CBPodrazdel.ItemsSource = Connect.db.Podrazdel.ToList();
            CBPodrazdel.DisplayMemberPath = "Name";

            
            Update();
        }
        public void Update()
        {
            BtnAddList.IsEnabled = false;
            BtnAdd.IsEnabled = false;


            if (TbFamiliya.Text.Length > 0 && TbName.Text.Length > 0 && TbOtchestvo.Text.Length > 0 && TbPassportSeriya.Text.Length > 0 && TbPassportNumber.Text.Length > 0)
            {
                BtnAddList.IsEnabled = true;
            }

            if (StartDate.SelectedDate != null && StopDate.SelectedDate != null && CBPoseshenie.SelectedIndex > -1 && CBPodrazdel.SelectedIndex > -1 && CBEmployes.SelectedIndex > -1
                  && count >= 5)
            {
                BtnAdd.IsEnabled = true;
            }

            if (count > 0)
            {
                ClientsList.ItemsSource = clients.ToList();
            }
        }
        private void BtnAddList_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите добавить в список для заявки на пропуска?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string num = TbPhone.Text;
                string email = TbMail.Text;
                if (Regex.IsMatch(num, @"^((\+?7|8)[ -]?)?([(]?\d[- ]?[()]?[- ]?){10}$") && Regex.IsMatch(num, @"^(\+?7|8)?[\s(-]?[(-]?\d{3,4}[)-]?[ )-]?\d{2,7}[ -]?\d{2,4}[ -]?\d{0,2}$"))
                {

                    if (Regex.IsMatch(email, @"^[\w.-]+@\w+\.\w+$"))
                    {
                        Client client = new Client();
                        client.FName = TbFamiliya.Text;
                        client.LName = TbOtchestvo.Text;
                        client.Name = TbName.Text;
                        client.Mail = TbMail.Text;
                        client.NameOrganizatciya = TbOrganizaciya.Text;
                        client.Phone = TbPhone.Text;
                        client.Primechanie = TbPrim.Text;
                        client.DateBirthfay = DPDateBirthday.SelectedDate;
                        client.PassportSeries = int.Parse(TbPassportSeriya.Text);
                        client.PassportNumber = int.Parse(TbPassportNumber.Text);
                        clients.Add(client);
                        count++;
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



                Update();
            }
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            StopDate.IsEnabled = false;
            if (StartDate.SelectedDate != null && StartDate.SelectedDate < DateTime.Today.AddDays(1))
            {
                MessageBox.Show("Вы можете выбрать дату, начиная с завтрашнего дня", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    MessageBox.Show("Дата окончания должна быть позже и отличаться от даты начала действия пропуска", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    StopDate.SelectedDate = null;
                }
            }

            catch
            {

            }
        }

        private void CBPoseshenie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
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

        private void TbPhone_TextChanged(object sender, TextChangedEventArgs e)
        {




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
                    MessageBox.Show("Возраст должен быть больше 16 лет", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void BtnClearForms_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Хотите очистить форму?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new ZayavkaGroupPageAdd());
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            zayav.DateStart = StartDate.SelectedDate;
            zayav.DateStop = StopDate.SelectedDate;

            VisitPurpose visit = (VisitPurpose)CBPoseshenie.SelectedItem;
            Employee employee = (Employee)CBEmployes.SelectedItem;
            Podrazdel podrazdel = (Podrazdel)CBPodrazdel.SelectedItem;


            zayav.TypeZayavkaID = 2;
            zayav.VisitID = visit.id;
            zayav.StatusID = 1;
            zayav.PodrazdelEmplID = employee.id;
            zayav.UserID = Classess.UserClass.AuthUser.id;

            Connect.db.Zayavka.Add(zayav);
            Connect.db.SaveChanges();

            Zayavka zay = Connect.db.Zayavka.Where(x => x.DateStart == StartDate.SelectedDate && x.DateStop == StopDate.SelectedDate && x.PodrazdelEmplID == employee.id && x.StatusID == 1
           && x.TypeZayavkaID == 2 && x.VisitID == visit.id && x.UserID == (Classess.UserClass.AuthUser.id)).FirstOrDefault();

            for (int i = 0; i < clients.Count; i++)
            {
                Connect.db.Client.Add(clients[i]);
                Connect.db.SaveChanges();
                Client client3 = clients[i];
                Client client2 = Connect.db.Client.Where(x => x.FName == client3.FName && x.LName == client3.LName &&
           x.Name == client3.Name && x.Mail == client3.Mail && x.PassportNumber == client3.PassportNumber && x.PassportSeries == client3.PassportSeries
           && x.Phone == client3.Phone && x.Primechanie == client3.Primechanie && x.DateBirthfay == client3.DateBirthfay).FirstOrDefault();

                ZayavkaClient zayClient = new ZayavkaClient();
                zayClient.ClientID = client2.id;
                zayClient.ZayavkaID = zay.id;

                Connect.db.ZayavkaClient.Add(zayClient);

                Connect.db.SaveChanges();
            }

            DBConnect.Connect.db.SaveChanges();
            MessageBox.Show("Заявка создана", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new ListGroupZayavkaPage());




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
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
