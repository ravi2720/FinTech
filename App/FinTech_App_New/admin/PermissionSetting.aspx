<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="PermissionSetting.aspx.cs" Inherits="Admin_PermissionSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="./Component/toggole.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper" id="app">
        <section class="content" v-for="item in MainData">
            <div class="container-fluid">
                <div class="card">
                    <div class="card-header"><strong>Admin Login Permission</strong> </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3">
                                <togglebtn coln="TwoWay" title="Two Way Authentication Admin" v-bind:check="item.TwoWay"></togglebtn>
                            </div>
                            <div class="col-md-3">
                                <togglebtn title="OTP Login For Admin Login" coln="OTP" v-bind:check="item.OTP"></togglebtn>
                            </div>
                            <div class="col-md-2">
                                <togglebtn title="TPIN Login For Admin Login" coln="TPIN" v-bind:check="item.TPIN"></togglebtn>
                            </div>
                            <div class="col-md-2">
                                <togglebtn title="System Down" coln="SystemDown" v-bind:check="item.SystemDown"></togglebtn>
                            </div>
                            <div class="col-md-2">
                                <togglebtn title="Google Auth" coln="GoogleAuth" v-bind:check="item.GoogleAuth"></togglebtn>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Image ID="imgQrCode" runat="server" />
                            </div>
                            <div class="col-md-6">
                                <div>
                                    <span style="font-weight: bold; font-size: 14px;">Account Name:</span>
                                </div>
                                <div>
                                    <asp:Label runat="server" ID="lblAccountName"></asp:Label>
                                </div>

                                <div>
                                    <span style="font-weight: bold; font-size: 14px;">Secret Key:</span>
                                </div>
                                <div>
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
                <div class="card">
                    <div class="card-header"><strong>Admin Amount Add Permission</strong> </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3">
                                <togglebtn title="Add Balance OTP" coln="AddBalanceOTP" v-bind:check="item.AddBalanceOTP"></togglebtn>
                            </div>
                            <div class="col-md-3">
                                <togglebtn title="Add Balance TPIN" coln="AddBalanceTPIN" v-bind:check="item.AddBalanceTPIN"></togglebtn>
                            </div>
                            <div class="col-md-3">
                                <togglebtn title="Deduct Balance OTP" coln="DeductBalanceOTP" v-bind:check="item.DeductBalanceOTP"></togglebtn>
                            </div>

                            <div class="col-md-3">
                                <togglebtn title="Deduct Balance TPIN" coln="DeductBalanceTPIN" v-bind:check="item.DeductBalanceTPIN"></togglebtn>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="card">
                    <div class="card-header"><strong>Member Registration Permission</strong> </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3">
                                <togglebtn title="Registration WIth OTP Verification" coln="RegistratioWIthOTPVerification" v-bind:check="item.RegistratioWIthOTPVerification"></togglebtn>
                            </div>
                            <div class="col-md-3">
                                <togglebtn title="Mail Send Registration" coln="MailSendRegistration" v-bind:check="item.MailSendRegistration"></togglebtn>
                            </div>
                            <div class="col-md-3">
                                <togglebtn title="OTP Send Registration" coln="OTPSendRegistration" v-bind:check="item.OTPSendRegistration"></togglebtn>
                            </div>
                            <div class="col-md-3">
                                <togglebtn title="Mail Send Registration OTP" disable="1" coln="MailSendRegistrationOTP" v-bind:check="item.MailSendRegistrationOTP"></togglebtn>
                            </div>
                            <div class="col-md-3">
                                <togglebtn title="OTP Send Registration OTP" coln="OTPSendRegistrationOTP" v-bind:check="item.OTPSendRegistrationOTP"></togglebtn>
                            </div>
                            <div class="col-md-3">
                                <togglebtn title="Mail Send Password ChangeOTP" coln="MailSendPasswordChangeOTP" v-bind:check="item.MailSendPasswordChangeOTP"></togglebtn>
                            </div>
                            <div class="col-md-3">
                                <togglebtn title="OTP Send Password Change OTP" coln="OTPSendPasswordChangeOTP" v-bind:check="item.OTPSendPasswordChangeOTP"></togglebtn>
                            </div>
                            <div class="col-md-3">
                                <togglebtn title="Mail Send TPIN Change OTP" coln="MailSendTPINChangeOTP" v-bind:check="item.MailSendTPINChangeOTP"></togglebtn>
                            </div>
                            <div class="col-md-3">
                                <togglebtn title="OTP Send TPIN Change OTP" coln="OTPSendTPINChangeOTP" v-bind:check="item.OTPSendTPINChangeOTP"></togglebtn>
                            </div>
                        </div>
                    </div>


                </div>
                <div class="card">
                    <div class="card-header"><strong>Member Permission</strong> </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3">
                                <togglebtn title="Working Without KYC" coln="WorkingWithoutKYC" v-bind:check="item.WorkingWithoutKYC"></togglebtn>
                            </div>
                            <div class="col-md-3">
                                <togglebtn title="Member Panel Disable" coln="PanelDisable" v-bind:check="item.PanelDisable"></togglebtn>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <div class="card">
                            <div class="card-header"><strong>Member Bank Add Permission</strong> </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <togglebtn title="Add Bank With OTP" coln="BankAddOTP" v-bind:check="item.BankAddOTP"></togglebtn>
                                    </div>
                                    <div class="col-md-6">
                                        <togglebtn title="Add Bank With TPIN" coln="BankAddTPIN" v-bind:check="item.BankAddTPIN"></togglebtn>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="card">
                            <div class="card-header"><strong>Member Transaction Permission</strong> </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <togglebtn title="Cashout Transaction OTP" coln="CashoutTransactionOTP" v-bind:check="item.CashoutTransactionOTP"></togglebtn>
                                    </div>
                                    <div class="col-md-6">
                                        <togglebtn title="Cashout Transaction TPIN" coln="CashoutTransactionTPIN" v-bind:check="item.CashoutTransactionTPIN"></togglebtn>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </section>
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
                        "MethodName": "getdate"
                    }
                    axios.post("PermissionSetting.aspx/GetData", article)
                        .then(response => {
                            debugger;
                            this.MainData = JSON.parse(response.data.d);
                        });// .then(function (response) { debugger; this.MainData =response.data.d; });
                }
            },
            mounted() {
                this.init();
            },
        })
    </script>
</asp:Content>

