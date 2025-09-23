var GetPIString = '';
var GetPAString = '';
var GetPFAString = '';
var DemoFinalString = '';
var select = '';
select += '<option val=0>Select</option>';
for (i = 1; i <= 100; i++) {
    select += '<option val=' + i + '>' + i + '</option>';
}
$('#drpMatchValuePI').html(select);
$('#drpMatchValuePFA').html(select);
$('#drpLocalMatchValue').html(select);
$('#drpLocalMatchValuePI').html(select);
var finalUrl = "";
var MethodInfo = "";
var MethodCapture = "";
//var primaryUrl = "https://127.0.0.1:";// For HTTPS
var primaryUrl = "http://127.0.0.1:";// For HTTP

function reset() {
    $('#txtWadh').val('');
    $('#txtPidData').val('');
}

function discoverAvdm(transactiondata) {
    openNav();
    $('#txtWadh').val('');
    $('#txtPidData').val('');
    var SuccessFlag = 0;
    try {
        var protocol = window.location.href;
        if ((protocol.indexOf("https") >= 0) && ((transactiondata.response.capture) != "nextrd")) {
            primaryUrl = "http://127.0.0.1:";
        } else if ((transactiondata.response.capture) == "nextrd") {
            primaryUrl = "http://127.0.0.1:";
        } else {
            primaryUrl = "http://127.0.0.1:";
        }
    } catch (e)
    { }

    url = "";

    for (var i = 11100; i <= 11120; i++) {
        $("#lblstatus").text("Discovering RD service on port : " + i.toString());
        var verb = "RDSERVICE";
        var err = "";
        SuccessFlag = 0;
        var res;
        $.support.cors = true;
        var httpStaus = false;
        var jsonstr = "";
        var data = new Object();
        var obj = new Object();
        $.ajax({
            type: "RDSERVICE",
            async: false,
            crossDomain: true,
            dataType: "xml",
            url: primaryUrl + i.toString(),
            contentType: "text/xml; charset=\"utf-8\"",
            processData: false,
            cache: false,

            success: function (data) {
                debugger;

                httpStaus = true;
                res = { httpStaus: httpStaus, data: data };

                finalUrl = primaryUrl + i.toString();

                var $doc = data;//$.parseXML(data);
                var CmbData1 = $($doc).find('RDService').attr('status');
                var CmbData2 = $($doc).find('RDService').attr('info');

                if (RegExp('\\b' + 'Mantra' + '\\b').test(CmbData2) == true || RegExp('\\b' + 'Morpho_RD_Service' + '\\b').test(CmbData2) == true || RegExp('\\b' + 'SecuGen India Registered device Level 0' + '\\b').test(CmbData2) == true || RegExp('\\b' + 'Precision - Biometric Device is ready for capture' + '\\b').test(CmbData2) == true || RegExp('\\b' + 'RD service for Startek FM220 provided by Access Computech' + '\\b').test(CmbData2) == true || RegExp('\\b' + 'NEXT' + '\\b').test(CmbData2) == true) {
                    closeNav();
                    console.log($($doc).find('Interface').eq(0).attr('path'));

                    if (RegExp('\\b' + 'Mantra' + '\\b').test(CmbData2) == true) {

                        if ($($doc).find('Interface').eq(0).attr('path') == "/rd/capture") {
                            MethodCapture = $($doc).find('Interface').eq(0).attr('path');
                        }
                        if ($($doc).find('Interface').eq(1).attr('path') == "/rd/capture") {
                            MethodCapture = $($doc).find('Interface').eq(1).attr('path');
                        }
                        if ($($doc).find('Interface').eq(0).attr('path') == "/rd/info") {
                            MethodInfo = $($doc).find('Interface').eq(0).attr('path');
                        }
                        if ($($doc).find('Interface').eq(1).attr('path') == "/rd/info") {
                            MethodInfo = $($doc).find('Interface').eq(1).attr('path');
                        }
                        $("#hdnDevice").val('mantra');
                    } else if (RegExp('\\b' + 'Morpho_RD_Service' + '\\b').test(CmbData2) == true) {
                        MethodCapture = $($doc).find('Interface').eq(0).attr('path');
                        MethodInfo = $($doc).find('Interface').eq(1).attr('path');
                        $("#hdnDevice").val('morpho');

                    } else if (RegExp('\\b' + 'SecuGen India Registered device Level 0' + '\\b').test(CmbData2) == true) {
                        MethodCapture = $($doc).find('Interface').eq(0).attr('path');
                        MethodInfo = $($doc).find('Interface').eq(1).attr('path');
                        $("#hdnDevice").val('secugen');
                    } else if (RegExp('\\b' + 'Precision - Biometric Device is ready for capture' + '\\b').test(CmbData2) == true) {
                        MethodCapture = $($doc).find('Interface').eq(0).attr('path');
                        MethodInfo = $($doc).find('Interface').eq(1).attr('path');
                        $("#hdnDevice").val('precision');
                    } else if (RegExp('\\b' + 'RD service for Startek FM220 provided by Access Computech' + '\\b').test(CmbData2) == true) {
                        MethodCapture = $($doc).find('Interface').eq(0).attr('path');
                        MethodInfo = $($doc).find('Interface').eq(1).attr('path');
                        $("#hdnDevice").val('startek');
                    } else if (RegExp('\\b' + 'NEXT' + '\\b').test(CmbData2) == true) {
                        MethodCapture = $($doc).find('Interface').eq(0).attr('path');
                        MethodInfo = $($doc).find('Interface').eq(1).attr('path');
                        $("#hdnDevice").val('nextrd');
                    }


                    if (CmbData1 == 'READY') {
                        SuccessFlag = 1;

                        if (transactiondata == 'CW') {
                            alertify.success("Device detected successfully");
                            $('#method').val(finalUrl + MethodCapture);

                           
                            //$('#datascan').prop('disabled', true);
                            $('#datasubmit').prop('disabled', false);
                            $('#btnCW').prop('disabled', false);
                            
                            
                            alert('Please put finger on your device!');
                            BalanceEnquiry();
                            // CaptureAvdm();
                        } else if (transactiondata == 'BE') {
                            alertify.success("Device detected successfully");



                            $('#method').val(finalUrl + MethodCapture);
                           
                            //$('#datascan').prop('disabled', true);
                            $('#datasubmit').prop('disabled', false);

                            $('#btnBalance').prop('disabled', false);

                            alert('Please put finger on your device!');
                            BalanceEnquiry();

                            //BalanceEnquiry();	
                        } else if (transactiondata == 'M') {
                            alert("Device detected successfully");
                            $('#method').val(finalUrl + MethodCapture);
                            
                        } else if (transactiondata.transactiontype == 'CD') {
                            alert("Device detected successfully");
                            $('#method').val(finalUrl + MethodCapture);
                            $("input[name=adhaarnumber]").val(transactiondata.response.adhaarnumber),
                                $("input[name=mobilenumber]").val(transactiondata.response.mobilenumber),
                                $("input[name=requestremarks]").val(transactiondata.response.requestremarks),
                                $("input[name=nationalbankidentification]").val(transactiondata.response.nationalbankidentification),
                                $("input[name=language]").val(transactiondata.response.language),
                                $("input[name=latitude]").val(transactiondata.response.latitude),
                                $("input[name=longitude]").val(transactiondata.response.longitude),
                                $("input[name=timestamp]").val(transactiondata.response.timestamp),
                                $("input[name=transcationtype]").val(transactiondata.response.transcationtype),
                                $("input[name=submerchantid]").val(transactiondata.response.submerchantid),
                                $("input[name=merchantTranId]").val(transactiondata.transferid),
                                //$("input[name=capdata]").val(transactiondata.response.capture),
                                $('input:radio[name="capdata"]').filter('[value=' + transactiondata.response.capture + ']').attr('checked', true);
                            $('#datascan').prop('disabled', true);
                            $('#datasubmit').prop('disabled', false);

                            //BalanceEnquiry();	
                        } else if (transactiondata == 'MS') {
                            alert("Device detected successfully");
                            $('#method').val(finalUrl + MethodCapture);
                           
                            $('#datascan').prop('disabled', true);
                            $('#btnMS').prop('disabled', false);
                            
                            alert('Please put finger on your device!');
                            BalanceEnquiry();

                        }
                        return;
                    }
                    else if (CmbData1 == 'NOTREADY') {
                        alert("Device Not Discover");
                        return false;
                    }
                }
            },
            error: function (jqXHR, ajaxOptions, thrownError) {
                //res = { httpStaus: httpStaus, err: getHttpError(jqXHR) };
            },
        });

        if (SuccessFlag == 1) {
            break;
        }
    }
    if (SuccessFlag == 0) {
        alert("Connection failed Please try again or device not connected.");
    }
    closeNav();
    return res;
}

