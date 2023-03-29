using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class Rebatebillverificationentities
    {


        public string message {get;set;}
        



        public Rebatebillverificationentities()
        {
            Getdetails = new List<Getdetails>();
          
        }
        public List<Getdetails> Getdetails { get; set; }
      
    }
    public class Getdetails
    {
        public string SrNo { get; set; }
        public string ContainerNo { get; set; }
        public string JONo { get; set; }
        public string JOType { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string INDateandTime { get; set; }
        public string LineName { get; set; }
        public string CustID { get; set; }
        public string CustomerNmae { get; set; }
        public string IsDPD { get; set; }
        public string IsUB { get; set; }
        public string PaidAmount { get; set; }
        public string Lineid { get; set; }

    }



    public class BilltypeEntities
    {
        public int BillID { get; set; }
        public string BillName { get; set; }
    }
}
