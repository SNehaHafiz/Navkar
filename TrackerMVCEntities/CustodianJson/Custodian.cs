using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP = TrackerMVCEntities.CustodianJsonDP;
using ASR = TrackerMVCEntities.CustodianJsonASR;
using AR = TrackerMVCEntities.CustodianJsonAR;
using DT = TrackerMVCEntities.CustodianJsonDT;
namespace TrackerMVCEntities.CustodianJson
{
    public class Custodian
    {
        public Custodian()
        {
            custodianJsonSF = new CustodianJson();
            custodianJsonASR = new ASR.CustodianJson();
            custodianJsonAR = new AR.CustodianJson();
            custodianJsonDP = new DP.CustodianJson();
            custodianJsonDT = new DT.CustodianJson();
        }

        public CustodianJson custodianJsonSF { get; set; }
        public AR.CustodianJson custodianJsonAR { get; set; }
        public ASR.CustodianJson custodianJsonASR { get; set; }
        public DP.CustodianJson custodianJsonDP { get; set; }
        public DT.CustodianJson custodianJsonDT { get; set; }
        public string FileType { get; set; }
    }
}
