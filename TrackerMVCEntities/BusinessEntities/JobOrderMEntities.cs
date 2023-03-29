using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class JobOrderMEntities
    {
        public long JONo { get; set; }

        public DateTime JODate { get; set; }

        public int AgentID { get; set; }

        public int SLID { get; set; }

        public string ViaNo { get; set; }

        public int VesselID { get; set; }

        public string Voyage { get; set; }

        public int PortID { get; set; }

        public int Tot20 { get; set; }

        public int Tot40 { get; set; }

        public int Tot45 { get; set; }

        public string IGMNo { get; set; }

        public DateTime IGMDate { get; set; }

        public DateTime berthingdate { get; set; }

        public long shipperid { get; set; }

        public long Importerid { get; set; }

        public long Chaid { get; set; }

        public string BLNumber { get; set; }

        public DateTime BLReceivedDate { get; set; }

        public int Haulage_Type_Id { get; set; }

        public int File_Status_Id { get; set; }

        public int Transport_Type_Id { get; set; }

        public int POL_ID { get; set; }

        public int Sales_Person_Id { get; set; }

        public bool IsCancel { get; set; }

        public bool IsDPD { get; set; }

        public short AddedBy { get; set; }

        public DateTime AddedOn { get; set; }

        public short ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int CancelledBy { get; set; }

        public DateTime CancelledOn { get; set; }

        public string JoRcvdfrom { get; set; }

        public string ContainerNo { get; set; }

        public int Size { get; set; }

        public string FL { get; set; }

        public int ContainerTypeID { get; set; }
        public string Status { get; set; }

        public string ISOCode { get; set; }

        public int Cargotypeid { get; set; }

        public int Commodity_group_id { get; set; }

        public String berthingDateInstring { get; set; }

        public string  Remarks { get; set; }
        public string CustomerName { get; set; }
        public string TEUS { get; set; }
        public string userName { get; set; }
        public string BLReceivedDateInString { get; set; }
        public string JODateInString { get; set; }
        public string SubmitCss { get; set; }
        public string Editcss { get; set; }
        public string SalesPersonName { get; set; }
        public string HouseBLNumber { get; set; }

        public string AttachmentCount { get; set; }
        public int CancelReasonID { get; set; }

        public int KAMID { get; set; }

        public string PODETADate { get; set; }
        public string PODETADateString { get; set; }

        public string shippingLineName { get; set; }
        public string HBLNumber { get; set; }
        public string VesselName { get; set; }
        public int SRNO { get; set; }
        public string Dwell_Days { get; set; }
        public string File_status_Name { get; set; }
        public string ContainerType { get; set; }
        public DateTime SMTPRCVDDate { get; set; }
        public int JoTypeId { get; set; }
        public string JoRemarks { get; set; }
        public int FileTypeId { get; set; }
        public int FileId { get; set; }
        public string PortName { get; set; }
        public string RotationNo { get; set; }

        public JobOrderMEntities()
        {
            BLNumber = "";
            HouseBLNumber = "";
            FileId = 0;
            PortName = "";
            RotationNo = "";
        }
    }
}
