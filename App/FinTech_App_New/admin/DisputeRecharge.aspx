<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="DisputeRecharge.aspx.cs" Inherits="Admin_DisputeRecharge" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="updateRolePanel">
        <ContentTemplate>
            <div class="content-wrapper">
                <section class="content">
                    <div class="card panel-primary">
                        <div class="card-header">Dispute Recharge Transaction</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <label>From Date</label>
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                        TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2">
                                    <label>To Date</label>
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="MM-dd-yyyy" Animated="False"
                                        PopupButtonID="txttodate" TargetControlID="txttodate">
                                    </cc1:CalendarExtender>
                                </div>

                                <div class="col-md-2">
                                    <label>To Date</label>
                                    <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" CssClass="form-control"></asp:Button>
                                </div>
                            </div>
                            <br />
                            <div class="table-responsive">
                                <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                    <thead>
                                        <tr>
                                        </tr>
                                        <tr>
                                            <th>SNO</th>
                                            <th>Action</th>
                                            <th>TXID</th>
                                            <th>Operator</th>
                                            <th>Number</th>
                                            <th>Amount</th>
                                            <th>Status</th>
                                            <th>Operator Id</th>
                                            <th>Recharge By</th>
                                            <th>Date Time</th>

                                            <th>Receipt</th>


                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="rptDataRecharge" OnItemCommand="rptDataRecharge_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.ItemIndex+1 %></td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnCancel" OnClientClick="return confirm('Do you want to Cancel Dispute')" CommandName="Cancel" Text="Cancel" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-danger" />
                                                        <asp:Button runat="server" ID="btnDispute" OnClientClick="return confirm('Do you want to clear Dispute')" CommandName="Dispute" Text="Clear" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-danger" />
                                                    </td>
                                                    <td><%# Eval("TransID") %></td>
                                                    <td><%# Eval("OPERATORNAME") %></td>
                                                    <td><%# Eval("MobileNo") %></td>
                                                    <td><%# Eval("Amount") %></td>
                                                    <td><%# Eval("Status") %></td>
                                                    <td><%# Eval("APIMessage") %></td>
                                                    <td>
                                                        <%# Eval("LOGINID") %><br />
                                                        <%# Eval("Name") %>

                                                    </td>
                                                    <td><%# Eval("CreatedDate") %></td>
                                                    <td>
                                                        <a style="color: red" href='<%# "BillPayReceipt.aspx?Transid="+Eval("TransID") %>'>View Receipt</a>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
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


