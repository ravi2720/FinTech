<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Member_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" />
    <link href="../app_js/alertifyjs/css/alertify.min.css" rel="stylesheet" />
    <script src="../app_js/alertifyjs/alertify.min.js"></script>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <style>
        #load {
            width: 100%;
            height: 100%;
            position: fixed;
            z-index: 9999;
            background: url("../img/Loading.gif") no-repeat center center rgba(0,0,0,0.25);
        }

        @media only screen and (max-width: 960px) {
            .col-xs-12 {
                display: flex;
                flex-direction: column-reverse;
            }
        }


        * {
            overflow-x: hidden;
        }

        .input-container {
            display: -ms-flexbox; /* IE10 */
            display: flex;
            width: 100%;
            margin-bottom: 15px;
        }

        .icon {
            padding: 10px;
            background: #273896;
            color: white;
            min-width: 50px;
            text-align: center;
        }

        .input-field {
            width: 100%;
            padding: 10px;
            outline: none;
        }

            .input-field:focus {
                border: 2px solid #273896;
            }

        .top-header li {
            margin: 10px 30px 0px;
        }

        .language-select {
            float: right;
            display: flex;
            height: 50px;
            align-items: center;
            font-size: 14px;
            color: #323232;
            font-weight: normal;
        }

            .language-select label {
                margin-right: 10px;
                display: flex;
                align-items: center;
                cursor: pointer;
            }

            .language-select input {
                vertical-align: middle;
                margin: 0 5px;
            }

        .top-header {
            background-color: #F2F2F2;
        }

        @media (max-width: 767px) {
            .d {
                display: none;
            }
        }

        @media (min-width: 767px) {
            .f {
                display: none;
            }
        }

        .bg {
            background-color: #F2F2F2;
        }

        .t {
            background-color: #EBEBEB;
            border-radius: 20px;
            box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
            color: black;
        }

            .t:hover {
                transition: 2s;
                background-color: whitesmoke;
                color: red;
                transform: translateX(-10px);
            }
    </style>
    <style>
        .ticker-wrapper {
            width: 100%;
            margin: 0 auto;
            overflow: hidden;
            white-space: nowrap;
            height: 3em;
            position: static;
            background-color: white;
        }

        .ticker-transition {
            display: inline-block;
            animation: marqueehorizontal 50s linear infinite;
        }

            .ticker-transition:hover {
                animation-play-state: paused;
            }



        .tickeritemcont--1 {
            position: relative;
            left: 0%;
            animation: swap 50s linear infinite;
        }

        .ticker-item {
            display: inline-block;
            padding: 0 1rem;
            font-size: 1.5em;
            color: black;
            font-weight: 800;
            font-family: sans-serif;
            color: red
        }

        .ticker-item1 {
            display: inline-block;
            padding: 0 1rem;
            font-size: 1.5em;
            color: black;
            font-weight: 800;
            font-family: sans-serif;
            color: blue;
        }
        /* Transition */
        @keyframes marqueehorizontal {
            0% {
                transform: translateX(0)
            }

            100% {
                transform: translateX(-100%)
            }
        }

        @keyframes swap {
            0%, 50% {
                left: 0%;
            }

            50.01%, 100% {
                left: 100%;
            }
        }
    </style>



    <script>
        function Op() { $('#pdwTPIN').modal('show'); }
    </script>
    <script>
        function myFunction() {
            var x = document.getElementById("passwordfield");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="load" style="visibility: hidden;"></div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel runat="server" ID="update">
                <ContentTemplate>
                    <div class="top-header">
                        <div class="row mt-2">
                            <div class="col-md-5  col-12 d-flex">
                                <div class="col-md-4">
                                    <img src='<%= "../images/Company/"+company.Logo.ToString() %>' style="height: 50px;" alt="logo">
                                </div>
                                <div class="col-md-4">
                                    <button type="button" class="btn btn-primary " style="background-color: #273896;">Join Us</button>
                                </div>
                                <div class="col-md-4">
                                    <a href='<%=company.FaceBook %>'>
                                        <i class="bi bi-facebook" style="font-size: xx-large; color: #026CDF;"></i></a>
                                    &nbsp; &nbsp; &nbsp;
                                    <a href='<%=company.Youtube %>'>
                                        <i class="bi bi-youtube" style="font-size: xx-large; color: #F70000;"></i></a>
                                </div>
                            </div>
                            <div class="col-md-3 col-3 text-center mt-3 d">
                                <i class="bi bi-envelope-fill"></i>&nbsp; <b><%=company.Email %></b>
                            </div>
                            <div class="col-md-2 col-3 text-center mt-3 d">
                                <i class="bi bi-telephone-fill"></i>&nbsp;<b><%=company.Mobile %></b>
                            </div>
                            <div class="col-md-2 col-3 mt-3 d text-center pe-4">
                                <input type="radio" name="language" id="1" /><b>&nbsp;English</b>
                                <input type="radio" name="language" id="2" /><b>&nbsp;हिन्दी</b>
                            </div>
                        </div>
                    </div>
                    <div class="row f">
                        <div class="col-md-12 text-end pe-4">
                            <input type="radio" name="language" id="1" /><b>&nbsp;English</b>
                            <input type="radio" name="language" id="2" /><b>&nbsp;हिन्दी</b>
                        </div>
                    </div>
                    <section class="mt-4">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="">
                                    <div class="ticker-wrapper">
                                        <div class="ticker-transition">
                                            <span class="tickeritemcont--1">
                                                <asp:Repeater runat="server" ID="NewsData">
                                                    <ItemTemplate>
                                                        <span class="ticker-item" style="font-size: 22px"><b><%# Eval("Description") %></b></span>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row col-xs-12">
                                <div class="col-md-8 col-xs-12">
                                    <div id="carouselExampleIndicators" class="carousel slide" data-bs-ride="carousel">
                                        <div class="carousel-indicators">
                                            <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
                                            <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="1" aria-label="Slide 2"></button>
                                            <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="2" aria-label="Slide 3"></button>
                                        </div>
                                        <div class="carousel-inner">
                                            <asp:Repeater runat="server" ID="rptDashBoardBanner">
                                                <ItemTemplate>
                                                    <div class='<%# (Container.ItemIndex.ToString()=="0"?"carousel-item active":"carousel-item") %>'>
                                                        <img class="d-block w-100 img-fluid" style="max-height: 520px" src='<%# "../images/Banner/"+ Eval("ImagePath").ToString()%>' alt="First slide">
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="prev">
                                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                            <span class="visually-hidden">Previous</span>
                                        </button>
                                        <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
                                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                            <span class="visually-hidden">Next</span>
                                        </button>
                                    </div>
                                </div>

                                <div class="col-md-4 col-12">
                                    <div class="container">
                                        <div class="row text-center">
                                            <a href="#" style="color: #273896; font-size: larger; text-decoration: none;">Login to Continue</a>
                                        </div>

                                        <asp:MultiView runat="server" ID="multi" ActiveViewIndex="0">

                                            <asp:View runat="server" ID="view1">
                                                <asp:Panel ID="p1" runat="server" DefaultButton="btnLogin">
                                                    <div class="text-center mt-4">
                                                        <div class="input-container">
                                                            <i class="bi bi-person-fill icon"></i>
                                                            <input type="text" class="input-field" runat="server" id="email" placeholder="Enter your Agent ID" />
                                                        </div>
                                                    </div>
                                                    <div class="text-center mt-4">
                                                        <div class="input-container">
                                                            <i class="bi bi-key-fill icon"></i>
                                                            <input type="password" class="input-field" runat="server" id="passwordfield" placeholder="Enter Your Password" value="FakePSW" />
                                                            <i class="bi bi-eye-slash fw-bold mt-2" onclick="myFunction()" style="margin-left: -30px; cursor: pointer; margin-right: 17px;"
                                                                id="togglePassword"></i>
                                                        </div>

                                                    </div>
                                                    <div class="row mt-4">
                                                        <div class="input-container">
                                                            <asp:CheckBox ID="chkterms" runat="server" Checked="true" CssClass="form-check" />
                                                            <label for="checkbox">&nbsp; I agree to usage | &nbsp;<a data-target="#TermsCondition" style="cursor: pointer;" data-toggle="modal" style="color: #273896;">Terms & conditions</a></label>
                                                        </div>
                                                    </div>
                                                    <div class="row mt-4">
                                                        <div class="input-container">
                                                            <asp:Label runat="server" ID="lblMessage" Style="font-weight: bold; color: red"></asp:Label>
                                                            <asp:Label runat="server" ID="lblName" Style="font-weight: bold;"></asp:Label>
                                                            <asp:Button ID="btnLogin" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...';" runat="server" OnClick="btnLogin_Click" Text="Login" ClientIDMode="Static" CssClass="btn btn-primary w-100" Style="background-color: #273896;" />
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </asp:View>



                                            <asp:View runat="server" ID="view2">
                                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnttpin">
                                                    <div class="col-md-12">
                                                        <div class="login-detail">
                                                            <div class="container outer-box">
                                                                <div>
                                                                    <div class="otp-row">
                                                                        <asp:TextBox runat="server" ID="txtotp" CssClass="input-field" placeholder="Enter TPIN"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Button runat="server" ID="btnttpin" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...';" Text="Verify" CssClass="btn btn-primary w-100 mt-2" Style="background-color: #273896;" OnClick="btnttpin_Click"></asp:Button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </asp:View>
                                        </asp:MultiView>
                                        <div class="row mt-1">
                                            <div class="input-container">
                                                <a href="../register.aspx" style="color: #273896;">Sign Up?</a>&nbsp; | &nbsp;<a href="javascript:void(0)" data-target="#pwdModal" data-toggle="modal" style="color: #273896;">Forget Password?</a>
                                            </div>
                                        </div>
                                        <div class="t mb-2 text-center">
                                            <div class="row text-center m-2">
                                                <p><b>Download <%=company.Name %> Mobile App</b></p>
                                            </div>
                                            <center>
                                                   <a href='<%=company.AndroidURL %>' class="btn btn-primary mb-4" style="background-color: #273896;" >Download Now

                                                   </a>
                                                    </center>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </section>
                    <asp:HiddenField runat="server" ID="hdnLongitude" ClientIDMode="Static" />
                    <asp:HiddenField runat="server" ID="hdnLatitude" ClientIDMode="Static" />
                </ContentTemplate>
            </asp:UpdatePanel>


        </div>
        <div id="pdwTPIN" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="text-center">2 Step Authentication</h1>
                    </div>
                    <div class="modal-body">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="text-center">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>

                                                <p>Your account login in other device.</p>
                                                <div class="panel-body">
                                                    <fieldset>
                                                        <div class="form-group">
                                                            <asp:TextBox runat="server" ID="txtTpin" MaxLength="4" TextMode="Password" ForeColor="Red" CssClass="form-control" placeholder="Enter tpin"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group">
                                                            <%--<asp:Button runat="server" ID="btnttpin1" Text="Verify" CssClass="btn btn-danger" OnClick="btnttpin_Click"></asp:Button>--%>
                                                        </div>
                                                    </fieldset>
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="col-md-12">
                            <button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />

        <footer class="bg mt-4">
            <div class="container">
                <div class="row text-center mt-2">
                    <div class="col-md-6 col-12"><i class="bi bi-envelope-fill"></i>&nbsp; <b><%=company.Email %></b></div>
                    <div class="col-md-6 col-12"><i class="bi bi-telephone-fill"></i>&nbsp;<b><%=company.Mobile %></b></div>
                    <div class="col-md-12">
                        <p>© Copyright 2024 <%=company.Name %>. All right reserved. | About <%=company.Name %></p>
                    </div>

                </div>
            </div>
        </footer>

        <div id="TermsCondition" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="text-center">TermsCondition</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>

                    </div>
                    <div class="modal-body">
                        <div class="col-md-12">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                <ContentTemplate>


                                    <asp:Repeater runat="server" ID="rptDataTermsCondition">
                                        <ItemTemplate>

                                            <!--team-1-->
                                            <div class="col-lg-12">



                                                <div class="team-back">
                                                    <span>
                                                        <%# Eval("Description") %>
                                                    </span>
                                                </div>


                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <asp:HiddenField runat="server" ID="HiddenField1" ClientIDMode="Static" />
                                    <asp:HiddenField runat="server" ID="HiddenField2" ClientIDMode="Static" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <div class="col-md-12">
                            <button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="pwdPanel">
            <div id="pwdModal" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h1 class="text-center">What's My Password?</h1>
                        </div>
                        <div class="modal-body">
                            <div class="col-md-12">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="text-center">
                                            <asp:UpdatePanel runat="server" ID="passupdate">
                                                <ContentTemplate>

                                                    <p>If you have forgotten your password you can reset it here.</p>
                                                    <div class="panel-body">
                                                        <fieldset>
                                                            <div class="form-group">
                                                                <asp:TextBox runat="server" ID="txtMobile" ForeColor="Red" CssClass="form-control" placeholder="Enter LoginID"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:TextBox runat="server" ForeColor="Red" ID="txtOTPVerify" Visible="false" CssClass="form-control input-lg" placeholder="Enter OTP"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:TextBox runat="server" ForeColor="Red" ID="txtNewPassword" Visible="false" CssClass="form-control input-lg" placeholder="Enter New Password"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:Button runat="server" ID="btnForgetPassword" Text="Verify" CssClass="btn btn-primary" Style="background-color: #273896;" OnClick="btnForgetPassword_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...';"></asp:Button>
                                                            </div>
                                                        </fieldset>
                                                    </div>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-md-12">
                                <button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <div class="modal fade" id="global-modal" role="dialog">
            <div class="modal-dialog modal-lg">
                <!--Modal Content-->
                <div class="modal-content">
                    <div class="modal-header">

                        <div id="myCarousel" class="carousel slide" data-ride="carousel">


                            <!-- Wrapper for slides -->
                            <div class="carousel-inner">
                                <asp:Repeater runat="server" ID="RptData">
                                    <ItemTemplate>
                                        <div class='<%# (Container.ItemIndex.ToString()=="0"?"carousel-item active":"carousel-item") %>'>
                                            <%-- <img src='<%# "../images/Banner/"+ Eval("ImagePath").ToString()%>' alt="First slide">--%>
                                            <%# Eval("Description") %>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <!-- Left and right controls -->
                            <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                                <span class="glyphicon glyphicon-chevron-left"></span>
                                <span class="sr-only">Previous</span>
                            </a>
                            <a class="right carousel-control" href="#myCarousel" data-slide="next">
                                <span class="glyphicon glyphicon-chevron-right"></span>
                                <span class="sr-only">Next</span>
                            </a>
                        </div>

                    </div>
                </div>
            </div>
        </div>
          <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
        <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
        <script>
            $(document).ready(function () {
                $('#global-modal').modal('show');
            });
        </script>
        <script>
            var req = Sys.WebForms.PageRequestManager.getInstance();
            req.add_beginRequest(function () {
                document.getElementById('load').style.visibility = "visible";
            });
            req.add_endRequest(function () {
                document.getElementById('load').style.visibility = "hidden";



            });


        </script>
        <script>
            function Op1() { $('#pdwTPIN').modal('show'); }

            $(document).ready(function () {
                getLocation();
            });
            function getLocation() {
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(showPosition, showError);
                }
                else {
                    alert("Geolocation is not supported by this browser.");
                }
            }
            function showPosition(position) {
                var latlondata = position.coords.latitude + "," + position.coords.longitude;
                $('#btnLogin').removeClass('btn-disabled');
                $('#hdnLatitude').val(position.coords.latitude);
                $('#hdnLongitude').val(position.coords.longitude);

            }
            function showError(error) {
                if (error.code == 1) {
                    $('#btnLogin').addClass('btn-disabled');
                    alert("Allow location for log in");
                }
                else if (err.code == 2) {
                    $('#btnLogin').addClass('btn-disabled');
                    alert("Allow location for log in");
                }
                else if (err.code == 3) {
                    $('#btnLogin').addClass('btn-disabled');
                    alert("Allow location for log in");
                }
                else {
                    $('#btnLogin').addClass('btn-disabled');
                    alert("Allow location for log in");
                }
            }


            function chnagefocus(id, event1) {
                try {
                    document.getElementById(id).focus();
                } catch (e) {

                }

                var indew = id.replace("otpValue", "");


                var keycode = (event1.keyCode ? event1.keyCode : event1.which);
                if (keycode == '8') {
                    indew = "otpValue" + (parseFloat(indew) - 2);
                    document.getElementById(indew).focus();
                    document.getElementById(indew).value = "";
                    document.getElementById(id).value = "";
                } else {

                }
            }
            function backCall(id, ids) {
                var keycode = (id.keyCode ? id.keyCode : id.which);
                if (keycode == '8') {
                    document.getElementById(ids).focus();
                    document.getElementById(ids).value = "";
                }

            }

        </script>
      
    </form>

</body>
</html>
