<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="OffLineTransactionList.aspx.cs" Inherits="Admin_OffLineTransactionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:updatepanel runat="server" id="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">OffLine Service Details</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="ddlMember">From Date : </label>
                                    <asp:TextBox ID="txtfromdate"  TextMode="Date" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                    
                                </div>
                                <div class="col-md-2">
                                    <label for="ddlMember">To Date : </label>
                                    <asp:TextBox ID="txttodate" autocomplete="off" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                    
                                </div>
                                
                                <div class="col-md-2 mt-4">
                                    <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger" Text="Search" OnClick="btnSearch_Click" />
                                </div>

                            </div>
                            <table class="table table-hover">
                                <tbody>
                                    <tr>
                                        <th>SNO</th>
                                        <th>Form Name</th>
                                        <th>LoginID</th>
                                        <th>Price</th>
                                        <th>Status</th>
                                        <th>Date</th>
                                        <th>View</th>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptData" >
                                        <ItemTemplate>
                                            <tr class="row1">
                                               <td>
                                                   <%# Container.ItemIndex+1 %>
                                               </td>
                                                <td><span><%# Eval("ServiceName") %></span></td>
                                                <td><span><%# Eval("LoginID") %></span></td>
                                                <td><span><%# Eval("Amount") %></span></td>
                                                <td><span><%# Eval("Status") %></span></td>
                                                <td><span><%# Eval("AddDate") %></span></td>
                                                <td>
                                                    <a href='<%# "OffLineTransaction.aspx?FormID="+Eval("ID").ToString() %>' style="color:black">View</a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:updatepanel>
        </section>
    </div>
</asp:Content>




