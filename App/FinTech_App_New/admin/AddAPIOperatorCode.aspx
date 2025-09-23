<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AddAPIOperatorCode.aspx.cs" Inherits="Admin_AddAPIOperatorCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Complain List</div>
                        <div class="panel-body">
                            <asp:GridView ID="gvOperator" runat="server" CssClass="table table-bordered table-hover tablesorter gridView" AutoGenerateColumns="false"
                                Width="100%" ShowHeaderWhenEmpty="true" Font-Size="10px">
                                <Columns>
                                    <asp:BoundField DataField="ID" />
                                    <asp:BoundField HeaderText="Service Type" DataField="ServiceTypeName" />
                                    <asp:BoundField HeaderText="Operator Name" DataField="Name" />
                                    <asp:TemplateField HeaderText="Operator Code">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOperatorCode" runat="server" CssClass="form-control" Width="50" Text='<%# Eval("OPCode") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Commission">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCommission" runat="server" CssClass="form-control" Width="50" MaxLength="5" Text='<%# String.IsNullOrEmpty(Convert.ToString(Eval("Commission"))) ? "0" : Eval("Commission") %>'></asp:TextBox>

                                            <asp:CheckBox ID="chkCommissionIsFlat" runat="server" CssClass="form-control" Text="Is Flat" Style="float: left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Surcharge" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSurcharge" runat="server" CssClass="form-control" Width="50" MaxLength="5" Text='<%# String.IsNullOrEmpty(Convert.ToString(Eval("Surcharge"))) ? "0" : Eval("Surcharge") %>'></asp:TextBox>

                                            <asp:CheckBox ID="chkSurchargeIsFlat" runat="server" Text="Is Flat" Style="float: left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="RowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerSettings Position="Bottom" />
                            </asp:GridView>
                            <asp:Button runat="server" ID="btnsubmit" CssClass="btn btn-primary" Text="Submit" OnClick="btnsubmit_Click" />
                        </div>
                    </div>
                    </div>
                <asp:HiddenField runat="server" ID="hdnAPIID" Value="0" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

