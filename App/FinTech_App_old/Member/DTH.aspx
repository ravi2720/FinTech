<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="DTH.aspx.cs" Inherits="Member_DTH" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="main-container container-fluid" id="prepaid">
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
                                                                <img src='<%# "../images/icon/"+Eval("Image") %>' alt="postpaid" class="img-fluid">
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
                                                <p class="fw-bold">DTH Recharges</p>
                                                <div class="row">
                                                    <div class="col-12 py-md-2 py-2">
                                                        <asp:TextBox ID="txtNumber" runat="server" placeholder="Enter DTH Number" CssClass="form-control mb-2"></asp:TextBox>

                                                        <asp:DropDownList ID="ddlOperator" runat="server" CssClass="form-control mb-2"></asp:DropDownList>

                                                        <asp:TextBox ID="txtAmount" runat="server" placeholder="Enter Amount" CssClass="form-control mb-2"></asp:TextBox>

                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-control mb-2"></asp:DropDownList>

                                                        <asp:Button ID="btnRecharge" runat="server" CssClass="btn btn-primary w-100" Text="Recharge Now" OnClick="btnRecharge_Click" />

                                                        <asp:Label ID="lblMessage" runat="server" CssClass="d-block mt-2" ForeColor="Red"></asp:Label>
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
                    Plan: ""
                }
            },
            methods: {
                GetPrepaidVal: function () {
                    if ($("#txtMobileNo").val().length >= 10 || ($("#txtMobileNo").val().length <= 12 && $("#txtMobileNo").val().length >= 10)) {
                        document.getElementById('load').style.visibility = "visible";
                        var el = this;
                        el.Amt = 0;
                        el.Plan = "";
                        const article = {
                            "searchTerm": $("#txtMobileNo").val()
                        }
                        axios.post("DTH.aspx/DTHOpDetails", article)
                            .then(response => {
                                var str = JSON.parse(response.data.d.split(','));
                                var op = "";
                                if (str.records.status == 1) {
                                    if (str.records.Operator == "AirtelDth") {
                                        op = "17";
                                    }
                                    else if (str.records.Operator == "SunDirect") {
                                        op = "20";
                                    }
                                    else if (str.records.Operator == "DishTv") {
                                        op = "18";
                                    }
                                    else if (str.records.Operator == "Videocon") {
                                        op = "22";
                                    }
                                    else if (str.records.Operator == "Tata Sky") {
                                        op = "21";
                                    }

                                    $("#ddlOperator").val(op);
                                    $('#ddlOperator').trigger("chosen:updated");
                                    el.GetPlan($("#ddlOperator option:selected").val());
                                } else {
                                    document.getElementById('load').style.visibility = "hidden";

                                    alertify.error(str.records.desc);
                                }
                            }).catch(err => {
                                document.getElementById('load').style.visibility = "hidden";
                            });
                    }
                },
                GetPlan: function (ID) {
                    var el = this;
                    ID = $("#ddlOperator option:selected").val();
                    const article = {
                        "OperatorID": ID
                    }
                    axios.post("DTH.aspx/DTHPlan", article)
                        .then(response => {
                            el.PlanData = JSON.parse(response.data.d).records.Plan;
                            document.getElementById('load').style.visibility = "hidden";
                            $('#viewplans').modal('toggle');
                        }).catch(err => {
                            document.getElementById('load').style.visibility = "hidden";
                        });
                },
                GetCustomerDetails: function () {
                    document.getElementById('load').style.visibility = "visible";
                    var el = this;
                    const article = {
                        "searchTerm": $("#txtMobileNo").val(),
                        "OperatorID": $("#ddlOperator option:selected").val()
                    }
                    axios.post("DTH.aspx/DTHCustomerInfo", article)
                        .then(response => {
                            var str = JSON.parse(response.data.d);
                            if (str.status == 1) {
                                el.Customerinfo = str.records;
                                document.getElementById('load').style.visibility = "hidden";
                            } else {
                                alertify.error(str.records.desc);
                            }
                        }).catch(err => {
                            document.getElementById('load').style.visibility = "hidden";
                        });
                },
                GetAmt: function (Amt, Plan) {
                    var el = this;
                    el.Amt = Amt;
                    el.Plan = Plan;
                    alertify.success("Amount Applied")
                }
            },
            mounted() {

            },
        });


    </script>
</asp:Content>

