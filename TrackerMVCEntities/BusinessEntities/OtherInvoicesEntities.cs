using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class OtherInvoicesEntities
    {
        public string Amount { get;set;}
        public string AccountID { get;set;}
        public string AccountName { get;set;}
        public string GroupID { get;set;}
        public string ContainerNo { get;set;}
        public string Size { get;set;}
        public string IGSTADD { get;set;}
        public string CGSTADD { get;set;}
        public string SGSTADD { get;set;}
        public string PKGS { get;set;}
        public string Weight { get;set;}
        public string Vehicleno { get;set;}
        public string Fromlocation { get;set;}
        public string TOlocation { get;set;}
        public string Frlocid { get;set;}
        public string Tolocid { get;set;}
        public string TransportID { get; set; }
        public string CargoTypeID { get; set; }

        public int TaxID { get; set; }
        public string Activity { get; set; }
        public string AssessNo { get; set; }
        public string WorkYear { get; set; }
        public string Remarks { get; set; }
        public string Invoiceno { get; set; }

        
    }
}
