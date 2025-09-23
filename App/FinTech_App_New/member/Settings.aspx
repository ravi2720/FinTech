<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Member_Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="./Component/MemberToggle.js"></script>
    <style>
        .modal-header {
            display: block !important;
        }

        .badge {
            border-radius: 20px;
            font-size: 12px;
            line-height: 1;
            padding: 0.375rem 0.5625rem;
            font-weight: normal;
            background-color: white !important;
            color: black !important;
        }

        label {
            font-weight: bold
        }

        .card-header {
            font-weight: bold
        }

        .switch {
            position: relative;
            display: inline-block;
            width: 60px;
            height: 34px;
        }

            .switch input {
                display: none;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: 0.4s;
            transition: 0.4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 26px;
                width: 26px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: 0.4s;
                transition: 0.4s;
            }

        input:checked + .slider {
            background-color: #101010;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #101010;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid" id="app">
        <!-- main-content-body -->
        <div class="main-content-body" v-for="item in MainData">
            <div class="row">
                <div class="col-lg-12 col-xl-12">
                    <div class="card custom-card">
                        <div class="card-header">
                            <div class="card-title">Login Permission(Two Way Authentication Necessary On When Any setting on)</div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-xl-6 col-md-12 col-sm-12 p-2">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="d-flex">
                                                <div>
                                                    <p class="tx-20 font-weight-semibold d-flex mb-0">
                                                        <togglebtn Auth="Msrno" coln="IsSeparateProfitWallet" title="Profit Wallet" v-bind:check="item.IsSeparateProfitWallet"></togglebtn>
                                                    </p>
                                                    <p class="tx-13 text-muted mb-0">Do you Want Saparte Profit Wallet.</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 col-xl-6 col-md-12 col-sm-12 p-2">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="d-flex">
                                                <div>
                                                    <p class="tx-20 font-weight-semibold d-flex mb-0">
                                                        <togglebtn Auth="Msrno" coln="TwoWay" title="Two Way Authentication" v-bind:check="item.TwoWay"></togglebtn>
                                                    </p>
                                                    <p class="tx-13 text-muted mb-0">Two-factor authentication (2FA) is an identity and access management security.</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 col-xl-6 col-md-12 col-sm-12 p-2">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="d-flex">
                                                <div>
                                                    <p class="tx-20 font-weight-semibold d-flex mb-0">
                                                        <togglebtn Auth="Msrno" title="OTP Login" coln="IsOTP" v-bind:check="item.IsOTP"></togglebtn>
                                                    </p>
                                                    <p class="tx-13 text-muted mb-0">A one-time password (OTP) is an automatically generated numeric.An OTP is more secure than a static password, especially a user-created password, which can be weak and/or reused across multiple accounts.</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-12 col-xl-6 col-md-12 col-sm-12 p-2">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="d-flex">
                                                <div>
                                                    <p class="tx-20 font-weight-semibold d-flex mb-0">
                                                        <togglebtn Auth="Msrno" title="TPIN Login" coln="IsTpin" v-bind:check="item.IsTpin"></togglebtn>
                                                    </p>
                                                    <p class="tx-13 text-muted mb-0">Provide an additional layer of security to the electronic transaction process.</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-12 col-xl-6 col-md-12 col-sm-12 p-2">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="d-flex">
                                                <div>
                                                    <p class="tx-20 font-weight-semibold d-flex mb-0">
                                                        <togglebtn Auth="Msrno" title="Pattern Login" coln="IsPattern" v-bind:check="item.IsPattern"></togglebtn>
                                                    </p>
                                                    <p class="tx-13 text-muted mb-0">Pattern based authentication allows user to login to Zig bank mobile application by drawing a pattern on screen rather than entering his user id and password.</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--/ row -->
            </div>
            <div class="row">
                <div class="col-lg-12 col-xl-12 col-md-12 col-sm-12 p-2">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                    <div class="col-md-4">
                                        <asp:Image ID="imgQrCode" runat="server" />
                                    </div>
                                    <div class="col-md-6">
                                        <p class="tx-20 font-weight-semibold d-flex mb-0">
                                                        <togglebtn  title="Google Auth" coln="IsGoogleAuth" v-bind:check="item.IsGoogleAuth"></togglebtn>
                                                    </p>
                                        <div>
                                            <span style="font-weight: bold; font-size: 14px;">Account Name:</span>
                                             <asp:Label runat="server" ID="lblAccountName"></asp:Label>
                                        </div>
                                        <div>
                                            <span style="font-weight: bold; font-size: 14px;">Secret Key:</span>
                                             <asp:Label runat="server" ID="lblManualSetupCode"></asp:Label>
                                        </div>
                                        
                                        <div>
                                            <div class="section">
                                                <h3 class="text-info">Step 2: Link your device to your account:  
                                                </h3>
                                                <p>You have two options to link your device to your account:</p>
                                                <p><b>Using QR Code:</b> Select<b> Scan a barcode</b>. If the Authenticator app cannot locate a barcode scanner app on your mobile device, you might be prompted to download and install one. If you want to install a barcode scanner app so you can complete the setup process, select Install, then go through the installation process. Once the app is installed, reopen Google Authenticator, then point your camera at the QR code on your computer screen.</p>

                                                <p>
                                                    <b>Using Secret Key:</b>
                                                    Select <b>Enter provided key</b>, then enter account name of your account in the <b>"Enter account name"</b> box. Next, enter the secret key appear on your computer screen in the <b>"Enter your key"</b> box. Make sure you've chosen to make the key Time based, then select Add.  
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>



            </div>

            <!-- Container closed -->
        </div>
        <input type="hidden" runat="server" id="hdnAuth" />
    </div>
    <script>
        var app = new Vue({
            el: '#app',
            data() {
                return {
                    MainData: []
                }
            },
            methods: {
                init() {
                    const article = {
                        "MethodName": $("#ContentPlaceHolder1_hdnAuth").val()
                    }
                    axios.post("Settings.aspx/GetData", article)
                        .then(response => {
                            this.MainData = JSON.parse(response.data.d);
                        });
                }
            },
            mounted() {
                this.init();
            },
        })
    </script>
</asp:Content>

