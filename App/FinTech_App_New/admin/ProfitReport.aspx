<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ProfitReport.aspx.cs" Inherits="Admin_ProfitReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="row">

                        <div class="col-md-3">
                            <asp:TextBox ID="txtfromdate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                TargetControlID="txtfromdate">
                            </cc1:CalendarExtender>

                        </div>

                        <div class="col-md-3">
                            <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtresult"></asp:TextBox>
                        </div>

                    </div>
                    <div class="row">

                        <div class="card">
                            <div class="card-body">
                                <h4 class="card-title">Profit Report</h4>
                                <div class="col-12">
                                    <div class="card">

                                        <div class="table-responsive">
                                            <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                <thead class="thead-light">

                                                    <th scope="col" style="color: blue">SERVICE</th>
                                                    <th scope="col" style="color: blue">API_PANEL</th>
                                                    <th scope="col" style="color: blue">SELF_PANEL</th>
                                                    <th scope="col" style="color: blue">PROFIT</th>

                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td><strong>Opening Amount</strong></td>
                                                        <td><%= ServiceTotal.OpeningBalance %></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>Recharge</strong></td>
                                                        <td><%= ServiceTotal.Recharge %></td>
                                                        <td><%= ServiceTotalSelf.Recharge %></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>Recharge-Commission</strong></td>
                                                        <td><%= ServiceTotal.RechargeCommission %></td>
                                                        <td><%= ServiceTotalSelf.RechargeCommission %></td>
                                                        <td><%= Convert.ToDecimal(ServiceTotal.RechargeCommission)-Convert.ToDecimal(ServiceTotalSelf.RechargeCommission) %></td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>DMT</strong></td>
                                                        <td><%= ServiceTotal.DMT %></td>
                                                        <td><%= ServiceTotalSelf.DMT %></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>DMT-Surcharge</strong></td>
                                                        <td><%= ServiceTotal.DMTSurcharge %></td>
                                                        <td><%= ServiceTotalSelf.DMTSurcharge %></td>
                                                        <td><%= Convert.ToDecimal(ServiceTotalSelf.DMTSurcharge)-Convert.ToDecimal(ServiceTotal.DMTSurcharge) %></td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>AEPS</strong></td>
                                                        <td><%= ServiceTotal.AEPS %></td>
                                                        <td><%= ServiceTotalSelf.AEPS %></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>AEPS-Commission</strong></td>
                                                        <td><%= ServiceTotal.AEPSCommission %></td>
                                                        <td><%= ServiceTotalSelf.AEPSCommission %></td>
                                                        <td><%= Convert.ToDecimal(ServiceTotal.AEPSCommission)-Convert.ToDecimal(ServiceTotalSelf.AEPSCommission) %></td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>Payout</strong></td>
                                                        <td><%= ServiceTotal.Payout %></td>
                                                        <td><%= ServiceTotalSelf.Payout %></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>Payout-Surcharge</strong></td>
                                                        <td><%= ServiceTotal.PayoutSurcharge %></td>
                                                        <td><%= ServiceTotalSelf.PayoutSurcharge %></td>
                                                        <td><%= Convert.ToDecimal(ServiceTotalSelf.PayoutSurcharge)-Convert.ToDecimal(ServiceTotal.PayoutSurcharge) %></td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>MATM</strong></td>
                                                        <td><%= ServiceTotal.MATM %></td>
                                                        <td><%= ServiceTotalSelf.MATM %></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>MATM-Commission</strong></td>
                                                        <td><%= ServiceTotal.MATMCommission %></td>
                                                        <td><%= ServiceTotalSelf.MATMCommission %></td>
                                                        <td><%= Convert.ToDecimal(ServiceTotal.MATMCommission)-Convert.ToDecimal(ServiceTotalSelf.MATMCommission) %></td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>AadharPay</strong></td>
                                                        <td><%= ServiceTotal.AadharPay %></td>
                                                        <td><%= ServiceTotalSelf.AadharPay %></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>AadharPay-Surcharge</strong></td>
                                                        <td><%= ServiceTotal.AadharPaySurcharge %></td>
                                                        <td><%= ServiceTotalSelf.AadharPaySurcharge %></td>
                                                        <td><%= Convert.ToDecimal(ServiceTotalSelf.AadharPaySurcharge)-Convert.ToDecimal(ServiceTotal.AadharPaySurcharge) %></td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>UPI</strong></td>
                                                        <td><%= ServiceTotal.UPI %></td>
                                                        <td><%= ServiceTotalSelf.UPI %></td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>UPI-Surcharge</strong></td>
                                                        <td><%= ServiceTotal.UPISurcharge %></td>
                                                        <td><%= ServiceTotalSelf.UPISurcharge %></td>
                                                        <td><%= Convert.ToDecimal(ServiceTotalSelf.UPISurcharge)-Convert.ToDecimal(ServiceTotal.UPISurcharge) %></td>

                                                    </tr>
                                                     <tr>
                                                        <td><strong>Pan</strong></td>
                                                        <td><%= ServiceTotal.Pan %></td>
                                                        <td><%= ServiceTotalSelf.Pan %></td>
                                                        <td><%= Convert.ToDecimal(ServiceTotalSelf.Pan)-Convert.ToDecimal(ServiceTotal.Pan) %></td>

                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td><strong class="btn btn-success">Total Profit <%= (Convert.ToDecimal(ServiceTotalSelf.UPISurcharge)-Convert.ToDecimal(ServiceTotal.UPISurcharge)+Convert.ToDecimal(ServiceTotalSelf.AadharPaySurcharge)-Convert.ToDecimal(ServiceTotal.AadharPaySurcharge)+Convert.ToDecimal(ServiceTotal.MATMCommission)-Convert.ToDecimal(ServiceTotalSelf.MATMCommission)+Convert.ToDecimal(ServiceTotalSelf.PayoutSurcharge)-Convert.ToDecimal(ServiceTotal.PayoutSurcharge)+Convert.ToDecimal(ServiceTotal.AEPSCommission)-Convert.ToDecimal(ServiceTotalSelf.AEPSCommission)+Convert.ToDecimal(ServiceTotalSelf.DMTSurcharge)-Convert.ToDecimal(ServiceTotal.DMTSurcharge)+Convert.ToDecimal(ServiceTotal.RechargeCommission)-Convert.ToDecimal(ServiceTotalSelf.RechargeCommission)) %></strong></td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
    <script>
        $(function () {
            Action(DataObject);
        });

        var DataObject = {
            ID: "example1",
            iDisplayLength: 5,
            bPaginate: true,
            bFilter: true,
            bInfo: true,
            bLengthChange: true,
            searching: true
        };
        function LoadData() {

            Action(DataObject);
        }

        var req = Sys.WebForms.PageRequestManager.getInstance();
        req.add_beginRequest(function () {

        });
        req.add_endRequest(function () {
            LoadData();

        });
        function showModal() {
            $('#myModal').modal();
        }


    </script>
</asp:Content>

