<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="TotalRecharge.aspx.cs" Inherits="Admin_TotalRecharge" %>

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
                                    <div class="card-header">Total Recharge History</div>
                                    <div class="card-body">
                                        <div class="table-responsive">
                                            <table id="example1" class="table table-striped table-bordered">

                                                <thead>
                                                    <tr>
                                                        <th>SL</th>
                                                        <th>Status</th>
                                                        <th>Amount</th>
                                                        <th>Operator</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="rptData">
                                                        <ItemTemplate>
                                                            <tr class="row1">
                                                                <td><%#Container.ItemIndex+1 %>                                            
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnStatus" runat="server" CssClass="btn btn-success" Text='<%#Eval("Status") %>' />
                                                                <td><%#Eval("AMOUNT") %></td>
                                                                <td><%#Eval("OPERATORNAME") %></td>
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

