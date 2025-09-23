<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="DMTHistory.aspx.cs" Inherits="Member_DMTHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">

        <div class="main-content-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <%--                    <div class="card">
                        <h4 class="card-header">DMT History
                             <span class="btn btn-success">Success <span class="badge" runat="server" id="lblSCount">0</span></span><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="btn btn-success">Success <span class="badge" runat="server" id="lblSsum">0</span></span>
                            <span class="btn btn-warning">Pending <span class="badge" runat="server" id="lblPCount">0</span></span><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="btn btn-warning">Pending <span class="badge" runat="server" id="lblPSum">0</span></span>
                            <span class="btn btn-danger">Failed <span class="badge" runat="server" id="lblFCount">0</span></span><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="btn btn-danger">Failed <span class="badge" runat="server" id="lblFSum">0</span></span>
                        </h4>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <label for="ddlMember">From Date : </label>
                                    <asp:TextBox ID="txtfromdate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                        TargetControlID="txtfromdate">
                                    </cc1:CalendarExtender>

                                </div>
                                <div class="col-md-3">
                                    <label for="ddlMember">To Date : </label>
                                    <asp:TextBox ID="txttodate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="MM-dd-yyyy" Animated="False"
                                        PopupButtonID="txttodate" TargetControlID="txttodate">
                                    </cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2">
                                    <label for="ddlMember">Member ID : </label>
                                    <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member."  class="form-control select2"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger" Text="Search" OnClick="btnSearch_Click" />
                                </div>
                            </div>





                            <div class="row mt-2">
                                <div class="col-lg-12">
                                    <div class="ibox float-e-margins">
                                        <div class="ibox-content collapse show">
                                            <div class="widgets-container">
                                                <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>S.No</th>
                                                            <th>Member Name</th>
                                                            <th>AddDate</th>
                                                            <th>Amount</th>
                                                            <th>Surcharge</th>
                                                            <th>Total Amount</th>
                                                            <th>TransID</th>
                                                            <th>Bank Number</th>
                                                            <th>Bank Details</th>
                                                            <th>Status</th>
                                                            <th>Receipt</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                  
                                                                    <td><%# Container.ItemIndex + 1 %></td>
                                                                    <td><%# Eval("NAME") %><br />
                                                                        <%# Eval("LOGINID") %></td>
                                                                    <td><%# Eval("AddDate") %></td>
                                                                    <td><%# Eval("Amount") %></td>
                                                                    <td><%# Eval("Surcharge") %></td>
                                                                    <td><%# Eval("FinalAmount") %></td>
                                                                    <td><%# Eval("OrderID") %></td>
                                                                    <td><%# Eval("VendorID") %></td>
                                                                    <td>
                                                                        <table class="table table-responsive">
                                                                            <tr>
                                                                                <th>
                                                                                    <strong>Bank Number</strong>
                                                                                    <strong>IFSC</strong>
                                                                                </th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><%# Eval("accountNumber") %></td>
                                                                                <td>
                                                                                    <%# Eval("ifsc") %>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                        <span class='<%#(Eval("Status").ToString().ToUpper()=="SUCCESS"?"btn btn-success":"btn btn-danger") %>'><%# (Eval("Status").ToString().ToUpper()=="PROCESSED"?"Sucess":Eval("Status").ToString()) %></span>
                                                                        <asp:HiddenField runat="server" ID="hdnRefNumber" Value='<%# Eval("OrderID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <a style="color: red;" href='<%# "ReceiptDMTNew.aspx?GUID="+Eval("OrderID").ToString() %>'>Receipt</a>
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
                        </div>
                    </div>--%>

                    
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
                                    <h3 class="card-title">DMT History
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
                                                    <th class="border-bottom-0">Member Name</th>
                                                    <th class="border-bottom-0">AddDate</th>
                                                    <th class="border-bottom-0">Amount</th>
                                                    <th class="border-bottom-0">Surcharge</th>
                                                    <th class="border-bottom-0">Total Amount</th>
                                                    <th class="border-bottom-0">TransID</th>
                                                    <th class="border-bottom-0">Bank Number</th>
                                                    <th class="border-bottom-0">Bank Details</th>
                                                    <th class="border-bottom-0">Status</th>
                                                    <th class="border-bottom-0">Receipt</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                    <ItemTemplate>
                                                        <tr>

                                                            <td><%# Container.ItemIndex + 1 %></td>
                                                            <td><%# Eval("NAME") %><br />
                                                                <%# Eval("LOGINID") %></td>
                                                            <td><%# Eval("AddDate") %></td>
                                                            <td><%# Eval("Amount") %></td>
                                                            <td><%# Eval("Surcharge") %></td>
                                                            <td><%# Eval("FinalAmount") %></td>
                                                            <td><%# Eval("OrderID") %></td>
                                                            <td><%# Eval("VendorID") %></td>
                                                            <td>
                                                                <table class="table table-responsive">
                                                                    <tr>
                                                                        <th>
                                                                            <strong>Bank Number</strong>
                                                                            <strong>IFSC</strong>
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><%# Eval("accountNumber") %></td>
                                                                        <td>
                                                                            <%# Eval("ifsc") %>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <span class='<%#(Eval("Status").ToString().ToUpper()=="SUCCESS"?"btn btn-success":"btn btn-danger") %>'><%# (Eval("Status").ToString().ToUpper()=="PROCESSED"?"Sucess":Eval("Status").ToString()) %></span>
                                                                <asp:HiddenField runat="server" ID="hdnRefNumber" Value='<%# Eval("OrderID") %>' />
                                                            </td>
                                                            <td>
                                                                <a style="color: red;" href='<%# "ReceiptDMTNew.aspx?GUID="+Eval("OrderID").ToString() %>'>Receipt</a>
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
        });
        function showModal() {
            $('#myModal').modal();
        }


    </script>
</asp:Content>

