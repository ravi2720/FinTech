<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="CompanyBankDetails.aspx.cs" Inherits="Admin_CompanyBankDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnID" runat="server" Value="0" />
                    <div class="card card-primary">
                        <div class="card-header">Company Bank Details</div>
                        <div class="card-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label for="txtAccountNo">IFSC Code  : </label>
                                        <asp:TextBox runat="server" ID="txtIFSCCode" AutoPostBack="true" OnTextChanged="txtIfscCode_TextChanged" ToolTip="Enter IFSC Code" class="form-control ValiDationR"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Enter IFSC Code " ControlToValidate="txtIFSCCode" ValidationGroup="vh" runat="server" ForeColor="red"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="ddlBank">Bank Name : </label>
                                        <asp:DropDownList runat="server" ID="ddlBankList" ToolTip="Select Bank Name." class="form-control select2"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Select Bank" ControlToValidate="ddlBankList" ValidationGroup="vh" InitialValue="0" runat="server" ForeColor="red"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="txtAccountNo">Account Holder : </label>
                                        <asp:TextBox runat="server" ID="txtAccountHolder" ToolTip="Enter Account Holder Name" class="form-control ValiDationR"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Enter Account Holder Name " ControlToValidate="txtAccountHolder" ValidationGroup="vh" runat="server" ForeColor="red"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="col-md-3">
                                        <label for="txtAccountNo">Account Number : </label>
                                        <asp:TextBox runat="server" ID="txtAccountNumber" ToolTip="Enter Account Number" Text="" class="form-control ValiDationR"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Enter Account Number " ControlToValidate="txtAccountNumber" ValidationGroup="vh" runat="server" ForeColor="red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label for="txtAccountNo">Branch Name  : </label>
                                        <asp:TextBox runat="server" ID="txtbranch" ToolTip="Enter Branch Name " class="form-control ValiDationR"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Enter Branch Name " ControlToValidate="txtbranch" ValidationGroup="vh" runat="server" ForeColor="red"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="txtAccountNo">Billing Info  : </label>
                                        <asp:TextBox runat="server" ID="txtBillinginfo" ToolTip="Enter Billing Info" Text="" class="form-control ValiDationR"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="Enter Billing Info" ControlToValidate="txtBillinginfo" ValidationGroup="vh" runat="server" ForeColor="red"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="txtAccountNo">Cash Deposite Charge  : </label>
                                        <asp:TextBox runat="server" ID="txtCashDepositeCharge" ToolTip="Enter Cash Deposite Charge" Text="" class="form-control ValiDationR"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ErrorMessage="Enter Cash Deposite Charge" ControlToValidate="txtCashDepositeCharge" ValidationGroup="vh" runat="server" ForeColor="red"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="txtAccountNo">QR Logo : </label>
                                        <asp:FileUpload runat="server" ID="flieUpload" CssClass="form-control" />
                                    </div>
                                </div>

                            </div>
                            <div class="form-group">
                                <table class="table table-responsive">
                                    <tr>
                                        <th>
                                            <label>BRANCH</label></th>
                                        <th>
                                            <label>ADDRESS</label></th>
                                        <th>
                                            <label>BANK</label></th>
                                    </tr>
                                    <tr>

                                        <td>
                                            <label runat="server" id="lblBranch"></label>
                                        </td>

                                        <td>
                                            <label runat="server" id="lblADDRESS"></label>
                                        </td>

                                        <td>
                                            <label runat="server" id="lblBANK"></label>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label for="txtAccountNo">Bank Logo : </label>
                                        <asp:FileUpload runat="server" ID="FileUploadBanklogo" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <label for="chkActive">Active : </label>
                                        <asp:CheckBox runat="server" ID="chkActive" Checked="true" ToolTip="Active" class="form-control ValiDationR"></asp:CheckBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary mt-4" ValidationGroup="vh" />
                                    </div>
                                </div>
                            </div>

                            <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                <thead>
                                    <tr>
                                        <th>S.No</th>
                                        <th>Active/DeActive</th>
                                        <th>Edit</th>
                                        <th>Bank Name</th>
                                        <th>Branch Name </th>
                                        <th>Account Holder</th>
                                        <th>Account Number</th>
                                        <th>IFSC Code</th>
                                        <th>Billing Info</th>
                                        <th>Cash Deposite Charge</th>
                                        <th>QR Logo </th>
                                        <th>Bank Logo </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="row1">
                                                <td><%# Container.ItemIndex + 1 %></td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgDelete" OnClientClick="javascript:return confirm('Are you sure you want to Active/DeActive this Role?');" CommandName="Active" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("IsActive"))==true ? ConstantsData.ActiveIcon:ConstantsData.DeActiveIcon)  %>' Height="20" Width="20" ToolTip="Active/DeActive Row" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# ConstantsData.EditIcon %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                                <td><span><%# Eval("BankName") %></span></td>
                                                <td><span><%# Eval("BranchName") %></span></td>
                                                <td><span><%# Eval("AccountHolderName") %></span></td>
                                                <td><span><%# Eval("AccountNumber") %></span></td>
                                                <td><span><%# Eval("IFSCCode") %></span></td>
                                                <td><span><%# Eval("Billinginfo") %></span></td>
                                                <td><span><%# Eval("Cashdepositecharge") %></span></td>
                                                <td>
                                                    <asp:Image runat="server" ID="img" Width="100" Height="100" ImageUrl='<%# "./images/"+Eval("Qrlogo").ToString() %>' />
                                                </td>
                                                <td>
                                                    <asp:Image runat="server" ID="imgBanklogo" Width="100" Height="100" ImageUrl='<%# "./images/"+Eval("Banklogo").ToString() %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
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

            Action(DataObject);
        }
        $(function () {
            Action(DataObject);
        });
        var req = Sys.WebForms.PageRequestManager.getInstance();
        req.add_beginRequest(function () {

        });
        req.add_endRequest(function () {
            LoadData();

        });
        function showModal() {
            $('#myModal').modal();
        }


    </script>
</asp:Content>

