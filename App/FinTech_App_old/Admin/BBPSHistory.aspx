<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="BBPSHistory.aspx.cs" Inherits="Admin_BBPSHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="updateRolePanel">
        <ContentTemplate>
            <div class="content-wrapper">
                <section class="content">
                    <div class="card card-primary">
                        <div class="card-header">
                            BBPS Transaction
                             <span class="btn btn-success">Success <span class="badge" runat="server" id="lblSCount">0</span></span><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="btn btn-success">Success <span class="badge" runat="server" id="lblSsum">0</span></span>
                            <span class="btn btn-warning">Pending <span class="badge" runat="server" id="lblPCount">0</span></span><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="btn btn-warning">Pending <span class="badge" runat="server" id="lblPSum">0</span></span>
                            <span class="btn btn-danger">Failed <span class="badge" runat="server" id="lblFCount">0</span></span><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="btn btn-danger">Failed <span class="badge" runat="server" id="lblFSum">0</span></span>
                        </div>
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
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                        TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2">
                                    <label>To Date</label>
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="MM-dd-yyyy" Animated="False"
                                        PopupButtonID="txttodate" TargetControlID="txttodate">
                                    </cc1:CalendarExtender>
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
                                    <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-danger form-control mt-2"></asp:Button>
                                </div>
                            </div>
                            <br />
                            <div class="table-responsive">
                                <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                    <thead>
                                        <tr>
                                            <th>SNO</th>
                                            <th>Status</th>
                                            <th>Recharge By</th>
                                            <th>TXID</th>
                                            <th>API Name</th>
                                            <th>Operator</th>
                                            <th>Number</th>
                                            <th>Amount</th>
                                            <th>Commission</th>
                                            <th>Operator Id</th>

                                            <th>Date Time</th>
                                            <th>Receipt</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="rptDataRecharge">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.ItemIndex+1 %></td>
                                                    <td>
                                                        <span class='<%# Eval("Status").ToString().GetColor() %>'>
                                                            <%# Eval("Status") %>
                                                        </span>
                                                    </td>
                                                    <td><%# Eval("LOGINID") %></td>
                                                    <td><%# Eval("TransID") %></td>
                                                    <td><%# Eval("APIName") %></td>
                                                    <td><%# Eval("OPERATORNAME") %></td>
                                                    <td><%# Eval("MobileNo") %></td>
                                                    <td><%# Eval("Amount") %></td>
                                                    <td><%# Eval("Commission") %></td>
                                                    <td><%# Eval("APIMessage") %></td>
                                                    <td><%# Eval("CreatedDate") %></td>
                                                    <td>
                                                        <a style="color: red" href='<%# "BillPayReceipt.aspx?Transid="+Eval("TransID") %>'>View Receipt</a>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        $(function () {
            Action(DataObject);
        });

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

            Action(DataObject);
        }

        var req = Sys.WebForms.PageRequestManager.getInstance();
        req.add_beginRequest(function () {

        });
        req.add_endRequest(function () {
            LoadData();
        });
        function showModal() {
            $('#myModal').modal();
        }


    </script>
</asp:Content>



