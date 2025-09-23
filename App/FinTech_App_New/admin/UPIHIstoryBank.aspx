<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="UPIHIstoryBank.aspx.cs" Inherits="Admin_UPIHIstoryBank" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card panel-primary">
                        <div class="card-header">UPI History</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <label for="ddlMember">From Date : </label>
                                    <asp:TextBox ID="txtfromdate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                        TargetControlID="txtfromdate">
                                    </cc1:CalendarExtender>

                                </div>
                                <div class="col-md-3">
                                    <label for="ddlMember">To Date : </label>
                                    <asp:TextBox ID="txttodate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="MM-dd-yyyy" Animated="False"
                                        PopupButtonID="txttodate" TargetControlID="txttodate">
                                    </cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2">
                                    <label for="ddlMember">Member ID : </label>
                                    <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member." class="form-control select2"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger" Text="Search" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-lg-12">
                                    <div class="ibox float-e-margins">
                                        <div class="ibox-content collapse show">
                                            <div class="widgets-container">
                                                <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>S.No</th>
                                                            <th>Txn ID</th>
                                                            <th>Payer Name</th>
                                                            <th>Source</th>
                                                            <th>Mobile</th>
                                                            <th>Txn Type</th>
                                                            <th>Bank Ref No</th>
                                                            <th>Txn Amount</th>
                                                            <th>Charges</th>
                                                            <th>Settled Amount</th>
                                                            <th>Date</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%# Container.ItemIndex + 1 %></td>
                                                                    <td><%# Eval("txn_id") %></td>
                                                                    <td><%# Eval("payer_name") %></td>
                                                                    <td><%# Eval("source") %></td>
                                                                    <td><%# Eval("payer_mobile") %></td>
                                                                    <td><%# Eval("payment_type") %></td>
                                                                    <td><%# Eval("bank_ref_num") %></td>
                                                                    <td><%# Eval("amount") %></td>
                                                                    <td><%# Eval("charges_gst") %></td>
                                                                    <td><%# Eval("settled_amount") %></td>
                                                                    <td><%# Eval("AddDate") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
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

