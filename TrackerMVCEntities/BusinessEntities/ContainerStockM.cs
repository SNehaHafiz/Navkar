using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ContainerStockM
    {
        public string ContainerNo { get; set; }
        public int SrNo { get; set; }
        public int Size { get; set; }
        public int ID { get; set; }
        public int TransID { get; set; }
        public int TotalCount { get; set; }
        public int DeptID { get; set; }
        public string DisplayDate { get; set; }
        public string Title { get; set; }
        public string DeptName { get; set; }
        public string DisplayPurchaseDate { get; set; }
        public string TransMadeAgainst { get; set; }
        public string ContactPerson { get; set; }
        public Boolean IsAvailable { get; set; }
        public string ContactNo { get; set; }
        public string Type { get; set; }
        public string VendorName { get; set; }
        public string Remarks { get; set; }
        public int EntryID { get; set; }
        public int AddedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string DisplayAddedOn { get; set; }
        public string AddedByName { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsNew { get; set; }
        public Boolean EditMode { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime SoldDate { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CancelledOn { get; set; }
        public DateTime INDate { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
