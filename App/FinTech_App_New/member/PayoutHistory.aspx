<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="PayoutHistory.aspx.cs" Inherits="Member_PayoutHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <div class="main-content-body">


            <asp:UpdatePanel runat="server" ID="update">
                <ContentTemplate>
                    <div class="row row-sm collapse" id="collapseExample">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label>Search</label>
                                            <asp:TextBox runat="server" ID="txtSeach" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2">
                                            <label>From Date</label>
                                            <asp:TextBox runat="server" TextMode="Date" ID="txtFromDate" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="col-md-2">
                                            <label>To Date</label>
                                            <asp:TextBox runat="server" TextMode="Date" ID="txtToDate" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="col-md-2">
                                            <label>Status</label>
                                            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                                <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Success" Value="Success"></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                <asp:ListItem Text="Failed" Value="Failed"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <label for="ddlMember">Member ID : </label>
                                            <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member." class="form-control select2"></asp:DropDownList>
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
                                    <h3 class="card-title">Payout History 
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
                                                    <th class="border-bottom-0">Type</th>
                                                    <th class="border-bottom-0">OrderID</th>
                                                    <th class="border-bottom-0">Bank Details</th>
                                                    <th class="border-bottom-0">Status</th>
                                                    <th class="border-bottom-0">RRN</th>
                                                    <th class="border-bottom-0">Member ID</th>
                                                    <th class="border-bottom-0">Name</th>
                                                    <th class="border-bottom-0">Amount</th>
                                                    <th class="border-bottom-0">Charge</th>
                                                    <th class="border-bottom-0">Transaction Mode</th>
                                                    <th class="border-bottom-0">AEPS Approve Amount</th>
                                                    <th class="border-bottom-0">Request ID</th>
                                                    <th class="border-bottom-0">Transaction ID</th>
                                                    <th class="border-bottom-0">Add Date</th>
                                                    <th class="border-bottom-0">Approve Date</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <asp:Repeater ID="repeater1" runat="server" OnItemCommand="repeater1_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Container.ItemIndex+1 %>
                                                                <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ID") %>' />
                                                                <asp:Label ID="lblId" runat="server" Visible="false" Text='<%#Eval("ID") %>'></asp:Label>
                                                            </td>

                                                            <td><%#Eval("RequestType") %></td>
                                                            <td><%#Eval("RequestID") %></td>
                                                            <td>
                                                                <asp:Button runat="server" ID="btnBankDetails" CssClass="btn btn-danger" data-bs-toggle="modal" data-bs-target="#BankDetails" CommandName="ViewBankDetails" CommandArgument='<%#Eval("BankID") %>' Text="View" />
                                                            </td>
                                                            <td>
                                                                <%# Eval("RequestSTatus") %>
                                                            </td>
                                                            <td><%#Eval("RRN") %></td>
                                                            <td><%#Eval("LoginID") %></td>
                                                            <td><%#Eval("NAME") %></td>
                                                            <td><%#Eval("AMOUNT") %></td>
                                                            <td><%#Eval("Charge") %></td>
                                                            <td><%#Eval("TransMode") %></td>
                                                            <td><%#Eval("AEPSApproveAmount") %></td>

                                                            <td><%#Eval("RequestID") %></td>
                                                            <td><%#Eval("TransactionID") %></td>
                                                            <td><%#Eval("RequestDate") %></td>
                                                            <td><%#Eval("PaidDate") %></td>
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


                    <asp:HiddenField ID="hdnIDValue" runat="server" />
                </ContentTemplate>

            </asp:UpdatePanel>

        </div>
    </div>

    <!-- Central Modal Medium Success -->
    <div class="modal fade" id="centralModalSuccess" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-notify modal-success" role="document">
            <!--Content-->
            <div class="modal-content">
                <!--Header-->
                <div class="modal-header">
                    <p class="heading lead">AEPS Withdraw</p>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true" class="white-text">&times;</span>
                    </button>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>

                        <!--Body-->
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="exampleInputPassword1">Bank Refference ID</label>
                                <asp:TextBox ID="txtBenkRefID" runat="server" placeholder="Bank Refference ID" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Bank Refference ID" Font-Bold="True" ForeColor="Red" ControlToValidate="txtBenkRefID" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <!--Footer-->
                        <div class="modal-footer justify-content-center">
                            <asp:Button ID="Button1" runat="server" data-dismiss="modal" CssClass="btn btn-warning" Text="Close" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <!--/.Content-->
        </div>
    </div>
    <!-- Central Modal Medium Success-->

    <!-- Central Modal Medium Success -->
    <div class="modal fade" id="BankDetails" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-notify modal-success" role="document">
            <!--Content-->
            <div class="modal-content">
                <!--Header-->
                <div class="modal-header">
                    <p class="heading lead">Bank Details</p>
                     <button aria-label="Close" class="btn-close" data-bs-dismiss="modal" type="button"><span aria-hidden="true">×</span></button>
                </div>

                <!--Body-->
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                            <asp:Repeater runat="server" ID="rptBankData">
                                <ItemTemplate>
                                    <div class="form-group">
                                        <label for="exampleInputPassword1"><b>Name :</b>  </label>
                                        <%# Eval("Name") %>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputPassword1"><b>IFSC</b> </label>
                                        <%# Eval("IFSCCode") %>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputPassword1"><b>AccountNo</b> </label>
                                        <%# Eval("AccountNumber") %>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputPassword1"><b>Acount Holder Name</b> </label>
                                        <%# Eval("AccountHolderName") %>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputPassword1"><b>Branch Name</b> </label>
                                        <%# Eval("BranchName") %>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>

                <!--Footer-->
                <div class="modal-footer justify-content-center">
                    <asp:Button ID="Button4" runat="server" data-dismiss="modal" CssClass="btn btn-warning" Text="Close" />
                </div>
            </div>
            <!--/.Content-->
        </div>

    </div>
    <!-- Central Modal Medium Success-->

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

