using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ImportFinalOutEntities
    {
        public string DeliveryType { get; set; }
        public string For { get; set; }
        public int PortID { get; set; }
        public int LineID { get; set; }
        public ImportFinalOutEntities()
        {
            PortList = new List<PortEntities>();
            LineList = new List<LineEntities>();
        }
        public List<PortEntities> PortList { get; set; }

        public List<LineEntities> LineList { get; set; }
        public string SrNo	{ get; set; }
    public string GPNo	{ get; set; }
public string GPDate	{ get; set; }
public string ContainerNo	{ get; set; }
public string Size { get; set; }
public string Type { get; set; }
public string IGMNo	{ get; set; }
public string ItemNo	{ get; set; }
public string BLNo	{ get; set; }
public string InDate	{ get; set; }
public string OutDate	{ get; set; }
public string DwellDays	{ get; set; }
public string VehicleNo	{ get; set; }
public string PKGS { get; set; }
public string PKGSType	{ get; set; }
public string TareWeight	{ get; set; }
public string CargoWeight	{ get; set; }
public string GrossWeight	{ get; set; }
public string Commodity { get; set; }
public string HaulagetType  { get; set; }
public string Line { get; set; }
public string Customer { get; set; }
public string Importer { get; set; }
public string CHAName	{ get; set; }
public string BerthingDateTime	{ get; set; }
public string JoDateTime	{ get; set; }
public string PortName	{ get; set; }
public string GoodsDesc	{ get; set; }
public string BOENo	{ get; set; }
public string BOEDate	{ get; set; }
public string OOCNo	{ get; set; }
public string OOCDate	{ get; set; }
public string Value { get; set; }
public string Duty { get; set; }
public string PreparedBy	{ get; set; }
        public string DoValidDate { get; set; }
        public string CargoType { get; set; }
    }
    public class PortEntities
    {
        public int PortID { get; set; }

        public string PortName { get; set; }
    }
    public class LineEntities
    {
        public int LineID { get; set; }

        public string LineName { get; set; }
    }
}
