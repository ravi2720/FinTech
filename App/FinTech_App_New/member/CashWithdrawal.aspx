<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="CashWithdrawal.aspx.cs" Inherits="Member_CashWithdrawal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <link href="./css/aepsBank.css" rel="stylesheet" />
    <script src="./app_js/aeps-custom-new.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-container container-fluid"> 
        <div class="main-content-body">
    <asp:UpdatePanel runat="server" ID="updateAEPSBE">
        <ContentTemplate>
            <div class="content-wrapper">
                <section class="content">
                    <div class="container-fluid">
                        <div class="aeps_tabs">
                            <ul>
                                <li class="active"><a href="CashWithdrawal.aspx">Cash Withdrawal</a></li>
                                <li><a href="aeps-balance-enquiry.aspx">Balance Enquiry</a></li>
                                <li><a href="Ministatement.aspx">Ministatement</a></li>
                                <li><a href="CashDeposit.aspx">Cash Deposit</a></li>
                            </ul>
                        </div>
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
                            <input type="hidden" name="merchantTranId">
                            <input type="hidden" name="unicode">
                            <input type="hidden" name="submerchantid">
                            <input type="hidden" name="txtWadh" id="txtWadh">
                            <asp:HiddenField runat="server" ID="hdnDevice" ClientIDMode="Static" />
                            <asp:HiddenField runat="server" ID="hdnCapture" ClientIDMode="Static" />

                            <div class="form-group">
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
                                    <div class="col-sm-4">
                                        <label>
                                            <img src="https://rnfi.in/asset/images/_default/aeps/mobile.png">
                                            Mobile Number:</label>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="mobile_blk_wrap aadhar_blk">
                                            <div class="mobile_blk">
                                                <img src="https://rnfi.in/asset/images/_default/aeps/flag.jpg">
                                            </div>
                                            <asp:TextBox runat="server" ID="formmobile" data-inputmask="'mask': '9999 9999 9999'" onkeypress="return isNumber(event)" class="form-control Validate" MinLength="10" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label class="line_h_92">
                                            <img src="https://rnfi.in/asset/images/_default/aeps/money.png">
                                            Amount:</label>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="fast_cash js_fast_cash">
                                            <button type="button" name="transcationamount" class="Amountcl active_cash" onclick="newfunction_(500,this);" value="500">Rs. 500/-</button>
                                            <button type="button" name="transactionamount" class="Amountcl" value="1000" onclick="newfunction_(1000,this);">Rs. 1000/-</button>
                                            <button type="button" name="transactionamount" class="Amountcl" value="2000" onclick="newfunction_(2000,this);">Rs. 2000/-</button>
                                            <button type="button" name="transactionamount" class="Amountcl" value="3000" onclick="newfunction_(3000,this);">Rs. 3000/-</button>
                                            <button type="button" name="transcationamount" class="Amountcl" value="5000" onclick="newfunction_(5000,this);">Rs. 5000/-</button>
                                            <button type="button" name="transcationamount" class="Amountcl" value="10000" onclick="newfunction_(10000,this);">Rs. 10000/-</button>
                                        </div>
                                        <div class="mobile_blk_wrap">
                                            <div class="mobile_blk">
                                                <img src="https://rnfi.in/asset/images/_default/aeps/money_icon.png" />
                                                Rs.
                                            </div>
                                            <asp:TextBox runat="server" ID="txtAmount" ClientIDMode="Static" class="form-control Validate"></asp:TextBox>
                                            <input type="hidden" name="new" id="method" class="form-control">
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
                                            <asp:TextBox runat="server" ID="aadhar_number" ClientIDMode="Static" onkeypress="return isNumber(event)" MaxLength="12" data-inputmask="'mask': '9999 9999 9999'" class="form-control Validate"></asp:TextBox>
                                            <span style="color: red; display: none;" id="aadhar-error">Please enter valid Aadhaar Number.</span>

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
                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="nationalbank" CssClass="form-control select2" data-placeholder="Select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <img src="https://rnfi.in/asset/images/_default/aeps/fingerprint.png">
                                    </div>
                                    <div class="col-sm-8">
                                        <p class="line_h_40">Please ask customer to place his thumb for impression on biometric scanner. (कृपया ग्राहक का अँगूठा बायोमेट्रिक स्कैनर पर रखे)</p>
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

                                        <input type="button" id="datascan" onclick="discoverAvdm('CW')" class="btn btn-danger" value="Check Device" />
                                        <%--<input type="button" onclick="balancealert();" id="datasubmit" class="btn btn-success" value="Scan" disabled="" />--%>
                                        <asp:Button runat="server" ID="btnCW" disabled='disabled' Text="Cash Withdraw" ClientIDMode="Static" CssClass="btn btn-success" OnClick="btnCW_Click" OnClientClick="return ValidateData('.Validate')" />
                                    </div>
                                </div>
                            </div>



                        </div>

                    </div>
                </section>
            </div>
            <asp:HiddenField runat="server" ID="bankID" ClientIDMode="Static" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCW" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="modal fade" id="ErrorMessage" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center">
                            <div class="modal-header" style="text-align: center">
                                <div class="col-md-6">
                                    <img src='<%=  "./images/Company/"+company.Logo %>' height="50" />

                                </div>
                                <div class="col-md-6">
                                    <img src="./img/aadharpay.png" height="50" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12 center-block" style="text-align: center">
                                    <img runat="server" id="imgSet" height="50" /><br />
                                    <a style="color: blue;" runat="server" id="Message"></a>
                                </div>

                                <div class="col-md-12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table class="table table-responsive">
                                            <tr>
                                                <td><strong>BankName</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblBankName"> </asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><strong>Aadhar No</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAadharNo"> </asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><strong>Customer Mobile</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="CusMobile"> </asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><strong>BC Code</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblBcCode"> </asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><strong>BC Name</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="BCName"></asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td><strong>TransactionID</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblTransID"></asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td><strong>RRN</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblBankNumber"> </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><strong>Amount
                                                </strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblamountCW"> </asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td><strong>Balance</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAmount"> </asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td><strong>Remark</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblMark"> </asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td><strong>Date</strong></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbldate"> </asp:Label>

                                                </td>
                                            </tr>

                                        </table>
                                    </asp:Panel>
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer">
                            Powered By   
                            <img src="./img/powerdby.png" style="width: 200px;" />
                            <button type="button" class="btn btn-default" data-bs-dismiss="modal">Close</button>
                            <asp:Button ID="btnexportPdf" runat="server" Text="PRINT" CssClass="btn btn-info"
                                CausesValidation="false" OnClientClick="printDiv('ErrorMessage')" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
          </div>
        </div>
    <script>
        function newfunction_(data, ObjData) {
            $('.Amountcl').each(function () {
                $(this).removeClass("active_cash");
            });
            $(ObjData).addClass("active_cash");

            $("#txtAmount").val(data);
        }
        function newfunction(data) {
            $("#bankID").val(data);

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
            var c = 0;
            var invertedArray = invArray(array);
            for (var i = 0; i < invertedArray.length; i++) {
                c = d[c][p[(i % 8)][invertedArray[i]]];
            }
            return (c === 0);
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
    </script>
</asp:Content>


