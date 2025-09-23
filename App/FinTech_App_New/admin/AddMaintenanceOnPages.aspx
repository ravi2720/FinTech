<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AddMaintenanceOnPages.aspx.cs" Inherits="Admin_AddMaintenanceOnPages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">
        <section class="content">

            <div class="row">
                <div class="box box-primary">
                    <div class="box-body box-profile">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                                <div class="clearfix"></div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <table class="table pge_tbl">
                                            <thead>
                                                <tr>
                                                    <th>Sl</th>
                                                    <th style="width: 300px;">URL</th>
                                                    <th>Page Name</th>
                                                    <th>
                                                        <asp:Button runat="server" Text="Add" OnClick="Unnamed_Click" ID="btnAdd" /></th>
                                                    <th>Remove</th>
                                                    <th>MAINTENANCE ON PAGE</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <asp:Repeater runat="server" ID="rptShortkData" OnItemCommand="rptShortkData_ItemCommand" OnItemDataBound="rptShortkData_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Container.ItemIndex+1 %></td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox1" runat="server" placeholder="Page URL" Font-Bold="true" Text='<%# Eval("PAGEURL") %>' CssClass="form-control" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox2" runat="server" placeholder="Page URL" Font-Bold="true" Text='<%# Eval("PageName") %>' CssClass="form-control" />
                                                            </td>
                                                            <td>
                                                                <asp:Button runat="server" Visible='<%# ((Eval("PAGEURL").ToString()=="") ==true?true:false) %>' ID="Button1" Text="Save" CssClass="btn btn-info btn-sm pull-left m" CommandName="Save" CommandArgument='<%#Eval("ID") %>' />
                                                                <asp:Button runat="server" Visible='<%# ((Eval("PAGEURL").ToString()!="") ==true?true:false) %>' ID="btnUpdate" Text="Update" CssClass="btn btn-info btn-sm pull-left m" CommandName="Update" CommandArgument='<%#Eval("ID") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Button runat="server" ID="btnRemove" Text="Remove" CssClass="btn btn-danger btn-sm pull-left m" CommandName="Remove" CommandArgument='<%#Eval("ID") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Button runat="server" ID="btnADctive" Text='<%# (Convert.ToInt32(Eval("ISACTIVE"))==1?"MAINTENANCE ON":"OFF") %>' CssClass='<%# "btn " + (Convert.ToInt32(Eval("isactive"))==1?"btn-success":"btn-danger")+" btn-sm pull-left m"%>' CommandName="ADbtn" CommandArgument='<%#Eval("ID") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>


                                    </div>
                                    <br />
                                    <br />

                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>

