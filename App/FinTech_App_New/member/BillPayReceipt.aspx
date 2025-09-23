<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="BillPayReceipt.aspx.cs" Inherits="Member_BillPayReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="./css/receiptcss.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid" id="prepaid">
        <!-- main-content-body -->
        <div class="main-content-body">
            <div class="row row-sm">
                <div class="col-lg-12" v-for="item in BBPSData">
                    <div class="outer_05">
                        <div class="outer_last">
                            <div class="last_content">
                                <img v-bind:src="(item.Status=='Success'?'./images/icon/verified.gif':'')" alt="sucess">
                                <p style="font-size: 25px;" class="text_center">Congratulations!</p>
                                <h1 class="text_center">{{item.APIErrorCode}}</h1>
                                <h2 class="text_center pad_10">Transaction details are here :-</h2>
                                <div class="table_05" id="dvContents">
                                    <table id="print">
                                        <tr>
                                            <th>Recharge Type</th>
                                            <th>{{item.ServiceName}}</th>
                                        </tr>
                                        <tr>
                                            <th>Operator Name</th>
                                            <th>{{item.OperatorName}}</th>
                                        </tr>
                                        <tr>
                                            <th>Mobile No</th>
                                            <th>{{item.MobileNo}}</th>
                                        </tr>
                                        <tr v-if="item.BillFetchResult!=''">
                                            <td>Consumer Name</td>
                                            <td>{{JSON.parse(item.BillFetchResult).Name}}</td>
                                        </tr>
                                        <tr v-if="item.BillFetchResult!=''">
                                            <td>Bill Due Date</td>
                                            <td>{{JSON.parse(item.BillFetchResult).DueDate}}</td>
                                        </tr>
                                        <tr v-if="item.BillFetchResult!=''">
                                            <td>BillDate</td>
                                            <td>{{JSON.parse(item.BillFetchResult).BillDate}}</td>
                                        </tr>
                                        <tr>
                                            <th>Amount</th>
                                            <th>Rs. {{item.Amount}}</th>
                                        </tr>
                                        <tr>
                                            <th>Txn ID</th>
                                            <th>{{item.TransID}}</th>
                                        </tr>
                                        <tr>
                                            <th>Operator Ref No</th>
                                            <th>{{item.APIMessage}}</th>
                                        </tr>
                                        <tr>
                                            <th>Date</th>
                                            <th>{{item.CreatedDate}}</th>
                                        </tr>
                                        <tr>
                                            <th>Status</th>
                                            <th>{{item.Status}}</th>
                                        </tr>

                                    </table>
                                </div>
                                <div class="btn_05">
                                    <input type="button" value="Print" onclick="PrintTable();" class="btn btn-danger" />
                                    <a href="Prepaid.aspx" class="btn">Go Back</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        new Vue({
            el: '#prepaid',
            data() {
                return {
                    BBPSData: []
                }
            },
            methods: {
                GetVal: function () {
                    var vars = [], hash;
                    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                    for (var i = 0; i < hashes.length; i++) {
                        hash = hashes[i].split('=');
                        vars.push(hash[0]);
                        vars[hash[0]] = hash[1];
                    }
                    return vars;
                },
                BBPSDataF: function () {
                    var el = this;
                    const article = {
                        "MethodName": "BillData",
                        "TransID": el.GetVal()["TransID"]
                    }
                    axios.post("BillPayReceipt.aspx/BillData", article)
                        .then(response => {
                            debugger;
                            el.BBPSData = JSON.parse(response.data.d)
                        });

                }
            },
            mounted() {
                var el = this;
                el.BBPSDataF();
            }
        });




        function PrintTable() {
            var element = document.getElementById('dvContents');
            html2pdf(element, {
                margin: 1,
                padding: 0,
                filename: 'myfile.pdf',
                image: { type: 'jpeg', quality: 1 },
                html2canvas: { scale: 2, logging: true },
                jsPDF: { unit: 'in', format: 'A2', orientation: 'P' },
                class: createPDF
            });
        }
    </script>
</asp:Content>

