using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class RevenueAlloperationEntities
    {

        public string PortName { get; set; }
        public string Road20 { get; set; }
        public string Road40 { get; set; }
        public string Road45 { get; set; }
        public string Rail20 { get; set; }
        public string Rail40 { get; set; }
        public string Rail45 { get; set; }
        public string TotalTues { get; set; }
        //change by rahul
        public string MonthName { get; set; }
        public RevenueAlloperationEntities()
        {

            ICDFullImport = new List<ICDFullImport>();
            ICDFullExport = new List<ICDFullExport>();
            ICDFullExportRepo = new List<ICDFullExportRepo>();
        }

        public List<ICDFullImport> ICDFullImport { get; set; }
        public List<ICDFullExport> ICDFullExport { get; set; }
        public List<ICDFullExportRepo> ICDFullExportRepo { get; set; }
        //end by rahul

    }
    //added by rahul
    public class ICDFullImport
    {

        public string PortName { get; set; }
        public string Road20 { get; set; }
        public string Road40 { get; set; }
        public string Road45 { get; set; }
        public string Rail20 { get; set; }
        public string Rail40 { get; set; }
        public string Rail45 { get; set; }
        public string TotalTues { get; set; }

    }
    public class ICDFullExport
    {

        public string PortName { get; set; }
        public string Road20 { get; set; }
        public string Road40 { get; set; }
        public string Road45 { get; set; }
        public string Rail20 { get; set; }
        public string Rail40 { get; set; }
        public string Rail45 { get; set; }
        public string TotalTues { get; set; }

    }
    public class ICDFullExportRepo
    {

        public string PortName { get; set; }
        public string Road20 { get; set; }
        public string Road40 { get; set; }
        public string Road45 { get; set; }
        public string Rail20 { get; set; }
        public string Rail40 { get; set; }
        public string Rail45 { get; set; }
        public string TotalTues { get; set; }

    }
    //end by rahul
}
