<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="RechargeHistory.aspx.cs" Inherits="Member_RechargeHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/receiptcss.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- main-content -->
    <!-- container -->
    <div class="main-container container-fluid" id="his">
        <!-- main-content-body -->
        <div class="main-content-body">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdnServiceID" Value="0" />

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
                                            <label>Service</label>
                                            <asp:DropDownList runat="server" ID="ddlService" CssClass="form-control"></asp:DropDownList>
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
                                                <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
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
                                            <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" CssClass="form-control"></asp:Button>
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
                                    <h3 class="card-title">Recharge History
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
                                                    <th class="border-bottom-0">Status</th>
                                                    <th class="border-bottom-0">Action</th>
                                                    <th class="border-bottom-0">Recharge By</th>
                                                    <th class="border-bottom-0">TXID</th>
                                                    <th class="border-bottom-0">Operator</th>
                                                    <th class="border-bottom-0">Number</th>
                                                    <th class="border-bottom-0">Amount</th>
                                                    <th class="border-bottom-0">Commission</th>
                                                    <th class="border-bottom-0">Operator Id</th>
                                                    <th class="border-bottom-0">Date Time</th>
                                                    <th class="border-bottom-0">Receipt</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="rptDataRecharge" OnItemCommand="rptDataRecharge_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Container.ItemIndex+1 %></td>
                                                            <td>
                                                                <span class='<%# Eval("Status").ToString().GetColor() %>'>
                                                                    <%# Eval("Status") %>
                                                                </span>

                                                            </td>
                                                            <td>
                                                                <ul class="navbar-nav ms-auto text-xl-center">
                                                                    <li class="nav-item dropdown">
                                                                        <a class="nav-link dropdown-toggle" href="#" id='<%# "navbarDropdown"+Container.ItemIndex+1 %>' role="button" data-bs-toggle="dropdown" aria-expanded="false">Action
                                                                        </a>
                                                                        <ul class="dropdown-menu" style="text-align: center;" aria-labelledby='<%# "navbarDropdown"+Container.ItemIndex+1 %>'>
                                                                            <li><a class="dropdown-item" href="#">
                                                                                <asp:Button runat="server" OnClientClick="return confirm('do you want to create dispute')" ID="btnDispute"
                                                                                    Enabled='<%# ((Eval("Status").ToString().ToUpper() == "SUCCESS" && Convert.ToBoolean(Eval("Dispute").ToString())==false && Convert.ToBoolean(Eval("ReSolve").ToString())==false)==true ? true:false) %>'
                                                                                    Text='<%# ((Eval("Status").ToString().ToUpper() == "SUCCESS" && Convert.ToBoolean(Eval("Dispute").ToString())==false && Convert.ToBoolean(Eval("ReSolve").ToString() )==false && Convert.ToBoolean(Eval("RejectDispute").ToString() )==false)==true ? "Dispute":((Eval("Status").ToString().ToUpper() == "SUCCESS" && Convert.ToBoolean(Eval("Dispute").ToString())==true && Convert.ToBoolean(Eval("ReSolve").ToString())==false && Convert.ToBoolean(Eval("RejectDispute").ToString())==false))==true?"Pending":((Convert.ToBoolean(Eval("Dispute").ToString())==true && Convert.ToBoolean(Eval("RejectDispute").ToString())==true )==true?"Rejected":"Resolved")) %>'
                                                                                    CssClass='<%# ((Eval("Status").ToString().ToUpper() == "SUCCESS" && Convert.ToBoolean(Eval("Dispute").ToString())==false && Convert.ToBoolean(Eval("ReSolve").ToString() )==false && Convert.ToBoolean(Eval("RejectDispute").ToString() )==false)==true ? "btn btn-primary":((Eval("Status").ToString().ToUpper() == "SUCCESS" && Convert.ToBoolean(Eval("Dispute").ToString())==true && Convert.ToBoolean(Eval("ReSolve").ToString())==false && Convert.ToBoolean(Eval("RejectDispute").ToString())==false))==true?"btn btn-warning":((Convert.ToBoolean(Eval("Dispute").ToString())==true && Convert.ToBoolean(Eval("RejectDispute").ToString())==true )==true?"btn btn-danger":"btn btn-success")) %>'
                                                                                    Visible='<%# (Eval("Status").ToString().ToUpper() == "SUCCESS" ? true:false) %>' CommandName="Dispute"
                                                                                    CommandArgument='<%# Eval("ID") %>' /></a></li>
                                                                            <li>
                                                                                <asp:Button runat="server" Text="Complain" ID="btnComplain" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#ComplainBox" CommandName="c" CommandArgument='<%# Eval("ID") %>' />
                                                                        </ul>
                                                                    </li>
                                                                </ul>
                                                            </td>
                                                            <td><%# Eval("LOGINID") %></td>
                                                            <td><%# Eval("TransID") %></td>
                                                            <td><%# Eval("OPERATORNAME") %></td>
                                                            <td><%# Eval("MobileNo") %></td>
                                                            <td><%# Eval("Amount") %></td>
                                                            <td><%# Eval("Commission") %></td>
                                                            <td><%# Eval("APIMessage") %></td>
                                                            <td><%# Eval("CreatedDate") %></td>
                                                            <td>
                                                                <input type="button" style="color: red" data-bs-toggle="modal" data-bs-target="#recipt" v-on:click="BBPSDataF('<%# Eval("TransID") %>')" value="View Receipt" />
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
            </asp:UpdatePanel>
        </div>
        <div class="modal upi-modal" id="ComplainBox" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Complain Box</h5>
                        <button aria-label="Close" class="btn-close" data-bs-dismiss="modal" type="button"><span aria-hidden="true">×</span></button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel runat="server" ID="uptre">
                            <ContentTemplate>
                                <div class="card card-primary">
                                    <div class="card-header">Rise Support Ticket</div>
                                    <div class="card card-body">
                                        <div class="form-group">
                                            <asp:Repeater runat="server" ID="rptDataOneRow">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label>Mobile</label>
                                                            <label class="form-control"><%# Eval("MobileNo") %></label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label>TXNID</label>
                                                            <label class="form-control"><%# Eval("TransID") %></label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label>OPERATOR NAME</label>
                                                            <label class="form-control"><%# Eval("OPERATORNAME") %></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>Status</label>
                                                            <span class='<%# Eval("Status").ToString().GetColor() %>'>
                                                                <%# Eval("Status") %>
                                                            </span>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>Amount</label>
                                                            <label class="form-control"><%# Eval("Amount") %></label>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </div>
                                        <div class="form-group">
                                            <label for="txtMessage">Message :</label>
                                            <asp:TextBox runat="server" ID="txtMessage" TextMode="MultiLine" Rows="5" Columns="5" ToolTip="Enter Message Regarding Your Complain" class="form-control"></asp:TextBox>
                                        </div>
                                        <asp:Button ID="btnTicket" class="btn btn-primary" Text="Submit" runat="server" OnClick="btnTicket_Click" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
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
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                         <a href="#!" onclick="window.print()" class="btn">
			Print
		</a>
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
                        axios.post("BillPayReceipt.aspx/BillData", article)
                            .then(response => {
                                el.BBPSData = JSON.parse(response.data.d)
                            });

                    }
                },
                mounted() {
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
                }
            });
        });
        function showModal() {
            $('#myModal').modal();
        }


    </script>



</asp:Content>

