<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="UPIHistory.aspx.cs" Inherits="Member_UPIHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/receiptcss.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid" id="his">
        <div class="main-content-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row row-sm collapse" id="collapseExample">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">

                                        <div class="col-md-2">
                                            <label>From Date</label>
                                            <asp:TextBox runat="server" TextMode="Date" ID="txtfromdate" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="col-md-2">
                                            <label>To Date</label>
                                            <asp:TextBox runat="server" TextMode="Date" ID="txttodate" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="col-md-2">
                                            <label for="ddlMember">Member ID : </label>
                                            <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member." class="form-control select2"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <label></label>
                                            <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Search" CssClass="form-control"></asp:Button>
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
                                    <h3 class="card-title">UPI Transfer History
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
                                                    <th class="border-bottom-0">Check Status</th>
                                                    <th class="border-bottom-0">SNO</th>
                                                    <th class="border-bottom-0">Member Name</th>
                                                    <th class="border-bottom-0">AddDate</th>
                                                    <th class="border-bottom-0">Amount</th>
                                                    <th class="border-bottom-0">TransID</th>
                                                    <th class="border-bottom-0">Bank Number</th>
                                                    <th class="border-bottom-0">Bank Details</th>
                                                    <th class="border-bottom-0">Vendor Id</th>
                                                    <th class="border-bottom-0">Status</th>
                                                    <th class="border-bottom-0">Receipt</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="gvTransactionHistory" runat="server" OnItemCommand="gvTransactionHistory_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Button runat="server" ID="btnCheckStatus" CommandName="CheckStatus" CommandArgument='<%# Eval("OrderID") %>' Text="Check Status" Visible='<%# (Eval("status").ToString().ToUpper()=="PENDING"?true:false) %>' CssClass="btn btn-danger" />
                                                            </td>
                                                            <td><%# Container.ItemIndex + 1 %></td>
                                                            <td><%# Eval("MemberName") %><br />
                                                                <%# Eval("LOGINID") %></td>
                                                            <td><%# Eval("AddDate") %></td>
                                                            <td><%# Eval("FinalAmount") %></td>
                                                            <td><%# Eval("OrderID") %></td>
                                                            <td><%# Eval("Name") %></td>
                                                            <td><%# Eval("UPIID") %></td>

                                                            <td><%# Eval("VendorID") %></td>

                                                            <td>
                                                                <span class='<%#(Eval("Status").ToString().ToUpper()=="SUCCESS"?"btn btn-success":"btn btn-danger") %>'><%# (Eval("Status").ToString().ToUpper()=="SUCCESS"?"Sucess":Eval("Status").ToString()) %></span>
                                                                <asp:HiddenField runat="server" ID="hdnRefNumber" Value='<%# Eval("OrderID") %>' />
                                                            </td>
                                                            <td>
                                                                <input type="button" style="color: red" data-bs-toggle="modal" data-bs-target="#recipt" v-on:click="BBPSDataF('<%# Eval("OrderID") %>')" value="View Receipt" />
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
                            <h5 class="modal-title">UPI Transfer Receipt</h5>
                            <button aria-label="Close" class="btn-close" data-bs-dismiss="modal" type="button"><span aria-hidden="true">×</span></button>
                        </div>
                        <div class="modal-body print">
                            <div class="row row-sm">
                                <div class="col-lg-12" v-for="item in BBPSData">
                                    <div class="outer_05">
                                        <div class="outer_last">
                                            <div class="last_content">
                                                <img v-bind:src="(item.Status=='Success'?'./images/icon/verified.gif':'./images/icon/Error.gif')" alt="sucess">
                                                <h1 class="text_center">{{item.Status}}</h1>
                                                <h2 class="text_center pad_10">Transaction details are here :-</h2>
                                                <div class="table_05" id="dvContents">
                                                    <table id="print">
                                                        <tr>
                                                            <th>Service Type</th>
                                                            <th>UPI Transfer</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Name</th>
                                                            <th>{{item.Name}}</th>
                                                        </tr>
                                                        <tr>
                                                            <th>UPIID</th>
                                                            <th>{{item.UPIID}}</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Amount</th>
                                                            <th>Rs. {{item.amount}}</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Txn ID</th>
                                                            <th>{{item.OrderID}}</th>
                                                        </tr>
                                                        <tr>
                                                            <th>RRN</th>
                                                            <th>{{item.RRN}}</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Date</th>
                                                            <th>{{item.AddDate}}</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Status</th>
                                                            <th>{{item.Status}}</th>
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
            var table = $('#file-datatable').DataTable({
                buttons: ['copy', 'excel', 'pdf', 'colvis'],
                language: {
                    searchPlaceholder: 'Search...',
                    scrollX: "100%",
                    sSearch: '',
                }
            });
            table.buttons().container()
                .appendTo('#file-datatable_wrapper .col-md-6:eq(0)');

            new Vue({
                el: '#his',
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
                        axios.post("BillPayReceipt.aspx/UPITransferData", article)
                            .then(response => {
                                el.BBPSData = JSON.parse(response.data.d)
                            });

                    }
                },
                mounted() {

                }
            });
        });
        function showModal() {
            $('#myModal').modal();
        }


    </script>

</asp:Content>

