//------------------------------------------------------------------------------
// <auto-generated>
//    這個程式碼是由範本產生。
//
//    對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//    如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Del
{
    using System;
    using System.Collections.Generic;
    
    public partial class productcode
    {
        public productcode()
        {
            this.eorderitem = new HashSet<eorderitem>();
        }
    
        public long prodID { get; set; }
        public string prodAcc { get; set; }
        public string prodDesc { get; set; }
        public string prodEnDesc { get; set; }
        public string prodType { get; set; }
        public string pWeight { get; set; }
        public string srcPartNo { get; set; }
        public string prodSize { get; set; }
        public string prodSCode { get; set; }
        public Nullable<short> bUnitID { get; set; }
        public Nullable<short> pUnitID { get; set; }
        public Nullable<short> ppID { get; set; }
        public string prodSeq { get; set; }
        public Nullable<double> prodSellPrice { get; set; }
        public Nullable<double> pHighCost { get; set; }
        public Nullable<double> pLowCost { get; set; }
        public Nullable<double> pAvgCost { get; set; }
        public Nullable<int> pOrderQty { get; set; }
        public Nullable<int> pNowQty { get; set; }
        public Nullable<long> supID { get; set; }
        public string pMemo { get; set; }
        public string pImg { get; set; }
        public string pStorageSpaces { get; set; }
        public Nullable<System.DateTime> pInDate { get; set; }
        public Nullable<System.DateTime> pOutDate { get; set; }
        public Nullable<System.DateTime> pBuildDate { get; set; }
        public Nullable<System.DateTime> pTranDate { get; set; }
        public Nullable<int> pClerk { get; set; }
    
        public virtual ICollection<eorderitem> eorderitem { get; set; }
    }
}
