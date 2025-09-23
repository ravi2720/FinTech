<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="IntantPayActivation.aspx.cs" Inherits="Member_IntantPayActivation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/aepsBank.css" rel="stylesheet" />
    <script src="../app_js/aeps-custom-new.js"></script>
    <style>
        @media screen {
            #printSection {
                display: none;
            }
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

            <asp:UpdatePanel runat="server" ID="updateAEPSBE">
                <ContentTemplate>

                    <div class="content-wrapper">
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
                        <input type="hidden" name="txtWadh" id="txtWadh">
                        <input type="hidden" name="new" id="method" class="form-control">
                        <asp:HiddenField runat="server" ID="hdnDevice" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="hdnCapture" ClientIDMode="Static" />

                        <section class="content">
                            <div class="container-fluid">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-4"></div>
                                        <div class="col-sm-8">
                                            <input type="button" id="datascan" onclick="discoverAvdm('BE')" class="btn btn-danger" value="Check Device" />
                                            <asp:Button runat="server" disabled='disabled' ID="btnBalance" ClientIDMode="Static" Text="Verify Your Identity" CssClass="btn btn-success" OnClick="btnOutletLogin_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" style="display: none">
                                <div class="aeps_tabs">
                                    <ul>
                                        <li><a href="CashWithdrawal.aspx">Cash Withdrawal</a></li>
                                        <li class="active"><a href="aeps-balance-enquiry.aspx">Balance Enquiry</a></li>
                                        <li><a href="Ministatement.aspx">Ministatement</a></li>
                                        <li><a href="CashDeposit.aspx">Cash Deposit</a></li>
                                    </ul>
                                </div>

                                <div class="aeps_form">
                                    <div class="form-group" style="display: none">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label>
                                                    <img src="../img/mpos.png" />Select Device:</label>
                                            </div>
                                            <div class="col-sm-8 ">
                                                <label>
                                                    <input type="radio" name="capdata" value="mantra" checked="" class="default_rd_serv device-radio" />
                                                    Mantra</label>
                                                <label>
                                                    <input type="radio" name="capdata" value="morpho" class="device-radio" />
                                                    Morpho</label>
                                                <label>
                                                    <input type="radio" name="capdata" value="secugen" class="device-radio" />
                                                    Secugen</label>
                                                <label>
                                                    <input type="radio" name="capdata" value="precision" class="device-radio" />
                                                    Precision</label>
                                                <label>
                                                    <input type="radio" name="capdata" value="startek" class="device-radio" />
                                                    Startek</label>
                                                <label>
                                                    <input type="radio" name="capdata" value="nextrd" class="device-radio" />
                                                    NEXT</label>
                                                <label>
                                                    <input type="radio" name="capdata" value="iris" class="device-radio" />
                                                    IRIS</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label>
                                                    <i class="fa fa-mobile"></i>
                                                    Mobile Number:</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="mobile_blk_wrap">
                                                    <div class="mobile_blk">
                                                        <i class="flag flag-in"></i>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="formmobile" class="form-control Validate" MinLength="10" MaxLength="10" onkeyup="if (/\D/g.test(this.value)) this.value = this.value.replace(/\D/g,'')"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label>
                                                    <img src="../img/aadhar.png" />
                                                    Aadhar Number:</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="mobile_blk_wrap aadhar_blk">
                                                    <div class="mobile_blk">
                                                        <img src="../img/fingerprint_icon.png" />
                                                    </div>
                                                    <asp:TextBox runat="server" ID="aadhar_number" ClientIDMode="Static" data-inputmask="'mask': '9999 9999 9999'" MaxLength="12" class="form-control Validate"></asp:TextBox>
                                                    <span style="color: red; display: none;" id="aadhar-error">Please enter valid Aadhaar Number.</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label>
                                                    <img src="../img/bank.png" />

                                                    Bank:</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="fast_cash js_fast_bank">
                                                </div>
                                                <div class="enter_amount">
                                                    <asp:DropDownList runat="server" ID="nationalbank" CssClass="form-control select2" data-placeholder="Select">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
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
                            </div>
                        </section>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnBalance" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </div>
    <script>
        try {

            function ShowModal() {
                $("#ErrorMessage1").modal('show');
            }
            function HideModal() {
                $("#ErrorMessage").modal('hide');
            }

            function printDiv(divName) {
                var elem = document.getElementById(divName);
                var printContents = document.getElementById(divName).innerHTML;
                var originalContents = document.body.innerHTML;

                //  document.body.innerHTML = printContents;
                //          window.print();
                // document.body.innerHTML = originalContents;

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
        } catch (err) {

        }
    </script>
</asp:Content>

