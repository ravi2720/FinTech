<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="PayoutQueuedTransaction.aspx.cs" Inherits="Admin_PayoutQueuedTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Queued History</div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="box">

                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <asp:TextBox runat="server" ID="txtSearch" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtfromdate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                                    TargetControlID="txtfromdate">
                                                </cc1:CalendarExtender>

                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txttodate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="MM-dd-yyyy" Animated="False"
                                                    PopupButtonID="txttodate" TargetControlID="txttodate">
                                                </cc1:CalendarExtender>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList runat="server" ID="dllStatus" CssClass="form-control">
                                                    <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Processing" Value="processing"></asp:ListItem>
                                                    <asp:ListItem Text="failed" Value="failed"></asp:ListItem>
                                                    <asp:ListItem Text="processed" Value="processed"></asp:ListItem>
                                                    <asp:ListItem Text="Queued" Value="Queued"></asp:ListItem>
                                                    <asp:ListItem Text="Refunded" Value="Refunded"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger" Text="Search" OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                        <br />
                                        <br />

                                        <div class="col-md-12">

                                            <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <asp:Button runat="server" ID="btnAllProcess" OnClick="btnAllProcess_Click" Text="SendAll" CssClass="btn btn-danger" /></th>
                                                        <th>
                                                            <label>Transaction Type</label>
                                                            <asp:DropDownList runat="server" ID="dllType" CssClass="form-control">
                                                                <asp:ListItem Text="Select Type" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="IMPS" Value="IMPS"></asp:ListItem>
                                                                <asp:ListItem Text="NEFT" Value="NEFT"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </th>
                                                        <th>
                                                            <asp:Button runat="server" ID="btnCheckStatus" OnClick="btnCheckStatus_Click" Text="CheckStatusAll" OnClientClick="return confirm('Do You want to checkstatus')" CssClass="btn btn-danger" /></th>
                                                        </th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblRazorPayBalance" ForeColor="Red" Font-Size="X-Large"></asp:Label>
                                                        </th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblICICI" ForeColor="Red" Font-Size="X-Large"></asp:Label>

                                                        </th>
                                                        <th></th>
                                                        <th></th>
                                                        <th></th>
                                                        <th></th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <thead>
                                                    <tr>
                                                        <th>S.No</th>
                                                        <th>Action</th>
                                                        <th>Member Name</th>
                                                        <th>AddDate</th>
                                                        <th>RazorpayID</th>
                                                        <th>Amount</th>
                                                        <th>Trans Mode</th>
                                                        <th>Trans Hit</th>
                                                        <th>Message</th>
                                                        <th>TransID</th>
                                                        <th>Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="gvTransactionHistory" runat="server" OnItemCommand="gvTransactionHistory_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Container.ItemIndex + 1 %></td>
                                                                <td>
                                                                    <asp:Button runat="server" ID="btnReject"
                                                                        Visible='<%# (Eval("RequestStatus").ToString().ToUpper()=="REFUNDED" || Eval("RequestStatus").ToString().ToUpper()=="PROCESSED"?false:true) %>'
                                                                        Text="Reject" OnClientClick="return confirm('DO You want to Reject')" CssClass="btn btn-danger" CommandName="Reject" CommandArgument='<%# Eval("ID") %>' />

                                                                    <asp:Button runat="server" ID="btnProcess"
                                                                        Visible='<%# (Eval("RequestStatus").ToString().ToUpper()=="REFUNDED" || Eval("RequestStatus").ToString().ToUpper()=="PROCESSED" || Eval("RequestStatus").ToString().ToUpper()=="PROCESSING" || Eval("RequestStatus").ToString().ToUpper()=="FAILED"?false:true) %>'
                                                                        OnClientClick="return confirm('DO You want to Send')" CommandName="Processed" CommandArgument='<%# Eval("ID") %>' Text="Send" CssClass="btn btn-primary" />

                                                                    <asp:Button runat="server" ID="btmReSend"
                                                                        Visible='<%# (Eval("RequestStatus").ToString().ToUpper()=="FAILED"?true:false) %>'
                                                                        OnClientClick="return confirm('DO You want to ReSend')" CommandName="ReTry" CommandArgument='<%# Eval("ID") %>' Text="ReTry" CssClass="btn btn-primary" />

                                                                </td>
                                                                <td><%# Eval("NAME") %><br />
                                                                    <%# Eval("LOGINID") %></td>
                                                                <td><%# Eval("RequestDate") %></td>
                                                                <td><%# Eval("TransactionID") %></td>
                                                                <td><%# Eval("Amount") %></td>
                                                                <td><%# Eval("TransMode") %></td>
                                                                <td><%# Eval("ManualSystem") %></td>
                                                                <td>
                                                                    <%# Eval("StatusMessage") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("RequestID") %>
                                                                    <asp:HiddenField runat="server" ID="hdnMsrno" Value='<%# Eval("Msrno") %>' />
                                                                    <asp:HiddenField runat="server" ID="hdnBankID" Value='<%# Eval("BankID") %>' />
                                                                    <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>' />
                                                                    <asp:HiddenField runat="server" ID="hdnAmount" Value='<%# Eval("Amount") %>' />
                                                                    <asp:HiddenField runat="server" ID="hdnTransMode" Value='<%# Eval("TransMode") %>' />
                                                                    <asp:HiddenField runat="server" ID="hdnRequestID" Value='<%# Eval("RequestID") %>' />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("RequestStatus") %>
                                                                    <asp:HiddenField runat="server" ID="hdnRefNumber" Value='<%# Eval("RequestID") %>' />
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

