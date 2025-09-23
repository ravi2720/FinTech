<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="UploadKYC.aspx.cs" Inherits="Member_BankDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <div class="main-container container-fluid">
        <div class="main-content-body">
                    <asp:UpdatePanel runat="server" ID="updateRolePanel">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdnMsrno" runat="server" />
                            <asp:HiddenField ID="hdnID" runat="server" />
                            <div class="card card-primary">
                                <div class="card-header"><b>Upload KYC Documents</b></div>
                                <div class="card card-body">
                                    <table class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th>Document</th>
                                                <th>File</th>
                                                <th>Number</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField ID="hdnSide" runat="server" />
                                                    <asp:DropDownList ID="ddlDocument" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDocument_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDocument" ForeColor="Red" InitialValue="0" ErrorMessage="Select Document" Display="Dynamic" ValidationGroup="chkaddfund" />
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="fuDoc" CssClass="form-control" Enabled="false" runat="server" />
                                                    <asp:RequiredFieldValidator ID="rfvFuDoc" runat="server" ControlToValidate="fuDoc" ForeColor="Red" ErrorMessage="Select File" Display="Dynamic" ValidationGroup="chkaddfund" />
                                                    <asp:FileUpload ID="fuDocBack" CssClass="form-control" Visible="false" Enabled="false" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Visible="false" Enabled="false" ControlToValidate="fuDocBack" ForeColor="Red" ErrorMessage="Select File" Display="Dynamic" ValidationGroup="chkaddfund" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDocNumber" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDocNumber" ForeColor="Red" ErrorMessage="Enter Document Number" Display="Dynamic" ValidationGroup="chkaddfund" />
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnSave" Visible="false" class="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="chkaddfund" /></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <hr />
                            
                    <div class="row row-sm">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-header">
                                    <h3 class="card-title">KYC Documents
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
                                                    <th class="border-bottom-0">Document Name</th>
                                                     <th class="border-bottom-0">Status</th>
                                                    <th class="border-bottom-0">Reason</th>
                                                    <th class="border-bottom-0">View</th>
                                                    <th class="border-bottom-0">AddDate</th>
                                                   
                                                </tr>
                                            </thead>
                                            <tbody>
                                                
                                            <asp:Repeater runat="server" ID="rptData">
                                                <ItemTemplate>
                                                    <tr class="row1">
                                                        <td><%# Container.ItemIndex+1 %>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("DocName") %>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblSide" runat="server" Text='<%# Eval("Status") %>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Reason") %>'></asp:Label></td>
                                                        <td>
                                                            <a href='<%# "./images/KYC/"+Eval("DocImage") %>' target="_blank" >View</a>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblAdddate" runat="server" Text='<%# Eval("Adddate") %>'></asp:Label></td>
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
                            <asp:PostBackTrigger ControlID="btnSave" />
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

