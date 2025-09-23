

// use for change Text box Color when automatic fillup operator
function hlme(a) {
    a.setAttribute("style", "font-size:16px; background:#bfff7f; color:#000");

}
function dhlme(a) {
    a.setAttribute("style", "");
}
//--------------------------------------------------------------

// Operator plan for PrePaid Recharges
function GetPrepaid() {
    $.ajax({
        type: "POST",
        url: "index.aspx/GetPrepaid",
        data: '{searchTerm: "' + $("#txtMobileNo").val() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var str = response.d.split(',');
            $("#ddlOperator").val(str[0]);
            $('#ddlOperator').trigger("chosen:updated");
            $('#btnplan').click();
            //GetTariffPlansPrepaid();
        },
        failure: function (response) {
        },
        error: function (response) {
        }
    });
}
//----------------------------------------------------------------


// Operator plan for Postpaid Recharges
function GetPostpaid() {
    $.ajax({
        type: "POST",
        url: "Services.aspx/GetPostpaid",
        data: '{searchTerm: "' + $("#txtNumberPostpaid").val() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var str = response.d.split(',');
            $("#ddlOperatorPostpaid").val(str[0]);
            $("#ddlCirclePostpaid").val(str[1]);
            $('#ddlOperatorPostpaid').trigger("chosen:updated");
            $('#ddlCirclePostpaid').trigger("chosen:updated");
            GetTariffPlansPostpaid();
        },
        failure: function (response) {
        },
        error: function (response) {
        }
    });
}
//-------------------------------------------------------------------

// Get surcharge
function Getsurcharge(a, lbl) {
    $.ajax({
        type: "POST",
        url: "Services.aspx/GetSurcharge",
        data: '{searchTerm: "' + a + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var str = response.d.split(',');
            document.getElementById(lbl).innerHTML = str[0];
            if (str[0] != "")
                document.getElementById(lbl).setAttribute("style", "display:block;");
        },
        failure: function (response) {
        },
        error: function (response) {
        }
    });
}
//------------------------------------------------------------------


// Call Tariff Plan popup for fatching Prepaid Plans
function GetTariffPlansPrepaid() {
    var operId = $("#ddlOperator option:selected").val();
    if (operId != undefined) {

        $('#btnplan').click();
    }
    else {
        $("#ddlOperator").val(0);
    }
}
//-------------------------------------------------------------------------------------------------------------------------------------------------

// Call Tariff Plan popup for fatching Postpaid Plans
function GetTariffPlansPostpaid() {
    var operId = $("#ddlOperatorPostpaid option:selected").val();
    var cirId = $("#ddlCirclePostpaid option:selected").val();

    if ((operId != '0') && (cirId != '') && (operId != undefined)) {

        var str = "../../TariffPlan/PopupTariffPlan_Api_Cyrus.aspx?OperatorID=" + operId + "&CircleID=" + cirId + "&stp=postpaid";
        $('#PostpaidLink').css('display', 'block');
        $("#iframe_OperatorPlan").attr("src", str);

        // showWithoutPopup Call this function if you dont want popup and set Div ID where you show plans
        // showWithoutPopup(str);
    }
    else {
        $("#ddlOperatorPostpaid").val(0);
        $('#PostpaidLink').css('display', 'none');
        $("#iframe_OperatorPlan").attr("src", "");
    }
}
//-----------------------------------------------------------------------------------------------------------------------------------------------

// Call Tariff Plan popup for fatching Datacard Plans
function GetTariffPlansDataCard() {
    var operId = $("#ddlOperatorDatacard option:selected").val();
    var cirId = $("#ddlCircleDatacard option:selected").val();

    if ((operId != '0') && (cirId != '') && (operId != undefined)) {

        var str = "../../TariffPlan/PopupTariffPlan_Api_Cyrus.aspx?OperatorID=" + operId + "&CircleID=" + cirId + "&stp=dc";
        $('#DatacardLink').css('display', 'block');
        $("#iframe_OperatorPlan").attr("src", str);

        // showWithoutPopup Call this function if you dont want popup and set Div ID where you show plans
        // showWithoutPopup(str);

    }
    else {
        $("#ddlOperatorDatacard").val(0);
        $('#DatacardLink').css('display', 'none');
        $("#iframe_OperatorPlan").attr("src", "");
    }
}
function GetTariffPlanso(o, p) {
    var operId = p;
    var cirId = 19;
    if ((operId != '0')) {
        var str = "../../TariffPlan/PopupTariffPlan_Api_Cyrus.aspx?OperatorID=" + operId + "&CircleID=" + cirId + "&stp=dth";
        document.getElementById(o).style.display = 'block';
        $("#iframe_OperatorPlan").attr("src", str);

        // showWithoutPopup Call this function if you dont want popup and set Div ID where you show plans
        // showWithoutPopup(str);
    }
    else {
        document.getElementById(o).style.display = 'none';
        $("#iframe_OperatorPlan").attr("src", str);
    }
}

