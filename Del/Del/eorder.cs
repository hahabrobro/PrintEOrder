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
    
    public partial class eorder
    {
        public eorder()
        {
            this.eorderitem = new HashSet<eorderitem>();
            this.invoice = new HashSet<invoice>();
        }
    
        public long odID { get; set; }
        public string orderNo { get; set; }
        public string storeNumber { get; set; }
        public Nullable<short> storeID { get; set; }
        public Nullable<long> cusID { get; set; }
        public Nullable<short> langID { get; set; }
        public Nullable<short> payID { get; set; }
        public string invNo { get; set; }
        public Nullable<double> total { get; set; }
        public Nullable<decimal> tax { get; set; }
        public Nullable<double> noTax { get; set; }
        public Nullable<short> sendID { get; set; }
        public int eoClerk { get; set; }
        public Nullable<short> curID { get; set; }
        public Nullable<System.DateTime> eoDate { get; set; }
        public string isTax { get; set; }
        public Nullable<int> invTitleCode { get; set; }
        public string isMerger { get; set; }
        public string isClose { get; set; }
        public string isPayment { get; set; }
        public Nullable<int> eStatusID { get; set; }
        public string payment_no { get; set; }
        public string eoMemo { get; set; }
        public string companyMemo { get; set; }
        public Nullable<short> shipment { get; set; }
    
        public virtual customercode customercode { get; set; }
        public virtual sendcode sendcode { get; set; }
        public virtual ICollection<eorderitem> eorderitem { get; set; }
        public virtual ICollection<invoice> invoice { get; set; }
    }
}
