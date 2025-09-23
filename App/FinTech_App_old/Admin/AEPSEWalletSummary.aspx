<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AEPSEWalletSummary.aspx.cs" Inherits="Admin_AEPSEWalletSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="content-wrapper">
        <section class="content">
            <div class="card">
                <h4 class="card-header">Manage Wallet</h4>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <label for="ddlMember">From Date : </label>
                            <asp:TextBox ID="txtfromdate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                TargetControlID="txtfromdate">
                            </cc1:CalendarExtender>

                        </div>
                        <div class="col-md-2">
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
                        <div class="col-md-2">
                            <label for="ddlMember">Select Mode : </label>
                            <asp:DropDownList runat="server" ID="dllDRCR" CssClass="form-control">
                                <asp:ListItem Text="Select Mode" Value="0"></asp:ListItem>
                                <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                <asp:ListItem Text="CR" Value="CR"></asp:ListItem>
                            </asp:DropDownList>
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
                                        <div>

                                            <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>SL</th>
                                                        <th>MemberID</th>
                                                        <th>Name</th>
                                                        <th>Amount</th>
                                                        <th>Balance</th>
                                                        <th>Factor</th>
                                                        <th>Narration</th>
                                                        <th>TransferDate</th>
                                                    </tr>
                                                </thead>
                                                <tfoot>
                                                    <tr>
                                                        <th>SL</th>
                                                        <th>MemberID</th>
                                                        <th>Name</th>
                                                        <th>Amount</th>
                                                        <th>Balance</th>
                                                        <th>Factor</th>
                                                        <th>Narration</th>
                                                        <th>TransferDate</th>
                                                    </tr>
                                                </tfoot>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="rptData">
                                                        <ItemTemplate>
                                                            <tr class="row1">
                                                                <td><%#Container.ItemIndex+1 %>
                                            
                                                                </td>
                                                                <td><%#Eval("LoginID") %></td>
                                                                <td><%#Eval("NAME") %></td>
                                                                <td><%#Eval("AMOUNT") %></td>
                                                                <td><%#Eval("BALANCE") %></td>
                                                                <td>
                                                                    <span class='<%# (Eval("factor").ToString().ToUpper()=="CR"?"btn btn-success":"btn btn-danger") %>'>
                                                                        <%#Eval("FACTOR") %>
                                                                    </span>
                                                                </td>
                                                                <td><%#Eval("NARRATION") %></td>
                                                                <td><%#Eval("ADDDATE") %></td>

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

