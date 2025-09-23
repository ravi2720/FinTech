<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AddAPI.aspx.cs" Inherits="Admin_AddAPI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="card card-primary" id="DataCard">
                <div class="card-header">API Integration</div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtAPIName">API Name * :</label>
                                <asp:TextBox runat="server" class="form-control" ID="txtAPIName" data-toggle="tooltip" ToolTip="Please Enter APIName" placeholder="Please Enter APINamer*"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtURL">Recharge URL * :</label>
                                <asp:TextBox runat="server" class="form-control" ID="txtURL" data-toggle="tooltip" ToolTip="Please Enter RechargeURL" placeholder="Please Enter RechargeURL*"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revURL" runat="server" ErrorMessage="URL is not valid !"
                                    ControlToValidate="txtURL" Display="Dynamic" SetFocusOnError="True" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                                    ValidationGroup="v"><img src="../images/warning.png" /></asp:RegularExpressionValidator>
                            </div>
                            <div class="form-group">
                                <h3 class="StepTitle">Recharge API Parameters</h3>
                                <asp:TextBox ID="txtprm1" runat="server" MaxLength="50" ValidationGroup="v" Text="uid" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvprm1" runat="server" ErrorMessage="Please Enter Parameter-1 !"
                                    ControlToValidate="txtprm1" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtprm1val" runat="server" MaxLength="50" ValidationGroup="v" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvprm1val" runat="server" ErrorMessage="Please Enter Parameter-1 Value !"
                                    ControlToValidate="txtprm1val" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtprm2" runat="server" MaxLength="50" ValidationGroup="v" Text="pin" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtprm2val" runat="server" MaxLength="50" ValidationGroup="v" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtURL">Splitter * :</label>
                                <asp:TextBox ID="txtSplitter" runat="server" MaxLength="1" Width="30px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSplitter" runat="server" ErrorMessage="Please Enter Splitter !"
                                    ControlToValidate="txtSplitter" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label for="txtURL">Parameter 3 * :</label>
                                <asp:TextBox ID="txtprm3" runat="server" MaxLength="50" ValidationGroup="v" Text="number"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:CheckBox runat="server" ID="chktxtprm3" TagName="prm3" onchange="GetCombineSTR()" CssClass="Combine" />
                                <asp:RequiredFieldValidator ID="rfvprm3" runat="server" ErrorMessage="Please Enter Parameter-3 !"
                                    ControlToValidate="txtprm3" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label for="txtURL">Parameter 4 * :</label>
                                <asp:TextBox ID="txtprm4" runat="server" MaxLength="50" ValidationGroup="v" Text="operator"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:CheckBox runat="server" ID="chktxtprm4" TagName="prm4" onchange="GetCombineSTR()" CssClass="Combine" />
                                <asp:RequiredFieldValidator ID="rfvprm4" runat="server" ErrorMessage="Please Enter Parameter-4 !"
                                    ControlToValidate="txtprm4" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label for="txtURL">Parameter 5 * :</label>
                                <asp:TextBox ID="txtprm5" runat="server" MaxLength="50" ValidationGroup="v" Text="circle"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:CheckBox runat="server" ID="chktxtprm5" TagName="prm5" onchange="GetCombineSTR()" CssClass="Combine" />
                            </div>
                            <div class="form-group">
                                <label for="txtURL">Parameter 6 :</label>
                                <asp:TextBox ID="txtprm6" runat="server" MaxLength="50" ValidationGroup="v" Text="amount"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:CheckBox runat="server" ID="chktxtprm6" TagName="prm6" onchange="GetCombineSTR()" CssClass="Combine" />
                            </div>
                            <div class="form-group">
                                <label for="txtURL">Parameter 7 :</label>
                                <asp:TextBox ID="txtprm7" runat="server" MaxLength="50" ValidationGroup="v" Text="account"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:CheckBox runat="server" ID="chktxtprm7" onchange="GetCombineSTR()" TagName="prm7" CssClass="Combine" />
                            </div>
                            <div class="form-group">
                                <label for="txtURL">Parameter 8 :</label>
                                <asp:TextBox ID="txtprm8" runat="server" MaxLength="50" ValidationGroup="v" Text="usertx"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:CheckBox runat="server" ID="chktxtprm8" onchange="GetCombineSTR()" TagName="prm8" CssClass="Combine" />
                            </div>
                            <div class="form-group">
                                <label for="txtURL">Format</label>
                                <asp:TextBox ID="txtprm9" runat="server" MaxLength="50" ValidationGroup="v" Text="format"></asp:TextBox>
                                <asp:TextBox ID="txtprm9val" runat="server" MaxLength="50" ValidationGroup="v" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtURL">version</label>
                                <asp:TextBox ID="txtprm10" runat="server" MaxLength="50" ValidationGroup="v" Text="version"></asp:TextBox>
                                <asp:TextBox ID="txtprm10val" runat="server" MaxLength="50" ValidationGroup="v" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtURL">TxID Position</label>
                                <asp:TextBox ID="txtTxIDPosition" runat="server" MaxLength="2" Width="30"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label for="txtURL">Status Position</label>
                                <asp:TextBox ID="txtStatusPosition" runat="server" MaxLength="2" Width="30"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label for="txtURL">Status msg for Success</label>
                                <asp:TextBox ID="txtSuccess" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSuccess" runat="server" ErrorMessage="Please Enter Status msg for Success !"
                                    ControlToValidate="txtSuccess" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>

                            </div>
                            <div class="form-group">
                                <label for="txtURL">Status msg for Failed</label>
                                <asp:TextBox ID="txtFailed" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFailed" runat="server" ErrorMessage="Please Enter Status msg for Failed !"
                                    ControlToValidate="txtFailed" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>

                            </div>
                            <div class="form-group">
                                <label for="txtURL">Status msg for Pending</label>
                                <asp:TextBox ID="txtPending" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPending" runat="server" ErrorMessage="Please Enter Status msg for Pending !"
                                    ControlToValidate="txtPending" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>

                            </div>
                            <div class="form-group">
                                <label for="txtURL">Operator Ref Position</label>
                                <asp:TextBox ID="txtOperatorRefPosition" runat="server" MaxLength="2" Width="30"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label for="txtURL">ErrorCode Position</label>
                                <asp:TextBox ID="txtErrorCodePosition" runat="server" MaxLength="2" Width="30"></asp:TextBox>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-primary">
                <div class="card-header">Balance API Parameters</div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox ID="txtB_prm1" runat="server" MaxLength="50" Text="uid" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtB_prm1val" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtB_prm2" runat="server" MaxLength="50" Text="pin" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtB_prm2val" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtURL">Balance URL</label>
                                <asp:TextBox ID="txtBalanceURL" runat="server" MaxLength="250" CssClass="form-control"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revBalanceURL" runat="server" ErrorMessage="Balance URL is not valid !"
                                    ControlToValidate="txtBalanceURL" Display="Dynamic" SetFocusOnError="True" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                                    ValidationGroup="v"><img src="../images/warning.png" /></asp:RegularExpressionValidator>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtB_prm3" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:TextBox ID="txtB_prm3val" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtB_prm4" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:TextBox ID="txtB_prm4val" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtURL">Balance Position</label>
                                <asp:TextBox ID="txtB_BalancePosition" runat="server" MaxLength="2" Width="30"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-primary">
                <div class="card-header">Status API Parameters</div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox ID="txtS_prm1" runat="server" MaxLength="50" Text="uid" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtS_prm1val" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtS_prm2" runat="server" MaxLength="50" Text="pin" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtS_prm2val" runat="server" MaxLength="50" Text="pin" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtURL">Status URL</label>
                                <asp:TextBox ID="txtStatusURL" runat="server" MaxLength="250" CssClass="form-control"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revStatusURL" runat="server" ErrorMessage="Status URL is not valid !"
                                    ControlToValidate="txtStatusURL" Display="Dynamic" SetFocusOnError="True" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                                    ValidationGroup="v"><img src="../images/warning.png" /></asp:RegularExpressionValidator>
                            </div>

                            <div class="form-group">
                                <label for="txtURL">Parameter 3</label>
                                <asp:TextBox ID="txtS_prm3" runat="server" MaxLength="50" Text="txid" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtURL">Parameter 4</label>
                                <asp:TextBox ID="txtS_prm4" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtURL">Status Position</label>
                                <asp:TextBox ID="txtS_StatusPosition" runat="server" MaxLength="2" Width="30"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtURL">Operator Referance Position</label>
                                <asp:TextBox ID="txtS_OpratrRefPosition" runat="server" MaxLength="2" Width="30"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtURL">Api Transaction Position</label>
                                <asp:TextBox ID="txtS_ApiTransPosition" runat="server" MaxLength="2" Width="30"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="card card-primary">
                        <div class="card-header">Status API Parameters</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <table id="example1" class="table table-bordered table-striped glyphicon-hover" style="width: 100% !important">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>Service Type</th>
                                                <th>Operator Name</th>
                                                <th>Operator Code</th>
                                                <th>Commission</th>
                                                <th>Surcharge</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater runat="server" ID="gvOperator">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex+1 %></td>
                                                        <td><%# Eval("ServiceTypeName") %></td>
                                                        <td>
                                                            <%# Eval("Name") %>
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField runat="server" ID="hdnOperatorID" Value='<%# Eval("ID") %>' />
                                                            <asp:TextBox ID="txtOperatorCode" runat="server" CssClass="form-control" Text='<%# Eval("OPCode") %>'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Style="width: 200px;" ID="txtCommission" TextMode="Number" CssClass="form-control" runat="server" MaxLength="5" Text='<%# String.IsNullOrEmpty(Convert.ToString(Eval("Commission"))) ? "0" : Eval("Commission") %>'></asp:TextBox>
                                                            <asp:CheckBox ID="chkCommissionIsFlat" runat="server" Text="Is Flat" Style="float: left" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Style="width: 200px;" ID="txtSurcharge" TextMode="Number" CssClass="form-control" runat="server" MaxLength="5" Text='<%# String.IsNullOrEmpty(Convert.ToString(Eval("Surcharge"))) ? "0" : Eval("Surcharge") %>'></asp:TextBox>
                                                            <asp:CheckBox ID="chkSurchargeIsFlat" runat="server" Text="Is Flat" Style="float: left" />
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
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-default" ValidationGroup="v" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-danger" ValidationGroup="v" OnClick="btnSubmit_Click" />
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" ClientIDMode="Static"
                        ValidationGroup="v" />
                </div>
            </div>
        </section>
    </div>

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

