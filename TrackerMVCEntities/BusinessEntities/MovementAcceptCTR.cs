using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class MovementAcceptCTR
    {
        public int SrNo { get;set;}
        public int ISWeighed { get;set;}
        public int Size {get;set;}
        public bool Select { get;set;}
        public string MoveType { get;set;}
        public string ContainerNo { get;set;}
        public string SealNo { get;set;}
        public int EntryID { get;set;}
        public int AgencyID { get;set;}
        public string AgentSeal { get;set;}
        public string AgentName { get;set;}
        public string PODName { get;set;}
        public string Shippername { get;set;}
        public string Remarks { get;set;}
        public string CHAName { get;set;}
        public string SLName { get;set;}
        public string Cargotype { get;set;}
        public string Viano { get;set;}
        public string LEONo { get;set;}
        public string LEODate { get;set;}
        public string TransportType { get;set;}
        public string OnAccount { get;set;}
        public int POLID { get;set;}
        public int TransTypeID { get;set;}
    }
}
