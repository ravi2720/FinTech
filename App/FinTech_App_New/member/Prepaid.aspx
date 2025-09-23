<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="Prepaid.aspx.cs" Inherits="Member_Prepaid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .light {
            padding: 20px;
            border-radius: 16px;
            -moz-border-radius: 16px;
            -webkit-border-radius: 16px;
            -webkit-box-shadow: 0 0 8px rgb(0 0 0 / 5%);
            box-shadow: 0 0 8px rgb(0 0 0 / 5%);
            max-width: 1600px;
        }

        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: Arial, Helvetica, sans-serif;
        }

        .bg-gray {
            background-color: rgb(247, 247, 247);
        }

        .paymengt-main-page-sec {
            width: 100%;
            height: 250px;
            background-color: #263159;
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
            .loader-overlay {
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                background: rgba(255,255,255,0.7);
                display: flex;
                align-items: center;
                justify-content: center;
                z-index: 9999;
            }

            .loader {
                border: 6px solid #f3f3f3;
                border-top: 6px solid #e62e2e;
                border-radius: 50%;
                width: 50px;
                height: 50px;
                animation: spin 1s linear infinite;
            }

            @keyframes spin {
                0% { transform: rotate(0deg); }
                100% { transform: rotate(360deg); }
            }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid" id="prepaid">
        <!-- Loader -->
        <div v-if="loading" class="loader-overlay">
            <div class="loader"></div>
        </div>
        <!-- main-content-body -->
        <div class="main-content-body">
            <div class="row row-sm">
                <div class="col-lg-12">
                    <asp:UpdatePanel runat="server" ID="uprecharge">
                        <ContentTemplate>
                            <section class="paymengt-main-page-sec">
                                <div class="container-fluid">
                                    <div class="row py-md-2">
                                        <div class="col-12 pt-md-2">
                                            <ul class="list-unstyled list-inline payment-logo-icon">
                                                <asp:Repeater runat="server" ID="rptServiceData">
                                                    <ItemTemplate>
                                                        <li class="list-inline-item">
                                                            <a href='<%# Eval("URL") %>'>
                                                                <img src='<%# "./images/icon/"+Eval("Image") %>' alt="postpaid" class="img-fluid">
                                                                <span><%# Eval("Name") %></span>
                                                            </a>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="row g-3">
                                        <div class="col-md-4 col-12 bg-white rounded  px-md-4 py-md-5 p-3 shadow">
                                            <div>
                                                <p class="fw-bold">Prepaid Mobile Recharge</p>
                                                <div class="row">
                                                    <div class="col-12 py-md-2 py-2">
                                                        <div class="form-floating mb-3">
                                                            <input type="number" runat="server" clinetidmode="static" v-model="Number" class="form-control ValDataRecharge" clientidmode="static" id="txtMobileNo" tooltip="Please Enter Mobile Number" placeholder="Please Enter Mobile Number*" maxlength="10" />
                                                            <label for="txtMobileNo">Enter mobile number:</label>
                                                        </div>
                                                        <asp:HiddenField ID="hfCircleID" runat="server" ClientIDMode="Static" />
                                                        <div class="mb-3">
                                                            <asp:DropDownList runat="server" ID="ddlOperator" aria-label="Default select example" ClientIDMode="Static" CssClass="form-select form-control-lg">
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="form-floating mb-3">

                                                            <asp:TextBox runat="server" v-model="Amt" autocomplete="off" EnableViewState="false" class="form-control ValDataRecharge" ClientIDMode="Static" ID="txtAmount" placeholder="Amount" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" MaxLength="4"></asp:TextBox>
                                                            <label for="txtAmount">
                                                                Enter Amount ( ₹ )
                                                            :
                                                            </label>
                                                        </div>
                                                        <div class="d-grid mt-md-5 mt-3">
                                                            <asp:Button runat="server" ID="btnRecharge" name="btnRecharge" OnClientClick="return CheckValiadte('.ValDataRecharge')" OnClick="btnRecharge_Click" class="btn btn-primary btn-lg" Text="Proceed to Pay Bill" />
                                                        </div>
                                                        <div>                                                            
                                                            <asp:Label ID="lblMessage" runat="server" CssClass="d-block mt-2" ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-8 col-12 d-flex  ">
                                            <div class="bg-white rounded  p-md-4 p-3 w-100 shadow">
                                                <div class="">
                                                    <p class="fw-bold ">Browse Plan</p>
                                                    <hr>
                                                </div>
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="tabs">
                                                            <div class="tabs-background">
                                                                <ul id="tabs-nav">
                                                                    <li v-for="(item,index) in PlanDataType" data-bs-toggle="tab"><a v-bind:href="'#'+item.replace(' ','').replace('/','').replace(/[0-9]/g, '')">{{item}}</a></li>
                                                                </ul>
                                                            </div>
                                                            <!-- END tabs-nav -->
                                                            <div id="tabs-content">
                                                                <div class="">
                                                                    <table class="table mb-0">
                                                                        <thead>
                                                                            <td>Validity</td>
                                                                            <td class="description">Description</td>
                                                                            <td class="price">Price</td>
                                                                        </thead>
                                                                    </table>
                                                                </div>
                                                                <div id="tab1" v-for="(item,index) in PlanDataType" 
                                                                     v-bind:id="item.replace(' ','').replace('/','').replace(/[0-9]/g, '')" 
                                                                     class="tab-content">
                                                                    <table class="table table-hover">
                                                                        <tr v-for="itemn in PlanData.records[item]">
                                                                            <td>{{itemn.validity}}</td>
                                                                            <td class="description">{{itemn.desc}}</td>
                                                                            <td class="price">
                                                                                <a href="#" class="btn btn-primary-outline btn-sm" 
                                                                                   v-on:click="GetAmt(itemn.rs,itemn.desc)">
                                                                                   ₹. {{itemn.rs}}
                                                                                </a>
                                                                            </td>
                                                                        </tr>
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
                                </div>
                            </section>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>

    <script>
        new Vue({
            el: '#prepaid',
            data() {
                return {
                    PlanData: [],
                    PlanDataType: [],
                    Number: "",
                    Amt: 0,
                    Com: 0,
                    Plan: "",
                    CircleID: "",
                    loading: false
                }
            },
            methods: {
                GetPrepaidVal: function () {
                    if ($("#txtMobileNo").val().length == 10) {
                        var el = this;
                        el.Amt = 0;
                        el.Plan = "";
                        el.loading = true; // show loader
                        const article = {
                            "searchTerm": $("#txtMobileNo").val()
                        }
                        axios.post("PrePaid.aspx/GetPrepaid", article)
                            .then(response => {
                                var str = response.data.d.split(',');
                                $("#ddlOperator").val(str[0]);
                                $('#ddlOperator').trigger("chosen:updated");
                                el.CircleID = str[1];
                                document.getElementById("hfCircleID").value = el.CircleID;
                                el.GetPlan();
                            })
                            .catch(() => {
                                el.loading = false;
                            });
                    }
                },
                GetPlan: function () {
                    debugger
                    var el = this;
                    const article = {
                        "operatorId": $("#ddlOperator option:selected").val(),
                        "circle": el.CircleID
                    }
                    axios.post("PrePaid.aspx/GetPrePaidPlan", article)
                        .then(response => {
                            var data = JSON.parse(response.data.d);

                            // transform array into dictionary for Vue template
                            var recordsDict = {};
                            data.records.forEach(r => {
                                recordsDict[r.planType] = r.plans;
                            });

                            el.PlanData = { records: recordsDict };
                            el.PlanDataType = Object.keys(el.PlanData.records);
                        })
                        .finally(() => {
                            el.loading = false; // hide loader
                        });
                },
                GetAmt: function (Amt, Plan) {
                    var el = this;
                    el.Amt = Amt;
                    el.Plan = Plan;
                    alertify.success("Amount Applied")

                    el.GetCommission();
                },
                GetCommission: function () {
                    var el = this;
                    const article = {
                        "Amt": el.Amt,
                        "Op": $("#ddlOperator option:selected").val()
                    }
                    axios.post("PrePaid.aspx/GetCommission", article)
                        .then(response => {
                            el.Com = response.data.d;
                        });
                }
            },
            mounted() {

            },
        });

        
    </script>
</asp:Content>

