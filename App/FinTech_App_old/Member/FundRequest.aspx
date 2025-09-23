<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="FundRequest.aspx.cs" Inherits="Member_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <div class="main-content-body">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header"><b>Fund Request</b></div>
                        <div class="card card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <label for="banklist">Bank List</label>
                                    <asp:DropDownList runat="server" ID="dllBankList" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:HiddenField ID="hdnid" runat="server" />
                                    <label for="exampleInputPassword1">
                                        Balance
                                    </label>
                                    <asp:Label ID="lblBalance" Text="0.00" class="form-control m-t-xxs" runat="server" />
                                </div>

                                <div class="col-md-3">
                                    <label for="exampleInputPassword1">Amount</label>
                                    <asp:TextBox ID="txtamount" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Amount" Font-Bold="True" ForeColor="Red" ControlToValidate="txtAmount" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" ValidChars="." TargetControlID="txtamount" />
                                </div>

                                <div class="col-md-3">
                                    <label for="exampleInputPassword1">Bank Referance No.</label>
                                    <asp:TextBox ID="txtReferanceNo" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Referance No" Font-Bold="True" ForeColor="Red" ControlToValidate="txtReferanceNo" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-3">
                                    <label for="exampleInputPassword1">Payment Mode</label>
                                    <asp:DropDownList ID="ddlPaymentMode" runat="server" class="form-control m-t-xxs">
                                        <asp:ListItem Value="0" Text="-- Select --"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Payment Mode" InitialValue="0" Font-Bold="True" ForeColor="Red" ControlToValidate="ddlPaymentMode" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-3">
                                    <label for="exampleInputPassword1">Payment Date</label>
                                    <asp:TextBox ID="txtPaymentDate" TextMode="Date" runat="server" CssClass="form-control m-t-xxs"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Payment Date" Font-Bold="True" ForeColor="Red" ControlToValidate="txtPaymentDate" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-3">
                                    <label for="exampleInputPassword1">Remark</label>
                                    <asp:TextBox ID="txtRemark" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Remark" Font-Bold="True" ForeColor="Red" ControlToValidate="txtRemark" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                </div>


                                <div class="col-md-3">
                                    <asp:Button ID="btnSubmit" class="btn btn-primary mt-4" runat="server" Text="Save" OnClick="btnSubmit_Click" ValidationGroup="chkaddfund" />
                                    <asp:Label ID="lblMessage" CssClass="center-block" Style="text-align: center; font-weight: 600" ForeColor="Red" runat="server" />
                                </div>
								<h5>Company Details</h5>
                        <div class="col-md-12 mt-4">
                            <div class="table table-responsive">
                                <table class="table table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Bank Name</th>
                                            <th>Branch Name</th>
                                            <th>Account Holder Name</th>
                                            <th>Account Number</th>
                                            <th>IFSC Code</th>
											<th>Billing Info</th>
                                            <th>QR Code</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptBankDetails" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("BankName") %></td>
                                                    <td><%#Eval("BranchName") %></td>
                                                    <td><%#Eval("AccountHolderName") %></td>
                                                    <td><%#Eval("AccountNumber") %></td>
                                                    <td><%#Eval("IFSCCode") %></td>
													<td><%#Eval("Billinginfo") %></td>
                                                    <td>
                                                        <a href="#" id="link1" data-bs-toggle="modal" data-bs-target="#myModal">
                                                            <img src='./images/<%#Eval("Qrlogo") %>' id="img1" class="img-responsive" style="width:150px">
                                                        </a>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                                <asp:HiddenField runat="server" ID="HiddenField1" ClientIDMode="Static" />
                            </div>
                        </div>
                    </div>

                    <div class="row row-sm">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-header">
                                    <h3 class="card-title">Fund Request List
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
                                                    <th class="border-bottom-0">Member ID</th>
                                                    <th class="border-bottom-0">Amount</th>
                                                    <th class="border-bottom-0">Name</th>
                                                    <th class="border-bottom-0">Company Bank Name</th>
                                                    <th class="border-bottom-0">Bank Ref ID</th>
                                                    <th class="border-bottom-0">Payment Date</th>
                                                    <th class="border-bottom-0">Payment Mode</th>
                                                    <th class="border-bottom-0">Remark</th>
                                                    <th class="border-bottom-0">Status</th>
                                                    <th class="border-bottom-0">Approve Date</th>
                                                    <th class="border-bottom-0">Add Date</th>


                                                </tr>
                                            </thead>
                                            <tbody>

                                                <asp:Repeater ID="repeater1" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Container.ItemIndex+1 %></td>
                                                            <td><%#Eval("LoginID") %></td>
                                                            <td><%#Eval("NAME") %></td>
                                                            <td><%#Eval("AMOUNT") %></td>
                                                            <td><%#Eval("BankName") %></td>
                                                            <td><%#Eval("BANKREFID") %></td>
                                                            <td><%#Eval("PAYMENTDATE") %></td>
                                                            <td><%#Eval("PAYMENTMODE") %></td>
                                                            <td><%#Eval("REMARK") %></td>
                                                            <td>
                                                                <asp:Label ID="lblStatus" runat="server" CssClass='<%# Convert.ToInt16(Eval("IsApprove")) == 1 ? "btn btn-success" : "btn btn-warning" %>' Text='<%#Eval("Status") %>'></asp:Label></td>
                                                            <td><%#Eval("ApproveDate") %></td>
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
                    <asp:HiddenField runat="server" ID="hdnUPIData" ClientIDMode="Static" />
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