function openNav() {
    //document.getElementById("myNav").style.width = "100%";
    //$("#myNav").css('width', '100%');
}

function closeNav() {
    //document.getElementById("myNav").style.width = "0%";
    $("#myNav").css('width', '0%');
}

function capturealert() {

    alert('Please put finger on your device!');
    CaptureAvdm();
}


/*Cashwithdrawal call function*/
function CaptureAvdm() {

    var run = $("input[name='capdata']:checked").val();
    var devicetype = false;
    if (run == "iris") {
        var devicetype = true;
        var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="0" fType="2" iCount="1" pCount="0" format="0" pidVer="2.0" timeout="10000" posh="UNKNOWN" env="P" /> ' + DemoFinalString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
    } else {
        var devicetype = false;
        if ($("#txtWadh").val().trim() != "") {
            var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="1" fType="2" iCount="0" pCount="0" format="0" pidVer="2.0" timeout="10000" wadh="' + $("#txtWadh").val() + '" posh="UNKNOWN" env="P" /> ' + DemoFinalString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
        }
        else {
            var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="1" fType="2" iCount="0" pCount="0" format="0" pidVer="2.0" timeout="10000" posh="UNKNOWN" env="P" /> ' + DemoFinalString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
        }
    }

    $('#loaderbala').css("display", "block");
    var finUrl = $('#method').val();
    var verb = "CAPTURE";
    var err = "";
    var res;
    $.support.cors = true;
    var httpStaus = false;

    var jsonstr = "";
    $.ajax({
        type: "CAPTURE",
        crossDomain: true,
        url: finUrl,
        data: XML,
        contentType: "text/xml; charset=utf-8",
        processData: false,
        success: function (data) {
            if (run == "morpho") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //morpho
            } else if (run == "mantra") {
                var xmlString = data;  //mantra
            } else if (run == "secugen") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //secugen
            } else if (run == "precision") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //precision
            } else if (run == "startek") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //startek
            } else if (run == "nextrd") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //next rd
            } else if (run == "iris") {
                var xmlString = data;   //MANTRA IRIS
            }
            httpStaus = true;
            res = { httpStaus: httpStaus, data: xmlString };

            $('#txtPidData').val(xmlString);
            var $doc = data;//$.parseXML(data);
            var Message = $($doc).find('Resp').attr('errInfo');
            var errorcode = $($doc).find('Resp').attr('errCode');
            if (errorcode == 0) {

                var aaa = "'" + $('input[name=adhaarnumber]').val() + "'"
                var aad = aaa.replace(/ /g, '');
                var ana = aad.trim();
                var myStr = ana.replace(/'/g, '');
                $.ajax({
                    type: 'POST',
                    url: "" + baseURL + "/aeps-request-cashwithdrawal",
                    data: {
                        httpStaus: httpStaus, data: xmlString,
                        merchantusername: $("input[name=merchantusername]").val(),
                        adhaarnumber: myStr,
                        mobilenumber: $("input[name=mobilenumber]").val(),
                        transcationamount: $("input[name=transcationamount]").val(),
                        requestremarks: $("input[name=requestremarks]").val(),
                        indicatorforuid: $("input[name=indicatorforuid]").val(),
                        nationalbankidentification: $("input[name=nationalbankidentification]").val(),
                        language: $("input[name=language]").val(),
                        latitude: $("input[name=latitude]").val(),
                        longitude: $("input[name=longitude]").val(),
                        paymentType: $("input[name=paymentType]").val(),
                        timestamp: $("input[name=timestamp]").val(),
                        transcationtype: $("input[name=transcationtype]").val(),
                        merchantpin: $("input[name=merchantpin]").val(),
                        submerchantid: $("input[name=submerchantid]").val(),
                        merchantTranId: $("input[name=merchantTranId]").val(),
                        devicetype: devicetype,
                    },
                    crossDomain: true,
                    success: function (data) {
                        var read = jQuery.parseJSON(data);
                        if (read.status == true) {
                            $('#loaderbala').css("display", "none");
                            //alert('Response:' + read.message);
                            $("#sign").modal("toggle");
                            $('#balanceamount').val(read.balanceamount);
                            $('#transcationamount').val(read.amount);
                            $('#transunicode').val(read.txnid);
                            $("#balance").html("An amount of <br><strong>Rs. " + read.amount + " </strong><br>has been withdrawn from your bank account.");
                            //  $(".modal-body").html(read.message);
                            $('#newscan').trigger("reset");
                            $('#datasubmit').attr('disabled', 'disabled');
                        } else {
                            $('#loaderbala').css("display", "none");
                            alert('Response:' + read.message);
                            window.location.reload();
                            // $('#newscan').trigger("reset");
                            //$('#datasubmit').attr('disabled', 'disabled');
                        }
                    }
                });
            } else {
                $('#loaderbala').css("display", "none");
                alert('Capture Failed');
                window.location.reload();
                //$('#newscan').trigger("reset");
                //$('#datasubmit').attr('disabled', 'disabled');
            }
        },
        error: function (jqXHR, ajaxOptions, thrownError) {
            res = { httpStaus: httpStaus, err: getHttpError(jqXHR) };
        },
    });

    return res;
}

