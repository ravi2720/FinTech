<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="CheckEWalletTransaction.aspx.cs" Inherits="Reatiler_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card card-primary">
                                <div class="card-header">Search E-Wallet Transactions</div>
                                <div class="card-body">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="txtTransactionID" class="form-control" placeholder="Transaction ID" data-toggle="tooltip" title="Please Enter New Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTransactionID" ForeColor="Red" ErrorMessage="Enter Transaction ID" Display="Dynamic" ValidationGroup="chkaddfund" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="chkaddfund" Text="Submit" />
                                        <span id="wait_tip" style="display: none;">Please wait...<img src="images/ajax-loader2.gif" id="loading_img"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="card panel-primary">
                            <div class="card-header">E-Wallet Transactions</div>
                            <div class="card-body">
                                <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                    <thead>
                                        <tr>
                                            <th>S.No</th>
                                            <th>MemberID</th>
                                            <th>Factor</th>
                                            <th>Amount</th>
                                            <th>Transaction ID</th>
                                            <th>Narration</th>
                                            <th>Description</th>
                                            <th>Add Date</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        <asp:Repeater runat="server" ID="rptData">
                                            <ItemTemplate>
                                                <tr class="row1">
                                                    <td><%#Container.ItemIndex+1 %></td>
                                                    <td><%#Eval("LoginID") %></td>
                                                    <td><%#Eval("Factor") %></td>
                                                    <td><%#Eval("Amount") %></td>
                                                    <td><%#Eval("TRANSACTIONID") %></td>
                                                    <td><%#Eval("NARRATION") %></td>
                                                    <td><%#Eval("Description") %></td>
                                                    <td><%#Eval("AddDate") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
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

