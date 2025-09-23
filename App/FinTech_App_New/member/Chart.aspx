<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="Chart.aspx.cs" Inherits="Member_Chart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!--- Internal Morris js --->
    <script src="../assets/plugins/raphael/raphael.min.js"></script>
    <script src="../assets/plugins/morris.js/morris.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid" id="appchart">
        <!-- main-content-body -->
        <div class="main-content-body">
            <div class="row row-sm">
                <div class="col-md-6">
                    <div class="card mg-b-md-20">
                        <div class="card-body">
                            <div class="main-content-label mg-b-5">
                                AEPS
                                <input type="month" v-on:change="initAEPS" v-model="AEPSMonth" class="form-control" />
                            </div>

                            <div class="morris-donut-wrapper-demo" id="morrisDonut1"></div>
                        </div>
                    </div>
                </div>
                <!-- col-6 -->
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-body">
                            <div class="main-content-label mg-b-5">
                                Recharge Chart                                
                            </div>
                            <div class="row row-sm">
                                <div class="col-md-6 text-center">
                                    <input type="month" v-on:change="init" v-model="Month" class="form-control" />
                                    <div class="morris-donut-wrapper-demo" id="morrisDonut2"></div>
                                    <strong>Month Wise</strong>
                                </div>
                                <div class="col-md-6 text-center">
                                    <input type="date" v-on:change="init" v-model="MonthDate" class="form-control" />
                                    <div class="morris-donut-wrapper-demo" id="morrisDonut2day"></div>
                                    <strong>Days Wise</strong>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- col-6 -->
            </div>
        </div>
    </div>
    <script>

        new Vue({
            el: '#appchart',
            data() {
                return {
                    MainData: [],
                    Month: "",
                    MonthDate: "",
                    AEPSMonth: ""
                }
            },
            methods: {
                init() {
                    var el = this;
                    const article = {
                        "Method": "RechargeChart",
                        "Month": el.Month,
                        "Date": el.MonthDate
                    }
                    axios.post("Chart.aspx/FindChartData", article)
                        .then(response => {
                            this.MainData = JSON.parse(response.data.d);
                            $("#morrisDonut2").empty()
                            new Morris.Donut({
                                element: 'morrisDonut2',
                                data: this.MainData.filter(function(d){return d.Type=='Month'}),
                                colors: ['#FF0000', '#285cf7', '#285cf7', '#FFA500'],
                                resize: true,
                                labelColor: "#8c9fc3"
                            });

                            $("#morrisDonut2day").empty()
                            new Morris.Donut({
                                element: 'morrisDonut2day',
                                data: this.MainData.filter(function(d){return d.Type=='Day'}),
                                colors: ['#FF0000', '#285cf7', '#285cf7', '#FFA500'],
                                resize: true,
                                labelColor: "#8c9fc3"
                            });
                        });
                },
                initAEPS() {
                    var el = this;
                    const article = {
                        "Method": "AEPSChart",
                        "Month": el.AEPSMonth
                    }
                    axios.post("Chart.aspx/FindChartData", article)
                        .then(response => {
                            $("#morrisDonut1").empty()
                            new Morris.Donut({
                                element: 'morrisDonut1',
                                data: JSON.parse(response.data.d),
                                colors: ['#FF0000', '#285cf7', '#285cf7', '#FFA500'],
                                resize: true,
                                labelColor: "#8c9fc3"
                            });
                        });
                }
            },
            mounted() {
                this.init();
                this.initAEPS();
            }
        });
    </script>

</asp:Content>

