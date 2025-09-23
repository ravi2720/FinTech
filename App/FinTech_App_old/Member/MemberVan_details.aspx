<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="MemberVan_details.aspx.cs" Inherits="Member_memberupi_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <div class="main-content-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div role="tabpanel" class="profile-tab-pane tab-pane " id="UPIID">
                        <div class="card-group padding-20" id="accordion" role="tablist" aria-multiselectable="true">
                            <div class="card card-primary">
                                <div class="card-header"><b>Van Details</b>  </div>
                                <div class="card-body">
                                    <div class="row">
                                        <table class="table" style="background: #011f3f; color: white">
                                            <tbody>
                                                <asp:Repeater ID="rptVANDetails" runat="server">
                                                    <ItemTemplate>

                                                        <tr>
                                                            <td>Name :</td>
                                                            <td>
                                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Mobile :</td>
                                                            <td>
                                                                <asp:Label ID="lblPAN" runat="server" Text='<%#Eval("Mobile") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>AccountNo :</td>
                                                            <td>
                                                                <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("AccountNo") %>'></asp:Label></td>
                                                        </tr>

                                                        <tr>
                                                            <td>IFSC :</td>
                                                            <td>
                                                                <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("IFSC") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Created At :</td>
                                                            <td>
                                                                <asp:Label ID="lblAddDate" runat="server" Text='<%#Eval("AddDate") %>'></asp:Label></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <br />
                                    <div class="row row-sm">
                                        <div class="col-lg-12">
                                            <div class="card">
                                                <div class="card-body">

                                                    <div class="table-responsive">
                                                        <table id="file-datatable" class="border-top-0  table table-bordered text-nowrap key-buttons border-bottom">
                                                            <thead>



                                                                <tr>
                                                                    <th class="border-bottom-0">SNO</th>
                                                                    <th class="border-bottom-0">Txn ID</th>
                                                                    <th class="border-bottom-0">Remitter Name</th>
                                                                    <th class="border-bottom-0">Remitter AccountNo</th>
                                                                    <th class="border-bottom-0">Remitter BankName</th>
                                                                    <th class="border-bottom-0">Beneficiary Ifsc</th>
                                                                    <th class="border-bottom-0">Bank Ref No</th>
                                                                    <th class="border-bottom-0">Amount</th>
                                                                    <th class="border-bottom-0">Date</th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%# Container.ItemIndex + 1 %></td>
                                                                            <td><%# Eval("transactionRequestId") %></td>
                                                                            <td><%# Eval("remitterName") %></td>
                                                                            <td><%# Eval("remitterAccountNumber") %></td>
                                                                            <td><%# Eval("remitterBankName") %></td>
                                                                            <td><%# Eval("beneficiaryIfsc") %></td>
                                                                            <td><%# Eval("bankTxnIdentifier") %></td>
                                                                            <td><%# Eval("Amount") %></td>
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
                    </div>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </div>
    <script>


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

            // Action(DataObject);
        }

        var req = Sys.WebForms.PageRequestManager.getInstance();
        req.add_beginRequest(function () {

        });
        req.add_endRequest(function () {
            var table = $('#file-datatable').DataTable({
                buttons: ['copy', 'excel', 'pdf', 'colvis'],
                language: {
                    searchPlaceholder: 'Search...',
                    scrollX: "100%",
                    sSearch: '',
                }
            });
            table.buttons().container()
                .appendTo('#file-datatable_wrapper .col-md-6:eq(0)');
        });
        function showModal() {
            $('#myModal').modal();
        }


    </script>
</asp:Content>

