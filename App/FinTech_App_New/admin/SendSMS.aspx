<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="SendSMS.aspx.cs" Inherits="Admin_SendSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div class="content-wrapper">
                <section class="content">
                    <asp:UpdatePanel runat="server" ID="updateRolePanel">
                        <ContentTemplate>
                            <div class="panel panel-primary">
                                <div class="panel-heading">Manage SMS</div>
                                <div class="panel-body">
                                    <asp:HiddenField ID="hdnid" Value="0" runat="server" />
                                    <div class="form-group">
                                        <label for="exampleInputPassword1">MemberID(For Single Person Message)</label>
                                        <asp:TextBox runat="server" ID="txtMemberID" CssClass="form-control" ></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputPassword1">Description</label>
                                        <asp:TextBox runat="server" ID="txtSMS" CssClass="form-control">

                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtSMS" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSubmit_Click" ValidationGroup="chkaddfund" />
                                        <asp:Label ID="lblMessage" CssClass="center-block" Style="text-align: center; font-weight: 600" ForeColor="Red" runat="server" />
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <hr />
                    <div class="container-fluid">
                        <div class="panel panel-primary">
                            <div class="panel-heading">News List</div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="ibox float-e-margins">

                                            <div class="ibox-content collapse show">
                                                <div class="widgets-container">
                                                    <div>

                                                        <table id="example6" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                            <thead>
                                                                <tr>
                                                                    <th>S.No</th>
                                                                    <th>Description</th>
                                                                    <th>Add Date</th>
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th>S.No</th>
                                                                    <th>Description</th>
                                                                    <th>Add Date</th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody>
                                                                <asp:Repeater ID="repeater1" runat="server" OnItemCommand="repeater1_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%# Container.ItemIndex+1 %></td>
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
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </section>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

