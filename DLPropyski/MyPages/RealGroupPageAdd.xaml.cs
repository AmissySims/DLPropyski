using DLPropyski.DBConnect;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace DLPropyski.MyPages
{
    /// <summary>
    /// Логика взаимодействия для RealGroupPageAdd.xaml
    /// </summary>
    public partial class RealGroupPageAdd : Page
    {
        DBConnect.Zayavka zayavka = new Zayavka();


        int count = 0;
        List<Client> clients = new List<Client>();
        public RealGroupPageAdd()
        {
            InitializeComponent();
            MessageBox.Show("Заявка может быть оформлена только при количестве участников от 5 человек и более.","Уведомление");
            StopDate.IsEnabled = false;

            BtnAdd.IsEnabled = false;
            CBPoseshenie.ItemsSource = ConnectClass.db.VisitPurpose.ToList();
            CBPoseshenie.DisplayMemberPath = "Name";

            CBPodrazdel.ItemsSource = ConnectClass.db.Podrazdel.ToList();
            CBPodrazdel.DisplayMemberPath = "Name";

            TbPassportNumber.MaxLength = 6;
            TbPassportSeriya.MaxLength = 4;
            Up();
        }
        public void Up()
        {
            BtnAddList.IsEnabled = false;
            BtnAdd.IsEnabled = false;


            if ( TbFamiliya.Text.Length > 0 && TbName.Text.Length > 0 && TbOtchestvo.Text.Length > 0 && TbPassportSeriya.Text.Length > 0 && TbPassportNumber.Text.Length > 0)
            {
                BtnAddList.IsEnabled = true;
            }

            if(StartDate.SelectedDate != null && StopDate.SelectedDate != null && CBPoseshenie.SelectedIndex > -1 && CBPodrazdel.SelectedIndex > -1 && CBEmployes.SelectedIndex > -1
                  && count >= 5)
            {
                BtnAdd.IsEnabled = true;
            }

            if(count > 0)
            {
                ClientsList.ItemsSource = clients.ToList();
            }
        }
        private void BtnAddList_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы хотите добавить в список для заявки на пропуска?","Уведомление",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
                        MessageBox.Show("Электронная почта введена не верно", "Error", MessageBoxButton.OK, MessageBoxImage.Error );
                    }
                }
                else
                {
                    MessageBox.Show("Номер введен не верно", "Error", MessageBoxButton.OK, MessageBoxImage.Error    );
                }
                

                
                Up();
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
                MessageBox.Show("Вы не можете выбрать дату окончания, если промежуток с началом более 15 дней", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
            Up();
        }

        private void CBPodrazdel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBEmployes.IsEnabled = false;

            Podrazdel podrazdel = (Podrazdel)CBPodrazdel.SelectedItem;
            List<Employee> employees = ConnectClass.db.Employee.Where(x => x.PodrazdelID == podrazdel.id).ToList();
            CBEmployes.ItemsSource = employees;
            CBEmployes.DisplayMemberPath = "FIO";

            if (CBPodrazdel.SelectedIndex > -1)
            {
                CBEmployes.IsEnabled = true;
            }
        }

        private void CBEmployes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Up();
        }

        private void TbFamiliya_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            Up();
        }

        private void TbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Up();
        }

        private void TbOtchestvo_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            Up();
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
                


                var mind = new DateTime((tod.Year - 16), tod.Month,tod.Day);
                if (date >= mind)
                {
                    MessageBox.Show("Возраст должен быть более 16 лет", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    DPDateBirthday.SelectedDate = null;
                }

                Up();
            }
            catch { }
        }

        private void TbPassportSeriya_TextChanged(object sender, TextChangedEventArgs e)
        {
            
         
            Up();
        }

        private void TbPassportNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            Up();
           
        }

        private void BtnClearForms_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Хотите очистить форму?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new RealGroupPageAdd());
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
         
                zayavka.DateStart = StartDate.SelectedDate;
                zayavka.DateStop = StopDate.SelectedDate;

                VisitPurpose visit = (VisitPurpose)CBPoseshenie.SelectedItem;
                Employee employee = (Employee)CBEmployes.SelectedItem;
                Podrazdel podrazdel = (Podrazdel)CBPodrazdel.SelectedItem;


                zayavka.TypeZayavkaID = 2;
                zayavka.VisitID = visit.id;
                zayavka.StatusID = 1;
                zayavka.PodrazdelEmplID = employee.id;
                zayavka.UserID = Classesss.UserClass.AuthUser.id;

                ConnectClass.db.Zayavka.Add(zayavka);
                ConnectClass.db.SaveChanges();

                Zayavka zayavka1 = ConnectClass.db.Zayavka.Where(x => x.DateStart == StartDate.SelectedDate && x.DateStop == StopDate.SelectedDate && x.PodrazdelEmplID == employee.id && x.StatusID == 1
               && x.TypeZayavkaID == 2 && x.VisitID == visit.id && x.UserID == (Classesss.UserClass.AuthUser.id)).FirstOrDefault();

                for (int i = 0; i < clients.Count; i++)
                {
                    ConnectClass.db.Client.Add(clients[i]);
                    ConnectClass.db.SaveChanges();
                Client client3 = clients[i];
                    Client client2 = ConnectClass.db.Client.Where(x => x.FName == client3.FName && x.LName == client3.LName &&
               x.Name == client3.Name && x.Mail == client3.Mail && x.PassportNumber == client3.PassportNumber && x.PassportSeries == client3.PassportSeries
               && x.Phone == client3.Phone && x.Primechanie == client3.Primechanie && x.DateBirthfay == client3.DateBirthfay).FirstOrDefault();

                    ZayavkaClient zayavkaClient = new ZayavkaClient();
                    zayavkaClient.ClientID = client2.id;
                    zayavkaClient.ZayavkaID = zayavka1.id;

                    ConnectClass.db.ZayavkaClient.Add(zayavkaClient);

                    ConnectClass.db.SaveChanges();
                }

                DBConnect.ConnectClass.db.SaveChanges();
                MessageBox.Show("Заявка отправлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ListGroupZayavka());
            
        
            
           
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
