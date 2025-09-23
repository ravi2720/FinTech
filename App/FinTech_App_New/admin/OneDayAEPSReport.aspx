<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="OneDayAEPSReport.aspx.cs" Inherits="Admin_OneDayAEPSReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="panel panel-primary">
                <div class="panel-heading">Single Day Report</div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <asp:TextBox ID="txtfromdate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:calendarextender runat="server" id="txtfromdate_ce" format="MM-dd-yyyy" popupbuttonid="txtfromdate"
                                    targetcontrolid="txtfromdate">
                                                </cc1:calendarextender>

                            </div>
                           
                           
                            <div class="col-md-3">
                                <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger" Text="Search" OnClick="btnSearch_Click" />
                            </div>
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
                                                        <th>SL</th>
                                                        <th>MemberID</th>
                                                        <th>Name</th>
                                                        <th>Amount</th>
                                                    </tr>
                                                </thead>
                                                <tfoot>
                                                    <tr>
                                                        <th>SL</th>
                                                        <th>MemberID</th>
                                                        <th>Name</th>
                                                        <th>Amount</th>
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

