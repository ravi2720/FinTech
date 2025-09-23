<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="BalanceTransfer.aspx.cs" Inherits="Member_BalanceTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <div class="main-content-body">

            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <asp:MultiView ID="multiview1" ActiveViewIndex="0" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="card card-primary">
                                <div class="card-header">Balance Credit/Debit</div>
                                <div class="card card-body">
                                    <div class="form-group">
                                        <%--<label for="txtAccountNo">Member : </label>--%>
                                        <asp:DropDownList ID="ddlMember" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <div class="row row-sm">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header">
                                            <h3 class="card-title">Credit/Debit
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
                                                            <th class="border-bottom-0">>User ID</th>
                                                            <th class="border-bottom-0">Name</th>
                                                            <th class="border-bottom-0">MemberID</th>
                                                            <th class="border-bottom-0">Balance</th>
                                                            <th class="border-bottom-0">AEPS Balance</th>
                                                            <th class="border-bottom-0">Add Balance</th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater runat="server" ID="rptDataRecharge">
                                                            <ItemTemplate>
                                                                <tr class="row1">
                                                                    <td><%# Container.ItemIndex+1 %></td>
                                                                    <td><%#Eval("Name") %></td>
                                                                    <td><%#Eval("LoginID") %></td>
                                                                    <td><%#Eval("Balance") %></td>
                                                                    <td><%#Eval("AEPSBalance") %></td>
                                                                    <td><a title="Transfer Money" href='AddFund.aspx?usr=<%#Eval("Msrno") %>' class="btn btn-primary">Add Balance</a> </td>
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



                        </asp:View>
                    </asp:MultiView>
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

