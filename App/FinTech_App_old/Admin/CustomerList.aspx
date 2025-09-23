<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="CustomerList.aspx.cs" Inherits="Admin_CustomerList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card">
                        <h4 class="card-header">Customer History</h4>

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
                                    <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member."  class="form-control select2"></asp:DropDownList>
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
                                                            <th>Member Name</th>
                                                            <th>Mobile</th>
                                                            <th>Pan</th>
                                                            <th>Aadhar</th>
                                                            <th>Address</th>
                                                            <th>KYC</th>
                                                            <th>Limit</th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="gvTransactionHistory" runat="server" OnItemCommand="gvTransactionHistory_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%# Container.ItemIndex + 1 %></td>
                                                                    <td><%# Eval("NAME") %><br />
                                                                        <%# Eval("LOGINID") %></td>
                                                                    <td><%# Eval("MOBILE") %></td>
                                                                    <td><%# Eval("Pan") %></td>
                                                                    <td><%# Eval("Aadhar") %></td>
                                                                    <td><%# Eval("Address") %></td>
                                                                    <td><%# Eval("ISKYC") %></td>
                                                                    <td>
                                                                        <asp:TextBox Width="200" runat="server" ID="txtLimit" Text='<%# Eval("Limit") %>' CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button runat="server" ID="btnSetLimit" CssClass="btn btn-danger" Text="Set Limit" CommandName="SetLimit" CommandArgument='<%# Eval("ID") %>' />
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



