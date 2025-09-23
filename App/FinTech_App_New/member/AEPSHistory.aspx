<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="AEPSHistory.aspx.cs" Inherits="Member_AEPSHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/receiptcss.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid" id="hisAeps">
        <div class="main-content-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row row-sm collapse" id="collapseExample">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label>Search</label>
                                            <asp:TextBox runat="server" ID="txtSeach" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2">
                                            <label>From Date</label>
                                            <asp:TextBox runat="server" TextMode="Date" ID="txtFromDate" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="col-md-2">
                                            <label>To Date</label>
                                            <asp:TextBox runat="server" TextMode="Date" ID="txtToDate" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="col-md-2">
                                            <label>Status</label>
                                            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                                <asp:ListItem Text="Select Status" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Success" Value="Success"></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                <asp:ListItem Text="Failed" Value="Failed"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <label for="ddlMember">Member ID : </label>
                                            <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member." class="form-control select2"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <label></label>
                                            <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Submit" CssClass="form-control"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="row row-sm">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-header">
                                    <h3 class="card-title">Manage AEPS
                                <a href="#collapseExample" class="font-weight-bold" aria-controls="collapseExample" aria-expanded="false" data-bs-toggle="collapse" role="button">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" class="bi bi-filter" viewBox="0 0 16 16">
                                        <path d="M6 10.5a.5.5 0 0 1 .5-.5h3a.5.5 0 0 1 0 1h-3a.5.5 0 0 1-.5-.5zm-2-3a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7a.5.5 0 0 1-.5-.5zm-2-3a.5.5 0 0 1 .5-.5h11a.5.5 0 0 1 0 1h-11a.5.5 0 0 1-.5-.5z" />
                                    </svg>
                                </a>
                                    </h3>
                                </div>
                                <div class="card-body">

                                    <div class="table-responsive">
                                        <table id="file-datatable" class="border-top-0  table table-bordered text-nowrap key-buttons border-bottom">
                                            <thead>


                                                <tr>
                                                    <th class="border-bottom-0">SNO</th>
                                                    <th class="border-bottom-0">Member Id</th>
                                                    <th class="border-bottom-0">Member Name</th>
                                                    <th class="border-bottom-0">AadharNumber</th>
                                                    <th class="border-bottom-0">Transaction Type</th>
                                                    <th class="border-bottom-0">Amount</th>
                                                    <th class="border-bottom-0">Bank TransID</th>
                                                    <th class="border-bottom-0">Status</th>
                                                    <th class="border-bottom-0">Transaction Date</th>
                                                    <th class="border-bottom-0">View Receipt</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Container.ItemIndex + 1 %></td>
                                                            <td><%# Eval("LOGINID") %></td>
                                                            <td><%# Eval("Name") %></td>
                                                            <td><%# Eval("adhaarnumber") %></td>
                                                            <td><%# Eval("transcationtype") %></td>
                                                            <td><%# Eval("Amount") %></td>
                                                            <td><%# Eval("bankrrn") %></td>
                                                            <td class='<%# (Eval("status").ToString().ToUpper()=="SUCCESS"?"btn btn-success":"btn btn-danger") %>'>
                                                                <%# Eval("Status") %>
                                                            </td>
                                                            <td><%# Eval("AddDate") %></td>
                                                            <td>
                                                                <%--<a href='AEPS_2_WalletInvoice.aspx?ID=<%# Eval("referenceno") %>' style="color: black;" target="_blank">View Receipt</a>--%>
                                                                <input type="button" style="color: red" data-bs-toggle="modal" data-bs-target="#recipt" v-on:click="BBPSDataF('<%# Eval("referenceno") %>')" value="View Receipt" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>

            <div class="modal upi-modal" id="recipt" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Receipt</h5>
                            <button aria-label="Close" class="btn-close" data-bs-dismiss="modal" type="button"><span aria-hidden="true">×</span></button>
                        </div>
                        <div class="modal-body print">
                            <div class="row row-sm">
                                <div class="col-lg-12" v-for="item in BBPSData">
                                    <div class="outer_05">
                                        <div class="outer_last">
                                            <div class="last_content">
                                                <img v-bind:src="(item.Status=='Success'?'./images/icon/verified.gif':'./images/icon/Error.gif')" alt="sucess">
                                                <h1 class="text_center">{{item.APIErrorCode}}</h1>
                                                <h2 class="text_center pad_10">Transaction details are here :-</h2>
                                                <div class="table_05" id="dvContents">
                                                    <table id="print">
                                                        <tr>
                                                            <th>Bank Name</th>
                                                            <th>{{item.bankname}}</th>
                                                        </tr>
                                                        <tr>
                                                            <th>BC Code</th>
                                                            <th>{{item.LOGINID}}</th>
                                                        </tr>
                                                        <tr>
                                                            <th>BC Name</th>
                                                            <th>{{item.NAME}}</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Customer Mobile</th>
                                                            <th>{{item.mobilenumber}}</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Avl Balance</th>
                                                            <th>₹ {{item.mobilenumber}}</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Aadhar No</th>
                                                            <th>{{item.adhaarnumber}}</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Transaction ID</th>
                                                            <th>{{item.referenceno}}</th>
                                                        </tr>
                                                        <tr>
                                                            <th>RRN</th>
                                                            <th>{{item.bankrrn}}</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Date</th>
                                                            <th>{{item.AddDate}}</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Remark</th>
                                                            <th>{{item.message}}</th>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <a href="#!" onclick="window.print()" class="btn">Print
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>


        var DataObject = {
            ID: "example1",
            iDisplayLength: 5,
            bPaginate: true,
            bFilter: true,
            bInfo: true,
            bLengthChange: true,
            searching: true
        };
        function LoadData() {

            // Action(DataObject);
        }

        var req = Sys.WebForms.PageRequestManager.getInstance();
        req.add_beginRequest(function () {

        });
        req.add_endRequest(function () {
            
            try {
                new Vue({
                    el: '#hisAeps',
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
                        BBPSDataF: function (ID) {
                            var el = this;
                            const article = {
                                "MethodName": "BillData",
                                "TransID": ID
                            }
                            axios.post("BillPayReceipt.aspx/AEPSData", article)
                                .then(response => {
                                    el.BBPSData = JSON.parse(response.data.d)
                                });

                        }
                    },
                    mounted() {

                    }
                });
            } catch (err) {

            }
        });
        function showModal() {
            $('#myModal').modal();
        }


    </script>
</asp:Content>

