<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AepsRegistration.aspx.cs" Inherits="Admin_AepsRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">

                <div class="col-md-4">
                    <strong>MemberID</strong>
                    <asp:TextBox runat="server" CssClass="form-control ValidationVal" ID="txtMemberID" placeholder="Enter Member ID" AutoPostBack="true" OnTextChanged="txtMemberID_TextChanged"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>First Name</strong>
                        <asp:TextBox runat="server" ID="txtFirstName" onkeyup="onKeyPress(this)" onkeypress="onKeyPress(this)" placeholder="First Name *" CssClass="form-control ValidationVal"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Middle Name</strong>
                        <asp:TextBox runat="server" ID="txtMiddleName" placeholder="Middle Name" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

            </div>
            <div class="row">

                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Last Name</strong>

                        <asp:TextBox runat="server" ID="txtLastName" onkeyup="onKeyPress(this)" onkeypress="onKeyPress(this)" placeholder="Last Name *" CssClass="form-control ValidationVal"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Email ID</strong>
                        <asp:TextBox runat="server" lookName="Email" Enabled="false" onkeyup="onKeyPress(this)" onkeypress="onKeyPress(this)" ID="txtEmialid" placeholder="Email ID *" CssClass="form-control ValidationVal"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Primary Mobile Number</strong>
                        <asp:TextBox runat="server" ID="txtPMobileName" Enabled="false" MaxLength="10" ClientIDMode="Static" onkeypress="return isNumber(event)" lookName="PMobile" placeholder="Primary Mobile Number *" CssClass="form-control ValidationVal"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Alternate Mobile Number</strong>
                        <asp:TextBox runat="server" ID="txtAMobileName" MaxLength="10" onkeypress="return isNumber(event)" placeholder="Alternate Mobile Number " CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>DOB</strong>
                        <asp:TextBox runat="server" ID="txtDOB" onkeyup="onKeyPress(this)" onkeypress="onKeyPress(this)" placeholder="Date of Birth (DD-MM-YYYY) *" CssClass="form-control ValidationVal"></asp:TextBox>
                        <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="dd-MM-yyyy" PopupButtonID="txtDOB"
                            TargetControlID="txtDOB">
                        </cc1:CalendarExtender>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>State</strong>
                        <asp:DropDownList runat="server" ID="dllState" AutoPostBack="true" OnSelectedIndexChanged="dllState_SelectedIndexChanged" CssClass="form-control ValidationVal"></asp:DropDownList>
                    </div>
                </div>

            </div>
            <div class="row">

                <div class="col-md-4">
                    <div class="form-group">
                        <strong>District</strong>
                        <asp:DropDownList ID="dllDistrict" runat="server" CssClass="form-control ValidationVal"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Block</strong>
                        <asp:TextBox ID="txtBlock" onkeyup="onKeyPress(this)" onkeypress="onKeyPress(this)" runat="server" CssClass="form-control ValidationVal" placeholder="Block *"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>City</strong>
                        <asp:TextBox ID="txtCity" onkeyup="onKeyPress(this)" onkeypress="onKeyPress(this)" runat="server" CssClass="form-control ValidationVal" placeholder="City *"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <strong>Address</strong>
                        <asp:TextBox ID="txtAdddress" onkeyup="onKeyPress(this)" onkeypress="onKeyPress(this)" runat="server" TextMode="MultiLine" Rows="3" placeholder="Address....*" CssClass="form-control ValidationVal"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">

                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Landmark</strong>
                        <asp:TextBox ID="txtLandMark" onkeyup="onKeyPress(this)" onkeypress="onKeyPress(this)" runat="server" CssClass="form-control ValidationVal" placeholder="Landmark *"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Location</strong>
                        <asp:TextBox ID="txtLocation" onkeyup="onKeyPress(this)" onkeypress="onKeyPress(this)" runat="server" CssClass="form-control ValidationVal" placeholder="Location *"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Mohhalla</strong>
                        <asp:TextBox ID="txtMohhalla" onkeyup="onKeyPress(this)" onkeypress="onKeyPress(this)" runat="server" CssClass="form-control ValidationVal" placeholder="Mohhalla *"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Pan Number</strong>
                        <asp:TextBox ID="txtPanNo" onkeyup="onKeyPress(this)" onkeypress="onKeyPress(this)" runat="server" CssClass="form-control ValidationVal" placeholder="Pan Number *"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Pincode</strong>
                        <asp:TextBox ID="txtPinCode" onkeyup="onKeyPress(this)" onkeypress="onKeyPress(this)" runat="server" CssClass="form-control ValidationVal" placeholder="Pincode *"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Shop Name</strong>
                        <asp:TextBox runat="server" onkeyup="onKeyPress(this)" onkeypress="onKeyPress(this)" ID="txtShopName" CssClass="form-control ValidationVal" placeholder="ShopName *"></asp:TextBox>
                    </div>
                </div>
            </div>
            <%--<div class="row">
                    
                    <div class="col-md-4">
                        <div class="form-group">
                            <strong>Identity Proof</strong>
                            <asp:FileUpload ID="fileIdentityP"  runat="server" onchange="loadFile(event,'ImgFileIdentity')" CssClass="form-control" />
                             <img id="ImgFileIdentity" alt="your image" width="100" height="100" />
                        </div>
                    </div>
               

                    <div class="col-md-4">
                        <div class="form-group">
                            <strong>Address Proof</strong>
                            <asp:FileUpload ID="fileAddressP" runat="server" onchange="loadFile(event,'ImgfileAddressP')" CssClass="form-control" />
                             <img id="ImgfileAddressP" alt="your image" width="100" height="100" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <strong>Shop Photo</strong>
                            <asp:FileUpload ID="fileShopPhotoP" onchange="loadFile(event,'blahfileShopPhotoP')" runat="server" CssClass="form-control" />
                            <img id="blahfileShopPhotoP" alt="your image" width="100" height="100" />
                        </div>
                    </div>
                     </div>--%>
            <div class="row">
                <%-- <div class="col-md-4">
                        <div class="form-group">
                            <strong>Passport Size Photo</strong>
                            <asp:FileUpload ID="filePassSizePhotoP" onchange="loadFile(event,'ImgfilePassSizePhotoP')" runat="server" CssClass="form-control" />
                             <img id="ImgfilePassSizePhotoP" alt="your image" width="100" height="100" />
                        </div>
                    </div>--%>

                <div class="col-md-4">
                    <div class="form-group">
                        <strong>TypeofShop</strong>
                        <asp:DropDownList ID="dllTypeOfShop" runat="server" CssClass="form-control ValidationVal" onchange="ChangeData(this)">
                            <asp:ListItem Text="Select TypeofShop" Value="Select TypeofShop"></asp:ListItem>

                            <asp:ListItem Text="Mobile Shop" Selected="True" Value="Mobile Shop"></asp:ListItem>
                            <asp:ListItem Text="Copier Shop" Value="Copier Shop"></asp:ListItem>
                            <asp:ListItem Text="Internet cafe" Value="Internet cafe"></asp:ListItem>
                            <asp:ListItem Text="other" Value="other"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtOtherType" placeholder="Enter Other Shop Type" BorderColor="Red" ClientIDMode="Static" Style="display: none;" CssClass="form-control"></asp:TextBox>

                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Type of qualification</strong>
                        <asp:DropDownList ID="ddlQualification" runat="server" CssClass="form-control ValidationVal">
                            <asp:ListItem Text="Select Qualification" Value="Select Qualification"></asp:ListItem>
                            <asp:ListItem Text="SSC" Value="SSC"></asp:ListItem>
                            <asp:ListItem Text="HSC" Value="HSC"></asp:ListItem>
                            <asp:ListItem Text="Graduate" Selected="True" Value="Graduate"></asp:ListItem>
                            <asp:ListItem Text="Post Graduate" Value="Post Graduate"></asp:ListItem>
                            <asp:ListItem Text="Diploma" Value="Diploma"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Area Population</strong>
                        <asp:DropDownList ID="ddlAreaPopulation" runat="server" CssClass="form-control ValidationVal">
                            <asp:ListItem Text="Select Population" Value="Select Population"></asp:ListItem>
                            <asp:ListItem Text="0 to 2000" Value="0 to 2000"></asp:ListItem>
                            <asp:ListItem Text="2000 to 5000" Value="2000 to 5000"></asp:ListItem>
                            <asp:ListItem Text="5000 to 10000" Selected="True" Value="5000 to 10000"></asp:ListItem>
                            <asp:ListItem Text="10000 to 50000e" Value="10000 to 50000"></asp:ListItem>
                            <asp:ListItem Text="50000 to 100000" Value="50000 to 100000"></asp:ListItem>
                            <asp:ListItem Text="100000+" Value="100000+"></asp:ListItem>
                        </asp:DropDownList>

                    </div>
                </div>

                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Type of location</strong>
                        <asp:DropDownList ID="ddlTypeOfLocation" runat="server" CssClass="form-control ValidationVal">
                            <asp:ListItem Text="Select location" Value="Select location"></asp:ListItem>
                            <asp:ListItem Text="Rural" Value="Rural"></asp:ListItem>
                            <asp:ListItem Text="Urban" Selected="True" Value="Urban"></asp:ListItem>
                            <asp:ListItem Text="Metro Semi Urban" Value="Metro Semi Urban"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <br />
                    <strong></strong>
                    <asp:Button runat="server" Text="Submit" OnClientClick="return CheckVal('.ValidationVal')" CssClass="form-control btn btn-success" OnClick="Unnamed_Click" />
                </div>
            </div>
        </section>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <script>
        var loadFile = function (event, outputID) {
            var reader = new FileReader();
            reader.onload = function () {
                var output = document.getElementById(outputID);
                output.src = reader.result;
            };
            reader.readAsDataURL(event.target.files[0]);
        };
        function CheckVal(cl) {
            try {
                var flag = true;
                $(cl).each(function () {
                    debugger;
                    var lookName = (this.getAttribute("lookName") == null ? "" : this.getAttribute("lookName"));
                    if (this.value == "" || this.value == "Select District" || this.value == "Select State" || this.value == "Select TypeofShop" || this.value == "Select location" || this.value == "Select Population" || this.value == "Select Qualification") {
                        this.style.border = "1px solid red";
                        flag = false;
                    } else {
                        if (lookName == "PMobile") {
                            if (this.value.length < 10) {
                                this.style.border = "1px solid red";
                                this.focus();
                                flag = false;
                            }
                        } else if (lookName == "Email") {
                            if (checkEmail(this) == false) {
                                this.style.border = "1px solid red";
                                flag = false;
                            }
                        }
                        else {
                            this.style.border = "1px solid lightgrey";
                        }
                    }
                });
            } catch (err) {
            }
            return flag;
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function onKeyPress(obj) {
            if (obj.value == "") {
                obj.style.border = "1px solid red";
            } else {
                obj.style.border = "1px solid lightgrey";
            }
        }
        function checkEmail(obj) {
            debugger;
            var email = obj;
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            if (!filter.test(email.value)) {
                alert('Please provide a valid email address');
                email.focus();
                return false;
            }
        }
        function ChangeData(ObjData) {
            try {
                debugger;
                if (ObjData.selectedOptions[0].value == "other") {
                    document.getElementById("txtOtherType").style.display = "";
                } else {
                    document.getElementById("txtOtherType").style.display = "none";
                }

            } catch (err) {
            }
        }
    </script>
</asp:Content>

