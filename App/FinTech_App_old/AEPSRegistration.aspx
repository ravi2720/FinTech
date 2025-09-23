<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AEPSRegistration.aspx.cs" Inherits="AEPSRegistration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="Member/css/style.css">
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css">
    <link rel="icon" href="images/fevi.png" type="image/png" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="app_css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link href="app_js/alertifyjs/css/alertify.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="app_css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="app_css/ionicons.min.css" />
    <link href="app_css/jquery-ui.min.css" rel="stylesheet" />

    <!-- dataTables -->
    <link href="app_css/datatable/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="app_css/datatable/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <link href="app_css/datatable/responsive.dataTables.min.css" rel="stylesheet" />
    <link href="app_css/datatable/fixedHeader.dataTables.min.css" rel="stylesheet" />
    <link href="app_css/Clockstyle.css" rel="stylesheet" />
    <script src="app_js/alertifyjs/alertify.min.js"></script>
    <link href="app_css/Clockstyle.css" rel="stylesheet" />
    <script src="app_js/qrcode.min.js"></script>

    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script src="https://cdn.jsdelivr.net/npm/vue@2/dist/vue.js"></script>
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css" />
    <!-- jvectormap -->
    <link rel="stylesheet" href="app_css/jquery-jvectormap.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="app_css/AdminLTE.min.css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the lo>d. -->
    <link rel="stylesheet" href="app_css/_all-skins.min.css" />
    <link href="app_css/krishcss.css" rel="stylesheet" />
    <link href="app_css/style.css" rel="stylesheet" />
    <link href="app_css/responsive-style.css" rel="stylesheet" />
    <!-- ==== jQuery Library ==== -->
    <script src="app_js/jquery-3.1.0.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-beta.1/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js" integrity="sha512-2ImtlRlf2VVmiGZsjm9bEyhjGW4dU7B6TNwh/hx/iSByxNENtj3WVE6o/9Lj4TJeVXPi4bnOIMXFIJJAeufa0A==" crossorigin="anonymous"></script>

    
    <link rel="stylesheet"
        href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
    <style>
        .ajax__calendar .ajax__calendar_container {
            z-index: 999999999999999 !important;
        }


        .dropdown-menu {
            border: 0px solid rgba(0,0,0,.15) !important;
        }

        #load {
            width: 100%;
            height: 100%;
            position: fixed;
            z-index: 9999;
            background: url("img/Loading.gif") no-repeat center center rgba(0,0,0,0.25);
        }


        html {
            background-color: rgb(246, 246, 246) !important;
        }

        .dropdown-menu {
            font-size: 1.5rem !important;
        }

        .panel-primary > .panel-heading {
            color: #fff;
            background-color: #072d4d !important;
            border-color: #337ab7;
        }

        .panel-primary > .panel-heading {
            color: #fff;
            background-color: #072d4d !important;
            border-color: #337ab7;
        }

        .btn-info {
            background-color: #072d4d !important;
            border-color: #00acd6;
        }

        .qr img {
            background: #fff;
            padding: 10px;
            box-shadow: 0px 0px 20px #d0d0d0;
            text-align: center;
            margin: 0 auto;
        }

        .logo {
            margin: 15px 0px;
            margin-top: 0px;
        }

        .scan {
            margin: 10px 0px;
        }

            .scan ul {
                list-style: none;
                padding: 0px;
                margin: 0px;
            }

                .scan ul li {
                    padding: 5px 20px;
                    display: inline-block;
                }
    </style>



    <script src="app_js/MainAction.js"></script>

    <!-- ==== jQuery UI ==== -->
    <script src="app_js/jquery-ui.min.js"></script>

    <!-- ==== jQuery UI Touch Punch Plugin ==== -->


    <!-- ==== Bootstrap ==== -->
    <script src="app_js/bootstrap.min.js"></script>



    <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link href='https://fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet' type='text/css' />

    <link href="css/styleheader.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="content-wrapper">
            <section class="content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>
                        <section class="dashboard">
                            <div class="left-dashboard">
                                <div class="container-fluid">
                                    <div class="row main-col card-white">

                                        <div id="main-div" class="col-lg-12 col-md-12 pos-rel o-hidden btm-space">
                                            <div class="right-part">
                                                <div class="row"></div>
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
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </ContentTemplate>
                </asp:UpdatePanel>
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

    
    <!-- ==== DataTable ==== -->
    <script src="app_js/datatable/jquery.dataTables.js"></script>
    <script src="app_js/datatable/dataTables.bootstrap4.min.js"></script>
    <script src="app_js/datatable/dataTables.buttons.min.js"></script>
    <script src="app_js/datatable/dataTables.responsive.min.js"></script>
    <script src="app_js/datatable/dataTables.fixedHeader.min.js"></script>

    <!-- ==== FakeLoader Plugin ==== -->
    <script src="app_js/fakeLoader.min.js"></script>

    <!-- ==== StickyJS Plugin ==== -->
    <script src="app_js/jquery.sticky.js"></script>

    <!-- ==== Owl Carousel Plugin ==== -->
    <script src="app_js/owl.carousel.min.js"></script>

    <!-- ==== jQuery Tubuler Plugin ==== -->
    <script src="app_js/jquery.tubular.1.0.js"></script>

    <!-- ==== Magnific Popup Plugin ==== -->
    <script src="app_js/jquery.magnific-popup.min.js"></script>

    <!-- ==== jQuery Validation Plugin ==== -->
    <script src="demo/app_js/jquery.validate.min.js"></script>

    <!-- ==== Animate Scroll Plugin ==== -->
    <script src="app_js/animatescroll.min.js"></script>

    <!-- ==== jQuery Waypoints Plugin ==== -->
    <script src="app_js/jquery.waypoints.min.js"></script>

    <!-- ==== jQuery CounterUp Plugin ==== -->
    <script src="app_js/jquery.counterup.min.js"></script>

    <!-- ==== jQuery CountDown Plugin ==== -->
    <script src="app_js/jquery.countdown.min.js"></script>

    <!-- ==== alertify ==== -->
    <script src="app_js/alertifyjs/alertify.min.js"></script>

    <!-- ==== RetinaJS ==== -->
    <script src="app_js/retina.min.js"></script>

    <!-- ==== Main JavaScript ==== -->
    <script src="app_js/main.js"></script>
    <script src="app_js/moment.min.js"></script>
    <script src="app_js/Clockscript.js"></script>
    <script src="app_js/fastclick.js"></script>
    <!-- AdminLTE App -->
    <script src="app_js/adminlte.min.js"></script>
    <!-- Sparkline -->
    <script src="app_js/jquery.sparkline.min.js"></script>
    <!-- jvectormap  -->
    <script src="app_js/jquery-jvectormap-1.2.2.min.js"></script>
    <script src="app_js/jquery-jvectormap-world-mill-en.js"></script>

    <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.6/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.print.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <script src="js/Main.js"></script>
</body>
</html>