function balancealert() {
    
    alert('Please put finger on your device!');
    BalanceEnquiry();
}
// BalanceEnquiry call function
function BalanceEnquiry() {
    var devicetype = false;
    var run = "mantra";//  $("input[name='capdata']:checked"). val();
    if (run == "iris") {
        var devicetype = true;
        var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="0" fType="2" iCount="1" pCount="0" format="0" pidVer="2.0" timeout="10000" posh="UNKNOWN" env="P" /> ' + DemoFinalString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
    } else {

        var devicetype = false;
        // debugger;
        if ($("#txtWadh").val().trim() != "") {
            var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="1" fType="2" iCount="0" pCount="0" format="0" pidVer="2.0" timeout="10000" wadh="' + $("#txtWadh").val() + '" posh="UNKNOWN" env="P" /> ' + DemoFinalString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
        }
        else {
            var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="1" fType="2" iCount="0" pCount="0" format="0" pidVer="2.0" timeout="10000" posh="UNKNOWN" env="P" /> ' + DemoFinalString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
        }
    }

    $('#loaderbala').css("display", "block");
    var finUrl = $('#method').val();
    var verb = "CAPTURE";
    var err = "";
    var res;
    $.support.cors = true;
    var httpStaus = false;
    var baseURL = $('head base').attr('href');
    var jsonstr = "";
    $.ajax({
        type: "CAPTURE",
        async: false,
        crossDomain: true,
        url: finUrl,
        data: XML,
        contentType: "text/xml; charset=utf-8",
        processData: false,
        success: function (data) {
            // debugger;
            run = $("#hdnDevice").val();
            if (run == "morpho") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //morpho
            } else if (run == "mantra") {
                var xmlString = data;  //mantra
            } else if (run == "secugen") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //secugen
            } else if (run == "precision") {
                var xmlString = (new XMLSerializer()).serializeToString(data);
            } else if (run == "startek") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //startek
            } else if (run == "nextrd") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //next rd
            } else if (run == "iris") {
                var xmlString = data;   //MANTRA IRIS
            }

            httpStaus = true;

            $("#hdnCapture").val(xmlString);
            //	res = { httpStaus: httpStaus, data: xmlString};  
            ////	$('#txtPidData').val(xmlString);                                  
            //	var $doc = data;//$.parseXML(data);         
            //	var Message =  $($doc).find('Resp').attr('errInfo');
            //	var errorcode =	 $($doc).find('Resp').attr('errCode');
            //		if(errorcode==0)
            //		{
            //		     var aaa= "'"+$('input[name=adhaarnumber]').val()+"'"
            //                       var aad  =   aaa.replace(/ /g,'');
            //                       var ana   = aad.trim();
            //                       var myStr = ana.replace(/'/g, '');

            // /*
            //                     var publicKey   = 'MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC+SJMkWLJ/NiKL6NRsIsjvdzyu\navEnbv+WzsHENko5AFGshfAbmjp19AJ/iaW0Jg1xu0XeEigT4UtnkTEuho8lEgRy\nULltedWgUprEGIwIHnAbJ1GJZCe3NtasaaleOPU67UkkQ9fKGXMujiCUTq1dTnd7\ntOosAeWrPpnOnx6gyQIDAQAB\n';
            //                     var params      = {'data':xmlString};
            //                     var encryptData = encryptPublicLong(JSON.stringify(params), publicKey);
            // */

            //			$.ajax({
            //			type: 'POST',
            //		    url: ""+baseURL+"/aeps-request-balance-enquiry",
            //			data: { httpStaus: httpStaus, data: xmlString,                   //
            //				merchantusername : $("input[name=merchantusername]").val(),
            //				adhaarnumber : myStr,
            //				mobilenumber : $("input[name=mobilenumber]").val(),
            //				requestremarks : $("input[name=requestremarks]").val(),
            //				indicatorforuid : $("input[name=indicatorforuid]").val(),
            //				nationalbankidentification : $("input[name=nationalbankidentification]").val(),
            //				language : $("input[name=language]").val(),
            //				latitude : $("input[name=latitude]").val(),
            //				longitude : $("input[name=longitude]").val(),
            //				paymentType : $("input[name=paymentType]").val(),
            //				timestamp : $("input[name=timestamp]").val(),
            //				transcationtype : $("input[name=transcationtype]").val(),
            //				merchantpin : $("input[name=merchantpin]").val(),
            //				submerchantid : $("input[name=submerchantid]").val(),
            //				merchantTranId : $("input[name=merchantTranId]").val(),
            //				devicetype : devicetype,
            //			},
            //			crossDomain: true,
            //			success: function(data){
            //			     var read =	jQuery.parseJSON(data);
            // 					     if(read.status == true)
            // 						    {
            // 						            $('#loaderbala').css("display","none");
            // 							        $("#sign").modal("toggle");
            //                                     $('#balanceamount').val(read.balanceamount);
            //                                     $('#transunicode').val(read.txnid);
            //                                     $("#balance").html("Your Account balance is <br><strong>Rs. "+read.balanceamount+" </strong>");
            // 							        $('#newscan').trigger("reset");
            // 				                    $('#datasubmit').attr('disabled', 'disabled');
            // 							}else{
            // 							    //debugger;
            // 							    $('#loaderbala').css("display","none");
            // 							        alert('Response:' + read.message);
            // 							        window.location.reload();
            // 							        $('#newscan').trigger("reset");
            // 				                    $('#datasubmit').attr('disabled', 'disabled');
            // 							}
            //			  }
            //			});
            //		}else{
            //		    //debugger;
            //		    $('#loaderbala').css("display","none");
            //		    alert('Capture Failed');
            //		    window.location.reload();
            //	        //$('#newscan').trigger("reset");
            //	        //$('#datasubmit').attr('disabled', 'disabled');
            //		}
        },
        error: function (jqXHR, ajaxOptions, thrownError) {
            //debugger;
            res = { httpStaus: httpStaus, err: getHttpError(jqXHR) };
            window.location.reload();
        },
    });

    return res;
}

