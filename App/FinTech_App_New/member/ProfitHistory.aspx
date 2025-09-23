<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="ProfitHistory.aspx.cs" Inherits="Member_ProfitHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="main-container container-fluid">
        <!-- main-content-body -->
        <div class="main-content-body">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>                                        
                    <div class="row row-sm collapse" id="collapseExample">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                    
                                        <div class="col-md-2">
                                            <label>From Date</label>
                                            <asp:TextBox runat="server" TextMode="Date" ID="txtfromdate" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="col-md-2">
                                            <label>To Date</label>
                                            <asp:TextBox runat="server" TextMode="Date" ID="txttodate" CssClass="form-control"></asp:TextBox>

                                        </div>
                                     
                                        <div class="col-md-2">
                                            <label for="ddlMember">Member ID : </label>
                                            <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member." class="form-control select2"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <label></label>
                                            <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Search" CssClass="form-control"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row row-sm">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-header">
                                    <h3 class="card-title">Profit History
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
                                                    <th class="border-bottom-0">SL</th>
                                                    <th class="border-bottom-0">MemberID</th>
                                                    <th class="border-bottom-0">Name</th>
                                                    <th class="border-bottom-0">Amount</th>
                                                    <th class="border-bottom-0">Balance</th>
                                                    <th class="border-bottom-0">Factor</th>
                                                    <th class="border-bottom-0">Narration</th>
                                                    <th class="border-bottom-0">Description</th>
                                                    <th class="border-bottom-0">TransferDate</th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="rptData">
                                                    <ItemTemplate>
                                                        <tr class="row1">
                                                            <td><%#Container.ItemIndex+1 %>
                                            
                                                            </td>
                                                            <td><%#Eval("LoginID") %></td>
                                                            <td><%#Eval("NAME") %></td>
                                                            <td><%#Eval("AMOUNT") %></td>
                                                            <td><%#Eval("BALANCE") %></td>
                                                            <td>
                                                                <span class='<%# (Eval("factor").ToString().ToUpper()=="CR"?"btn btn-success":"btn btn-danger") %>'>
                                                                    <%#Eval("FACTOR") %>
                                                                </span>
                                                            </td>
                                                            <td><%#Eval("NARRATION") %></td>
                                                            <td><%#Eval("Description") %></td>
                                                            <td><%#Eval("ADDDATE") %></td>

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



