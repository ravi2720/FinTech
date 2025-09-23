<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= company.Name %></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="icon" href="<%= "./images/Company/"+ company.Fevicon %>" type="image/png" />

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="cssL/style.css" />
    <link rel="stylesheet" type="text/css" href="cssL/owl.carousel.min.css" />
    <link rel="stylesheet" type="text/css" href="cssL/font-awesome.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/owl.carousel.min.js"></script>
    <link href="alertifyjs/css/alertify.min.css" rel="stylesheet" />
    <script src="alertifyjs/alertify.min.js"></script>
    <script>
        function Op() { $('#pdwTPIN').modal('show'); }
    </script>
    <style>
        img.logo {
            max-width: 225px;
        }

        .navbar {
            padding: 0px 0px 30px !important;
            background: #fff;
            border: none;
            border-radius: 0;
            box-shadow: 1px 1px 3px rgb(0 0 0 / 10%);
        }
    </style>
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
        <div id="load" style="visibility: hidden;"></div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="true"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="update">
            <ContentTemplate>
                <section class="top-menu">
                    <div class="container-fluid">
                        <div class="row">
                            <div class='col-md-12'>
                                <nav class="navbar fixed-top  p-0">
                                    <div class="navbar-brand pt-0 pl-1 logoPattern" style="cursor: pointer;" onclick="location = '/login'">
                                        <div class="d-inline-flex">
                                            <div>
                                                <img class="logo" style="height: 100px; max-height: 85px;" src="<%= Application["URL"].ToString() %>" alt="<%= company.Name %>">
                                            </div>
                                        </div>


                                    </div>
                                </nav>
                            </div>
                        </div>
                    </div>

                </section>

                <section class="mobile-app">
                    <div class="container">
                        <div class="row justify-content-center">
                            <div class="col-md-3"></div>
                            <div class="col-md-5">
                                <div class="login-part">

                                    <div class="form-box">
                                        <div>
                                            <div class="title">
                                                Sign In Here
                                            </div>
                                            <asp:MultiView runat="server" ID="mul" ActiveViewIndex="0">
                                                <asp:View runat="server" ID="view1">
                                                    <div class="form-group">
                                                        <label>Enter Your LoginID</label>
                                                        <asp:TextBox ID="username" runat="server" class="form-control" placeholder="Enter your user ID"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Button runat="server" ID="btnProcess" CssClass="btn btn-danger" Text="Process" OnClick="btnProcess_Click" />
                                                    </div>
                                                </asp:View>
                                                <asp:View runat="server" ID="view2">
                                                    <div class="form-group">
                                                        <label>Enter Your OTP</label>
                                                        <asp:TextBox ID="txtvOTP" runat="server" TextMode="Password" class="form-control" placeholder="Enter your OTP"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>Set Your Pass</label>
                                                        <asp:TextBox ID="txtSetPass" runat="server" TextMode="Password" class="form-control" placeholder="Set Your Password"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Button runat="server" ID="btnOtpVerify" CssClass="btn btn-danger" Text="Process" OnClick="btnOtpVerify_Click" />
                                                    </div>
                                                </asp:View>

                                                <asp:View runat="server" ID="view3">

                                                    <div class="form-group">
                                                        <%-- <asp:TextBox ID="password" runat="server" class="form-control" TextMode="Password" placeholder="your Password"></asp:TextBox>
                                                        <span toggle="#password-field" class="fa fa-fw fa-eye field-icon toggle-password"></span>--%>
                                                        <input type="password" class="form-control" runat="server" id="passwordfield" placeholder="Enter Your Password" value="FakePSW" />
                                                        <span class="fa fa-fw fa-eye field-icon" onclick="myFunction()" style="margin-left: -30px; cursor: pointer; margin-right: 17px;"
                                                            id="togglePassword"></span>
                                                    </div>
                                                    <div class="form-group">
                                                        <a href="javascript:void(0)" data-target="#pwdModal" data-toggle="modal" style="color: #273896;">Forget Password?</a>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Button ID="btnLogin" ClientIDMode="Static" runat="server" CssClass="btn btn-primary" OnClick="btnLogin_Click" Text="Sign In" />
                                                    </div>
                                                </asp:View>
                                            </asp:MultiView>





                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <div class='playstore-part'>
                                    <div class="heading">
                                        Download Mobile App <%= company.Name %>

                                    </div>
                                    <p class="store">
                                        <a href="<%= company.AndroidURL %>" target="_blank">
                                            <img src="images/play-store-grey.png" class="img-fluid"></a>
                                    </p>

                                </div>
                            </div>

                            <div class="col-sm-12">
                                <div class="cus_care_no">
                                    <p>
                                        <b>Important Note: Don't share your user credentials and otp with anyone. </b>
                                    </p>
                                </div>
                                <p class="copyright" style="text-align: center">
                                    Copyright&nbsp;©&nbsp;2024
                        <a class="myLink" href="#" target="_blank"><%= company.Name %>
                        </a>. All Rights Reserved.
                        <br>
                                </p>
                            </div>
                            <br>


                            <br>
                            <!--         <div class="footer">
                    <div class="container">
                        <div class="footerlinks">
                            <ul>
                                <li><a href="javascript:" data-toggle="modal" data-target="#about">About us</a></li>
                                <li><a href="javascript:" data-toggle="modal" data-target="#privacy">Privacy Policy</a></li>
                                <li><a href="javascript:" data-toggle="modal" data-target="#refund">Refund and Cancellation Policy</a></li>
                                <li><a href="javascript:">Customer Care Number: 8258888998</a></li>
                            </ul>
                        </div>
                    </div>
                </div> -->

                        </div>
                    </div>
                </section>
                <asp:HiddenField runat="server" ID="hdnLongitude" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnLatitude" ClientIDMode="Static" />
            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="pdwTPIN" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h1 class="text-center">2 Way Verification</h1>
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
                                                            <asp:Button runat="server" ID="btnttpin" Text="Verify" CssClass="btn btn-danger" OnClick="btnttpin_Click"></asp:Button>
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
                                                            <asp:TextBox runat="server" ID="txtMobile" ForeColor="Red" CssClass="form-control" placeholder="Enter Mobile No."></asp:TextBox>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:TextBox runat="server" ForeColor="Red" ID="txtOTPVerify" Visible="false" CssClass="form-control input-lg" placeholder="Enter OTP"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:TextBox runat="server" ForeColor="Red" ID="txtNewPassword" Visible="false" CssClass="form-control input-lg" placeholder="Enter New Password"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Button runat="server" ID="btnForgetPassword" Text="Verify" CssClass="btn btn-primary" Style="background-color: #273896;" OnClick="btnForgetPassword_Click"></asp:Button>
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
        <script>
            $(".toggle-password").click(function () {

                $(this).toggleClass("fa-eye fa-eye-slash");
                var input = $($(this).attr("toggle"));
                if (input.attr("type") == "password") {
                    input.attr("type", "text");
                } else {
                    input.attr("type", "password");
                }
            });


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
        </script>
    </form>
</body>
</html>
