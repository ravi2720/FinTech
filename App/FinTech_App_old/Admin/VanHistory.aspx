<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="VanHistory.aspx.cs" Inherits="Admin_VanHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div role="tabpanel" class="profile-tab-pane tab-pane " id="UPIID">
                        <div class="card-group padding-20" id="accordion" role="tablist" aria-multiselectable="true">
                            <div class="card card-primary">
                                <div class="card-header">Van Details  </div>
                                <div class="card-body">
                                    <div class="row">
                                        <table class="table" style="background: #011f3f; color: white">
                                            <tbody>
                                                <asp:Repeater ID="rptUPIDetails" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>Account No :</td>
                                                            <td>
                                                                <asp:Label ID="lblUpiID" runat="server" Text='<%#Eval("AccountNo") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Name :</td>
                                                            <td>
                                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>IFSC :</td>
                                                            <td>
                                                                <asp:Label ID="lblPAN" runat="server" Text='<%#Eval("IFSC") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Mobile :</td>
                                                            <td>
                                                                <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Mobile") %>'></asp:Label></td>
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td>Created At :</td>
                                                            <td>
                                                                <asp:Label ID="lblAddDate" runat="server" Text='<%#Eval("AddDate") %>'></asp:Label></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td>
                                                        Total Amount
                                                    </td>
                                                    <td>
                                                         <asp:Label ID="lblTotalAmount" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="ibox float-e-margins">
                                                <div class="ibox-content collapse show">
                                                    <div class="widgets-container">
                                                        <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th>S.No</th>
                                                                    <th>Txn ID</th>
                                                                    <th>Remitter Name</th>
                                                                    <th>Remitter AccountNo</th>
                                                                    <th>Remitter BankName</th>
                                                                    <th>Beneficiary Ifsc</th>
                                                                    <th>Bank Ref No</th>
                                                                    <th>Amount</th>
                                                                    <th>Date</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%# Container.ItemIndex + 1 %></td>
                                                                            <td><%# Eval("transactionRequestId") %></td>
                                                                            <td><%# Eval("remitterName") %></td>
                                                                            <td><%# Eval("remitterAccountNumber") %></td>
                                                                            <td><%# Eval("remitterBankName") %></td>
                                                                            <td><%# Eval("beneficiaryIfsc") %></td>
                                                                            <td><%# Eval("bankTxnIdentifier") %></td>
                                                                            <td><%# Eval("Amount") %></td>
                                                                            <td><%# Eval("AddDate") %></td>
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
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                </Triggers>
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


