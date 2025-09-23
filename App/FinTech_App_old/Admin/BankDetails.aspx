<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="BankDetails.aspx.cs" Inherits="Admin_BankDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnMsrno" runat="server" />
                    <asp:HiddenField ID="hdnID" runat="server" />
                    <div class="card card-primary">
                        <div class="card-header">Bank Details</div>
                        <div class="card-body">
                            <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
                                <asp:View ID="View1" runat="server">
                                    <div class="form-group">
                                        <label for="ddlMember">Member ID : </label>
                                        <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member." AutoPostBack="true" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged" class="form-control select2"></asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Select Member" ControlToValidate="ddlMember" ValidationGroup="vh" InitialValue="0" runat="server" ForeColor="red"></asp:RequiredFieldValidator>--%>
                                        <asp:Label ID="lblMessage" CssClass="center-block" Style="text-align: center; font-weight: 600" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </asp:View>
                                <asp:View ID="View2" runat="server">

                                    <div class="form-group">
                                        <label for="ddlBank">Bank Name : </label>
                                        <asp:DropDownList runat="server" ID="ddlBankList" ToolTip="Select Bank Name." class="form-control select2"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Select Bank" ControlToValidate="ddlBankList" ValidationGroup="vh" InitialValue="0" runat="server" ForeColor="red"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtIfscCode">IFSC Code : </label>
                                        <asp:TextBox runat="server" ID="txtIfscCode" ToolTip="Enter IFSC Code" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Enter IFSC Code" ControlToValidate="txtIfscCode" ValidationGroup="vh" runat="server" ForeColor="red"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtAccountNo">Account No : </label>
                                        <asp:TextBox runat="server" ID="txtAccountNo" ToolTip="Enter Bank Account No" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Enter Account Number" ControlToValidate="txtAccountNo" ValidationGroup="vh" runat="server" ForeColor="red"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtBranchName">Account Holder Name : </label>
                                        <asp:TextBox runat="server" ID="txtAccountHolderName" ToolTip="Enter Branch Name" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Enter Account Holder Name" ControlToValidate="txtAccountHolderName" ValidationGroup="vh" runat="server" ForeColor="red"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtBranchName">Branch Name : </label>
                                        <asp:TextBox runat="server" ID="txtBranchName" ToolTip="Enter Branch Name" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Enter Branch Name" ControlToValidate="txtBranchName" ValidationGroup="vh" runat="server" ForeColor="red"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group">
                                        <asp:Button runat="server" ID="btnSubmit" class="btn btn-primary" Text="Save" ValidationGroup="vh" OnClick="btnSubmit_Click" />
                                        <asp:Label ID="Label1" CssClass="center-block" Style="text-align: center; font-weight: 600" runat="server" ForeColor="Red"></asp:Label>
                                        <asp:Button runat="server" ID="btnDone" Visible="false" CssClass="btn btn-Warning center-block" Style="text-align: center;" Text="Done" OnClick="btnDone_Click" />
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <hr />
            <div class="container-fluid">
                <asp:UpdatePanel ID="updt1" runat="server">
                    <ContentTemplate>
                        <div class="card panel-primary">
                            <div class="card-header">Bank List</div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-content collapse show">
                                                <div class="widgets-container">
                                                    <div>
                                                        <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                            <thead>
                                                                <tr>
                                                                    <th>S.No.</th>
                                                                    <th>Action</th>
                                                                    <th>Name</th>
                                                                    <th>MemberID</th>
                                                                    <th>Bank Name</th>
                                                                    <th>IFSC Code</th>
                                                                    <th>Branch Name</th>
                                                                    <th>Account No</th>
                                                                    <th>AddDate</th>
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th>S.No.</th>
                                                                    <th>Action</th>
                                                                    <th>Name</th>
                                                                    <th>MemberID</th>

                                                                    <th>Bank Name</th>
                                                                    <th>IFSC Code</th>
                                                                    <th>Branch Name</th>
                                                                    <th>Account No</th>
                                                                    <th>AddDate</th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody>
                                                                <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <tr class="row1">
                                                                            <td><%# Container.ItemIndex+1 %>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnActive" runat="server" CommandName="IsActive" CommandArgument='<%# Eval("ID") %>' CssClass='<%# (Convert.ToBoolean(Eval("IsActive"))==true ? "btn btn-primary":"btn btn-warning")  %>' Text='<%# (Convert.ToBoolean(Eval("IsActive"))==true ? "Active":"InActive")%>' OnClientClick="if (!confirm('Are you sure do you want to Activate/Deactivate it?'))" CausesValidation="false" />
                                                                                <asp:Button ID="lbtnPwd" runat="server" CssClass="btn btn-info" Text="Edit" CommandName="Edit" CommandArgument='<%#Eval("ID") %>' />
                                                                                <asp:Button ID="btnDelete" runat="server" OnClientClick="return confirm('do you want to delete account')" CssClass="btn btn-danger" Text="Delete" CommandName="Delete" CommandArgument='<%#Eval("ID") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("AccountHolderName") %>'></asp:Label></td>
                                                                              <td>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("MemberID") %>'></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("Name") %>'></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblIFSC" runat="server" Text='<%# Eval("IFSCCode") %>'></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblAccountNumber" runat="server" Text='<%# Eval("AccountNumber") %>'></asp:Label></td>
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
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </section>
    </div>

    <script>
        $(function () {
            Action(DataObject);
        });

        var DataObject = {
            ID: "example1",
            iDisplayLength: 15,
            bPaginate: true,
            bFilter: true,
            bInfo: true,
            bLengthChange: true,
            searching: true
        };
        function LoadData() {

            Action(DataObject);
        }

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

