<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="MATMHistory.aspx.cs" Inherits="Admin_MATMHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card card-primary">
                        <h4 class="card-header">Manage MATM
                                <span class="btn btn-success">Success <span class="badge" runat="server" id="lblSCount">0</span></span><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="btn btn-success">Success <span class="badge" runat="server" id="lblSsum">0</span></span>
                            <span class="btn btn-warning">Pending <span class="badge" runat="server" id="lblPCount">0</span></span><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="btn btn-warning">Pending <span class="badge" runat="server" id="lblPSum">0</span></span>
                            <span class="btn btn-danger">Failed <span class="badge" runat="server" id="lblFCount">0</span></span><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="btn btn-danger">Failed <span class="badge" runat="server" id="lblFSum">0</span></span>
                        </h4>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <strong>Search</strong>
                                    <asp:TextBox runat="server" ID="txtSearch" placeholder="Enter Search" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <strong>From Date</strong>

                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                        TargetControlID="txtfromdate">
                                    </cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2">
                                    <strong>To Date</strong>

                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="MM-dd-yyyy" Animated="False"
                                        PopupButtonID="txttodate" TargetControlID="txttodate">
                                    </cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2">
                                    <strong>Status</strong>
                                    <asp:DropDownList runat="server" ID="dllStatus" CssClass="form-control select2">
                                        <asp:ListItem Text="Select Status" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Success" Value="Success"></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                        <asp:ListItem Text="Failed" Value="Failed"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label for="ddlMember">Member ID : </label>
                                    <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member." AutoPostBack="true" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged" class="form-control select2"></asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <br />
                                    <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click" CausesValidation="false"
                                        class="btn btn-primary" />
                                </div>
                            </div>

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
                                                        <th>Reason</th>
                                                        <th>Bank RRN</th>
                                                        <th>Card No</th>
                                                        <th>Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="gvTransactionHistory" runat="server" OnItemCommand="gvTransactionHistory_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Container.ItemIndex + 1 %></td>
                                                                <td>
                                                                      <asp:Button runat="server" ID="btnCheckStatus" CommandName="CheckStatus" CommandArgument='<%# Eval("RefID") %>' Text="Check Status" Visible='<%# (Eval("status").ToString().ToUpper()=="PENDING"?true:false) %>' CssClass="btn btn-danger" />
                                                                </td>
                                                                <td><%# Eval("NAME") %><br />
                                                                    <%# Eval("LOGINID") %></td>
                                                                <td><%# Eval("AddDate") %></td>
                                                                <td><%# Eval("Amount") %></td>
                                                                <td><%# Eval("RefID") %></td>
                                                                <td><%# Eval("Message") %></td>
                                                                <td><%# Eval("RRN") %></td>
                                                                <td><%# Eval("CardNo") %></td>
                                                                <td class='<%# (Eval("status").ToString().ToUpper()=="SUCCESS"?"btn btn-success":"btn btn-danger") %>'>
                                                                    <%# Eval("Status") %>
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



