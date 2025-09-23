<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="PayoutControlSystem.aspx.cs" Inherits="Admin_PayoutControlSystem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdnID" ClientIDMode="AutoID" Value="0" />
                    <div class="panel panel-primary">
                        <div class="panel-heading">Payout Setting</div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="box">

                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <label>Role</label>
                                                <asp:DropDownList ID="dllRole" AutoPostBack="true" OnSelectedIndexChanged="dllRole_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>

                                            </div>
                                            <div class="col-md-3">
                                                <label>Amount</label>
                                                <asp:TextBox ID="txtAmount" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Payout From</label>
                                                <asp:DropDownList ID="dllPayoutFrom" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="SoniTechno" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger" Text="Search" OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <br />

                                        <div class="col-md-12">

                                            <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                <thead>
                                                    <tr>
                                                        <th>S.No</th>
                                                        <th>Role</th>
                                                        <th>Amount</th>
                                                        <th>PayoutFrom</th>
                                                       <%-- <th>Edit</th>
                                                        <th>Delete</th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="gvTransactionHistory" runat="server" OnItemCommand="gvTransactionHistory_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Container.ItemIndex + 1 %></td>
                                                                <td><%# Eval("RoleName") %></td>
                                                                    <td><%# Eval("Amount") %></td>
                                                                <td><%# Eval("PayoutFrom") %></td>
                                                                <td>
                                                                    <asp:Button runat="server" ID="btnEdit" Text="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-danger" />
                                                                </td>
                                                                 <td>
                                                                    <asp:Button runat="server" ID="Button1" OnClientClick="return confirm('Do you want to delete?');" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-danger" />
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

