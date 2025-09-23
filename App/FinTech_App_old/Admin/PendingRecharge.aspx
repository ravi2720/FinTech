<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="PendingRecharge.aspx.cs" Inherits="Admin_PendingRecharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="updateRolePanel">
        <ContentTemplate>
            <div class="content-wrapper">
                <section class="content">
                    <div class="panel panel-primary">
                        <div class="panel-heading">Recharge Transaction</div>
                        <div class="panel-body">
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
                                </div>
                                <div class="col-md-2">
                                    <label>To Date</label>
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label>Status</label>
                                    <asp:DropDownList runat="server" ID="ddlStatus" Enabled="false" CssClass="form-control">
                                        <asp:ListItem Text="Select Status" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Success" Value="Success"></asp:ListItem>
                                        <asp:ListItem Selected="True" Text="Pending" Value="Pending"></asp:ListItem>
                                        <asp:ListItem Text="Failed" Value="Failed"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label>To Date</label>
                                    <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" CssClass="form-control"></asp:Button>
                                </div>
                            </div>
                            <br />
                            <div class="table-responsive">
                                <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                    <thead>                                        
                                        <tr>
                                            <th></th>
                                            <th>SNO</th>
                                            <th>TXID</th>
                                            <th>Operator</th>
                                            <th>Number</th>
                                            <th>Amount</th>
                                            <th>Status</th>
                                            <th>Operator Id</th>
                                            <th>Recharge By</th>
                                            <th>Date Time</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="rptDataRecharge">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><asp:CheckBox runat="server" ID="chk" /></td>
                                                    <td><%# Container.ItemIndex+1 %>
                                                        <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>' />
                                                    </td>
                                                    <td><%# Eval("TransID") %></td>
                                                    <td><%# Eval("OPERATORNAME") %></td>
                                                    <td><%# Eval("MobileNo") %></td>
                                                    <td><%# Eval("Amount") %></td>
                                                    <td><%# Eval("Status") %></td>
                                                    <td><%# Eval("APIMessage") %></td>
                                                    <td><%# Eval("LOGINID") %></td>
                                                    <td><%# Eval("CreatedDate") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <td>
                                                <asp:Button runat="server" ID="btnCheckStatus" Text="Check Status" OnClick="btnCheckStatus_Click" />
                                            </td>
                                        </tr>
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

