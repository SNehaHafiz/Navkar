using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class JobOrderDEntities
    {
        public long JONo { get; set; }

        public string ContainerNo { get; set; }
        public string Container { get; set; }

        public short Size { get; set; }

        public string FL { get; set; }

        public int ContainerTypeID { get; set; }

        public bool IsScan { get; set; }

        public string ScanType { get; set; }

        public string Status { get; set; }

        public string ISOCode { get; set; }

        public int Cargotypeid { get; set; }

        public int Commodity_group_id { get; set; }

        public string TrainNo { get; set; }

        public string ContainerSize { get; set; }
        public string Commodity_Group_Name { get; set; }
        public string ISOCodeName { get; set; }
        public string FLName { get; set; }
        public string Cargotype { get; set; }

        public long TempID { get; set; }

        public int validationMessage { get; set; }
        public string containerList { get; set; }
        public string Remarks { get; set; }
        public long surveyID { get; set; }
        public string Activity { get; set; }
        public string InDate { get; set; }
        public string srno { get; set; }
        public string Line { get; set; }
        public string sizeType { get; set; }
        public string validityDate { get; set; }

        public string Temp { get; set; }
        public int TempfLID { get; set; }

    }
}
