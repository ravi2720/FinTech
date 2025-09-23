<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" ViewStateMode="Enabled" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Member_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        input[type="radio"] {
            margin: 4px 10px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <div class="main-container container-fluid">
        <div class="main-content-body">



                    <asp:UpdatePanel runat="server" ID="updateRolePanel">
                        <ContentTemplate>
                            <div class="card card-primary">
                                <div class="card card-body">
                                    <asp:MultiView runat="server" ID="multiview" ActiveViewIndex="0">
                                        <asp:View runat="server" ID="view1">
                                            <div class="container my-4">
                                                <div class="row text-center">
                                                    <div class="col-md-12 mb-12">
                                                        <h1>I am intrested in</h1>
                                                    </div>
                                                </div>
                                                <!--Grid row-->
                                                <div class="row text-center">
                                                    <asp:Repeater runat="server" ID="rptRoleList" OnItemCommand="rptRoleList_ItemCommand">
                                                        <ItemTemplate>
                                                            <div class="col-md-3 mb-3">
                                                                <asp:ImageButton runat="server" ID="image1" CommandName="Role" CommandArgument='<%# Eval("ID") %>' class="rounded-circle" alt="100x100" ImageUrl="../img/retailer.png" /><br />
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
                                            <h1 class="main-header-title">Join <%= company.Name %></h1>
                                            <div class="row">

                                                <div class="col-md-6">
                                                    <label for="exampleInputPassword1">Mobile</label>
                                                    <asp:TextBox ID="txtMobile" ClientIDMode="Static" runat="server" MaxLength="10" class="form-control m-t-xxs"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Mobile" Font-Bold="True" ForeColor="Red" ControlToValidate="txtMobile" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtMobile" />
                                                </div>

                                                <div class="col-md-6">
                                                    <br />
                                                    <asp:Button runat="server" ID="btnProcess" Text="Processed" CssClass="btn btn-success" OnClick="btnProcess_Click" />
                                                </div>

                                            </div>
                                            <div class="row" runat="server" id="showOTPBox" visible="false">
                                                <div class="col-md-6">
                                                    <label for="exampleInputPassword1">Enter the OTP sent on your mobile number</label>
                                                    <asp:TextBox ID="txtOTP" runat="server" MaxLength="6" class="form-control m-t-xxs"></asp:TextBox>
                                                </div>

                                                <div class="col-md-6">
                                                    <label for="exampleInputPassword1">Alternative Mobile</label>
                                                    <asp:TextBox ID="txtAlterMobile" runat="server" MaxLength="10" class="form-control m-t-xxs"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtAlterMobile" />
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
                                                        <label for="exampleInputPassword1">Pin Code</label>
                                                        <asp:TextBox ID="txtpincode" TextMode="Number" OnTextChanged="txtpincode_TextChanged" AutoPostBack="true" runat="server" MaxLength="6" class="form-control m-t-xxs"></asp:TextBox>
                                                        <asp:RegularExpressionValidator runat="server" ID="rexNumber" ControlToValidate="txtpincode" ValidationExpression="^[0-9]{6}$" ErrorMessage="Please Enter pin Code" ValidationGroup="chkaddfund"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="exampleInputPassword1">Select PostOffice</label>
                                                        <asp:DropDownList ID="dllPostOffice" class="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label for="exampleInputPassword1">State</label>
                                                        <asp:DropDownList ID="dllState" class="form-control" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="dllState" InitialValue="0" Font-Bold="True" ForeColor="Red" ErrorMessage="Please select State" ValidationGroup="chkaddfund" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="exampleInputPassword1">City Name</label>
                                                        <asp:TextBox ID="txtCityName" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Please Enter City Name" Font-Bold="True" ForeColor="Red" ControlToValidate="txtCityName" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
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
                                                        <span style="color: red; display: none;" id="aadhar-error">Please enter valid Aadhaar Number.</span>

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
                                                        <asp:RadioButtonList ID="rdGender" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                                        </asp:RadioButtonList>
                                                    </div>


                                                    <div class="col-md-6">
                                                        <label for="exampleInputPassword1">Address</label>
                                                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="3" class="form-control m-t-xxs"></asp:TextBox>
                                                    </div>

                                                </div>
                                                <div class="row">



                                                    <div class="col-md-6">
                                                        <label for="exampleInputPassword1">DOB</label>
                                                       
                                                         <asp:TextBox runat="server" TextMode="Date" ID="txtDOB" CssClass="form-control"></asp:TextBox>
                                                     
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter DOB" Font-Bold="True" ForeColor="Red" ControlToValidate="txtDOB" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Button ID="btnSubmit" ClientIDMode="Static" OnClick="btnSubmit_Click" class="btn btn-primary btn-block" runat="server" Text="Save" ValidationGroup="chkaddfund" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            </div>
                    <asp:HiddenField runat="server" ID="hdnRoleID" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtMobile" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>




              
        </div>
    </div>
    <script>

        var d = [
            [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
            [1, 2, 3, 4, 0, 6, 7, 8, 9, 5],
            [2, 3, 4, 0, 1, 7, 8, 9, 5, 6],
            [3, 4, 0, 1, 2, 8, 9, 5, 6, 7],
            [4, 0, 1, 2, 3, 9, 5, 6, 7, 8],
            [5, 9, 8, 7, 6, 0, 4, 3, 2, 1],
            [6, 5, 9, 8, 7, 1, 0, 4, 3, 2],
            [7, 6, 5, 9, 8, 2, 1, 0, 4, 3],
            [8, 7, 6, 5, 9, 3, 2, 1, 0, 4],
            [9, 8, 7, 6, 5, 4, 3, 2, 1, 0]
        ];

        // permutation table p
        var p = [
            [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
            [1, 5, 7, 6, 2, 8, 3, 0, 9, 4],
            [5, 8, 0, 3, 7, 9, 6, 1, 4, 2],
            [8, 9, 1, 6, 0, 4, 3, 5, 2, 7],
            [9, 4, 5, 3, 1, 2, 6, 8, 7, 0],
            [4, 2, 8, 6, 5, 7, 3, 9, 0, 1],
            [2, 7, 9, 3, 8, 0, 6, 4, 1, 5],
            [7, 0, 4, 6, 9, 1, 3, 2, 5, 8]
        ];

        // inverse table inv
        var inv = [0, 4, 3, 2, 1, 5, 6, 7, 8, 9];

        // converts string or number to an array and inverts it
        function invArray(array) {
            if (Object.prototype.toString.call(array) === "[object Number]") {
                array = String(array);
            }
            if (Object.prototype.toString.call(array) === "[object String]") {
                array = array.split("").map(Number);
            }
            return array.reverse();
        }

        // generates checksum
        function generate(array) {
            var c = 0;
            var invertedArray = invArray(array);
            for (var i = 0; i < invertedArray.length; i++) {
                c = d[c][p[((i + 1) % 8)][invertedArray[i]]];
            }
            return inv[c];
        }

        // validates checksum
        function validate(array) {
            var c = 0;
            var invertedArray = invArray(array);
            for (var i = 0; i < invertedArray.length; i++) {
                c = d[c][p[(i % 8)][invertedArray[i]]];
            }
            return (c === 0);
        }

        $("#txtAadhar").change(function () {
            var aaa = $('#txtAadhar').val();
            var aad = aaa.replace(/ /g, '');
            var ana = aad.trim();
            var myStr = parseInt(ana.replace(/'/g, ''));

            if (validate(myStr)) {
                $('#aadhar-error').css("display", "none");
                $('#btnSubmit').removeAttr('disabled');
                console.log("Match");
            } else {
                $('#aadhar-error').css("display", "block");
                $('#btnSubmit').attr('disabled', 'disabled');
                console.log("UnMatch");
            }
        });
        /** Make sure to add the bootstrap-maxlength.js file to your html **/



    </script>
</asp:Content>