function showWithoutPopup(str) {
    document.getElementById('PlanDIV').innerHTML = '<iframe src=' + str + ' class="boxshadow" style="width: 100%;min-height: 300px;border: 0; background-color:#fff"></iframe>';
}
// Bill fetch for all payment services
function GetBillDetails(service_type, operatorid, number, canumber, amount, cycle, duedate, membertypeID) {
    var operatorid = $("#ddlElectricity option:selected").val();
    var canumber = $('#txtCANumber').val();
    var number = $('#txtCANumber').val();
    var amount = $('#txtAmountElectricity').val();
    if (amount == '')
        amount = 10;
    var cycle = $('#txtCycleElectricity').val();
    var duedate = $('#txtDateElectricity').val();
    if (operatorid != "" && amount != "" && canumber != "") {
        document.getElementById("btnOpenModal").click();
        var str = "../API/PopupBillFetch_Api.aspx?number=" + number + "&canumber=" + canumber + "&amount=" + amount + "&operatorid=" + operatorid + "&cycle=" + cycle + "&duedate=" + duedate + "&memberTypeID=" + membertypeID + "&servicetype=7";
        document.getElementById("IframeBillFetch").src = str;
    }
    else { alert('All field are mandatory'); }
}

function GetBillDetails_Gas(service_type, operatorid, number, canumber, amount, cycle, duedate, membertypeID) {

    var operatorid = $("#ddlGas option:selected").val();
    var canumber = $('#txtGasCANumber').val();
    var number = $('#txtGasCANumber').val();
    var amount = $('#txtAmountGas').val();
    if (amount == '')
        amount = 10;
    var cycle = $('#txtCycleGas').val();
    var duedate = $('#txtDateGas').val();
    if (operatorid != "" && amount != "" && canumber != "") {
        document.getElementById("btnOpenModal").click();
        var str = "../../TariffPlan/PopupBillFetch_Api.aspx?number=" + number + "&canumber=" + canumber + "&amount=" + amount + "&operatorid=" + operatorid + "&cycle=" + cycle + "&duedate=" + duedate + "&memberTypeID=" + membertypeID + "&servicetype=8";
        document.getElementById("IframeBillFetch").src = str;
    }
    else {
        alert('All field are mandatory');
    }
}
function GetBillDetails_Postpaid(service_type, operatorid, number, canumber, amount, cycle, duedate, membertypeID) {

    var operatorid = $("#ddlOperatorPostpaid option:selected").val();
    var canumber = $('#txtCANumberPostpaid').val();
    var number = $('#txtNumberPostpaid').val();
    var amount = $('#txtAmountPostpaid').val();
    if (amount == '')
        amount = 10;
    var cycle = $('#ddlCirclePostpaid option:selected').val();
    var duedate = $('#txtDatePostpaid').val();
    if (operatorid != "" && amount != "" && number != "" && cycle != "") {
        document.getElementById("btnOpenModal").click();
        var str = "../../TariffPlan/PopupBillFetch_Api.aspx?number=" + number + "&canumber=" + canumber + "&amount=" + amount + "&operatorid=" + operatorid + "&cycle=" + cycle + "&duedate=" + duedate + "&memberTypeID=" + membertypeID + "&servicetype=2";

        document.getElementById("IframeBillFetch").src = str;
    }
    else {
        alert('All field are mandatory');
    }
}
function GetBillDetails_Landline(service_type, operatorid, number, canumber, amount, cycle, duedate, membertypeID) {

    var operatorid = $("#ddlLandlineOperator option:selected").val();
    var canumber = $('#txtCANumberLandline').val();
    var number = $('#txtSTD').val() + $('#txtCustomerIDL').val();
    var amount = $('#txtAmountLandline').val();
    if (amount == '')
        amount = 10;
    var cycle = $('#ddlCircleLandline option:selected').val();
    var duedate = $('#txtDateLandline').val();
    if (operatorid != "" && amount != "" && number != "" && cycle != "" && canumber != "") {
        document.getElementById("btnOpenModal").click();
        var str = "../../TariffPlan/PopupBillFetch_Api.aspx?number=" + number + "&canumber=" + canumber + "&amount=" + amount + "&operatorid=" + operatorid + "&cycle=" + cycle + "&duedate=" + duedate + "&memberTypeID=" + membertypeID + "&servicetype=6";

        document.getElementById("IframeBillFetch").src = str;
    }
    else {
        alert('All field are mandatory');
    }
}
function GetBillDetails_Insurance(service_type, operatorid, number, canumber, amount, cycle, duedate, membertypeID) {
    var operatorid = $("#ddlInsurance option:selected").val();
    var canumber = $('#txtpolicynumber').val();
    var number = $('#txtpolicynumber').val();
    var amount = $('#txtInsuranceAmount').val();
    if (amount == '')
        amount = 10;
    var cycle = $('#ddlCircleInsurance option:selected').val();
    var duedate = $('#txtunsuranceDueDate').val();
    if (operatorid != "" && amount != "" && canumber != "") {
        document.getElementById("btnOpenModal").click();
        var str = "../../TariffPlan/PopupBillFetch_Api.aspx?number=" + number + "&canumber=" + canumber + "&amount=" + amount + "&operatorid=" + operatorid + "&cycle=" + cycle + "&duedate=" + duedate + "&memberTypeID=" + membertypeID + "&servicetype=9";

        document.getElementById("IframeBillFetch").src = str;
    }
    else { alert('All field are mandatory'); }
}
//----------------------------------------------------------------