/*Aadhar Pay call function*/

function aadharalert() {

    alert('Please put finger on your device!');
    Aadharpayupi();
}

function Aadharpayupi() {

    var devicetype = false;
    var run = $("input[name='capdata']:checked").val();
    if (run == "iris") {
        var devicetype = true;
        var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="0" fType="2" iCount="1" pCount="0" format="0" pidVer="2.0" timeout="10000" posh="UNKNOWN" env="P" /> ' + DemoFinalString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
    } else {
        var devicetype = false;

        if ($("#txtWadh").val().trim() != "") {
            var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="1" fType="2" iCount="0" pCount="0" format="0" pidVer="2.0" timeout="10000" wadh="' + $("#txtWadh").val() + '" posh="UNKNOWN" env="P" /> ' + DemoFinalString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
        }
        else {
            var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="1" fType="2" iCount="0" pCount="0" format="0" pidVer="2.0" timeout="10000" posh="UNKNOWN" env="P" /> ' + DemoFinalString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
        }
    }

    $('#loaderbala').css("display", "block");
    var finUrl = $('#method').val();
    var verb = "CAPTURE";
    var err = "";
    var res;
    $.support.cors = true;
    var httpStaus = false;
    var jsonstr = "";
    $.ajax({
        type: "CAPTURE",
        crossDomain: true,
        url: finUrl,
        data: XML,
        contentType: "text/xml; charset=utf-8",
        processData: false,
        success: function (data) {

            if (run == "morpho") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //morpho
            } else if (run == "mantra") {
                var xmlString = data;  //mantra
            } else if (run == "secugen") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //secugen
            } else if (run == "precision") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //precision
            } else if (run == "startek") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //startek
            } else if (run == "nextrd") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //next rd
            } else if (run == "iris") {
                var xmlString = data;  //mantra
            }
            httpStaus = true;
            res = { httpStaus: httpStaus, data: xmlString };

            $('#txtPidData').val(xmlString);
            var $doc = data;//$.parseXML(data);
            var Message = $($doc).find('Resp').attr('errInfo');
            var errorcode = $($doc).find('Resp').attr('errCode');
            if (errorcode == 0) {
                var aaa = "'" + $('input[name=adhaarnumber]').val() + "'"
                var aad = aaa.replace(/ /g, '');
                var ana = aad.trim();
                var myStr = ana.replace(/'/g, '');
                $.ajax({
                    type: 'POST',
                    url: "" + baseURL + "/aeps-request-aadharpay",
                    data: {
                        httpStaus: httpStaus, data: xmlString,
                        merchantusername: $("input[name=merchantusername]").val(),
                        adhaarnumber: myStr,
                        mobilenumber: $("input[name=mobilenumber]").val(),
                        transcationamount: $("input[name=transcationamount]").val(),
                        requestremarks: $("input[name=requestremarks]").val(),
                        indicatorforuid: $("input[name=indicatorforuid]").val(),
                        nationalbankidentification: $("input[name=nationalbankidentification]").val(),
                        language: $("input[name=language]").val(),
                        latitude: $("input[name=latitude]").val(),
                        longitude: $("input[name=longitude]").val(),
                        paymentType: $("input[name=paymentType]").val(),
                        timestamp: $("input[name=timestamp]").val(),
                        transcationtype: $("input[name=transcationtype]").val(),
                        merchantpin: $("input[name=merchantpin]").val(),
                        submerchantid: $("input[name=submerchantid]").val(),
                        merchantTranId: $("input[name=merchantTranId]").val(),
                        orderid: $("input[name=orderid]").val(),
                        invoiceno: $("input[name=invoiceno]").val(),
                        devicetype: devicetype
                    },
                    crossDomain: true,
                    success: function (data) {

                        var read = jQuery.parseJSON(data);
                        if (read.status == true) {
                            $('#loaderbala').css("display", "none");
                            //alert('Response:' + read.message);
                            $("#sign").modal("toggle");
                            $('#balanceamount').val(read.balanceamount);
                            $('#transcationamount').val(read.amount);
                            $('#transunicode').val(read.txnid);
                            $("#balance").html("An amount of <br><strong>Rs. " + read.amount + " </strong><br>has been withdrawn from your bank account.");
                            //  $(".modal-body").html(read.message);
                            $('#newscan').trigger("reset");
                            $('#datasubmit').attr('disabled', 'disabled');
                        } else {
                            $('#loaderbala').css("display", "none");
                            alert('Response:' + read.message);
                            var url = "" + baseURL + "/customer-hisaab";
                            window.location.href = url;
                            // window.location.reload();
                            // $('#newscan').trigger("reset");
                            //$('#datasubmit').attr('disabled', 'disabled');
                        }
                    }
                });
            } else {
                $('#loaderbala').css("display", "none");
                alert('Capture Failed');
                var url = "" + baseURL + "/customer-hisaab";
                window.location.href = url;
                //window.location.reload();
                //$('#newscan').trigger("reset");
                //$('#datasubmit').attr('disabled', 'disabled');
            }
        },
        error: function (jqXHR, ajaxOptions, thrownError) {
            res = { httpStaus: httpStaus, err: getHttpError(jqXHR) };
        },
    });

    return res;
}



