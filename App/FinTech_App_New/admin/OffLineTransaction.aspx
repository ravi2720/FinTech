<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="OffLineTransaction.aspx.cs" Inherits="Admin_OffLineTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        a {
            color: blue !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <div class="card card-primary">
                    <div class="card-header" id="divHeading" runat="server"></div>
                    <div class="card-body">
                        <table id="tbldynamicform" class="table table-responsive">
                            <tr>
                                <td>
                                    <asp:Panel ID="placeholder" runat="server">
                                        <asp:Table runat="server" CssClass="table table-responsive" ID="tbldynamic"></asp:Table>
                                    </asp:Panel>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                     <label>Select Status</label>
                                    <asp:DropDownList runat="server" ID="dllStatus" CssClass="form-control">
                                        <asp:ListItem Text="Select Status" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Success" Value="Success"></asp:ListItem>
                                        <asp:ListItem Text="Failed" Value="Failed"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <label>Remark</label>
                                    <asp:TextBox runat="server" ID="txtRemark" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-danger" />
                                </td>
                            </tr>

                        </table>
                    </div>

                </div>
            </div>
        </section>
    </div>
</asp:Content>

