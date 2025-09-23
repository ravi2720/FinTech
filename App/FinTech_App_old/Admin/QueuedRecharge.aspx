<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="QueuedRecharge.aspx.cs" Inherits="Admin_QueuedRecharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="updateRolePanel">
        <ContentTemplate>
            <div class="content-wrapper">
                <section class="content">
                    <div class="container">
                        <div class="card card-primary">
                            <div class="card-header">Recharge Transaction</div>
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
                                    </div>
                                    <div class="col-md-2">
                                        <label>To Date</label>
                                        <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Status</label>
                                        <asp:DropDownList runat="server" ID="ddlStatus"  CssClass="form-control">
                                            
                                            <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="Queued" Value="Queued"></asp:ListItem>
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
                                            </tr>
                                            <tr>
                                                <th>SNO</th>
                                                <th></th>
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
                                                        <td><%# Container.ItemIndex+1 %></td>
                                                        <td>
                                                            <asp:HiddenField runat="server" ID="hdnTransID" Value='<%# Eval("TransID") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnLOGINID" Value='<%# Eval("LOGINID") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>' />
                                                            <asp:CheckBox ID="chkRow" runat="server" />
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
                                        </tbody>
                                    </table>
                                </div>

                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Button ID="btnForceSuccess" OnClientClick='return confirm("Are You Sure Records To Force Success?")' runat="server" Text="Force Success"
                                                OnClick="btnForceSuccess_Click" CssClass="btn btn-primary" />
                                        </div>
                                         <div class="col-md-4"></div>
                                        <div class="col-md-4">
                                            <asp:Button ID="btnForceFail" OnClientClick='return confirm("Are You Sure Records To Force Fail?")' runat="server" Text="Force Fail"
                                                OnClick="btnForceFail_Click" CssClass="btn btn-danger" />
                                            <asp:Label ID="litResult" runat="server" BackColor="Yellow"></asp:Label>
                                        </div>
                                    </div>
                                </div>
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



