<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .form-control {
            color: #212529 !important;
            background-color: #fff !important;
            border-color: #86b7fe !important;
            outline: 0 !important;
            -webkit-box-shadow: 0 0 0 0.25rem rgb(13 110 253 / 25%) !important;
            box-shadow: 0 0 0 .25remrgba(13,110,253,.25) !important;
        } 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main class="page-wrapper1">
        <section class="Login-wrap ptb-100">
            <div class="container">
                <asp:UpdatePanel runat="server" ID="updateRolePanel">
                    <ContentTemplate>
                        <div class="row ">
                            <div class="col-lg-12offset-lg-2 col-md-10 offset-md-1">
                                <div class="card ">
                                    <div class="card-header">
                                        <strong>Register New Account</strong>
                                    </div>
                                    <div class="login-form">
                                        <div class="card-body">
                                            <asp:MultiView runat="server" ID="multiview" ActiveViewIndex="0">
                                                <asp:View runat="server" ID="view1">
                                                    <div class="container my-4">
                                                        <div class="row text-center">
                                                            <div class="col-md-12 mb-12">
                                                                <h1>I am intrested to become</h1>
                                                            </div>
                                                        </div>
                                                        <!--Grid row-->
                                                        <div class="row text-center">
                                                            <asp:Repeater runat="server" ID="rptRoleData" OnItemCommand="rptRoleData_ItemCommand">
                                                                <ItemTemplate>
                                                                    <div class="col-md-6 mb-4">
                                                                        <asp:ImageButton runat="server" ID="image1" CommandName="Res" CommandArgument='<%# Eval("ID").ToString() %>' class="rounded-circle" Height="100" Width="100" alt="100x100" ImageUrl="img/retailer.jpeg" /><br />
                                                                        <strong><%# Eval("Name") %></strong>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>

                                                            <!--Grid column-->

                                                        </div>
                                                        <!--Grid row-->

                                                    </div>
                                                </asp:View>
                                                <asp:View runat="server" ID="view2">
                                                    <div class="form-wrap">
                                                        <div class="row">
                                                            <div class="col-lg-6">
                                                                <div class="form-group">
                                                                    <label for="exampleInputPassword1">Mobile</label>
                                                                    <asp:TextBox ID="txtMobile" ClientIDMode="Static" runat="server" MaxLength="10" class="form-control m-t-xxs"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Mobile" Font-Bold="True" ForeColor="Red" ControlToValidate="txtMobile" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtMobile" />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <div class="form-group">

                                                                    <asp:Button runat="server" ID="btnProcess" Text="Processed" CssClass="btn btn-danger" OnClick="btnProcess_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="showOTPBox" visible="false">
                                                            <div class="col-lg-6">
                                                                <div class="form-group">
                                                                    <label for="exampleInputPassword1">Enter the OTP sent on your mobile number</label>
                                                                    <asp:TextBox ID="txtOTP" runat="server" MaxLength="6" class="form-control m-t-xxs"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="MAinBox" runat="server" visible="false">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <label for="exampleInputPassword1">Title</label>
                                                                    <asp:DropDownList ID="ddltitle" class="form-control" runat="server">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddltitle" InitialValue="0" Font-Bold="True" ForeColor="Red" ErrorMessage="Please select Title" ValidationGroup="chkaddfund" />
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <label for="exampleInputPassword1">Name</label>
                                                                    <asp:TextBox ID="txtName" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Name" Font-Bold="True" ForeColor="Red" ControlToValidate="txtName" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <label for="exampleInputPassword1">City Name</label>
                                                                    <asp:TextBox ID="txtCityName" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Please Enter City Name" Font-Bold="True" ForeColor="Red" ControlToValidate="txtCityName" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="exampleInputPassword1">State</label>
                                                                    <asp:DropDownList ID="dllState" class="form-control" runat="server">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="dllState" InitialValue="0" Font-Bold="True" ForeColor="Red" ErrorMessage="Please select State" ValidationGroup="chkaddfund" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <label for="exampleInputPassword1">Shop Name</label>
                                                                    <asp:TextBox ID="txtShopName" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter Shop Name" Font-Bold="True" ForeColor="Red" ControlToValidate="txtShopName" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <label for="exampleInputPassword1">Email</label>
                                                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                                                        ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                                        Display="Dynamic" ErrorMessage="Invalid email address" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Email" Font-Bold="True" ForeColor="Red" ControlToValidate="txtEmail" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                                                </div>

                                                            </div>

                                                            <div class="row">

                                                                <div class="col-md-6">
                                                                    <label for="exampleInputPassword1">Aadhar Number</label>
                                                                    <asp:TextBox ID="txtAadhar" ClientIDMode="Static" runat="server" MaxLength="12" data-type="adhaar-number" class="form-control m-t-xxs"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Enter Aadhar number" Font-Bold="True" ForeColor="Red" ControlToValidate="txtMobile" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtAadhar" />
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <label for="exampleInputPassword1">Pan Number</label>
                                                                    <asp:TextBox ID="txtPanNumber" runat="server" MaxLength="10" class="form-control m-t-xxs"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="rgxPANCard" runat="server" ValidationExpression="([a-z]){5}([0-9]){4}([a-z]){1}$" ControlToValidate="txtPanNumber" ErrorMessage="Invalid PAN Number" CssClass="error"></asp:RegularExpressionValidator>
                                                                </div>

                                                            </div>
                                                            <div class="row">

                                                                <div class="col-md-6">
                                                                    <label for="exampleInputPassword1">
                                                                        Gender : 
                                                                    </label>
                                                                    <asp:RadioButtonList ID="rdGender" GroupName="Radio" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                                                    </asp:RadioButtonList>
                                                                </div>


                                                                <div class="col-md-6">
                                                                    <label for="exampleInputPassword1">Address</label>
                                                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="3" class="form-control m-t-xxs"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                            <div class="row">

                                                                <div class="col-md-6">
                                                                    <label for="exampleInputPassword1">Pin Code</label>
                                                                    <asp:TextBox ID="txtpincode" runat="server" MaxLength="6" class="form-control m-t-xxs"></asp:TextBox>

                                                                    <asp:RegularExpressionValidator runat="server" ID="rexNumber" ControlToValidate="txtpincode" ValidationExpression="^[0-9]{6}$" ErrorMessage="Please Enter pin Code" ValidationGroup="chkaddfund"></asp:RegularExpressionValidator>
                                                                </div>


                                                                <div class="col-md-6">
                                                                    <label for="exampleInputPassword1">DOB</label>
                                                                    <asp:TextBox ID="txtDOB" runat="server" MaxLength="10" class="form-control m-t-xxs"></asp:TextBox>
                                                                    <cc1:CalendarExtender runat="server" ID="txttxtDOB_ce" Format="dd-MM-yyyy" PopupButtonID="txtDOB"
                                                                        TargetControlID="txtDOB">
                                                                    </cc1:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter DOB" Font-Bold="True" ForeColor="Red" ControlToValidate="txtDOB" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" class="btn btn-primary btn-block" runat="server" Text="Save" ValidationGroup="chkaddfund" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 col-12 mb-20">
                                                                    <div class="checkbox style3">
                                                                        <input type="checkbox" id="test_1">
                                                                        <label for="test_1">
                                                                            I Agree with the <a class="link style1" href="terms-of-service.aspx">Terms &amp; conditions</a>
                                                                        </label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <p class="mb-0">Have an Account? <a class="link style1" href="Member/Default.aspx">Sign In</a></p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:View>
                                            </asp:MultiView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField runat="server" ID="hdnRoleID" ClientIDMode="Static" Value="6" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtMobile" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </section>
    </main>
</asp:Content>

