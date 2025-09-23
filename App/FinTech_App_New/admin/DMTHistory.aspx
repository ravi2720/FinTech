<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="DMTHistory.aspx.cs" Inherits="Admin_DMTHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style3 {
            width: 199px;
        }
        .auto-style4 {
            text-align: left;
            width: 324px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card">
                        <h4 class="card-header">DMT History
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
                                                            <th>Check Status</th>
                                                            <th>S.No</th>
                                                            <th class="auto-style3">Member Name</th>
                                                            <th>AddDate</th>
                                                            <th>Amount</th>
                                                            <th>Surcharge</th>
                                                            <th>TransID</th>
                                                            <th>Bank Number</th>
                                                            <th class="auto-style4">Bank Details&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; IFSC Code</th>
                                                            <th>Status</th>
                                                            <th>Receipt</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="gvTransactionHistory" runat="server" OnItemCommand="gvTransactionHistory_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button runat="server" OnClientClick="return confirm('Do You Want To Force Failed on Your responsibility First Check All Status of Transaction Then take action')" ID="btnFailed" CommandName="Failed" CommandArgument='<%# Eval("OrderID") %>' Text="Force Failed"  CssClass="btn btn-danger" />
                                                                        <asp:Button runat="server" OnClientClick="return confirm('Do You Want To Force Success on Your responsibility')" ID="btnSuccess" CommandName="Success" CommandArgument='<%# Eval("OrderID") %>' Text="Force Success" Visible='<%# (Eval("status").ToString().ToUpper()=="PENDING"?true:false) %>' CssClass="btn btn-success" />
                                                                        <asp:Button runat="server" ID="btnCheckStatus" CommandName="CheckStatus" CommandArgument='<%# Eval("OrderID") %>' Text="Check Status" Visible='<%# (Eval("status").ToString().ToUpper()=="PENDING"?true:false) %>' CssClass="btn btn-danger" />
                                                                    </td>
                                                                    <td><%# Container.ItemIndex + 1 %></td>
                                                                    <td><%# Eval("NAME") %><br />
                                                                        <%# Eval("LOGINID") %></td>
                                                                    <td><%# Eval("AddDate") %></td>
                                                                    <td><%# Eval("Amount") %></td>
                                                                    <td><%# Eval("Surcharge") %></td>
                                                                    <td><%# Eval("OrderID") %></td>
                                                                    <td><%# Eval("VendorID") %></td>
                                                                    <td>
                                                                        <table class="table table-responsive">
                                                                            <tr>
                                                                              
                                                                            <tr>
                                                                                <td><%# Eval("accountNumber") %></td>
                                                                                <td>
                                                                                    <%# Eval("ifsc") %>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                        <span class='<%#(Eval("Status").ToString().ToUpper()=="SUCCESS"?"btn btn-success":"btn btn-danger") %>'><%# (Eval("Status").ToString().ToUpper()=="PROCESSED"?"Sucess":Eval("Status").ToString()) %></span>
                                                                        <asp:HiddenField runat="server" ID="hdnRefNumber" Value='<%# Eval("OrderID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <a style="color: red;" href='<%# "ReceiptDMTNew.aspx?GUID="+Eval("OrderID").ToString() %>'>Receipt</a>
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

