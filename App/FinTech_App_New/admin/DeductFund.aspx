<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="DeductFund.aspx.cs" Inherits="Admin_DeductFund" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <asp:MultiView ID="multiview1" ActiveViewIndex="0" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="panel panel-primary">
                                <div class="panel-heading">Select Member</div>
                                <div class="panel-body">
                                    <div class="form-group">
                                        <label for="txtAccountNo">Member : </label>
                                        <asp:DropDownList ID="ddlMember" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <div class="panel panel-primary">
                                <div class="panel-heading">Dedcut Balance</div>
                                <div class="panel-body">
                                    <div>
                                        <table class="table table-hover">
                                            <tbody>
                                                <asp:Repeater ID="rptMember" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>Current Balance :</td>
                                                            <td>
                                                                <asp:Label ID="lblBalance" runat="server" Text='<%#Eval("Balance") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Name :</td>
                                                            <td>
                                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Member ID :</td>
                                                            <td>
                                                                <asp:Label ID="lblLoginid" runat="server" Text='<%#Eval("LoginID") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Postal Address :</td>
                                                            <td>
                                                                <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Mobile :</td>
                                                            <td>
                                                                <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("Mobile") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Email ID :</td>
                                                            <td>
                                                                <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>'></asp:Label></td>
                                                        </tr>

                                                        <tr>
                                                            <td>Deduct Amount :</td>
                                                            <td>
                                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" placeholder="Please enter Transfer amount"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Remark</td>
                                                            <td>
                                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" placeholder="Please enter comment"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                                            </td>
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
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