/* cash deposit function*/

function depositalert() {

    alert('Please put finger on your device!');
    CashDeposit();
}

function CashDeposit() {

    var devicetype = false;
    var run = $("input[name='capdata']:checked").val();
    if (run == "iris") {
        var devicetype = true;
        var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="0" fType="2" iCount="1" pCount="0" format="0" pidVer="2.0" timeout="10000" posh="UNKNOWN" env="P" /> ' + DemoFinalString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
    } else {
        var devicetype = false;

        if ($("#txtWadh").val().trim() != "") {
            var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="1" fType="2" iCount="0" pCount="0" format="0" pidVer="2.0" timeout="10000" wadh="' + $("#txtWadh").val() + '" posh="UNKNOWN" env="P" /> ' + DemoFinalString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
        }
        else {
            var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="1" fType="2" iCount="0" pCount="0" format="0" pidVer="2.0" timeout="10000" posh="UNKNOWN" env="P" /> ' + DemoFinalString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
        }
    }

    $('#loaderbala').css("display", "block");
    var finUrl = $('#method').val();
    var verb = "CAPTURE";
    var err = "";
    var res;
    $.support.cors = true;
    var httpStaus = false;
    var jsonstr = "";
    $.ajax({
        type: "CAPTURE",
        crossDomain: true,
        url: finUrl,
        data: XML,
        contentType: "text/xml; charset=utf-8",
        processData: false,
        success: function (data) {
            if (run == "morpho") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //morpho
            } else if (run == "mantra") {
                var xmlString = data;  //mantra
            } else if (run == "secugen") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //secugen
            } else if (run == "precision") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //precision
            } else if (run == "startek") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //startek
            } else if (run == "nextrd") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //next rd
            } else if (run == "iris") {
                var xmlString = data;  //mantra
            }
            httpStaus = true;
            res = { httpStaus: httpStaus, data: xmlString };

            $('#txtPidData').val(xmlString);
            var $doc = data;//$.parseXML(data);
            var Message = $($doc).find('Resp').attr('errInfo');
            var errorcode = $($doc).find('Resp').attr('errCode');
            if (errorcode == 0) {
                //debugger; 
                var aaa = "'" + $('input[name=adhaarnumber]').val() + "'"
                var aad = aaa.replace(/ /g, '');
                var ana = aad.trim();
                var myStr = ana.replace(/'/g, '');
                $.ajax({
                    type: 'POST',
                    url: "" + baseURL + "/aeps-request-cashdeposit",
                    data: {
                        httpStaus: httpStaus, data: xmlString,
                        merchantusername: $("input[name=merchantusername]").val(),
                        adhaarnumber: myStr,
                        mobilenumber: $("input[name=mobilenumber]").val(),
                        transcationamount: $("input[name=transcationamount]").val(),
                        requestremarks: $("input[name=requestremarks]").val(),
                        indicatorforuid: $("input[name=indicatorforuid]").val(),
                        nationalbankidentification: $("input[name=nationalbankidentification]").val(),
                        language: $("input[name=language]").val(),
                        latitude: $("input[name=latitude]").val(),
                        longitude: $("input[name=longitude]").val(),
                        paymentType: $("input[name=paymentType]").val(),
                        timestamp: $("input[name=timestamp]").val(),
                        transcationtype: $("input[name=transcationtype]").val(),
                        merchantpin: $("input[name=merchantpin]").val(),
                        submerchantid: $("input[name=submerchantid]").val(),
                        merchantTranId: $("input[name=merchantTranId]").val(),
                        devicetype: devicetype
                    },
                    crossDomain: true,
                    success: function (data) {
                        var read = jQuery.parseJSON(data);
                        //debugger;
                        console.log(data);

                        if (read.status == true) {
                            $('#loaderbala').css("display", "none");
                            //alert('Response:' + read.message);
                            $("#sign").modal("toggle");
                            $('#balanceamount').val(read.balanceamount);
                            $('#transcationamount').val(read.amount);
                            $('#transunicode').val(read.txnid);
                            $("#balance").html("An amount of <br><strong>Rs. " + read.amount + " </strong><br>has been deposited to your bank account.");
                            //  $(".modal-body").html(read.message);
                            $('#newscan').trigger("reset");
                            $('#datasubmit').attr('disabled', 'disabled');
                        } else {
                            $('#loaderbala').css("display", "none");
                            alert('Response:' + read.message);
                            window.location.reload();
                            // $('#newscan').trigger("reset");
                            //$('#datasubmit').attr('disabled', 'disabled');
                        }

                    }
                });
            } else {
                $('#loaderbala').css("display", "none");
                alert('Capture Failed');
                window.location.reload();
                //$('#newscan').trigger("reset");
                //$('#datasubmit').attr('disabled', 'disabled');
            }
        },
        error: function (jqXHR, ajaxOptions, thrownError) {
            res = { httpStaus: httpStaus, err: getHttpError(jqXHR) };
        },
    });

    return res;
}



