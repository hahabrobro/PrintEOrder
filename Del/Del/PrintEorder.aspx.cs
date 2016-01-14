using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Del
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ReportDocument customerReport;
        protected void Page_Load(object sender, EventArgs e)
        {
            GH();
        }
        public void GH()
        {
            customerReport = new ReportDocument();
            customerReport.Load(Server.MapPath("Print_Eorder.rpt"));

            DataSet dssql = new DataSet();
            //DataTable dtsql = new DataTable();
            //dtsql.TableName = "Eorder";

            //dtsql.Columns.Add("CustomerName");
            //dtsql.Columns.Add("CompanyNo");
            //dtsql.Columns.Add("CustomerNo");
            //dtsql.Columns.Add("EorderNo");
            //dtsql.Columns.Add("InvoiceNo");
            //dtsql.Columns.Add("Sendway");
            //dtsql.Columns.Add("InvoiceTtile");
            //dtsql.Columns.Add("Payway");
            //dtsql.Columns.Add("Phone");
            //dtsql.Columns.Add("CustomerAddress");
            //dtsql.Columns.Add("EorderMemo");
            //dtsql.Columns.Add("EorderTotal");
            //dssql.Tables.Add(dtsql);

            DataTable dtsql2 = new DataTable();
            dtsql2.TableName = "Eorderitem";
            dtsql2.Columns.Add("ProdSeq");
            dtsql2.Columns.Add("ProdDesc");
            dtsql2.Columns.Add("Qty");
            //dtsql2.Columns.Add("ProdSellPrice");
            dtsql2.Columns.Add("UnitPrice");
            dtsql2.Columns.Add("SubTotal");
            dtsql2.Columns.Add("pStorageSpaces");
            dssql.Tables.Add(dtsql2);

            using (inikierpEntities ent = new inikierpEntities())
            {
                var v = from eo in ent.eorder
                        join cus in ent.customercode on eo.cusID equals cus.cusID
                        join send in ent.sendcode on eo.sendID equals send.sendID
                        join payway in ent.paywaycode on eo.payID equals payway.payID
                        //join invtitle in ent.invtitlecode on eo.invTitleCode equals invtitle.invTitleCode1
                        where eo.odID == 62636 //&& invtitle.invTitleCode1!=0
                        select new
                        {
                            eo.eoDate,
                            eo.total,
                            eo.orderNo,
                            eo.eoMemo,
                            eo.invTitleCode,
                            eo.odID,
                            cus.contactName,
                            cus.sendAddress,
                            cus.contactMobile,
                            cus.cusID,
                            payway.payDesc,
                            send.sendName
                        };
                int i = 0;
                foreach (var h in v)
                {
                    TextObject contactName = (TextObject)customerReport.ReportDefinition.ReportObjects["Text10"];
                    contactName.Text = h.contactName;
                    var g = from a in ent.invtitlecode
                            join e in ent.eorder on a.invTitleCode1 equals e.invTitleCode
                            where e.orderNo == h.orderNo && a.invTitleCode1 == h.invTitleCode
                            select new { a.invTitleDesc, a.invCode };
                    //int jj=g.ToList().Count();
                    foreach (var gg in g)
                    {
                        TextObject invCode = (TextObject)customerReport.ReportDefinition.ReportObjects["Text11"];
                        invCode.Text = gg.invCode;
                        TextObject invTitleDesc = (TextObject)customerReport.ReportDefinition.ReportObjects["Text16"];
                        invTitleDesc.Text = gg.invTitleDesc;
                    }
                    TextObject cusID = (TextObject)customerReport.ReportDefinition.ReportObjects["Text12"];
                    cusID.Text = h.cusID.ToString();
                    TextObject orderNo = (TextObject)customerReport.ReportDefinition.ReportObjects["Text13"];
                    orderNo.Text = h.orderNo;
                    TextObject Invoice = (TextObject)customerReport.ReportDefinition.ReportObjects["Text14"];
                    Invoice.Text = "發票號碼";
                    TextObject sendName = (TextObject)customerReport.ReportDefinition.ReportObjects["Text15"];
                    sendName.Text = h.sendName;
                    TextObject payDesc = (TextObject)customerReport.ReportDefinition.ReportObjects["Text17"];
                    payDesc.Text = h.payDesc;
                    TextObject contactMobile = (TextObject)customerReport.ReportDefinition.ReportObjects["Text25"];
                    contactMobile.Text = h.contactMobile;
                    
                    TextObject eoDate = (TextObject)customerReport.ReportDefinition.ReportObjects["Text18"];
                    eoDate.Text = h.eoDate.Value.ToString("yyyy-MM-dd");

                    TextObject sendAddress = (TextObject)customerReport.ReportDefinition.ReportObjects["Text19"];
                    sendAddress.Text = h.sendAddress;

                    TextObject eoMemo = (TextObject)customerReport.ReportDefinition.ReportObjects["Text20"];
                    eoMemo.Text = h.eoMemo;

                    TextObject total = (TextObject)customerReport.ReportDefinition.ReportObjects["Text21"];
                    total.Text = h.total.ToString();
                    
                    int j = 0;
                    var yy = from ei in ent.eorderitem 
                             join pd in ent.productcode on ei.prodID equals pd.prodID 
                             where ei.odID == h.odID select new 
                             { 
                                 pd.prodSeq, pd.prodDesc, ei.qty, ei.unitPrice, pd.pStorageSpaces 
                             };
                    foreach (var f in yy)
                    {
                        dssql.Tables["Eorderitem"].Rows.Add();
                        dssql.Tables["Eorderitem"].Rows[j]["ProdSeq"] = f.prodSeq;
                        dssql.Tables["Eorderitem"].Rows[j]["ProdDesc"] = f.prodDesc;
                        dssql.Tables["Eorderitem"].Rows[j]["Qty"] = f.qty;
                        dssql.Tables["Eorderitem"].Rows[j]["UnitPrice"] = f.unitPrice;
                        dssql.Tables["Eorderitem"].Rows[j]["SubTotal"] = f.qty * f.unitPrice;
                        dssql.Tables["Eorderitem"].Rows[j]["pStorageSpaces"] = f.pStorageSpaces;
                        j++;
                    }
                    i++;
                }
            }
            DataView dv = new DataView();
            dv = dssql.Tables["Eorderitem"].DefaultView;
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