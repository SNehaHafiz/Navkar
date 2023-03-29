using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class LoginEntites
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string UserPass { get; set; }

        public string UserType { get; set; }

        public string DepType { get; set; }

        public string email_ID { get; set; }

        public string email_Pswrd { get; set; }

        public string ContactNo { get; set; }

        public string Gender { get; set; }

        public string DOB { get; set; }

        public string EMPID { get; set; }

        public short agencyID { get; set; }

        public bool IsHoldAuth { get; set; }

        public bool IsLogin { get; set; }

        public bool IsActive { get; set; }

        public string FilePath { get; set; }

        public string MachineName { get; set; }

        public bool IsReadEInvoice { get; set; }

        public bool IsWebAccess { get; set; }

        public bool IsMLogin { get; set; }

        public byte[] DisplayPicture { get; set; }

        public int conid { get; set; }

        public bool IsAppsPath { get; set; }
        public bool rememberme { get; set; }
        public string ConCode { get; set; }
        public string LoginErrorMessage { get; set; }
    }
}
