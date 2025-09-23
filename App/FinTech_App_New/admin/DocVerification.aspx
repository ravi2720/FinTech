<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="DocVerification.aspx.cs" Inherits="Admin_DocVerification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="table table-hover mb-0">
                        <tbody>
                            <tr>
                                <th>Application ID</th>
                                <td>
                                    <asp:TextBox ID="txtapplicationId" CssClass="form-control" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search>>" /></td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">Member Details</div>
                                <div class="panel-body">
                                    <table class="table table-hover mb-0">
                                        <tbody>
                                            <tr>
                                                <th>MemberID</th>
                                                <td>
                                                    <asp:Label ID="lblmemberid" runat="server" Text=""></asp:Label></td>
                                                <th>Member Name</th>
                                                <td>
                                                    <asp:Label ID="lblmembername" runat="server" Text=""></asp:Label></td>
                                                <th>Application ID</th>
                                                <td>
                                                    <asp:Label ID="lblapplicationid" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <th>Loan Apply Date</th>
                                                <td>
                                                    <asp:Label ID="lblloanapplydate" runat="server" Text=""></asp:Label></td>
                                                <th>Loan Category</th>
                                                <td>
                                                    <asp:Label ID="lblloancategory" runat="server" Text=""></asp:Label></td>
                                                <th>Loan Sub Category</th>
                                                <td>
                                                    <asp:Label ID="lblloansubcategory" runat="server" Text=""></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <th>Loan Amount</th>
                                                <td>
                                                    <asp:Label ID="lblloanamt" runat="server" Text=""></asp:Label></td>
                                                <th>Mobile No</th>
                                                <td>
                                                    <asp:Label ID="lblmobileno" runat="server" Text=""></asp:Label></td>
                                                <th>Email ID</th>
                                                <td>
                                                    <asp:Label ID="lblemailid" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <th>DOB</th>
                                                <td>
                                                    <asp:Label ID="lbldob" runat="server" Text=""></asp:Label></td>
                                                <th>Loan Purpose</th>
                                                <td>
                                                    <asp:Label ID="lblloanpurpose" runat="server" Text=""></asp:Label></td>
                                                <th>Address</th>
                                                <td>
                                                    <asp:Label ID="lbladdress" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>
                    <br />

                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">Member Doc</div>
                                <div class="panel-body">
                                    <table class="table table-hover mb-0" style="width: 60%; margin-left: 20%;">
                                        <tbody>
                                            <tr>
                                                <th>Sr No</th>
                                                <th>Name</th>
                                                <th>Document</th>
                                            </tr>

                                            <asp:Repeater ID="rptdata" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#Container.ItemIndex+1 %></td>
                                                        <td>
                                                            <asp:Label ID="lbldocname" runat="server" Text='<%#Eval("DocumentName") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdndocid" runat="server" Value='<%#Eval("DOCUMENTID") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="Image1" ImageUrl='<%#"./images/Loan/"+Eval("Image") %>' Height="150px" Width="150px" runat="server" /></td>
                                                        <td></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr>
                                                <th>Approve Date</th>
                                                <td>
                                                    <asp:TextBox ID="txtdate" type="date" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:Button ID="btnapproveall" OnClick="btnapproveall_Click" runat="server" CssClass="btn btn-primary" Text="Approve" /></td>
                                                <td></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end col -->

                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>


