<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="memberupi_details.aspx.cs" Inherits="Member_memberupi_details" %>

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
                                <div class="card-header"><b>UPI Details</b>  </div>
                                <div class="card-body">
                                    <div class="row">
                                        <table class="table" style="background: #011f3f; color: white">
                                            <tbody>
                                                <asp:Repeater ID="rptUPIDetails" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>UPI Id :</td>
                                                            <td>
                                                                <asp:Label ID="lblUpiID" runat="server" Text='<%#Eval("ICICIUPI") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Name :</td>
                                                            <td>
                                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>PAN :</td>
                                                            <td>
                                                                <asp:Label ID="lblPAN" runat="server" Text='<%#Eval("Pan") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Category :</td>
                                                            <td>
                                                                <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Category") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Status :</td>
                                                            <td>
                                                                <asp:Label ID="lblStatusUPI" runat="server" Text="ACTIVATED"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Address :</td>
                                                            <td>
                                                                <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>'></asp:Label></td>
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
                                                                    <th class="border-bottom-0">Payer Name</th>
                                                                    <th class="border-bottom-0">Source</th>
                                                                    <th class="border-bottom-0">Mobile</th>
                                                                    <th class="border-bottom-0">Txn Type</th>
                                                                    <th class="border-bottom-0">Bank Ref No</th>
                                                                    <th class="border-bottom-0">Txn Amount</th>
                                                                    <th class="border-bottom-0">Charges</th>
                                                                    <th class="border-bottom-0">Settled Amount</th>
                                                                    <th class="border-bottom-0">Date</th>

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

