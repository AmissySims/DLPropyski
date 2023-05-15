using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLPropyski.DBConnect
{
   public partial class Employee
    {

        public string FIO
        {
            get
            {
                return (FName +" "+Name+" "+LName);
            }
        }
    }
}
