<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MATMOnBoard.aspx.cs" Inherits="MATMOnBoard" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="app_js/alertifyjs/css/alertify.min.css" rel="stylesheet" />
    <script src="app_js/alertifyjs/alertify.min.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="content-wrapper">
            <section class="content">
                <div class="container-fluid">
                    <div class="panel panel-primary">
                        <div class="panel-heading">MATM OnBoard</div>
                        <div class="panel-body">
                            <asp:MultiView runat="server" ID="multiple" ActiveViewIndex="0">
                                <asp:View runat="server" ID="view1">
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
                                                <cc1:calendarextender runat="server" id="txtfromdate_ce" format="dd-MM-yyyy" popupbuttonid="txtDOB"
                                                    targetcontrolid="txtDOB">
                                            </cc1:calendarextender>
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

                                    <div class="row">


                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <strong>TypeofShop</strong>
                                                <asp:DropDownList ID="dllTypeOfShop" runat="server" CssClass="form-control ValidationVal" onchange="ChangeData(this)">
                                                    <asp:ListItem Text="Select TypeofShop" Value="Select TypeofShop"></asp:ListItem>

                                                    <asp:ListItem Text="Mobile Shop" Value="Mobile Shop"></asp:ListItem>
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
                                                    <asp:ListItem Text="Graduate" Value="Graduate"></asp:ListItem>
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
                                                    <asp:ListItem Text="5000 to 10000" Value="5000 to 10000"></asp:ListItem>
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
                                                    <asp:ListItem Text="Urban" Value="Urban"></asp:ListItem>
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
                                </asp:View>
                                <asp:View runat="server" ID="view2">
                                </asp:View>
                            </asp:MultiView>


                        </div>

                    </div>

                </div>
            </section>
        </div>
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
    </form>
</body>
</html>
