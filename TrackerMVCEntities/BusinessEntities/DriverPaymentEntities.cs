using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public  class DriverPaymentEntities
    {
       public int SrNo { get; set; }
       public string CUSTOMER_CODE       { get; set; }
       public string BENEFICARY_BRANCH_CODE       { get; set; }
       public string AMOUNT       { get; set; }
       public string REMARKS_1       { get; set; }
       public string DEBIT_ACCOUNT_NO       { get; set; }
       public string CUSTOMER_NAME       { get; set; }
       public string CITY       { get; set; }

       public string CREDIT_ACCOUNT_NO       { get; set; }
       public string BENEFICARY_NAME       { get; set; }
       public string REMARK_2       { get; set; }
       public string PRODUCT_CODE       { get; set; }
       public string E_MAIL_ID       { get; set; }
       public string REMARK_3       { get; set; }
       public string REMARK_4       { get; set; }
       public string FILE_NAME       { get; set; }
    }
}
