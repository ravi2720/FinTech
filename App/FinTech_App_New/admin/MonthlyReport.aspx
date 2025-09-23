<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="MonthlyReport.aspx.cs" Inherits="Admin_MonthlyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper" id="Report">
        <div>
            <div class="row">
                <div class="col-12">
                    <div class="row">
                        <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card">
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="card-title">Select Date</h4>
                                    <div class="d-flex justify-content-between">
                                        <p class="text-muted">
                                            <input type="month" v-model="Datex" v-on:change="init" class="form-control" />
                                        </p>
                                    </div>
                                    <div class="progress progress-md">
                                        <div class="progress-bar bg-success w-25" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="row">
                        <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card"  v-for="post in MainData">
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="card-title">{{post.Name}}({{post.FACTOR}})</h4>
                                    <div class="d-flex justify-content-between">

                                        <p class="text-muted">max: {{post.Amount}}</p>
                                        <%--<p class="progress-bar bg-danger w-50 card-title text-white">Month.: {{post.MonthName}}</p>--%>
                                    </div>
                                    <div class="progress progress-md">
                                        <div :class="[post.FACTOR.toUpperCase()=='CR' ? 'progress-bar bg-success w-25' : 'progress-bar bg-danger w-25']" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                    </div>
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
            el: '#Report',
            data() {
                return {
                    MainData: [],
                    MainDataCount: [],
                    Search: "",
                    Datex: ""
                }
            },
            methods: {
                Getdate() {
                    var today = new Date();
                    var dd = String(today.getDate()).padStart(2, '0');
                    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
                    var yyyy = today.getFullYear();

                    today = yyyy + '-' + mm + '-' + dd;
                    return today;
                },
                init() {
                    var el = this;
                    const article = {
                        "MethodName": "getflight",
                        "Data": el.Datex
                    }
                    axios.post("MonthlyReport.aspx/GetData", article)
                        .then(response => {
                            debugger;
                            this.MainData = JSON.parse(response.data.d);
                        });
                }
            },
            mounted() {
                this.init();
            },


        });
    </script>
</asp:Content>

