<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="VirtualAccount_Details.aspx.cs" Inherits="Admin_UPIDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div role="tabpanel" class="profile-tab-pane tab-pane " id="UPIID">
                        <div class="panel-group padding-20" id="accordion" role="tablist" aria-multiselectable="true">
                            <div class="panel panel-primary">
                                <div class="panel-heading">Virtual Account Details  </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <table class="table" style="background: #011f3f; color: white">
                                            <tbody>
                                                <asp:Repeater ID="rptVirtualAccountDetails" runat="server" OnItemDataBound="rptVirtualAccountDetails_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>Account Details :</td>
                                                            <td>
                                                                <asp:HiddenField ID="hdnVirtualID" runat="server" Value='<%#Eval("ID") %>' />
                                                                <asp:Repeater ID="rptVirtualAccounts" runat="server">
                                                                    <ItemTemplate>
                                                                        <span style="padding: 2px"><%#Eval("account_number") %>/<%#Eval("account_ifsc") %>/(<%#Eval("payment_modes") %>)</span><br />
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Created At :</td>
                                                            <td>
                                                                <asp:Label ID="lblAddDate" runat="server" Text='<%#Eval("AddDate") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Reference Number :</td>
                                                            <td>
                                                                <asp:Label ID="lblReferenceNumber" runat="server" Text='<%#Eval("reference_number") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>UDF1 :</td>
                                                            <td>
                                                                <asp:Label ID="lblUDF1" runat="server" Text='<%#Eval("UDF1") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>UDF2 :</td>
                                                            <td>
                                                                <asp:Label ID="lblUDF2" runat="server" Text='<%#Eval("UDF2") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>UDF3 :</td>
                                                            <td>
                                                                <asp:Label ID="lblUDF3" runat="server" Text='<%#Eval("UDF3") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Status :</td>
                                                            <td>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="box">
                                            <div class="box-body">
                                                <div class="col-md-12">
                                                    <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>S.No</th>
                                                                <th>Txn ID</th>
                                                                <th>Payer Name</th>
                                                                <th>Source</th>
                                                                <th>Mobile</th>
                                                                <th>Txn Type</th>
                                                                <th>Bank Ref No</th>
                                                                <th>Txn Amount</th>
                                                                <th>Charges</th>
                                                                <th>Settled Amount</th>
                                                                <th>Date</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                                        <td><%# Eval("txn_id") %></td>
                                                                        <td><%# Eval("payer_name") %></td>
                                                                        <td><%# Eval("source") %></td>
                                                                        <td><%# Eval("payer_mobile") %></td>
                                                                        <td><%# Eval("payment_type") %></td>
                                                                        <td><%# Eval("bank_ref_num") %></td>
                                                                        <td><%# Eval("amount") %></td>
                                                                        <td><%# Eval("charges_gst") %></td>
                                                                        <td><%# Eval("settled_amount") %></td>
                                                                        <td><%# Eval("AddDate") %></td>
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
