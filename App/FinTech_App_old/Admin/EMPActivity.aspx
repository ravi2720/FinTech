<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="EMPActivity.aspx.cs" Inherits="Admin_EMPActivity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="content-wrapper">
        <section class="content">
            <div class="card">
                <h4 class="card-header">Manage Activity</h4>
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
                            <label for="ddlMember">EMP ID : </label>
                            <asp:DropDownList runat="server" ID="ddlEmp" ToolTip="Select EMP." class="form-control select2"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label for="ddlMember">Service : </label>
                            <asp:DropDownList runat="server" ID="ddlService" ToolTip="Select Service." class="form-control select2"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
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
                                                        <th>Member Name</th>
                                                        <th>Emp Name</th>
                                                        <th>Service Name</th>
                                                        <th>Description</th>
                                                        <th>Status</th>
                                                        <th>Date</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="rptData">
                                                        <ItemTemplate>
                                                            <tr class="row1">
                                                                <td><%#Container.ItemIndex+1 %></td>
                                                                <td><%#Eval("MemberName") %>(<%# Eval("LoginID") %>)</td>
                                                                <td><%#Eval("EMPName") %></td>
                                                                <td><%#Eval("ServiceName") %></td>
                                                                <td><%#Eval("Description") %></td>
                                                                <td><%#Eval("Status") %></td>
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

