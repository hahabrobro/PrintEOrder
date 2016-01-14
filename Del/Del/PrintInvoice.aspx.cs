using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Del
{
    public partial class PrintInvoice : System.Web.UI.Page
    {
        ReportDocument customerReport;
        protected void Page_Load(object sender, EventArgs e)
        {
            GH();
            
        }
        public string GetChineseNumber(string number)
        {
            string[] chineseNumber = { "零", "壹", "貳", "參", "肆", "伍", "陸", "柒", "捌", "玖" };
            string[] unit = { "", "拾", "佰", "仟", "萬", "拾萬", "佰萬", "仟萬", "億", "十億", "百億", "千億", "兆", "十兆", "百兆", "千兆" };
            StringBuilder ret = new StringBuilder();
            string inputNumber = number.ToString();
            int idx = inputNumber.Length;
            bool needAppendZero = false;
            foreach (char c in inputNumber)
            {
                idx--;
                if (c > '0')
                {
                    if (needAppendZero)
                    {
                        ret.Append(chineseNumber[0]);
                        needAppendZero = false;
                    }
                    ret.Append(chineseNumber[(int)(c - '0')] + unit[idx]);
                }
                else
                    needAppendZero = true;
            }
            return ret.Length == 0 ? chineseNumber[0] : ret.ToString();
        }
        public void GH()
        {
            customerReport = new ReportDocument();
            customerReport.Load(Server.MapPath("Ace.rpt"));

            DataSet dssql = new DataSet();
            
            DataTable dtsql2 = new DataTable();
            dtsql2.TableName = "Invoice";
            dtsql2.Columns.Add("ProdDesc");
            dtsql2.Columns.Add("Qty");
            dtsql2.Columns.Add("UnitPrice");
            dtsql2.Columns.Add("SubTotal");
            dssql.Tables.Add(dtsql2);
            using(inikierpEntities ent=new inikierpEntities())
            {
                //var v = from eo in ent.eorder
                //        join cus in ent.customercode on eo.cusID equals cus.cusID
                //        where eo.odID == 62636
                //        select new
                //        {
                //            eo.orderNo,eo.invNo
                //        };
                //foreach(var h in v)
                //{
                    
                //}
                var yy = from ei in ent.eorderitem
                         join pd in ent.productcode on ei.prodID equals pd.prodID
                         where ei.odID == 62636
                         select new { pd.prodDesc, ei.qty, ei.unitPrice };
                int j = 0;
                foreach(var f in yy)
                {
                    dssql.Tables["Invoice"].Rows.Add();
                    dssql.Tables["Invoice"].Rows[j]["ProdDesc"] = f.prodDesc;
                    dssql.Tables["Invoice"].Rows[j]["Qty"] = f.qty;
                    dssql.Tables["Invoice"].Rows[j]["UnitPrice"] = f.unitPrice;
                    dssql.Tables["Invoice"].Rows[j]["SubTotal"] = f.qty * f.unitPrice;
                    j++;
                }
            }
            DataView dv = new DataView();
            dv = dssql.Tables["Invoice"].DefaultView;
            customerReport.SetDataSource(dv);
            this.CrystalReportViewer1.ReportSource = customerReport;

            customerReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (customerReport != null)
            {
                customerReport.Close();
                customerReport.Dispose();
            }
        }
    }
}