<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="CashDepositTransaction.aspx.cs" Inherits="Admin_CashDepositTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Cash Deposit History</div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="box">

                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <asp:TextBox runat="server" ID="txtSearch" placeholder="Enter Search" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtfromdate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                                    TargetControlID="txtfromdate">
                                                </cc1:CalendarExtender>

                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txttodate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="MM-dd-yyyy" Animated="False"
                                                    PopupButtonID="txttodate" TargetControlID="txttodate">
                                                </cc1:CalendarExtender>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList runat="server" ID="dllStatus" CssClass="form-control select2">
                                                    <asp:ListItem Text="Select Status" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Success" Value="Success"></asp:ListItem>
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="Failed" Value="Failed"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger" Text="Search" OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                        <br />
                                        <br />

                                        <div class="col-md-12">

                                            <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                <thead>
                                                    <tr>
                                                        <th>S.No</th>
                                                        <th>Member Name</th>
                                                        <th>AddDate</th>
                                                        <th>Amount</th>
                                                        <th>TransID</th>
                                                        <th>Account Number</th>
                                                        <th>Bank RRN</th>
                                                        <th>Status</th>
                                                        <th>Receipt</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Container.ItemIndex + 1 %></td>
                                                                <td><%# Eval("NAME") %><br />
                                                                    <%# Eval("LOGINID") %></td>
                                                                <td><%# Eval("AddDate") %></td>
                                                                <td><%# Eval("Amount") %></td>
                                                                <td><%# Eval("referenceno") %></td>
                                                                <td><%# Eval("accountnumber") %></td>
                                                                <td><%# Eval("bankrrn") %></td>
                                                                <td>
                                                                    <%# Eval("Status") %>
                                                                    <asp:HiddenField runat="server" ID="hdnRefNumber" Value='<%# Eval("referenceno") %>' />
                                                                </td>
                                                                <td>
                                                                    <a style="color: red;" href='<%# "ReceiptCashDeposit.aspx?GUID="+Eval("referenceno").ToString() %>'>Receipt</a>
                                                                </td>
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

