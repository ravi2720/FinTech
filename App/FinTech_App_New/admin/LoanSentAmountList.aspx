<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="LoanSentAmountList.aspx.cs" Inherits="Admin_LoanSentAmountList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Loan Sent Amount</div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="box">

                                    <div class="box-body">
                                        <div class="col-md-12">

                                            <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                <thead>
                                                    <tr>
                                                        <th>Check Status</th>
                                                        <th>S.No</th>
                                                        <th>Member Id</th>
                                                        <th>Member Name</th>
                                                        <th>Loan Number</th>
                                                        <th>Loan Amount</th>
                                                        <th>Loan Approve Amount</th>
                                                        <th>TransID</th>                                                        
                                                        <th>Bank Details</th>
                                                        <th>Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="gvTransactionHistory" runat="server" OnItemCommand="gvTransactionHistory_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button runat="server" ID="btnCheckStatus" CommandName="CheckStatus" CommandArgument='<%# Eval("reference_number") %>' Text="Check Status" Visible='<%# (Eval("status").ToString().ToUpper()=="PENDING"?true:false) %>' CssClass="btn btn-danger" />
                                                                </td>
                                                                <td><%# Container.ItemIndex + 1 %></td>
                                                                <td><%# Eval("NAME") %></td>
                                                                <td><%# Eval("LOGINID") %></td>
                                                                <td><%# Eval("LoanNumber") %></td>
                                                                <td><%# Eval("LOANAMT") %></td>
                                                                <td><%# Eval("APPROVED_LOANAMT") %></td>                                                                
                                                                <td><%# Eval("reference_number") %></td>
                                                                <td>
                                                                    <table class="table table-responsive">
                                                                        <tr>
                                                                            <th>
                                                                                <strong>Bank Number</strong>
                                                                                <strong>IFSC</strong>
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><%# Eval("number") %></td>
                                                                            <td>
                                                                                <%# Eval("IFSC") %>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("Status") %>
                                                                    <asp:HiddenField runat="server" ID="hdnLoanNumber" Value='<%# Eval("LoanNumber") %>' />
                                                                    <asp:HiddenField runat="server" ID="hdnRefNumber" Value='<%# Eval("reference_number") %>' />
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

