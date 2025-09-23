<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="MoneyTransfer.aspx.cs" Inherits="Member_MoneyTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="./css/aepsBank.css" rel="stylesheet" />
    <script src="./app_js/aeps-custom-new.js"></script>
    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
        }

        .bg-gray {
            background-color: rgb(247, 247, 247);
        }

        .paymengt-main-page-sec .row .col-12 ul li {
            text-align: center;
            color: #fff;
            padding: 0px 50px 0px 0px;
        }

            .paymengt-main-page-sec .row .col-12 ul li a {
                color: #fff;
                text-decoration: none;
                cursor: pointer;
            }

            .paymengt-main-page-sec .row .col-12 ul li span {
                display: block;
                font-size: 12px;
            }

        .paymengt-main-page-sec .row .col-12 ul {
            display: flex;
            overflow-x: scroll;
            justify-content: space-between;
            /*flex-wrap: wrap;*/
        }

        .paymengt-main-page-sec ul#tabs-nav {
            list-style: none;
            margin: 0;
            padding: 0px 5px;
            overflow: auto;
        }

            .paymengt-main-page-sec ul#tabs-nav li {
                float: left;
                margin-right: 2px;
                padding: 5px 10px;
                cursor: pointer;
            }

            .paymengt-main-page-sec ul#tabs-nav li,
            .paymengt-main-page-sec ul#tabs-nav li {
                border-bottom: 2px solid transparent;
            }

                .paymengt-main-page-sec ul#tabs-nav li:hover,
                .paymengt-main-page-sec ul#tabs-nav li.active {
                    border-bottom: 2px solid #e62e2e;
                }

        .paymengt-main-page-sec #tabs-nav li a {
            text-decoration: none;
            color: #777;
        }

        .paymengt-main-page-sec table td {
            width: 12% !important;
            font-size: 14px !important;
            font-weight: 400;
        }

        .paymengt-main-page-sec .description {
            width: 37% !important;
        }

        .paymengt-main-page-sec .price {
            width: 15% !important;
        }

        .paymengt-main-page-sec .tabs-background {
            background-color: #f4f4f4;
        }

        .paymengt-main-page-sec .btn-primary-outline {
            border: 1px solid #e62e2e !important;
            padding: 4px 13px;
            width: 100%;
            transition: all 0.4s ease-in-out;
        }

        .paymengt-main-page-sec table tr:hover .btn-primary-outline {
            background-color: #e62e2e;
            color: #fff;
        }

        .paymengt-main-page-sec .btn-primary {
            background-color: #e62e2e;
            color: #fff;
            border-color: #e62e2e;
        }

        .paymengt-main-page-sec .disclaimer-text p {
            font-size: 13px !important;
        }

        .paymengt-main-page-sec .disclaimer-text {
            background-color: #e1e1e1;
        }

        .paymengt-main-page-sec .tabs #tabs-content {
            max-height: 250px;
            overflow-y: scroll;
        }

        .paymengt-main-page-sec .payment-logo-icon li a img {
            max-width: 50px;
            margin: 1px 0 10px 0;
            height: 36px;
            object-fit: cover;
            -webkit-transition: 0.3s;
            -o-transition: 0.3s;
            transition: 0.3s;
        }

        .paymengt-main-page-sec .payment-logo-icon li a img {
            -webkit-transition: all 0.5s ease;
            -moz-transition: all 0.5s ease;
            -ms-transition: all 0.5s ease;
            -o-transition: all 0.5s ease;
            transition: all 0.5s ease;
            -webkit-backface-visibility: hidden;
            backface-visibility: hidden;
        }

        .paymengt-main-page-sec .payment-logo-icon li:hover img {
            -webkit-transform: scale(1.1);
            -moz-transform: scale(1.1);
            -ms-transform: scale(1.1);
            -o-transform: scale(1.1);
            transform: scale(1.1);
        }

        .paymengt-main-page-sec .payment-logo-icon li {
            width: 8.333%;
            text-align: -webkit-center;
            text-align: center;
            margin: 0px auto;
        }

        .paymengt-main-page-sec #tabs-content table thead tr td {
            font-size: 13px !important;
            font-weight: 500 !important;
            color: #999999 !important;
        }

        .paymengt-main-page-sec #tabs-content .table-hover tr {
            padding: 7px 0px;
        }

        .paymengt-main-page-sec .row .col-md-4 {
            border: 1px dashed #ffa1a1;
        }

        .paymengt-main-page-sec .row .shadow {
            box-shadow: 0 .1rem 0.4rem rgba(0,0,0,0.1) !important;
        }
    </style>
    <style>
        #sendMoneyTable {
            border-collapse: collapse;
            width: 100%;
        }

            #sendMoneyTable > th, td {
                text-align: left;
                padding: 8px;
            }

            #sendMoneyTable > tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #sendMoneyTable > th {
                background-color: #4CAF50;
                color: white;
            }

        .tbl-width tbody tr td:nth-child(1) {
            width: 200px;
        }

        .ajs-modal {
            z-index: 99999999 !important;
        }

        .modal {
            z-index: 88888 !important;
        }


        #load {
            z-index: 99999999 !important;
        }
    </style>
    <script>
        function ShowMessage(cal, mess, id) {
            if (cal == 1) {
                alertify.success(mess);
                if (id != null) {
                    HideModal(id);
                }
            } else {
                alertify.error(mess);
            }
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function ShowAlertMessage(mess, id) {
            alertify.alert(mess, function () {
                if (id != null) {
                    HideModal(id);
                }
            });
        }
        function ShowAlertMessageShowPopUP(mess, id) {
            alertify.alert(mess, function () {
                if (id != null) {
                    $(id).modal('show');
                }
            });
        }
        function ShowAlertMessageOlny(mess) {
            alertify.alert(mess, function () {

            });
        }
        function HideModal(id) {
            document.getElementById(id).click();
            //$("#" + id).modal('hide');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <div class="main-content-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="row row-sm">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-header">
                                    <h3 class="card-title">Money Transfer</h3>
                                </div>
                                <div class="card-body">
                                    <marquee direction="left"><span style="color:blue">INSTANT MONEY TRANSFER:- The money will be transferred instantly by mentioning the IFSC code of the recipient bank branch. You can transfer funds instantly by using your mobile phone, tablet or laptop as per your convenience.
Avoid Frauds - IFSC code is used to transfer funds in a secured manner. IFSC code will be allotted to each branch of a bank. Hence, by using IFSC you can identify the bank as well as the branch.once your pending transaction please wait maximum 3 days for confirmation, Failed or Success.</span></marquee>
                                    <div class="row">
                                        <div class="box">
                                            <!-- /.box-header -->
                                            <div class="box-body">
                                                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                                       <asp:View ID="View1" runat="server">
                                                            <div class="row">
                                                                <!-- LEFT SECTION -->
                                                                <div class="col-md-4">
                                                                    <!-- Login Message -->
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblLoginMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                    </div>

                                                                    <!-- Mobile Number Input -->
                                                                    <div class="form-group">
                                                                        <strong>Enter Customer Mobile Number</strong>
                                                                        <asp:TextBox ID="txtMobile" runat="server"
                                                                            CssClass="form-control ValCheckS"
                                                                            MaxLength="10"
                                                                            AutoCompleteType="None"
                                                                            onkeypress="return isNumber(event)"></asp:TextBox>
                                                                    </div>

                                                                    <!-- Aadhaar Input -->
                                                                    <div class="form-group">
                                                                        <strong>Enter Aadhaar Number</strong>
                                                                        <asp:TextBox ID="txtAdharNumber" runat="server"
                                                                            CssClass="form-control ResVal"
                                                                            MaxLength="12"
                                                                            onkeyup="onKeyPress(this)"
                                                                            onkeypress="onKeyPress(this)"></asp:TextBox>
                                                                    </div>

                                                                    <!-- OTP input (initially hidden) -->
                                                                    <div class="form-group" runat="server" id="divOTP" visible="false">
                                                                        <strong>OTP</strong>
                                                                        <asp:TextBox ID="txtOTP" runat="server"
                                                                            CssClass="form-control"
                                                                            MaxLength="6"></asp:TextBox>
                                                                    </div>

                                                                    <!-- Button Group -->
                                                                    <div class="form-group">
                                                                        <asp:Button ID="btnValidate" runat="server"
                                                                            Text="Validate"
                                                                            CssClass="btn btn-warning"
                                                                            OnClick="btnValidate_Click" />

                                                                        <asp:Button ID="btnSubmit" runat="server"
                                                                            CssClass="btn btn-primary"
                                                                            Text="Submit"
                                                                            Enabled="false"
                                                                            ValidationGroup="v"
                                                                            OnClientClick="return CheckValD('.ValCheckS')"
                                                                            OnClick="btnSubmit_Click" />
                                                                    </div>

                                                                    <!-- Registration message -->
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblRegistrationMessage" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                                                    </div>
                                                                </div>

                                                                <!-- RIGHT SECTION -->
                                                                <div class="col-md-6">
                                                                    <img src="./img/moneytransfer.jpg" height="300px" />
                                                                </div>
                                                            </div>
                                                        </asp:View>
                                                    <asp:View ID="View2" runat="server">
                                                        <asp:UpdatePanel runat="server" ID="updateAEPSBE">
                                                            <ContentTemplate>
                                                                <div class="container-fluid">
                                                                    <input type="hidden" name="indicatorforuid">
                                                                    <input type="hidden" name="nationalbankidentification">
                                                                    <input type="hidden" name="language">
                                                                    <input type="hidden" name="latitude" id="latitude" value="">
                                                                    <input type="hidden" name="longitude" id="longitude" value="">
                                                                    <input type="hidden" name="paymentType">
                                                                    <input type="hidden" name="requestremarks">
                                                                    <input type="hidden" name="timestamp">
                                                                    <input type="hidden" name="transcationtype">
                                                                    <input type="hidden" name="merchantpin">
                                                                    <input type="hidden" name="merchantTranId">
                                                                    <input type="hidden" name="submerchantid">
                                                                    <input type="hidden" name="unicode">
                                                                    <asp:HiddenField ID="txtWadh" runat="server" ClientIDMode="Static" />

                                                                    <input type="hidden" name="new" id="method" class="form-control">
                                                                    <asp:HiddenField runat="server" ID="hdnDevice" ClientIDMode="Static" />
                                                                    <asp:HiddenField runat="server" ID="hdnCapture" ClientIDMode="Static" />
                                                                    <input type="hidden" name="new" id="method" class="form-control">

                                                                    <!-- Select Device -->
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <div class="col-sm-4">
                                                                                <label>
                                                                                    <img src="./img/mpos.png" /> Select Device:
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-sm-8">
                                                                                <label>
                                                                                    <input type="radio" name="capdata" value="mantra" checked="" class="default_rd_serv device-radio" />
                                                                                    Mantra
                                                                                </label>
                                                                                <label>
                                                                                    <input type="radio" name="capdata" value="morpho" class="device-radio" />
                                                                                    Morpho
                                                                                </label>
                                                                                <label>
                                                                                    <input type="radio" name="capdata" value="secugen" class="device-radio" />
                                                                                    Secugen
                                                                                </label>
                                                                                <label>
                                                                                    <input type="radio" name="capdata" value="precision" class="device-radio" />
                                                                                    Precision
                                                                                </label>
                                                                                <label>
                                                                                    <input type="radio" name="capdata" value="startek" class="device-radio" />
                                                                                    Startek
                                                                                </label>
                                                                                <label>
                                                                                    <input type="radio" name="capdata" value="nextrd" class="device-radio" />
                                                                                    NEXT
                                                                                </label>
                                                                                <label>
                                                                                    <input type="radio" name="capdata" value="iris" class="device-radio" />
                                                                                    IRIS
                                                                                </label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <!-- Check Device + Verify Identity (Registration) -->
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <div class="col-sm-4"></div>
                                                                            <div class="col-sm-8">
                                                                                <input type="button" id="datascan" onclick="discoverAvdm('RKYC')" class="btn btn-danger" value="Check Device" />
                                                                                 <asp:Button runat="server" disabled='disabled' ID="btnRegistration" ClientIDMode="Static" 
                                                                                     Text="Registration" 
                                                                                     CssClass="btn btn-success" 
                                                                                     OnClick="btnRegistration_Click" />
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <!-- Message -->
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <div class="col-sm-4">
                                                                                <i class="fa fa-fingerprint" style="font-size: xxx-large;"></i>
                                                                            </div>
                                                                            <div class="col-sm-8">
                                                                                <p class="line_h_40">Please ask customer to place his thumb for impression on biometric scanner. (कृपया ग्राहक का अँगूठा बायोमेट्रिक स्कैनर पर रखे)</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </asp:View>

                                                    <asp:View ID="View3" runat="server">
                                                        <div class="row g-3 paymengt-main-page-sec">
                                                            <div class="col-md-4 col-12 bg-white rounded  px-md-4 py-md-4 p-3 shadow">
                                                                <div>
                                                                    <p class="fw-bold">Custmors Details</p>
                                                                    <div class="row">
                                                                        <div class="col-12 py-md-2 py-2">
                                                                            <table class="table profile__table">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <th><strong>Customer Name:</strong></th>
                                                                                        <td>
                                                                                            <asp:Label runat="server" ID="lblDataCName"></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <th><strong>Customer Mobile:</strong></th>
                                                                                        <td>
                                                                                            <asp:Label runat="server" ID="lblDataCMobile"></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <th><strong>Total Limit:</strong></th>
                                                                                        <td>
                                                                                            <asp:Label runat="server" ID="lblDataCTotalLimit"></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <th><strong>Consumed Limit:</strong></th>
                                                                                        <td>
                                                                                            <asp:Label runat="server" ID="lblConsumedLimit"></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <th><strong>Address:</strong></th>
                                                                                        <td>
                                                                                            <asp:Label runat="server" ID="txtAddressCus"></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <a href="#" class="profile__contact-btn btn btn-lg btn-block btn-success" onclick="resetAddBeneficiaryForm();" data-bs-toggle="modal" data-bs-target="#myModal">Add Beneficiary
                                                                                            </a>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Button runat="server" Text="LogOut" CssClass="profile__contact-btn btn btn-lg btn-block btn-success" OnClick="Unnamed_Click1" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-8 col-12 d-flex  ">
                                                                <div class="bg-white rounded  p-md-4 p-3 w-100 shadow">
                                                                    <div class="mt-2">
                                                                        <p class="fw-bold">Beneficiary List</p>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <div class="tabs">
                                                                                <!-- END tabs-nav -->
                                                                                <div id="tabs-content">
                                                                                    <div class="">
                                                                                        <table class="table mb-0">
                                                                                            <thead>
                                                                                                <td><b>Bank</b></td>
                                                                                                <td><b>Account No.</b></td>
                                                                                                <td><b>Name</b></td>
                                                                                                <td><b>IFSC</b></td>

                                                                                                <td class="price"><b>Action</b></td>
                                                                                            </thead>
                                                                                        </table>
                                                                                    </div>
                                                                                    <div id="tab1" class="tab-content">
                                                                                        <table class="table table-hover">
                                                                                            <asp:Repeater runat="server" ID="rptDataBeneficiary" OnItemCommand="rptDataBeneficiary_ItemCommand">
                                                                                                <ItemTemplate>
                                                                                                    <tr>
                                                                                                        <td><%# Eval("name") %>
                                                                                                            <asp:HiddenField runat="server" ID="hdnBankID" Value='<%# Eval("id") %>' />
                                                                                                        </td>
                                                                                                        <td><asp:Label runat="server" ID="lblBAccountno" Text='<%# Eval("accountNumber") %>'></asp:Label></td>
                                                                                                        <td><asp:Label runat="server" ID="lblBName" Text='<%# Eval("name") %>'></asp:Label></td>
                                                                                                        <td><asp:Label runat="server" ID="lblBifsc" Text='<%# Eval("ifsc") %>'></asp:Label></td>
                                                                                                        <td>
                                                                                                            <asp:Button runat="server" ID="btnDelete" Text="Delete Beneficiary"
                                                                                                                OnClientClick="return confirm('Do you want to remove?');"
                                                                                                                CommandName="DeleteBeni"
                                                                                                                CommandArgument='<%# Eval("id") %>'
                                                                                                                CssClass="btn btn-danger"
                                                                                                                Style="float: right; margin-top: 5px;" />

                                                                                                            <asp:Button runat="server" ID="btnSenMoney" Text="Send Money"
                                                                                                                data-bs-toggle="modal"
                                                                                                                data-bs-target="#SendMonay"
                                                                                                                CommandName="SendMoney"
                                                                                                                CssClass="btn btn-success"
                                                                                                                Style="float: right; margin-top: 5px;" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                            </asp:Repeater>
                                                                                            <asp:HiddenField runat="server" ID="hddBeneficiaryId" />
                                                                                        </table>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="disclaimer-text px-2 py-2">
                                                                                    <p class="mb-0"><strong>Disclaimer :</strong> <span class="text-muted">While we support most recharges, we request you to verify with your operator once before proceeding.</span></p>
                                                                                </div>
                                                                                <!-- END tabs-content -->
                                                                            </div>
                                                                            <!-- END tabs -->
                                                                        </div>
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
                        </div>
                    </div>
                    <asp:HiddenField ID="hddGroupTransID" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger ControlID="btnAddBeneficiary" EventName="Click" />--%>
                    <asp:AsyncPostBackTrigger ControlID="btnRegistration" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
        <div class="modal fade" id="otpModal" tabindex="-1" aria-labelledby="otpModalLabel" aria-hidden="true">
              <div class="modal-dialog">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title">Enter OTP</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                    <asp:TextBox runat="server" ID="otpDeleteBeni" CssClass="form-control" placeholder="Enter OTP"></asp:TextBox>
                  </div>
                  <div class="modal-footer">
                    <asp:Button runat="server" ID="btnSubmitOtp" Text="Submit OTP" CssClass="btn btn-primary" OnClick="btnSubmitOtp_Click" />
                  </div>
                </div>
              </div>
            </div>

          <script type="text/javascript">
              document.addEventListener("DOMContentLoaded", function () {
                  var otpModalEl = document.getElementById('otpModal');
                  if (otpModalEl) {
                      otpModalEl.addEventListener('shown.bs.modal', function () {
                          // Clear OTP textbox when modal is shown
                          var otpInput = document.getElementById('<%= otpDeleteBeni.ClientID %>');
                    if (otpInput) otpInput.value = '';
                });
            }
        });
          </script>

        <script>
            function CheckValD(cl) {
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
        </script>

        <div class="modal fade" id="myModal" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h6 class="modal-title">Add Beneficiary</h6>
                        <button aria-label="Close" class="btn-close" data-bs-dismiss="modal" type="button"><span aria-hidden="true">×</span></button>
                    </div>
                    <div class="modal-body">
                        <div class="card card-default">
                            <div class="card-body">
                                <asp:UpdatePanel runat="server" ID="updateAddB">
                                    <ContentTemplate>
                                        <div class="row">
                                            <!-- Existing fields -->
                                            <div class="col-md-12">
                                                <strong>Select Bank</strong>
                                                <asp:DropDownList runat="server" ID="ddlBankList" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlBankList_SelectedIndexChanged"
                                                    CssClass="form-control AddBClass" />

                                            </div>
                                            <div class="col-md-12">
                                                <strong>Account No.</strong>
                                                <asp:TextBox ID="txtAddBAccountNo" runat="server" CssClass="form-control AddBClass" Enabled="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-12">
                                                <strong>IFSC</strong>
                                                <asp:TextBox ID="txtAddBIFSC" runat="server" MaxLength="11" CssClass="form-control AddBClass" Enabled="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-8">
                                                <strong>Mobile No.</strong>
                                                <asp:TextBox ID="txtAddBMobileNo" runat="server" MaxLength="10" CssClass="form-control AddBClass" Enabled="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                 <asp:Button ID="btnVerifyBAccount" runat="server" Text="Verify Account"
                                                    OnClick="btnVerifyBankAccount_Click" CssClass="btn btn-danger" />
                                            </div>
                                            <div class="col-md-12">
                                                <strong>Name</strong>
                                                <asp:TextBox ID="txtAddBName" runat="server" CssClass="form-control AddBClass" Enabled="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-12">
                                                <strong>Account Type</strong>
                                                <asp:DropDownList ID="accountTypeList" runat="server" CssClass="form-control AddBClass" Enabled="true">
                                                    <asp:ListItem Text="Saving" Value="Saving" />
                                                    <asp:ListItem Text="Current" Value="Current" />
                                                </asp:DropDownList>
                                            </div>
                                            <div id="transferTypeWrapper" runat="server" class="col-md-12" style="display: none;">
                                                <strong>Transfer Type</strong>
                                                <asp:DropDownList ID="transferTypeList" runat="server" CssClass="form-control AddBClass">
                                                </asp:DropDownList>
                                            </div>

                                            <!-- OTP Section -->
                                            <asp:Panel ID="pnlOTP" runat="server" Visible="false">
                                                <div class="col-md-12">
                                                    <strong>OTP</strong>
                                                    <asp:TextBox ID="txtOtpB" runat="server" CssClass="form-control" />
                                                </div>
                                            </asp:Panel>

                                            <!-- Buttons -->
                                            <div class="col-md-6" id="divSendOTP" runat="server" Visible="false">
                                                <asp:Button ID="btnSendOTP" runat="server" Text="Send OTP" CssClass="btn btn-warning mt-3"
                                                    OnClick="btnBeneficiarySendOTP_Click" />
                                            </div>

                                            <div class="col-md-6" id="divSubmitBeneficiary" runat="server" visible="false">
                                                <asp:Button ID="btnSubmitBeneficiary" runat="server" Text="Submit" CssClass="btn btn-success mt-3"
                                                    OnClick="btnSubmitBeneficiary_Click" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSendOTP" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnSubmitBeneficiary" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" id="AdCls" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>

        <div class="modal fade" id="SendMonay" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <asp:UpdatePanel runat="server" ID="updateSend">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <h6 class="modal-title">Send Money to this account</h6>
                                <button aria-label="Close" class="btn-close" data-bs-dismiss="modal" type="button"><span aria-hidden="true">×</span></button>
                            </div>
                            <div class="modal-body">

                                <table id="sendMoneyTable">
                                    <tr>
                                        <td><strong>Name :</strong></td>
                                        <td>
                                            <asp:Label runat="server" ID="lblSendMoneyName"></asp:Label></td>
                                    </tr>
                                    <tr runat="server" visible="false">
                                        <td><strong>Mobile :</strong></td>
                                        <td>
                                            <asp:Label runat="server" ID="lblSendMoneyMobile"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><strong>Account No. :</strong></td>
                                        <td>
                                            <asp:Label runat="server" ID="lblSendMoneyAccountNo"></asp:Label></td>
                                    </tr>
                                    <tr runat="server" visible="false">
                                        <td><strong>BankName :</strong></td>
                                        <td>
                                            <asp:Label runat="server" ID="lblbeneficiaryId"></asp:Label>
                                            <asp:Label runat="server" ID="lblSendAdd"></asp:Label>
                                            <asp:Label runat="server" ID="lblSendMoneyBankName"></asp:Label>
                                            <asp:Label runat="server" ID="lblValue" Text=")"></asp:Label>
                                            <asp:HiddenField runat="server" ID="hdnSendMoneyBankId" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>ifsc :</strong></td>
                                        <td>
                                            <asp:Label runat="server" ID="lblSendMoneyIFSC"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><strong>Amount :</strong></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtSendMoney" ClientIDMode="Static" MaxLength="7" CssClass="form-control BClass numeric"></asp:TextBox>
                                            <asp:LinkButton runat="server" ID="lnkButtonView" Style="color: red;" OnClick="lnkButtonView_Click" Text="View"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>Comment :</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtComment" MaxLength="50" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <div class="col-md-12">
                                        <strong>accountType</strong>
                                        <asp:DropDownList ID="dllSendAccountType" runat="server" CssClass="form-control AddBClass" ClientIDMode="Static">
                                            <asp:ListItem Text="IMPS" Value="IMPS"></asp:ListItem>
                                            <%--<asp:ListItem Text="NEFT" Value="NEFT"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button runat="server" ID="btnDMTSendOtp" Text="Send OTP" CssClass="btn btn-warning form-control"
                                                OnClick="btnDMTSendOtp_Click" />
                                        </td>
                                    </tr>

                                    <tr id="rowOtp" runat="server" visible="false">
                                        <td><strong>Enter OTP :</strong></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textDmtOTP" CssClass="form-control" MaxLength="6" />
                                        </td>
                                    </tr>

                                    <tr id="rowSendMoney" runat="server" visible="false">
                                        <td colspan="2">
                                            <asp:Button runat="server" ID="btnSendMonay" OnClick="btnSendMonay_Click" Text="Send Money"
                                                CssClass="form-control btn btn-danger" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table class="table table-responsive">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <strong>Sno</strong>
                                                        </th>
                                                        <th>
                                                            <strong>Amount</strong>
                                                        </th>
                                                        <th>
                                                            <strong>SurCharge</strong>
                                                        </th>
                                                    </tr>
                                                </thead>

                                                <asp:Repeater runat="server" ID="rptDataView">
                                                    <ItemTemplate>

                                                        <tr>
                                                            <td><%# Container.ItemIndex+1 %></td>
                                                            <td><%# Eval("Amt") %></td>
                                                            <td><%# Eval("Surcharge") %></td>
                                                        </tr>
                                                    </ItemTemplate>

                                                </asp:Repeater>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Label runat="server" Style="float: left; font-size: x-large;" ID="lblStatus"></asp:Label>
                                <button type="button" class="btn btn-default" data-bs-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtSendMoney" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnRegistration" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <script type="text/javascript">
            // Clear OTP textbox when modal is closed
            $('#myModal').on('hidden.bs.modal', function () {
                $('#<%= txtOtpB.ClientID %>').val('');
            });
        </script>
        <script type="text/javascript">
            function resetSendMoneyForm() {
                try {
                    // Reset TextBoxes
                    document.getElementById('txtSendMoney').value = '';
                    document.getElementById('txtComment').value = '';
                    document.getElementById('<%= textDmtOTP.ClientID %>').value = '';

                    // Reset DropDownList
                    document.getElementById('dllSendAccountType').selectedIndex = 0;

                    // Reset Labels
                    document.getElementById('<%= lblSendMoneyName.ClientID %>').innerText = '';
                    document.getElementById('<%= lblSendMoneyMobile.ClientID %>').innerText = '';
                    document.getElementById('<%= lblSendMoneyAccountNo.ClientID %>').innerText = '';
                    document.getElementById('<%= lblSendMoneyBankName.ClientID %>').innerText = '';
                    document.getElementById('<%= lblSendMoneyIFSC.ClientID %>').innerText = '';
                    document.getElementById('<%= lblbeneficiaryId.ClientID %>').innerText = '';
                    document.getElementById('<%= lblSendAdd.ClientID %>').innerText = '';
                    document.getElementById('<%= lblValue.ClientID %>').innerText = '';
                    document.getElementById('<%= lblStatus.ClientID %>').innerText = '';

                    // Hide OTP row and Send Money button row
                    document.getElementById('<%= rowOtp.ClientID %>').style.display = 'none';
                    document.getElementById('<%= rowSendMoney.ClientID %>').style.display = 'none';

                } catch (e) {
                    console.error("Error resetting Send Money form:", e);
                }
            }
        </script>

    <script>
        function CheckValSendMoney(cl) {
            try {
                var flag = true;

                $(cl).each(function () {
                    debugger;
                    var lookName = (this.getAttribute("lookName") == null ? "" : this.getAttribute("lookName"));
                    if (this.value == 0 || this.Text == "Select Bank") {
                        this.style.border = "1px solid red";
                        flag = false;
                    } else {
                        {
                            this.style.border = "1px solid lightgrey";
                        }
                    }
                });
            }
            catch (err) {
            }
            return flag;
        }

        function CheckVal(cl, val, name) {
            try {
                var flag = true;
                if (val == 0) {
                    $(cl).each(function () {
                        debugger;
                        var lookName = (this.getAttribute("lookName") == null ? "" : this.getAttribute("lookName"));
                        if (this.value == 0 || this.Text == "Select Bank") {
                            this.style.border = "1px solid red";
                            flag = false;
                        } else {
                            {
                                this.style.border = "1px solid lightgrey";
                            }
                        }
                    });
                }
                else {

                    $(cl).each(function () {
                        debugger;
                        var lookName = (this.getAttribute("lookName") == null ? "" : this.getAttribute("lookName"));
                        if (this.value == 0 || this.Text == "Select Bank") {
                            this.style.border = "1px solid red";
                            flag = false;
                        } else {
                            {
                                this.style.border = "1px solid lightgrey";
                            }
                        }
                        if ($('#txtAddBName').val() == '') {
                            $('#txtAddBName').css('border-color', 'red');
                            flag = false;
                        }
                        else {
                            $('#txtAddBName').css('border-color', '');
                        }
                        if (name == 'SendMonay') {
                            if (flag == true) {
                                $('#SendMonay').modal('show');
                            }
                            else {
                                $('#SendMonay').modal('hide');
                            }

                        }
                    });
                }

            } catch (err) {
            }
            return flag;
        }
        function ReceiptShow(v) {
            window.location = "WalletInvoice_New.aspx?GUID=" + v;
        }
        function HideDivBody(id) {
            try {
                document.getElementById(id).style.display = "none";
                document.getElementById("HeaderName").innerHTML = "Quick Money Transfer";
                document.getElementById("divInstantMoneySend").style.display = "";

            } catch (err) {
            }
        }
        function ShowDivBody(id) {
            try {

                document.getElementById(id).style.display = "none";
                document.getElementById("HeaderName").innerHTML = "Add Beneficiary";
                document.getElementById("divSubmitBeneficiary").style.display = "";

            } catch (err) {
            }
        }
        function onCaptureSuccess() {
            // Enable the registration button after successful capture
            document.getElementById('btnRegistration').style.display = 'inline-block';
        }
        
    </script>
    <script type="text/javascript">
        function resetAddBeneficiaryForm() {
            try {
                // 🔁 Reset TextBoxes
                const txtFields = [
                    '<%= txtAddBAccountNo.ClientID %>',
                    '<%= txtAddBIFSC.ClientID %>',
                    '<%= txtAddBMobileNo.ClientID %>',
                    '<%= txtAddBName.ClientID %>',
                    '<%= txtOtpB.ClientID %>'
                ];

                txtFields.forEach(id => {
                    const input = document.getElementById(id);
                    if (input) input.value = '';
                    else console.warn("Textbox not found:", id);
                });

                // 🔁 Reset DropDownLists
                const ddlIds = [
                    '<%= ddlBankList.ClientID %>',
                    '<%= accountTypeList.ClientID %>',
                    '<%= transferTypeList.ClientID %>'
                ];

                ddlIds.forEach(id => {
                    const ddl = document.getElementById(id);
                    if (ddl) ddl.selectedIndex = 0;
                    else console.warn("Dropdown not found:", id);
                });

                // 🔒 Hide OTP panel and buttons
                const otpPanel = document.getElementById('<%= pnlOTP.ClientID %>');
                if (otpPanel) otpPanel.style.display = 'none';

                const sendOtpDiv = document.getElementById('<%= divSendOTP.ClientID %>');
                if (sendOtpDiv) sendOtpDiv.style.display = 'none';

                const submitDiv = document.getElementById('<%= divSubmitBeneficiary.ClientID %>');
                if (submitDiv) submitDiv.style.display = 'none';

                // 👇 Hide transferTypeWrapper section explicitly
                const transferWrapper = document.getElementById('<%= transferTypeWrapper.ClientID %>');
                if (transferWrapper) transferWrapper.style.display = 'none';
                else console.warn("transferTypeWrapper not found.");

            } catch (e) {
                console.error("Error resetting form:", e);
            }
        }
    </script>

    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        $(function () {
            $('#txtSendMoney').keyup(function () {
                this.value = this.value.replace(/[^0-9\.]/g, '');
            });
            $("#txtSendMoney").bind("paste", function (e) {
                return false;
            });
            $("#txtSendMoney").bind("drop", function (e) {
                return false;
            });
        });
    </script>
    <script>                    
        var req = Sys.WebForms.PageRequestManager.getInstance();
        req.add_beginRequest(function () {
        });
        req.add_endRequest(function () {
            $('#txtSendMoney').keyup(function () {
                this.value = this.value.replace(/[^0-9\.]/g, '');
            });
            $("#txtSendMoney").bind("paste", function (e) {
                return false;
            });
            $("#txtSendMoney").bind("drop", function (e) {
                return false;
            });
        });
    </script>
</asp:Content>

