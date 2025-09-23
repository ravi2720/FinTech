<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="PagePermissionSetting.aspx.cs" Inherits="Admin_PagePermissionSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Component/PermissionToggle.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper" id="app">
        <div class="card-header">
            <select v-model="RoleID" class="form-control" @change="myEvent()">
    <option v-for="item in RoleData"  :value="item">{{item.Name}}</option>
</select>
        </div>
        <section class="content" v-for="item in MainData">
            <div class="container-fluid">
                <div class="card">
                    <div class="card-header"><strong>{{item.Name}}</strong> </div>
                    <div class="card-body">
                        <div class="row" v-for="itemT in JSON.parse(item.Permission)">
                            <div class="col-md-2">
                                <togglebtnpage coln="Active" v-bind:PageID="itemT.PageID" v-bind:RoleID="RoleID.ID"  title="Active" v-bind:check="itemT.Active"></togglebtnpage>
                            </div>
                            <div class="col-md-2">
                                <togglebtnpage title="Delete" v-bind:PageID="itemT.PageID" v-bind:RoleID="RoleID.ID"  coln="Delete" v-bind:check="itemT.Delete"></togglebtnpage>
                            </div>
                            <div class="col-md-2">
                                <togglebtnpage title="Edit" v-bind:PageID="itemT.PageID" v-bind:RoleID="RoleID.ID" coln="Edit" v-bind:check="itemT.Edit"></togglebtnpage>
                            </div>
                            <div class="col-md-2">
                                <togglebtnpage title="AddFund" v-bind:PageID="itemT.PageID" v-bind:RoleID="RoleID.ID" coln="AddFund" v-bind:check="itemT.AddFund"></togglebtnpage>
                            </div>
                            <div class="col-md-2">
                                <togglebtnpage title="DeductFund" v-bind:PageID="itemT.PageID" v-bind:RoleID="RoleID.ID" coln="DeductFund" v-bind:check="itemT.DeductFund"></togglebtnpage>
                            </div>
                            <div class="col-md-2">
                                <togglebtnpage title="Submit" v-bind:PageID="itemT.PageID" v-bind:RoleID="RoleID.ID" coln="Submit" v-bind:check="itemT.Submit"></togglebtnpage>
                            </div>
                            <div class="col-md-2">
                                <togglebtnpage title="CheckStatus" v-bind:PageID="itemT.PageID" v-bind:RoleID="RoleID.ID"  coln="CheckStatus" v-bind:check="itemT.CheckStatus"></togglebtnpage>
                            </div>
                            <div class="col-md-2">
                                <togglebtnpage title="ForceFailed" v-bind:PageID="itemT.PageID" v-bind:RoleID="RoleID.ID"  coln="ForceFailed" v-bind:check="itemT.ForceFailed"></togglebtnpage>
                            </div>
                            <div class="col-md-2">
                                <togglebtnpage title="ForceSuccess" v-bind:PageID="itemT.PageID" v-bind:RoleID="RoleID.ID"  coln="ForceSuccess" v-bind:check="itemT.ForceSuccess"></togglebtnpage>
                            </div>
                        </div>
                    </div>
                </div>
        </section>
    </div>
    <script>
        var app = new Vue({
            el: '#app',
            data() {
                return {
                    MainData: [],
                    RoleData: [],
                    RoleID:0
                }
            },
            methods: {
                init(ID) {
                    const article = {
                        "MethodName": "getdate",
                        "RoleID":ID
                    }
                    axios.post("PagePermissionSetting.aspx/GetPageList", article)
                        .then(response => {
                            debugger;
                            this.MainData = JSON.parse(response.data.d);
                        });// .then(function (response) { debugger; this.MainData =response.data.d; });
                },
                Role() {
                    const article = {
                        
                    }
                    axios.post("PagePermissionSetting.aspx/GetRoleData", article)
                        .then(response => {
                            this.RoleData = JSON.parse(response.data.d);
                        });// .then(function (response) { debugger; this.MainData =response.data.d; });
                },
                myEvent: function () {
                    this.init(this.RoleID.ID);
                }
            },
            mounted() {
                this.Role();
            },
        })
    </script>
</asp:Content>

