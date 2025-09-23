<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ActiveDeActiveService.aspx.cs" Inherits="Admin_ActiveDeActiveService" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        table td {
            border: 1px solid #ddd;
            padding: 2px;
        }

        .ajax__calendar_active {
            border: 1px solid #ddd;
            padding: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Active DeActive Service</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtfromdate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                            TargetControlID="txtfromdate">
                                        </cc1:CalendarExtender>

                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txttodate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="MM-dd-yyyy" Animated="False"
                                            PopupButtonID="txttodate" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger" Text="Search" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                    <thead>
                                        <tr>
                                            <th>S.No</th>
                                            <th>Member Name</th>
                                            <th>MobileNo</th>
                                            <th>Service</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="gvTransactionHistory" runat="server" OnItemDataBound="gvTransactionHistory_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.ItemIndex + 1 %></td>
                                                    <td><%# Eval("MemberName") %><br />
                                                        <%# Eval("LOGINID") %></td>
                                                    <td><%# Eval("Mobile") %></td>
                                                    <td>
                                                        <asp:HiddenField runat="server" ID="hdnMsrno" Value='<%# Eval("Msrno") %>' />
                                                        <div class="col-md-12">
                                                        <asp:DataList runat="server" RepeatDirection="Horizontal" ID="dltList">
                                                            <ItemTemplate>
                                                                <div class="col-md-2">
                                                                <%# Eval("Name") %>
                                                                <span class='<%# (Convert.ToBoolean(Eval("ActiveOrNot"))==true?"btn btn-success":"btn btn-danger") %>'><%# (Convert.ToBoolean(Eval("ActiveOrNot"))==true?"Active":"DeActive")  %></span>
                                                                    </div>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                            </div>

                                                        <%--<span class='<%# (Convert.ToBoolean(Eval("ActiveOrNot"))==true?"btn btn-success":"btn btn-danger") %>'><%# (Convert.ToBoolean(Eval("ActiveOrNot"))==true?"Active":"DeActive") %></span>--%>
                                                        <%--<%# HttpUtility.HtmlDecode(Eval("Service").ToString()) %>--%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
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

