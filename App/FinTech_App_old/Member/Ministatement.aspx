<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="Ministatement.aspx.cs" Inherits="Member_Ministatement" %>

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
                        <div class="aeps_wrap">
                            <div class="container-fluid">
                                <div class="aeps_tabs">
                                    <ul>
                                <li><a href="CashWithdrawal.aspx">Cash Withdrawal</a></li>
                                <li><a href="aeps-balance-enquiry.aspx">Balance Enquiry</a></li>
                                <li class="active"><a href="Ministatement.aspx">Ministatement</a></li>
                                <li><a href="CashDeposit.aspx">Cash Deposit</a></li>
                                    </ul>
                                </div>
                                <div class="aeps_form">
                                    <div>

                                        <input type="hidden" name="indicatorforuid">
                                        <input type="hidden" name="nationalbankidentification">
                                        <input type="hidden" name="language">
                                        <input type="hidden" name="latitude" id="latitude" value="26.858726299999997">
                                        <input type="hidden" name="longitude" id="longitude" value="75.7720215">
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
                                        <asp:HiddenField runat="server" ID="hdnCapture" ClientIDMode="Static" />
                                        <asp:HiddenField runat="server" ID="hdnDevice" ClientIDMode="Static" />

                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label>
                                                        <img src="https://rnfi.in/asset/images/_default/aeps/mpos.png">
                                                        Select Device:</label>
                                                </div>
                                                <div class="col-sm-8 ">
                                                    <label>
                                                        <input type="radio" name="capdata" value="mantra" checked="" class="default_rd_serv device-radio">
                                                        Mantra</label>
                                                    <label>
                                                        <input type="radio" name="capdata" value="morpho" class="device-radio">
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
                                                    <div class="mobile_blk_wrap">
                                                        <div class="mobile_blk">
                                                            <img src="https://rnfi.in/asset/images/_default/aeps/flag.jpg">
                                                            +91
                                                        </div>
                                                        <asp:TextBox runat="server" ID="formmobile" class="form-control Validate" MinLength="10" MaxLength="10" onkeypress="return onlyNumberKey(event)"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label>
                                                        <img src="https://rnfi.in/asset/images/_default/aeps/aadhar.png">
                                                        Aadhar Number:</label>
                                                </div>
                                                <div class="col-sm-8">
                                                    <div class="mobile_blk_wrap aadhar_blk">
                                                        <div class="mobile_blk">
                                                            <img src="https://rnfi.in/asset/images/_default/aeps/fingerprint_icon.png">
                                                        </div>
                                                        <asp:TextBox runat="server" ID="aadhar_number" data-inputmask="'mask': '9999 9999 9999'" onkeypress="return onlyNumberKey(event)" MaxLength="12" class="form-control Validate"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <!--<div class="col-sm-4"><label class="line_h_134"><img src=""> Bank:</label></div>-->
                                                <div class="col-sm-4">
                                                    <label>
                                                        <img src="https://rnfi.in/asset/images/_default/aeps/bank.png">
                                                        Bank:</label>
                                                </div>
                                                <div class="col-sm-8">
                                                    <div class="fast_cash js_fast_bank">
                                                        <%--<button type="button" onclick="newfunction(607094);" name="nationalbankidentification" class="active_cash" value="607094">SBI Bank</button>
                                                <button type="button" name="nationalbankidentification" onclick="newfunction(607027);" value="607027">Punjab National Bank</button>
                                                <button type="button" name="nationalbankidentification" onclick="newfunction(606985);" value="606985">Bank of Baroda</button>
                                                <button type="button" name="nationalbankidentification" onclick="newfunction(508505);" value="508505">Bank of India</button>
                                                <button type="button" name="nationalbankidentification" onclick="newfunction(607264);" value="607264">Central Bank of India</button>
                                                <button type="button" name="nationalbankidentification" onclick="newfunction(607153);" value="607153">Axis Bank</button>
                                                <button type="button" name="nationalbankidentification" onclick="newfunction(508534);" value="508534">ICICI Bank</button>
                                                <button type="button" name="nationalbankidentification" onclick="newfunction(607152);" value="607152">HDFC Bank</button>--%>
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
                                                    <img src="https://rnfi.in/asset/images/_default/aeps/fingerprint.png">
                                                </div>
                                                <div class="col-sm-8">
                                                    <p class="line_h_40">Please ask customer to place his thumb for impression on biometric scanner. (कृपया ग्राहक का अँगूठा बायोमेट्रिक स्कैनर पर रखे)</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4"></div>
                                                <div class="col-sm-8">

                                                    <input type="button" id="datascan" onclick="discoverAvdm('MS')" class="btn btn-danger" value="Check Device" />
                                                    <asp:Button runat="server" disabled='disabled' ID="btnMS" ClientIDMode="Static" Text="Mini Statement" CssClass="btn btn-danger" OnClick="btnMS_Click" OnClientClick="return ValidateData('.Validate')" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnMS" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

            <div class="modal" id="ErrorMessage" role="dialog" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog">
                    <asp:UpdatePanel runat="server" ID="updateSend">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <div class="modal-header" style="text-align: center">
                                        <div class="col-md-6">
                                            <img src='<%=  "../images/Company/"+company.Logo %>' height="50" />

                                        </div>
                                        <div class="col-md-6">
                                            <img src="../img/AEPS-Logo.png" height="50" />
                                            <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12" runat="server" id="errorMessage" visible="false">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblErrorMessage" ForeColor="Red" Font-Size="X-Large"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-12" runat="server" id="successMessage" visible="false">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <table class="table table-responsive">
                                                    <tr>
                                                        <td>status</td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblStatus" CssClass="btn btn-success"> </asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>rrn number</td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblBankNumber" CssClass="btn btn-success"> </asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Amount</td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblAmount" CssClass="btn btn-success"> </asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Remainig Balance</td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblRemainigBalance" CssClass="btn btn-success"> </asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Message</td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblSuccessMessage" ForeColor="Red" Font-Size="X-Large"></asp:Label>

                                                        </td>
                                                    </tr>
                                                </table>
                                                <table class="table table-responsive">
                                                    <tr>
                                                        <th><strong>SNo.</strong></th>
                                                        <th><strong>date</strong></th>
                                                        <th><strong>txnType</strong></th>
                                                        <th><strong>amount</strong></th>
                                                        <th><strong>narration</strong></th>
                                                    </tr>
                                                    <asp:Repeater runat="server" ID="rptMiniState">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Container.ItemIndex+1 %></td>
                                                                <td><%# Eval("date") %></td>
                                                                <td><%# Eval("txnType") %></td>
                                                                <td><%# Eval("amount") %></td>
                                                                <td><%# Eval("narration") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    Powered By   
                            <img src="../img/powerdby.png" style="width: 200px;" />
                                    <button type="button" class="btn btn-default" data-bs-dismiss="modal">Close</button>
                                    <button class="btn btn-default" onclick="printDiv('ErrorMessage')"><i class="fa fa-print" aria-hidden="true" style="font-size: 17px;">Print</i></button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <script>
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

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

