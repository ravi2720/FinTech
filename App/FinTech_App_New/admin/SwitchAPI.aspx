<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="SwitchAPI.aspx.cs" Inherits="Admin_SwitchAPI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="content-wrapper">
        <section class="content">

            <div class="card card-primary">
                <div class="card-header">Change API</div>
                <div class="card-body">
                    <div class="form-group">
                        <label for="ddlpackage">Select Package :</label>
                        <asp:DropDownList runat="server" ID="ddlpackage" AutoPostBack="true" OnSelectedIndexChanged="ddlpackage_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="table-responsive">
                        <table class="table">

                            <thead>
                                <tr>
                                    <th>Operator Name</th>
                                    <th>Code</th>
                                    <th>Default API</th>
                                    <th>Change API</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound" OnItemCommand="rptData_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><span id="comp_89"><%# Eval("Name") %></span></td>
                                            <td><span id="provider_89"><%# Eval("Code") %></span></td>
                                            <td><span id="api_89"><%# Eval("APIName") %></span></td>

                                            <td>

                                                <div class="form-group">
                                                    <asp:DropDownList runat="server" ID="dllAPILIST" CssClass="form-control">

                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Button runat="server" ID="btnChange" Text="Change" CommandName="Change" CommandArgument='<%# Eval("OperatorID") %>' CssClass="btn btn-danger" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </tbody>
                        </table>
                    </div>

                </div>

            </div>
        </section>
    </div>
</asp:Content>



