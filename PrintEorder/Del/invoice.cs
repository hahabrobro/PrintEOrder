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
    
    public partial class invoice
    {
        public long invID { get; set; }
        public long odID { get; set; }
        public string invNo { get; set; }
        public Nullable<System.DateTime> invalidDate { get; set; }
        public System.DateTime sDate { get; set; }
        public int eUid { get; set; }
        public double afterTax { get; set; }
        public double preTax { get; set; }
        public double tax { get; set; }
        public string Taxkind { get; set; }
        public string company { get; set; }
        public string invCode { get; set; }
        public string invTitle { get; set; }
        public string isTitle { get; set; }
        public string invKind { get; set; }
        public int checkNo { get; set; }
        public Nullable<int> org { get; set; }
        public string isVoid { get; set; }
        public string comment { get; set; }
        public Nullable<int> invalidClerk { get; set; }
        public Nullable<System.TimeSpan> invPrintTime { get; set; }
        public string invMemo { get; set; }
    
        public virtual eorder eorder { get; set; }
    }
}
