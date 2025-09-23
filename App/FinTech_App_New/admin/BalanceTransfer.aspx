<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="BalanceTransfer.aspx.cs" Inherits="Admin_BalanceTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:UpdatePanel runat="server" ID="updateRolePanel">
                    <ContentTemplate>
                        <asp:MultiView ID="multiview1" ActiveViewIndex="0" runat="server">
                            <asp:View ID="View2" runat="server">
                                <div class="card card-primary">
                                    <div class="card-header">Credit/Debit</div>
                                    <div class="card-body">
                                        <div class="table-responsive">
                                            <table id="example1" class="table table-striped table-bordered">

                                                <thead>
                                                    <tr>
                                                        <th>User ID</th>
                                                        <th>Name</th>
                                                        <th>MemberID</th>
                                                        <th>Balance</th>
                                                        <th>AEPS Balance</th>
                                                        <th>Add Balance</th>
                                                        <th>Revert Balance</th>
                                                        <th>Add AEPS Balance</th>
                                                        <th>AEPS Revert Balance</th>
                                                        <th>Login</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="rptDataRecharge">
                                                        <ItemTemplate>
                                                            <tr class="row1">
                                                                <td><%# Container.ItemIndex+1 %></td>
                                                                <td><%#Eval("Name") %></td>
                                                                <td><%#Eval("LoginID") %></td>
                                                                <td><%#Eval("Balance") %></td>
                                                                <td><%#Eval("AEPSBalance") %></td>
                                                                <td><a title="Transfer Money" href='AddFund.aspx?usr=<%#Eval("Msrno") %>' class="btn btn-primary">Add Balance</a> </td>
                                                                <td><a title="Revert Transaction" href='Deduct.aspx?usr=<%#Eval("Msrno") %>' class="btn btn-danger">Revert Balance</a> </td>
                                                                <td><a title="AEPS Transfer Money" href='AddAEPSFund.aspx?usr=<%#Eval("Msrno") %>' class="btn btn-primary">AEPS Add Balance</a> </td>
                                                                <td><a title="AEPS Revert Transaction" href='DeductAEPS.aspx?usr=<%#Eval("Msrno") %>' class="btn btn-danger">AEPS Revert Balance</a> </td>
                                                                <td></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>

                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </ContentTemplate>
                </asp:UpdatePanel>
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

