<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="Reatiler_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        input[type="radio"] {
            margin: 4px 10px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Registration</div>
                        <div class="card-body">
                            <asp:HiddenField ID="hdnid" runat="server" />
                            <div class="form-group">
                                <label for="exampleInputPassword1">
                                    Role
                                </label>
                                <asp:DropDownList ID="ddlRole" Style="display: none;" class="form-control" runat="server" Enabled="false"></asp:DropDownList>
                                <asp:Label ID="lblPackageName" runat="server" Style="color: green; font-weight: 600" class="form-control-label m-t-xxs"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlRole" InitialValue="0" Font-Bold="True" ForeColor="Red" ErrorMessage="Please select Role" ValidationGroup="chkaddfund" />
                            </div>

                            <div class="form-group">
                                <label for="exampleInputPassword1">Title</label>
                                <asp:DropDownList ID="ddltitle" class="form-control" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddltitle" InitialValue="0" Font-Bold="True" ForeColor="Red" ErrorMessage="Please select Title" ValidationGroup="chkaddfund" />
                            </div>

                            <div class="form-group">
                                <label for="exampleInputPassword1">Name</label>
                                <asp:TextBox ID="txtName" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Name" Font-Bold="True" ForeColor="Red" ControlToValidate="txtName" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                            </div>
                             <div class="form-group">
                                        <label for="exampleInputPassword1">Shop Name</label>
                                        <asp:TextBox ID="txtShopName" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please Enter Shop Name" Font-Bold="True" ForeColor="Red" ControlToValidate="txtShopName" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                    </div>


                            <div class="form-group">
                                <label for="exampleInputPassword1">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                    ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                    Display="Dynamic" ErrorMessage="Invalid email address" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Email" Font-Bold="True" ForeColor="Red" ControlToValidate="txtEmail" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label for="exampleInputPassword1">Mobile</label>
                                <asp:TextBox ID="txtMobile" runat="server" MaxLength="10" class="form-control m-t-xxs"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Mobile" Font-Bold="True" ForeColor="Red" ControlToValidate="txtMobile" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtMobile" />
                            </div>

                            <div class="form-group">
                                <label for="exampleInputPassword1">Alternative Mobile</label>
                                <asp:TextBox ID="txtAlterMobile" runat="server" MaxLength="10" class="form-control m-t-xxs"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter Mobile" Font-Bold="True" ForeColor="Red" ControlToValidate="txtMobile" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtAlterMobile" />
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Aadhar Number</label>
                                <asp:TextBox ID="txtAadhar" runat="server" MaxLength="12" class="form-control m-t-xxs"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Enter Aadhar number" Font-Bold="True" ForeColor="Red" ControlToValidate="txtMobile" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtAadhar" />
                            </div>

                            <div class="form-group">
                                <label for="exampleInputPassword1">Pan Number</label>
                                <asp:TextBox ID="txtPanNumber" runat="server" MaxLength="10" class="form-control m-t-xxs"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please Enter Pan number" Font-Bold="True" ForeColor="Red" ControlToValidate="txtMobile" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">
                                    Gender : 
                                </label>
                                <asp:RadioButtonList ID="rdGender" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                </asp:RadioButtonList>
                            </div>

                            <div class="form-group" style="display: none;">
                                <label for="exampleInputPassword1">Login Pin</label>
                                <asp:TextBox ID="txtLoginPin" runat="server" MaxLength="4" class="form-control m-t-xxs"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtLoginPin" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Login Pin" Font-Bold="True" ForeColor="Red" ControlToValidate="txtLoginPin" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group" style="display: none;">
                                <label for="exampleInputPassword1">Password</label>
                                <asp:TextBox ID="txtPassword" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Email" Font-Bold="True" ForeColor="Red" ControlToValidate="txtPassword" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label for="exampleInputPassword1">Address</label>
                                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="3" class="form-control m-t-xxs"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter Email" Font-Bold="True" ForeColor="Red" ControlToValidate="txtAddress" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>--%>
                            </div>

                             <div class="form-group">
                                    <label for="exampleInputPassword1">Pin Code</label>
                                    <asp:TextBox ID="txtpincode" runat="server" MaxLength="10" class="form-control m-t-xxs"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Enter pin Code" Font-Bold="True" ForeColor="Red" ControlToValidate="txtpincode" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="exampleInputPassword1">DOB</label>
                                    <asp:TextBox ID="txtDOB" runat="server" MaxLength="10" class="form-control m-t-xxs"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txttxtDOB_ce" Format="dd-MM-yyyy" PopupButtonID="txtDOB"
                                        TargetControlID="txtDOB">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Enter pin Code" Font-Bold="True" ForeColor="Red" ControlToValidate="txtDOB" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                </div>

                            <div class="form-group">
                                <label for="exampleInputPassword1">PackageID</label>
                                <asp:DropDownList ID="ddlPackage" class="form-control" runat="server">
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddltitle" InitialValue="0" Font-Bold="True" ForeColor="Red" ErrorMessage="Please select Title" ValidationGroup="chkaddfund" />--%>
                            </div>

                            <div class="form-group">
                                <label for="exampleInputPassword1">Active</label>
                                <asp:CheckBox ID="chkActive" Checked="true" class="form-control" runat="server"></asp:CheckBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddltitle" InitialValue="0" Font-Bold="True" ForeColor="Red" ErrorMessage="Please select Title" ValidationGroup="chkaddfund" />--%>
                            </div>

                            <div class="form-group">
                                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" class="btn btn-primary btn-block" runat="server" Text="Save" ValidationGroup="chkaddfund" />
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

