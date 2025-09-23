<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="aepsBank.aspx.cs" Inherits="Member_aepsBank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/aepsBank.css" rel="stylesheet" />
    <script src="../app_js/NewA.js"></script>

    <style>
        @media screen {
            #printSection {
                display: none;
            }
        }

        .table:not(.table-sm):not(.table-md):not(.dataTable) td, .table:not(.table-sm):not(.table-md):not(.dataTable) th {
            padding: 0 0px !important;
            height: 42px !important;
            vertical-align: middle !important;
        }

        @media print {
            body * {
                visibility: hidden;
            }

            #printSection, #printSection * {
                visibility: visible;
            }

            #printSection {
                position: absolute;
                left: 0;
                top: 0;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <div class="main-content-body">
            <div class="" id="aeps">
                <div class="content-wrapper">
                    <section class="content">
                        <div class="container-fluid">
                            <div class="aeps_form">
                                <input type="hidden" name="indicatorforuid">
                                <input type="hidden" name="nationalbankidentification">
                                <input type="hidden" name="language">
                                <input type="hidden" name="latitude" id="latitude" value="27.023803599999997">
                                <input type="hidden" name="longitude" id="longitude" value="74.21793260000001">
                                <input type="hidden" name="paymentType">
                                <input type="hidden" name="requestremarks">
                                <input type="hidden" name="timestamp">
                                <input type="hidden" name="transcationtype">
                                <input type="hidden" name="merchantpin">
                                <input type="hidden" name="new" id="method" class="form-control">

                                <input type="hidden" name="merchantTranId">
                                <input type="hidden" name="unicode">
                                <input type="hidden" name="submerchantid">
                                <input type="hidden" name="txtWadh" id="txtWadh">
                                <input type="hidden" v-model="AEPS.Capture" id="hdnDevice" />
                                <input type="hidden" v-model="AEPS.DeviceName" id="hdnCapture" />
                                <div v-if="Registration==true && Authencation==true && Bankselect==true">
                                    <div class="form-group" style="display: none">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label>
                                                    <img src="https://rnfi.in/asset/images/_default/aeps/mpos.png">
                                                    Select Device:</label>
                                            </div>
                                            <div class="col-sm-8 ">
                                                <label>
                                                    <input type="radio" name="capdata" value="mantra" class="default_rd_serv device-radio">
                                                    Mantra</label>
                                                <label>
                                                    <input type="radio" name="capdata" value="morpho" checked="" class="device-radio">
                                                    Morpho</label>
                                                <label>
                                                    <input type="radio" name="capdata" value="secugen" class="device-radio">
                                                    Secugen</label>
                                                <label>
                                                    <input type="radio" name="capdata" value="precision" class="device-radio">
                                                    Precision</label>
                                                <label>
                                                    <input type="radio" name="capdata" value="startek" class="device-radio">
                                                    Startek</label>
                                                <label>
                                                    <input type="radio" name="capdata" value="nextrd" class="device-radio">
                                                    NEXT</label>
                                                <label>
                                                    <input type="radio" name="capdata" value="iris" class="device-radio">
                                                    IRIS</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label>
                                                    <img src="https://rnfi.in/asset/images/_default/aeps/mobile.png">
                                                    Select Type:</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <select id="ddlDeviceList" v-model="AEPS.Type" v-on:change="changeItem($event)" class="form-control">
                                                    <option value="0">Select Type</option>
                                                    <option value="CW">Cash Withdrawal</option>
                                                    <option value="MS">Mini StateMent</option>
                                                    <option value="BE">Balance Enquiry</option>
                                                </select>
                                            </div>

                                            <div class="col-sm-3">
                                                <label>
                                                    <img src="https://rnfi.in/asset/images/_default/aeps/mobile.png">
                                                    Mobile Number:</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="mobile_blk_wrap aadhar_blk">
                                                    <div class="mobile_blk">
                                                        <img src="https://rnfi.in/asset/images/_default/aeps/flag.jpg">
                                                    </div>
                                                    <input type="text" id="formmobile" v-model="AEPS.Mobile" required="" data-inputmask="'mask': '9999 9999 9999'" onkeypress="return isNumber(event)" class="form-control Validate" maxlength="10" />
                                                    <span style="color: red; display: none;" id="Mobile-error">Mobile Number Should Be 10 digit</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" v-if="ShowDivCW">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label class="line_h_92">
                                                    <img src="https://rnfi.in/asset/images/_default/aeps/money.png">
                                                    Amount:</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="fast_cash js_fast_cash">
                                                    <button type="button" name="transcationamount" class="Amountcl active_cash" v-on:click="newfunction_(500,this);" value="500">Rs. 500/-</button>
                                                    <button type="button" name="transactionamount" class="Amountcl" value="1000" v-on:click="newfunction_(1000,this);">Rs. 1000/-</button>
                                                    <button type="button" name="transactionamount" class="Amountcl" value="2000" v-on:click="newfunction_(2000,this);">Rs. 2000/-</button>
                                                    <button type="button" name="transactionamount" class="Amountcl" value="3000" v-on:click="newfunction_(3000,this);">Rs. 3000/-</button>
                                                    <button type="button" name="transcationamount" class="Amountcl" value="5000" v-on:click="newfunction_(5000,this);">Rs. 5000/-</button>
                                                    <button type="button" name="transcationamount" class="Amountcl" value="10000" v-on:click="newfunction_(10000,this);">Rs. 10000/-</button>
                                                </div>
                                                <div class="mobile_blk_wrap">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="mobile_blk">
                                                                <img src="https://rnfi.in/asset/images/_default/aeps/money_icon.png" />
                                                                Rs.
                                                            </div>
                                                            <input type="text" required="" v-on:change="amtchnage" v-model="AEPS.Amount" id="txtAmount" maxlength="5" class="form-control Validate" />
                                                            <span style="color: red; display: none;" id="amount-error">Amount Required More than 100. and less than Equal 10000</span>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <span class="btn btn-success"><strong>Earning Commission-</strong>{{Com}}</span>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label>
                                                    <img src="https://rnfi.in/asset/images/_default/aeps/aadhar.png" />
                                                    Aadhar Number:</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="mobile_blk_wrap aadhar_blk">
                                                    <div class="mobile_blk">
                                                        <img src="https://rnfi.in/asset/images/_default/aeps/fingerprint_icon.png" />
                                                    </div>
                                                    <input type="text" id="aadhar_number" v-model="AEPS.AadharNo" onkeypress="return isNumber(event)" maxlength="12" data-inputmask="'mask': '9999 9999 9999'" class="form-control Validate" />
                                                    <span style="color: red; display: none;" id="aadhar-error">Please enter valid Aadhaar Number.</span>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-4"></div>
                                                    <div class="col-md-8">
                                                        <div class="row">
                                                            <div class="col-md-1 ddgyth">
                                                                <a href="#" class="" v-on:click="sl(508534,this)">
                                                                    <img src="./images/icici.png" /></a>
                                                            </div>
                                                            <div class="col-md-1 ddgyth">
                                                                <a href="#" class="" v-on:click="sl(607152,this)">
                                                                    <img src="./images/hdfc.png" /></a>
                                                            </div>
                                                            <div class="col-md-1 ddgyth">
                                                                <a href="#" class="" v-on:click="sl(607094,this)">
                                                                    <img src="./images/sbi.png" /></a>
                                                            </div>
                                                            <div class="col-md-1 ddgyth">
                                                                <a href="#" class="" v-on:click="sl(607027,this)">
                                                                    <img src="./images/punjab.png" /></a>
                                                            </div>
                                                            <div class="col-md-1 ddgyth">
                                                                <a href="#" class="" v-on:click="sl(607153,this)">
                                                                    <img src="./images/axis.png" /></a>
                                                            </div>
                                                            <div class="col-md-1 ddgyth">
                                                                <a href="#" class="" v-on:click="sl(608117,this)">
                                                                    <img src="./images/idfc.png" /></a>
                                                            </div>
                                                            <div class="col-md-1 ddgyth">
                                                                <a href="#" class="" v-on:click="sl(990320,this)">
                                                                    <img src="./images/airtelbank.png" /></a>
                                                            </div>
                                                            <div class="col-md-1 ddgyth">
                                                                <a href="#" class="" v-on:click="sl(607105,this)">
                                                                    <img src="./images/indianbank.png" /></a>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <label>
                                                                <img src="https://rnfi.in/asset/images/_default/aeps/bank.png" />
                                                                Bank:</label>
                                                        </div>
                                                        <div class="col-sm-8">

                                                            <div class="mobile_blk_wrap aadhar_blk">
                                                                <select id="nationalbank" v-model="AEPS.BankID" class="form-control select2">
                                                                    <option value="0">Select Bank</option>
                                                                    <option v-for="car in BankData" v-bind:value="{ id: car.iinno, text: car.bankName }">{{car.bankName}}</option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group" runat="server" id="ErrorGroup" visible="false">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="lblBankDown" ForeColor="White" CssClass="btn btn-danger" Font-Size="X-Large"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="ScanGroup" visible="true">
                                            <div class="row">
                                                <div class="col-sm-4"></div>
                                                <div class="col-sm-8">
                                                    <input type="button" id="authdata" v-if="AEPS.Type=='CW'" v-on:click="DOAuthData" class="btn btn-warning" value="Authentication" />
                                                    <input type="button" id="datascan" v-if="(CWAuth==true && AEPS.Type=='CW') || (AEPS.Type=='BE' || AEPS.Type=='MS')" v-on:click="DOtry" class="btn btn-danger" value="Check Device" />
                                                    <input type="button" id="btnBalance" style="display: none" v-on:click="DOAEPS" class="btn btn-danger" value="Cash Withdraw" />
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="form-control" v-if="Bankselect==false">
                                    <div class="row">
                                        <div class="col-md-4" v-for="itemp in BankAEPSPIPEList">
                                            <input type="button" v-bind:value="itemp.Name" v-on:click="selbank(itemp.Name)" v-bind:class="(itemp.Status=='Pending'?'btn btn-danger':'btn btn-success')" />
                                            ({{itemp.Status}})
                                        </div>
                                    </div>
                                    <div class="row pt-2">
                                        <div class="col-md-12">
                                            <div class="btn btn-warning">
                                              Note-This api provides unique merchant authencation txnid for every cash withdraw transaction
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-control" v-if="Bankselect==true">
                                    <div class="row">
                                        <div class="col-md-6" v-if="Registration==false">
                                            <input type="button" value="TWO Way Registration" v-on:click="Dossss" class="btn btn-danger" />
                                        </div>
                                        <div class="col-md-6" v-if="Registration==true && Authencation==false">
                                            <input type="button" value="TWO Way Authentication" v-on:click="DossssAuth" class="btn btn-danger" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </section>
                </div>
                <asp:HiddenField runat="server" ID="bankID" ClientIDMode="Static" />

                <div class="modal" id="ErrorMessage" role="dialog" data-backdrop="static" data-bs-keyboard="false">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="text-align: center">
                                <div class="modal-header" style="text-align: center">

                                    <div class="col-md-6">
                                        <img runat="server" id="imgSet" height="50" />
                                    </div>
                                    <div class="col-md-6">
                                        <img src='<%=  "./images/Company/"+company.Logo %>' height="50" />

                                    </div>
                                </div>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12 center-block" style="text-align: center">

                                        <a style="color: blue;" runat="server" id="Message"></a>
                                    </div>

                                    <div class="col-md-12">
                                        <table class="table table-responsive" v-for="item in Result">
                                            <tr>
                                                <td><strong>BankName</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblBankName">{{item.BankName}} </asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><strong>Aadhar No</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAadharNo">{{item.AadharNo}} </asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><strong>Customer Mobile</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="CusMobile">{{item.MobileNo}} </asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><strong>BC Code</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblBcCode">{{item.BCCode}} </asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><strong>BC Name</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="BCName">{{item.BCName}}</asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td><strong>TransactionID</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblTransID">{{item.TransID}}</asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td><strong>RRN</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblBankNumber">{{item.BankNumber}} </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><strong>Amount
                                                </strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblamountCW">{{item.Amount}} </asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td><strong>Balance</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAmount"> {{item.AccountBalance}}</asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td><strong>Remark</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblMark">{{item.Reason}} </asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td><strong>Date</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbldate">{{item.Date}} </asp:Label>

                                                </td>
                                            </tr>

                                        </table>
                                    </div>

                                </div>
                            </div>
                            <div class="modal-footer">
                                Powered By   
                            <img src="../img/powerdby.png" style="width: 200px;" />
                                <button type="button" class="btn btn-default" data-bs-dismiss="modal">Close</button>
                                <button id="btnPrint" type="button" class="btn btn-default" onclick="printDiv('ErrorMessage')">Print</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        function newfunction(data) {
            $("#bankID").val(data);
        }
        function HideModal() {
            $("#ErrorMessage").modal('hide');
        }
        function printDiv(divName) {
            var elem = document.getElementById(divName);
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            var domClone = elem.cloneNode(true);

            var $printSection = document.getElementById("printSection");

            if (!$printSection) {
                var $printSection = document.createElement("div");
                $printSection.id = "printSection";
                document.body.appendChild($printSection);
            }

            $printSection.innerHTML = "";
            $printSection.appendChild(domClone);
            window.print();

        }

        function ValidateData(cls) {
            var Flag = true;
            $(cls).each(function () {
                if ($(this).val() == "") {
                    $(this).css("border", "1px solid red");
                    Flag = false;
                } else {
                    var Len = $(this).attr('Maxlength');
                    if (Len != null) {
                        if ($(this).val().length != Len) {
                            $(this).css("border", "1px solid red");
                            Flag = false;
                            alertify.error('Enter ' + Len + ' digit value');
                        }
                    } else {
                        $(this).css("border", "1px solid green");

                    }
                }
            });
            return Flag;
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        $('[data-type="adhaar-number"]').keyup(function () {
            var value = $(this).val();
            value = value.replace(/\D/g, "").split(/(?:([\d]{4}))/g).filter(s => s.length > 0).join("-");
            $(this).val(value);
        });

        $('[data-type="adhaar-number"]').on("change, blur", function () {
            var value = $(this).val();
            var maxLength = $(this).attr("maxLength");
            if (value.length != maxLength) {
                $(this).addClass("highlight-error");
            } else {
                $(this).removeClass("highlight-error");
            }
        });

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
            try {
                var c = 0;
                var invertedArray = invArray(array);
                for (var i = 0; i < invertedArray.length; i++) {
                    c = d[c][p[(i % 8)][invertedArray[i]]];
                }
                return (c === 0);
            } catch (err) {

            }
        }

        $("#aadhar_number").change(function () {
            var aaa = $('#aadhar_number').val();
            var aad = aaa.replace(/ /g, '');
            var ana = aad.trim();
            var myStr = parseInt(ana.replace(/'/g, ''));
            if (validate(myStr)) {
                $('#aadhar-error').css("display", "none");
                if ($('#formmobile').val() != '' && $('#test').val() != '' && $('#nationalbank').val() != '') {
                    $('#datascan').removeAttr('disabled');
                } else {
                    $('#datascan').attr('disabled', 'disabled');
                }
                console.log("Match");
            } else {
                $('#aadhar-error').css("display", "block");
                $('#datascan').attr('disabled', 'disabled');
                console.log("UnMatch");
            }
        });



        $("#formmobile").change(function () {
            var aaa = $('#formmobile').val();
            var aad = aaa.replace(/ /g, '');
            var ana = aad.trim();
            var myStr = parseInt(ana.replace(/'/g, ''));
            if (ana.length == 10) {
                $('#Mobile-error').css("display", "none");
                $('#datascan').removeAttr('disabled');
                $(this).next('.inputs').focus();
            } else {
                $('#Mobile-error').css("display", "block");
                $('#datascan').attr('disabled', 'disabled');
            }
        });

        var objf;
        new Vue({
            el: '#aeps',
            data() {
                return {
                    Amt: 0,
                    Com: 0,
                    BankData: [],
                    Registration: false,
                    Authencation: false,
                    Bankselect: false,
                    CWAuth: false,
                    Bankselecttext: "",
                    MerchantDetails: [],
                    BankAEPSPIPEList: [],
                    Result: [],
                    ShowDivCW: false,
                    AEPS: {
                        Mobile: "",
                        Amount: 0,
                        BankID: {
                            id: 0,
                            text: "",
                        },
                        AadharNo: "",
                        Capture: "",
                        DeviceName: "",
                        Type: "BE",
                    },
                    AEPSMerchant: {
                        accessmodetype: "",
                        adhaarnumber: "",
                        mobilenumber: "",
                        latitude: "",
                        longitude: "",
                        referenceno: "",
                        submerchantid: "",
                        timestamp: "",
                        data: "",
                        ipaddress: ""
                    }
                }
            },
            methods: {
                sl: function (bankid, obj) {
                    var el = this;
                    el.AEPS.BankID.id = bankid;
                    el.AEPS.BankID.text = el.BankData.filter(function name(params) {
                        return params.iinno == bankid
                    })[0].bankName;
                    $(".select2").select2('destroy');
                    $('#nationalbank').val(el.AEPS.BankID).trigger("change");
                    $(".select2").select2();
                    $(".ddgyth").css("box-shadow", "rgba(0, 0, 0, 0.35) 0px 0px 0px");
                    $(obj.parentElement).css("box-shadow", "rgba(0, 0, 0, 0.35) 0px 5px 15px");
                },
                changeItem: function changeItem(event) {
                    var el = this;
                    var Type = event.target.value;
                    if (Type == "CW")
                        el.ShowDivCW = true;
                    else
                        el.ShowDivCW = false;
                },
                BankSelect: function (event) {
                    var el = this;
                    el.AEPS.BankID.id = event.target.value;

                },

                amtchnage: function () {
                    var el = this;
                    var aaa = el.AEPS.Amount;
                    var aad = aaa.replace(/ /g, '');
                    var ana = aad.trim();
                    var myStr = parseInt(ana.replace(/'/g, ''));

                    if (myStr >= 100 && myStr <= 10000) {
                        $('#amount-error').css("display", "none");
                        $('#datascan').removeAttr('disabled');
                    } else {
                        $('#amount-error').css("display", "block");
                        $('#datascan').attr('disabled', 'disabled');
                    }
                    el.GetCommission();
                },
                newfunction_: function (data, ObjData) {
                    $('.Amountcl').each(function () {
                        $(this).removeClass("active_cash");
                    });
                    $(ObjData).addClass("active_cash");
                    $('#amount-error').css("display", "none");
                    $('#datascan').removeAttr('disabled');
                    $("#txtAmount").val(data);
                    this.AEPS.Amount = data;
                    this.GetCommission();
                }
                ,
                GetCommission: function () {
                    var el = this;
                    const article = {
                        "Amt": el.AEPS.Amount
                    }
                    axios.post("AEPSBank.aspx/GetCommission", article)
                        .then(response => {
                            el.Com = response.data.d;
                        });
                },
                DOtry: function () {
                    var el = this;

                    var ss = objf.discoverAvdm('BE');
                    if (ss.Cap != "") {
                        el.DOAEPS();
                    }
                },
                DOAEPS: function () {
                    document.getElementById('load').style.visibility = "visible";
                    var el = this;
                    el.AEPS.Capture = $("#hdnCapture").val();
                    el.AEPS.DeviceName = $("#hdnDevice").val();
                    const article = {
                        "aEPS": el.AEPS,
                        "pipe": el.Bankselecttext
                    }
                    axios.post("AEPSBank.aspx/DOAEPS", article)
                        .then(response => {
                            el.Result = (response.data.d);
                            $('#ErrorMessage').modal('show');
                            el.CWAuth = false;
                            document.getElementById('load').style.visibility = "hidden";
                        });
                },
                DOAEPSRegistration: function () {
                    document.getElementById('load').style.visibility = "visible";
                    var el = this;
                    el.AEPSMerchant.data = $("#hdnCapture").val();
                    const article = {
                        "aEPS": el.AEPSMerchant,
                        "pipe": el.Bankselecttext
                    }
                    axios.post("AEPSBank.aspx/MerchantRegistration", article)
                        .then(response => {
                            el.Result = (response.data.d);
                            alertify.alert(JSON.parse(el.Result).status);
                            document.getElementById('load').style.visibility = "hidden";
                            el.GetMerchantDetails();
                        });
                },
                DOAEPSRegistrationAuth: function () {
                    document.getElementById('load').style.visibility = "visible";
                    var el = this;
                    el.AEPSMerchant.data = $("#hdnCapture").val();
                    const article = {
                        "aEPS": el.AEPSMerchant,
                        "pipe": el.Bankselecttext
                    }
                    axios.post("AEPSBank.aspx/AuthenticateRegistration", article)
                        .then(response => {
                            el.Result = (response.data.d);
                            alertify.alert(JSON.parse(el.Result).status);
                            document.getElementById('load').style.visibility = "hidden";
                            el.GetMerchantDetails();
                        });
                },
                GetBankList: function () {
                    var el = this;
                    const article = {

                    }
                    axios.post("AEPSBank.aspx/GetBankDetails", article)
                        .then(response => {
                            el.BankData = JSON.parse(response.data.d);
                        });
                }
                ,
                Dossss: function () {
                    var el = this;
                    var ss = objf.discoverAvdm('BE');
                    if (ss.Cap != "") {
                        el.DOAEPSRegistration();
                    }
                },
                selbank: function (ff) {
                    var el = this;
                    el.Bankselecttext = ff;
                    const article = {
                        "pipe": ff
                    }
                    axios.post("AEPSBank.aspx/CheckStatusPipe", article)
                        .then(response => {
                            var dd = JSON.parse(response.data.d);
                            if (dd.statuscode == "TXN") {
                                alertify.success(dd.status);
                                el.Bankselect = true;
                                el.GetMerchantDetails();
                                el.Bankselecttext = ff;
                            }
                            else {
                                alertify.error(dd.status);
                                el.Bankselect = false;
                            }

                            el.GetBankAEPSPIPEList();
                        });
                },
                DossssAuth: function () {
                    var el = this;
                    var ss = objf.discoverAvdm('BE');
                    if (ss.Cap != "") {
                        el.DOAEPSRegistrationAuth();
                    }
                },
                DOAuthData: function () {
                    var el = this;
                    var ss = objf.discoverAvdm('BE');
                    if (ss.Cap != "") {
                        document.getElementById('load').style.visibility = "visible";
                        var el = this;
                        el.AEPSMerchant.data = $("#hdnCapture").val();
                        const article = {
                            "aEPS": el.AEPSMerchant,
                            "pipe": el.Bankselecttext
                        }
                        axios.post("AEPSBank.aspx/AuthenticateRegistrationCW", article)
                            .then(response => {
                                el.Result = (response.data.d);
                                alertify.alert(JSON.parse(el.Result).status);
                                el.CWAuth = true;
                                document.getElementById('load').style.visibility = "hidden";
                            });
                    }
                },

                GetMerchantDetails: function () {
                    var el = this;
                    const article = {
                        "pipe": el.Bankselecttext
                    }
                    axios.post("AEPSBank.aspx/GetMerchantDetails", article)
                        .then(response => {
                            el.MerchantDetails = JSON.parse(response.data.d);
                            if (el.MerchantDetails.length > 0) {
                                if (el.MerchantDetails[0].AEPSTwoWayRegistration == false) {
                                    alert("Merchant Two Way Registration Not Complete so Apply Your thumb");

                                } else if (el.MerchantDetails[0].DailyAuthentication == false) {
                                    el.Registration = true;
                                    alert("Merchant Two Way Authenticate Not Complete so Apply Your thumb");

                                }
                                else {
                                    el.Registration = true;
                                    el.Authencation = true;
                                }
                            }
                        });
                },
                GetBankAEPSPIPEList: function () {
                    var el = this;
                    const article = {

                    }
                    axios.post("AEPSBank.aspx/GetBankAEPSStatus", article)
                        .then(response => {
                            el.BankAEPSPIPEList = JSON.parse(response.data.d);
                        });
                }
            },
            mounted() {
                this.GetBankList();
                //this.GetMerchantDetails();
                this.GetBankAEPSPIPEList();
            },
        });

    </script>
</asp:Content>

