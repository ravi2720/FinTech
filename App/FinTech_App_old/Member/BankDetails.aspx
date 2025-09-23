<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="BankDetails.aspx.cs" Inherits="Member_BankDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-container container-fluid">
        <div class="main-content-body">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnMsrno" runat="server" />
                    <asp:HiddenField ID="hdnID" runat="server" />
               
                    <div class="row row-sm collapse" id="collapseExample">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label>IFSC Code :</label>
                                            <asp:TextBox runat="server" ID="txtIfscCode" ToolTip="Enter IFSC Code" class="form-control" AutoPostBack="true" OnTextChanged="txtIfscCode_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Enter IFSC Code" ControlToValidate="txtIfscCode" ValidationGroup="vh" runat="server" ForeColor="red"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="col-md-2">
                                            <label>Bank Name :</label>
                                            <asp:DropDownList runat="server" ID="ddlBankList" ToolTip="Select Bank Name." class="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Select Bank" ControlToValidate="ddlBankList" ValidationGroup="vh" InitialValue="0" runat="server" ForeColor="red"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="col-md-2">
                                            <label>Account No :</label>
                                            <asp:TextBox runat="server" ID="txtAccountNo" ToolTip="Enter Bank Account No" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Enter Account Number" ControlToValidate="txtAccountNo" ValidationGroup="vh" runat="server" ForeColor="red"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group">
                                            <table class="table-responsive">
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



                                        <div class="col-md-2">
                                            <label>Account Holder Name : </label>
                                            <asp:TextBox runat="server" ID="txtAccountHolderName" ToolTip="Enter Branch Name" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Enter Account Holder Name" ControlToValidate="txtAccountHolderName" ValidationGroup="vh" runat="server" ForeColor="red"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="col-md-2">
                                            <label for="txtBranchName">Branch Name : </label>
                                            <asp:TextBox runat="server" ID="txtBranchName" ToolTip="Enter Branch Name" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Enter Branch Name" ControlToValidate="txtBranchName" ValidationGroup="vh" runat="server" ForeColor="red"></asp:RequiredFieldValidator>
                                        </div>



                                        <div class="col-md-2" runat="server" id="divOTP" visible="false">
                                            <label for="txtBranchName">OTP : </label>
                                            <asp:TextBox runat="server" ID="txtOTP" ToolTip="Enter OTP" class="form-control"></asp:TextBox>
                                        </div>


                                        <div class="col-md-2">
                                            <label></label>
                                            <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Save" CssClass="form-control" ValidationGroup="vh"></asp:Button>
                                            <asp:Label ID="Label1" CssClass="center-block" Style="text-align: center; font-weight: 600" runat="server" ForeColor="Red"></asp:Label>
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
                                    <h3 class="card-title">Bank Details 
                                        <marquee direction="left"><span style="color:black">You can register only 1 (One) Bank Account details ..!!Change Your bank account please contact 8258888998.</span></marquee>
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
                                                    <th class="border-bottom-0">IFSC Code</th>
                                                    <th class="border-bottom-0">Account No</th>
                                                    <th class="border-bottom-0">Branch Name</th>
                                                    <th class="border-bottom-0">Name</th>
                                                    <th class="border-bottom-0">Bank Name</th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptBankData" runat="server" OnItemCommand="rptBankData_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Container.ItemIndex + 1 %></td>
                                                            <td><%# Eval("IfscCode") %></td>
                                                            <td><%# Eval("AccountNumber") %></td>
                                                            <td><%# Eval("BranchName") %></td>
                                                            <td><%# Eval("AccountHolderName") %></td>
                                                            <td><%# Eval("Name") %></td>
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
                    <asp:AsyncPostBackTrigger ControlID="txtIfscCode" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>

