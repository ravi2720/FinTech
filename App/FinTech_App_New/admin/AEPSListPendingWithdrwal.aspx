<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AEPSListPendingWithdrwal.aspx.cs" Inherits="Admin_AEPSListPendingWithdrwal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="box">
                            <div class="box-body table-responsive">
                                <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                    <thead>
                                        <tr>

                                            <th>S.No</th>
                                            <th>Action</th>
                                            <th>AadharNumber</th>
                                            <th>Amount</th>
                                            <th>Narration</th>
                                            <th>Bank TransID</th>
                                            <th>Status</th>
                                            <th>AddDate</th>
                                            <th>View</th>
                                        </tr>
                                    </thead>
                                    <tbody>

                                        <asp:Repeater ID="gvTransactionHistory" runat="server">

                                            <ItemTemplate>
                                                <button type="button" class="btn btn-info btn-sm" data-toggle="modal" data-target="#myModal<%#Eval("ID") %>">Edit</button>
                                                <div class="modal fade" id='myModal<%#Eval("ID") %>' role="dialog">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                <h4 class="modal-title">(<%# Eval("LoginiD") %>)</h4>
                                                            </div>
                                                            <div class="modal-body">
                                                                <table class="table table-responsive table-condensed table-bordered">
                                                                    <tr style="display: none">
                                                                        <td>Deposit Bank Details</td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtDepositBankDetails" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Bank TransID </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtTransID" runat="server" CssClass="form-control" Text='<%# Eval("TransactionID") %>'></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Remark </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" Text='<%# Eval("RequestRemark") %>' TextMode="MultiLine"></asp:TextBox></td>
                                                                    </tr>

                                                                </table>
                                                                <asp:HiddenField ID="hddMsrNo" runat="server" Value='<%#Eval("MsrNo") %>' />
                                                                <asp:HiddenField ID="hddID" runat="server" Value='<%#Eval("ID") %>' />

                                                            </div>
                                                            <div class="modal-footer">
                                                                <asp:Button ID="btnOrderID" OnClick="btnOrderID_Click" runat="server" Text="Update" class="btn btn-primary" />
                                                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>


                                        </asp:Repeater>
                            </div>
                            </td>
                    </tr>
                </table>
                        </div>
                </ContentTemplate>
                <Triggers>
                    <%--<asp:PostBackTrigger ControlID="btnexportExcel" />
            <asp:PostBackTrigger ControlID="btnexportWord" />
            <asp:PostBackTrigger ControlID="btnexportpdf" />--%>
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

