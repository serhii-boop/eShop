<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportVi.aspx.cs" Inherits="WebUI.Views.Report.ReportView" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script runat="server">
       void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Domain.Entities.Wear> wears = null;
                using (Domain.Concrete.EFDbContext dc = new Domain.Concrete.EFDbContext())
                {
                    wears = dc.Wears.OrderBy(a => a.WearId).ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/rptWears.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("ShopDataSet", wears);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="875px" AsyncRendering="false" SizeToReportContent="true">
            </rsweb:ReportViewer>

        </div>
    </form>
</body>
</html>
