<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="EmployeeRegistration.aspx.cs" Inherits="EmployeeRegistration_Admin" %>

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
                        <div class="card-header">Employee Registration</div>
                        <div class="card-body">
                            <asp:HiddenField ID="hdnid" runat="server" />

                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label for="exampleInputPassword1">
                                        Role
                                    </label>
                                    <asp:DropDownList ID="ddlRole" class="form-control" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlRole" InitialValue="0" Font-Bold="True" ForeColor="Red" ErrorMessage="Please select Role" ValidationGroup="chkaddfund" />
                                </div>

                                <div class="form-group" style="display: none">
                                    <label for="exampleInputPassword1">Title</label>
                                    <asp:DropDownList ID="ddltitle" class="form-control" runat="server">
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddltitle" InitialValue="0" Font-Bold="True" ForeColor="Red" ErrorMessage="Please select Title" ValidationGroup="chkaddfund" />--%>
                                </div>

                                <div class="form-group col-md-4">
                                    <label for="exampleInputPassword1">Name</label>
                                    <asp:TextBox ID="txtName" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Name" Font-Bold="True" ForeColor="Red" ControlToValidate="txtName" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                </div>


                                <div class="form-group col-md-4">
                                    <label for="exampleInputPassword1">Age</label>
                                    <asp:TextBox ID="txtAge" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Enter Age" Font-Bold="True" ForeColor="Red" ControlToValidate="txtAge" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label for="exampleInputPassword1">Email</label>
                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                        ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                        Display="Dynamic" ErrorMessage="Invalid email address" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Email" Font-Bold="True" ForeColor="Red" ControlToValidate="txtEmail" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-4">
                                    <label for="exampleInputPassword1">Mobile</label>
                                    <asp:TextBox ID="txtMobile" runat="server" MaxLength="10" class="form-control m-t-xxs"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Mobile" Font-Bold="True" ForeColor="Red" ControlToValidate="txtMobile" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtMobile" />
                                </div>




                                <div class="form-group col-md-4">
                                    <label for="exampleInputPassword1">LoginID</label>
                                    <asp:TextBox ID="txtLoginID" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter LoginID" Font-Bold="True" ForeColor="Red" ControlToValidate="txtLoginID" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>

                                </div>

                            </div>
                            <div class="row">
                                <div class="form-group" style="display: none">
                                    <label for="exampleInputPassword1">
                                        Gender : 
                                    </label>
                                    <asp:RadioButtonList ID="rdGender" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                    </asp:RadioButtonList>
                                </div>

                                <div class="form-group col-md-4">
                                    <label for="exampleInputPassword1">Login Pin</label>
                                    <asp:TextBox ID="txtLoginPin" runat="server" MaxLength="4" class="form-control m-t-xxs"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtLoginPin" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Login Pin" Font-Bold="True" ForeColor="Red" ControlToValidate="txtLoginPin" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-4">
                                    <label for="exampleInputPassword1">Password</label>
                                    <asp:TextBox ID="txtPassword" runat="server" class="form-control m-t-xxs"></asp:TextBox>

                                </div>
                                <div class="form-group col-md-4">
                                    <label for="exampleInputPassword1">Address</label>
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="3" class="form-control m-t-xxs"></asp:TextBox>

                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label for="exampleInputPassword1">Pincode</label>
                                    <asp:TextBox ID="txtPincode" runat="server" MaxLength="6" class="form-control m-t-xxs"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtPincode" />
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter Email" Font-Bold="True" ForeColor="Red" ControlToValidate="txtAddress" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>--%>
                                </div>

                                <div class="form-group" style="display: none">
                                    <label for="exampleInputPassword1">PackageID</label>
                                    <asp:DropDownList ID="ddlPackage" class="form-control" runat="server">
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddltitle" InitialValue="0" Font-Bold="True" ForeColor="Red" ErrorMessage="Please select Title" ValidationGroup="chkaddfund" />--%>
                                </div>

                                <div class="form-group col-md-4">
                                    <label for="exampleInputPassword1">Active</label>
                                    <asp:CheckBox ID="chkActive" Checked="true" class="form-control" runat="server"></asp:CheckBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddltitle" InitialValue="0" Font-Bold="True" ForeColor="Red" ErrorMessage="Please select Title" ValidationGroup="chkaddfund" />--%>
                                </div>
                                <div class="form-group col-md-4">
                                    <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" CssClass="btn btn-primary mt-4" runat="server" Text="Save" ValidationGroup="chkaddfund" />
                                </div>
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

