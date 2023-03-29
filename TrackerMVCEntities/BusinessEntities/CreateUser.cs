using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrackerMVCEntities.BusinessEntities
{
  public  class CreateUser
    {
        
        public string firstname { get; set; }

        public string lastname { get; set; }
        public string gender { get; set; }
        public string DOB { get; set; }
        public string employeeid { get; set; }
        public string password { get; set; }
        public string department { get; set; }
        public string userType { get; set; }
        public string emailid { get; set; }
        public string passEmail { get; set; }
        public string Contact { get; set; }
        public string isactive { get; set; }
        public string GroupID { get; set; }
        public String Name { get; set; }
        public int UseriDN { get; set; }
        public string icon { get; set; }
        public string MenuDesc { get; set; }
        public string MenuDept { get; set; }
        public string fromName { get; set; }
        public string menuName { get; set; }
        public int menudeptid { get; set; }


        public string Controller { get; set; }
        public string Action { get; set; }
        public string Menu { get; set; }
        public string UserID { get; set; }
        public string USerName { get; set; }
        public int FuelVID { get; set; }
        public string FuelVName { get; set; }
        public int FuelTID { get; set; }
        public string FuelType { get; set; }
        public string fuelrate { get; set; }
        public string fuelDate { get; set; }
    }
}
