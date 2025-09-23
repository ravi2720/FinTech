<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="NSDLPanHistory.aspx.cs" Inherits="Admin_NSDLPanHistory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card">
                        <h4 class="card-header">NSDL Pan History
                             <span class="btn btn-success">Success <span class="badge" runat="server" id="lblSCount">0</span></span><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="btn btn-success">Success <span class="badge" runat="server" id="lblSsum">0</span></span>
                            <span class="btn btn-warning">Pending <span class="badge" runat="server" id="lblPCount">0</span></span><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="btn btn-warning">Pending <span class="badge" runat="server" id="lblPSum">0</span></span>
                            <span class="btn btn-danger">Failed <span class="badge" runat="server" id="lblFCount">0</span></span><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="btn btn-danger">Failed <span class="badge" runat="server" id="lblFSum">0</span></span>
                        </h4>
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
                                    <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member." AutoPostBack="true" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged" class="form-control select2"></asp:DropDownList>
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
                                                            <th>Action</th>
                                                            <th>Member Name</th>
                                                            <th>AddDate</th>
                                                            <th>Amount</th>
                                                            <th>TransID</th>
                                                            <th>UTR No</th>
                                                            <th>Status</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="gvTransactionHistory" runat="server" OnItemCommand="gvTransactionHistory_ItemCommand1">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%# Container.ItemIndex + 1 %></td>
                                                                    <td>
                                                                        <asp:Button runat="server" ID="btnCheckStatus" Visible='<%# (Eval("Status").ToString().ToUpper()=="PENDING"?true:false) %>' OnClientClick="return confirm('Do You Want To Check Status')" CssClass="btn btn-success" Text="Check Status" CommandName="ck" CommandArgument='<%# Eval("ID") %>' />
                                                                    </td>
                                                                    <td><%# Eval("NAME") %><br />
                                                                        <%# Eval("LOGINID") %></td>
                                                                    <td><%# Eval("AddDate") %></td>
                                                                    <td><%# Eval("CouponCharge") %></td>
                                                                    <td><%# Eval("OrderID") %></td>
                                                                    <td><%# Eval("utr_no") %></td>
                                                                    <td>
                                                                        <span class='<%#(Eval("Status").ToString().ToUpper()=="SUCCESS"?"btn btn-success":"btn btn-danger") %>'><%# (Eval("Status").ToString().ToUpper()=="PROCESSED"?"Sucess":Eval("Status").ToString()) %></span>
                                                                        <asp:HiddenField runat="server" ID="hdnRefNumber" Value='<%# Eval("OrderID") %>' />
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


