<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="CheckEWalletTransaction.aspx.cs" Inherits="Member_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <div class="main-content-body">

            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>             
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label>Transaction ID</label>
                                            <asp:TextBox runat="server" ID="txtTransactionID" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label></label>
                                            <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" CssClass="form-control mt-2"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                  
                    <div class="row row-sm">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-header">
                                    <h3 class="card-title">E-Wallet Transactions
                                <a href="#collapseExample" class="font-weight-bold" aria-controls="collapseExample" aria-expanded="false" data-bs-toggle="collapse" role="button">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" class="bi bi-filter" viewBox="0 0 16 16">
                                        <path d="M6 10.5a.5.5 0 0 1 .5-.5h3a.5.5 0 0 1 0 1h-3a.5.5 0 0 1-.5-.5zm-2-3a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7a.5.5 0 0 1-.5-.5zm-2-3a.5.5 0 0 1 .5-.5h11a.5.5 0 0 1 0 1h-11a.5.5 0 0 1-.5-.5z" />
                                    </svg>
                                </a>
                                    </h3>
                                </div>
                                <div class="card-body">

                                    <div class="table-responsive">
                                        <table id="file-datatable" class="border-top-0  table table-bordered text-nowrap key-buttons border-bottom">
                                            <thead>

                                                <tr>
                                                    <th class="border-bottom-0">SNO</th>
                                                    <th class="border-bottom-0">MemberID</th>
                                                    <th class="border-bottom-0">Factor</th>
                                                    <th class="border-bottom-0">Amount</th>
                                                    <th class="border-bottom-0">Transaction Id</th>
                                                    <th class="border-bottom-0">Narration</th>
                                                    <th class="border-bottom-0">Description</th>
                                                    <th class="border-bottom-0">Add Date</th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="rptData">
                                                    <ItemTemplate>
                                                        <tr class="row1">
                                                            <td><%#Container.ItemIndex+1 %></td>
                                                            <td><%#Eval("LoginID") %></td>
                                                            <td><%#Eval("Factor") %></td>
                                                            <td><%#Eval("Amount") %></td>
                                                            <td><%#Eval("TRANSACTIONID") %></td>
                                                            <td><%#Eval("NARRATION") %></td>
                                                            <td><%#Eval("Description") %></td>
                                                            <td><%#Eval("AddDate") %></td>
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

                </ContentTemplate>
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

