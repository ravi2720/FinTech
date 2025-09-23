<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="DayBook.aspx.cs" Inherits="Admin_DayBook" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="content-wrapper">
        <section class="content">
            <div class="card">
                <h4 class="card-header"><%= Request.QueryString["Type"].ToString() %> User Daybook</h4>
                <div class="card-body">
                    <asp:UpdatePanel runat="server" ID="upd">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select Date" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtFromDate"
                                        TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>

                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Search &gt;&gt;" OnClick="btnSearch_Click"
                                        ValidationGroup="vg1" />
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="ibox float-e-margins">
                                        <div class="ibox-content collapse show">
                                            <div class="widgets-container">
                                                <div>

                                                    <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th>S.No</th>
                                                                <th>LoginID</th>
                                                                <th>TotalHits</th>
                                                                <th>TotalAmount(₹)</th>
                                                                <th>SuccessHits</th>
                                                                <th>SuccessAmount(₹)</th>
                                                                <th>FailedHits</th>
                                                                <th>FailedAmount(₹)</th>
                                                                <th>PendingHits</th>
                                                                <th>PendingAmount(₹)</th>
                                                                <th>DirectCommission(₹)</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                                        <td><%# Eval("LoginID") %></td>
                                                                        <td><%# Eval("Hit") %></td>
                                                                        <td><%# Eval("TAmount") %></td>
                                                                        <td><%# Eval("SuccessCount") %></td>
                                                                        <td>
                                                                            <span class='<%# (Eval("SuccessSum").ToString()=="0"?"":"btn btn-success") %>'>
                                                                                <%# Eval("SuccessSum") %>
                                                                            </span>
                                                                        </td>
                                                                        <td><%# Eval("FailedCount") %></td>
                                                                        <td><%# Eval("FailedSum") %></td>
                                                                        <td>
                                                                            <span class='<%# (Eval("PendingCount").ToString()=="0"?"":"btn btn-warning") %>'>
                                                                                <%# Eval("PendingCount") %>
                                                                            </span>
                                                                        </td>
                                                                        <td>
                                                                            <span class='<%# (Eval("PendingSum").ToString()=="0.00"?"":"btn btn-warning") %>'>
                                                                                <%# Eval("PendingSum") %>
                                                                            </span>
                                                                        </td>
                                                                        <td>
                                                                            <span class='<%# (Eval("DirectCommission").ToString()=="0"?"":"btn btn-success") %>'>
                                                                                <%# Eval("DirectCommission") %>
                                                                            </span>
                                                                        </td>
                                                                       
                                                                    </tr>
                                                                </ItemTemplate>

                                                            </asp:Repeater>
                                                        </tbody>
                                                        <tfoot>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td><strong runat="server" id="tSuccessSum"></strong></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td><strong runat="server" id="tcommission"></strong></td>
                                                        </tfoot>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

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





