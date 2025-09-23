<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AEPSDayBook.aspx.cs" Inherits="Admin_AEPSDayBook" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="content-wrapper">
        <section class="content">
            <div class="card">
                <h4 class="card-header">AEPS User Daybook</h4>
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
                                                                        <td><%# Eval("SuccessSum") %></td>
                                                                        <td><%# Eval("FailedCount") %></td>
                                                                        <td><%# Eval("FailedSum") %></td>
                                                                        <td><%# Eval("PendingCount") %></td>
                                                                        <td><%# Eval("PendingCount") %></td>
                                                                        <td><%# Eval("DirectCommission") %></td>
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
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
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





