﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.CustodianJson
{
    public class EGM
    {
        public EGM()
        {
            headerField = new HeaderField();
            master = new master();
            //digSign = new DigSign();

        }
        public HeaderField headerField { get; set; }
        public master master { get; set; }
        //public DigSign digSign { get; set; }

    }
}