/* Statement Enquiry call function*/


function Balancestatement() {

    var devicetype = false;
    var run = $("input[name='capdata']:checked").val();
    if (run == "iris") {
        var devicetype = true;
        var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="0" fType="2" iCount="1" pCount="0" format="0" pidVer="2.0" timeout="10000" posh="UNKNOWN" env="P" /> ' + DemoFinalString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
    } else {
        var devicetype = false;

        if ($("#txtWadh").val().trim() != "") {
            var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="1" fType="2" iCount="0" pCount="0" format="0" pidVer="2.0" timeout="10000" wadh="' + $("#txtWadh").val() + '" posh="UNKNOWN" env="P" /> ' + DemoFinalString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
        }
        else {
            var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="1" fType="2" iCount="0" pCount="0" format="0" pidVer="2.0" timeout="10000" posh="UNKNOWN" env="P" /> ' + DemoFinalString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
        }
    }
    $('#loaderbala').css("display", "block");
    var finUrl = $('#method').val();
    var verb = "CAPTURE";
    var err = "";
    var res;
    $.support.cors = true;
    var httpStaus = false;
    var baseURL = $('head base').attr('href');
    var jsonstr = "";
    $.ajax({
        type: "CAPTURE",
        async: false,
        crossDomain: true,
        url: finUrl,
        data: XML,
        contentType: "text/xml; charset=utf-8",
        processData: false,
        success: function (data) {
            // debugger;
            if (run == "morpho") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //morpho
            } else if (run == "mantra") {
                var xmlString = data;  //mantra
            } else if (run == "secugen") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //secugen
            } else if (run == "precision") {
                var xmlString = (new XMLSerializer()).serializeToString(data);
            } else if (run == "startek") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //startek
            } else if (run == "nextrd") {
                var xmlString = (new XMLSerializer()).serializeToString(data);  //startek
            } else if (run == "iris") {
                var xmlString = data;  //IRIS
            }

            httpStaus = true;
            res = { httpStaus: httpStaus, data: xmlString };
            //$('#txtPidData').val(xmlString);                                  
            var $doc = data;//$.parseXML(data);         
            var Message = $($doc).find('Resp').attr('errInfo');
            var errorcode = $($doc).find('Resp').attr('errCode');
            if (errorcode == 0) {
                var aaa = "'" + $('input[name=adhaarnumber]').val() + "'"
                var aad = aaa.replace(/ /g, '');
                var ana = aad.trim();
                var myStr = ana.replace(/'/g, '');

                /*  
                var publicKey   = 'MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC+SJMkWLJ/NiKL6NRsIsjvdzyu\navEnbv+WzsHENko5AFGshfAbmjp19AJ/iaW0Jg1xu0XeEigT4UtnkTEuho8lEgRy\nULltedWgUprEGIwIHnAbJ1GJZCe3NtasaaleOPU67UkkQ9fKGXMujiCUTq1dTnd7\ntOosAeWrPpnOnx6gyQIDAQAB\n';
                var params      = {'data':xmlString};
                var encryptData = encryptPublicLong(JSON.stringify(params), publicKey);
                 */

                $.ajax({
                    type: 'POST',
                    url: "" + baseURL + "/aeps-request-balance-statement",
                    data: {
                        httpStaus: httpStaus, data: xmlString,                   //
                        merchantusername: $("input[name=merchantusername]").val(),
                        adhaarnumber: myStr,
                        mobilenumber: $("input[name=mobilenumber]").val(),
                        requestremarks: $("input[name=requestremarks]").val(),
                        indicatorforuid: $("input[name=indicatorforuid]").val(),
                        nationalbankidentification: $("input[name=nationalbankidentification]").val(),
                        language: $("input[name=language]").val(),
                        latitude: $("input[name=latitude]").val(),
                        longitude: $("input[name=longitude]").val(),
                        paymentType: $("input[name=paymentType]").val(),
                        timestamp: $("input[name=timestamp]").val(),
                        transcationtype: $("input[name=transcationtype]").val(),
                        merchantpin: $("input[name=merchantpin]").val(),
                        submerchantid: $("input[name=submerchantid]").val(),
                        merchantTranId: $("input[name=merchantTranId]").val(),
                        devicetype: devicetype
                    },
                    crossDomain: true,
                    success: function (data) {
                        debugger;
                        var read = jQuery.parseJSON(data);
                        if (read.status == true) {

                            $('#loaderbala').css("display", "none");
                            window.location = '/aepsstatementprint/?responsedata=' + read.data;
                        } else {
                            $('#loaderbala').css("display", "none");
                            alert('Response:' + read.message);
                            $('#newscan').trigger("reset");
                            $('#datasubmit').attr('disabled', 'disabled');
                        }
                    }
                });
            } else {

                $('#loaderbala').css("display", "none");
                alert('Capture Failed');
                window.location.reload();

            }
        },
        error: function (jqXHR, ajaxOptions, thrownError) {
            //debugger;
            res = { httpStaus: httpStaus, err: getHttpError(jqXHR) };
            window.location.reload();
        },
    });

    return res;
}

function balancestatementalert() {
    alert('Please put finger on your device!');
    Balancestatement();
}




function getHttpError(jqXHR) {
    var err = "Unhandled Exception";
    if (jqXHR.status === 0) {
        err = 'Service Unavailable';
    } else if (jqXHR.status == 404) {
        err = 'Requested page not found';
    } else if (jqXHR.status == 500) {
        err = 'Internal Server Error';
    } else if (thrownError === 'parsererror') {
        err = 'Requested JSON parse failed';
    } else if (thrownError === 'timeout') {
        err = 'Time out error';
    } else if (thrownError === 'abort') {
        err = 'Ajax request aborted';
    } else {
        err = 'Unhandled Error';
    }
    return err;
}



