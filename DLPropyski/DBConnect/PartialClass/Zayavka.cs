using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DLPropyski.DBConnect;


namespace DLPropyski.DBConnect
{
   public partial class Zayavka
    {

        public  string ZayvNumb
        {
            get
            {
                return ("Заявка №" + id + " с " + String.Format("{0:MM/dd/yy}", DateStart) + " по " + String.Format("{0:MM/dd/yy}", DateStop));
            }
        }

        public string GoalVisit
        {
            get
            {
                return ("Цель - " + VisitPurpose.Name);
            }
        }

        public string EmplPodrazdel
        {
            
            get
            {
                Employee employee = Connect.db.Employee.Where(x => x.id == PodrazdelEmplID).FirstOrDefault();
                return ("Сотрудник подразделения: " + employee.FName +" " + employee.Name +" " + employee.LName);
            }
        }

        public int Countsss
        {

            get
            {
                List<ZayavkaClient> cl = Connect.db.ZayavkaClient.Where(x => x.ZayavkaID == id).ToList();
                return cl.Count;
            }
        }

        public string StatusText
        {
            get
            {
                if (StatusID == 1 || StatusID == 2 || StatusID == 4)
                {
                    return ("Статус заявки - " + Status.Name);


                }
                else
                {
                    return ("Статус  заявки - " + Status.Name + ". Причина: " + ResultBecouse);

                }

            }
        }

        public Visibility visibilityBtn
        {
            get
            {
                if(StatusID == 1 || StatusID == 2)
                {
                    return Visibility.Visible;
                }
                else
                {
                  return  Visibility.Hidden;
                }
            }
        }

        public Visibility visibilityBtn2
        {
            get
            {
                if (Classess.UserClass.AuthUser.IsAdmin == true && StatusID == 1 )
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
        }


    }
}
