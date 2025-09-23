<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="AddFund.aspx.cs" Inherits="Member_AddFund" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <div class="main-content-body">

            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <asp:MultiView ID="multiview1" ActiveViewIndex="0" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="card card-primary">
                                <div class="card-header"><b>Add Balance</b></div>
                                <div class="card card-body">
                                    <div class="table-responsive"> 
                                        <table class="border-top-0  table table-bordered text-nowrap key-buttons border-bottom">
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
                                                            <td>Add Amount :</td>
                                                            <td>
                                                                <asp:TextBox ID="txtAmount" TextMode="Number" runat="server" CssClass="form-control" placeholder="Please enter Transfer amount"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Remark</td>
                                                            <td>
                                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" placeholder="Please enter comment"></asp:TextBox></td>
                                                        </tr>
                                                        <tr runat="server" id="rowhide" visible="false">
                                                            <td>
                                                                <asp:Label runat="server" id="lblAuth"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtOTP" CssClass="form-control"></asp:TextBox>
                                                            </td>
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


        </div>
    </div>
</asp:Content>

