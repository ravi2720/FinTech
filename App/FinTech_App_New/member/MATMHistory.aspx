<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="MATMHistory.aspx.cs" Inherits="Member_MATMHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <div class="main-content-body">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                   
                    <div class="row row-sm collapse" id="collapseExample">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label>Search</label>
                                            <asp:TextBox runat="server" ID="txtSearch" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2">
                                            <label>From Date</label>
                                            <asp:TextBox runat="server" TextMode="Date" ID="txtfromdate" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="col-md-2">
                                            <label>To Date</label>
                                            <asp:TextBox runat="server" TextMode="Date" ID="txttodate" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="col-md-2">
                                            <label>Status</label>
                                            <asp:DropDownList runat="server" ID="dllStatus" CssClass="form-control">
                                                <asp:ListItem Text="Select Status" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Success" Value="Success"></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                <asp:ListItem Text="Failed" Value="Failed"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <label for="ddlMember">Member ID : </label>
                                            <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member." AutoPostBack="true" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged" class="form-control select2"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <label></label>
                                            <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Submit" CssClass="form-control"></asp:Button>
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
                                    <h3 class="card-title">Manage MATM
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
                                                    <th class="border-bottom-0">Member Name</th>
                                                    <th class="border-bottom-0">AddDate</th>
                                                    <th class="border-bottom-0">Amount</th>
                                                    <th class="border-bottom-0">TransID</th>
                                                    <th class="border-bottom-0">Bank RRN</th>
                                                    <th class="border-bottom-0">Card No</th>
                                                    <th class="border-bottom-0">Status</th>

                                                </tr>
                                            </thead>
                                            <tbody>


                                                <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Container.ItemIndex + 1 %></td>
                                                            <td><%# Eval("NAME") %><br />
                                                                <%# Eval("LOGINID") %></td>
                                                            <td><%# Eval("AddDate") %></td>
                                                            <td><%# Eval("Amount") %></td>
                                                            <td><%# Eval("RefID") %></td>
                                                            <td><%# Eval("RRN") %></td>
                                                            <td><%# Eval("CardNo") %></td>
                                                            <td class='<%# (Eval("status").ToString().ToUpper()=="SUCCESS"?"btn btn-success":"btn btn-danger") %>'>
                                                                <%# Eval("Status") %>
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

