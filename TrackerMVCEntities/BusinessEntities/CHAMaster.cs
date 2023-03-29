using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class CHAMaster
    {
        public int CHAID { get; set; }
        public string CHAName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ContactPerson { get; set; }
        public string Designation { get; set; }
        public string ContactNo1 { get; set; }
        public string ContactNo2 { get; set; }
        public string FaxNumber { get; set; }
        public string CellNumber { get; set; }
        public string eMailIDs { get; set; }
        public string Remarks { get; set; }
        public string Addedby { get; set; }
        public bool IsActive { get; set; }
        public string CHACode { get; set; }
        public string CHANo { get; set; }
        public string errorMessage { get; set; }
    }
}
