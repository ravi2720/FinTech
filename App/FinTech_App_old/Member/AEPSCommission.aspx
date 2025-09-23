<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="AEPSCommission.aspx.cs" Inherits="Member_AEPSCommission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <div class="main-container container-fluid">
        <div class="main-content-body">
            <div class="row row-sm">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">AEPS Commission
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
                                            <th class="border-bottom-0">Package Name</th>
                                            <th class="border-bottom-0">StartVal</th>
                                            <th class="border-bottom-0">END</th>
                                            <th class="border-bottom-0">Commission</th>
                                            <th class="border-bottom-0">IsFlat</th>

                                        </tr>
                                    </thead>
                                    <tbody>

                                        <asp:Repeater runat="server" ID="rptAepsCommission">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.ItemIndex+1 %></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblPackageName" Text='<%# Eval("PackageName") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblStartVal" Text='<%# Eval("startval") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblEndVal" Text='<%# Eval("endval") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label1" Text='<%# Eval("SURCHARGE") %>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hdnUplineCommission" Value='<%# Eval("SURCHARGE") %>'></asp:HiddenField>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="isFlat" Text='<%# Eval("ISFLAT").ToString().IsFlatString() %>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hdnIsFlat" Value='<%# Eval("ISFLAT") %>'></asp:HiddenField>
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



