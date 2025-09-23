<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ProductFunctionList.aspx.cs" Inherits="Admin_RechargeHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="updateRolePanel">
        <ContentTemplate>
            <div class="content-wrapper">
                <section class="content">
                    <div class="card card-primary">
                        <div class="card-header">Product Funtion List</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Repeater runat="server" ID="rptDataRecharge" OnItemCommand="rptDataRecharge_ItemCommand">
                                        <ItemTemplate>
                                            <div class="form-group">
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="FP" CommandArgument='<%#Eval("ID") %>'> <%#Eval("Name") %></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="col-md-8">
                                    <div class="row" id="divfeature" runat="server" visible="false">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <label for="exampleInputPassword1">Feature Name  : </label>
                                                <asp:TextBox ID="txtfeature" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label for="exampleInputPassword1">BackGround Color  : </label>
                                                <input type="color" runat="server" id="bgcolor" />
                                            </div>
                                            <div class="col-md-3">
                                                <label for="exampleInputPassword1">Text Color  : </label>
                                                <input type="color" runat="server" id="txtColor" />
                                            </div>
                                            <div class="col-md-3">

                                                <asp:Button runat="server" ID="btnSave" Text="Submit" CssClass="btn btn-danger mt-2" OnClick="btnSave_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-12 mt-2">
                                            <div class="table-responsive">
                                                <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>S.No</th>
                                                            <th>Header</th>
                                                            <th>BackGround Color</th>
                                                            <th>Text Color</th>
                                                            <th>Add Product</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater runat="server" ID="rptFData" OnItemCommand="rptFData_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%# Container.ItemIndex+1 %></td>
                                                                    <td>
                                                                        <asp:Button runat="server" ID="btnEditFP" CssClass="btn btn-danger" Text="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' />
                                                                    </td>
                                                                    <td><%#Eval("Header") %></td>
                                                                    <td><%#Eval("BackGroundColor") %></td>
                                                                    <td><%#Eval("textcolor") %></td>
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
                </section>
            </div>
            <asp:HiddenField runat="server" ID="hdnFpEditID" Value="0" />
            <asp:HiddenField runat="server" ID="hdnFpID" Value="0" />
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

