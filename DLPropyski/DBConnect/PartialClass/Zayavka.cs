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

        public  string ZayvlaNumb
        {
            get
            {
                return ("Заявка №" + id + " с " + String.Format("{0:MM/dd/yy}", DateStart) + " по " + String.Format("{0:MM/dd/yy}", DateStop));
            }
        }

        public string TselVisit
        {
            get
            {
                return ("Цель заявки - " +VisitPurpose.Name);
            }
        }

        public string EmplPodrazdel
        {
            
            get
            {
                Employee employee = ConnectClass.db.Employee.Where(x => x.id == PodrazdelEmplID).FirstOrDefault();
                return ("Сотрудник подразделения: " + employee.FName +" " + employee.Name +" " + employee.LName);
            }
        }

        public int Countsss
        {

            get
            {
                List<ZayavkaClient> cl = ConnectClass.db.ZayavkaClient.Where(x => x.ZayavkaID == id).ToList();
                return cl.Count;
            }
        }

        public string StatusText
        {
            get
            {
                    if (StatusID == 1 || StatusID == 2 || StatusID == 4)
                    {
                    return ("статус вашей заявки - " + Status.Name);
                   

                    }
                    else  
                    {
                         return ("статус вашей заявки - " + Status.Name + ". Причина: " + ResultBecouse);

                    }
                        
            }
        }

        public Visibility visibilityBtn
        {
            get
            {
                if(Classesss.UserClass.AuthUser.IsAdmin == true && (StatusID == 1 || StatusID == 2))
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
                if (Classesss.UserClass.AuthUser.IsAdmin == true && StatusID == 1 )
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
