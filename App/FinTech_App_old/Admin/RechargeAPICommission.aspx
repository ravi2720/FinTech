<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="RechargeAPICommission.aspx.cs" Inherits="Admin_RechargeAPICommission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>

                    <div class="panel panel-primary">
                        <div class="panel-heading">API Commission List</div>
                        <div class="panel-body">
                            <div class="page-header">
                                <h1>
                                    <asp:Label ID="lblAddEdit" runat="server"></asp:Label></h1>
                            </div>
                            <asp:GridView ID="gvOperator" runat="server" CssClass="table table-bordered table-dataTable gridView" AutoGenerateColumns="false"
                                Width="100%" ShowHeaderWhenEmpty="true" Font-Size="10px">
                                <Columns>
                                    <asp:BoundField DataField="OperatorID" />
                                    <asp:BoundField HeaderText="Service Type" DataField="ServiceTypeName" />
                                    <asp:BoundField HeaderText="Operator Name" DataField="OperatorName" />
                                    <asp:TemplateField HeaderText="Operator Code">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOperatorCode" runat="server" Width="50" Text='<%# Eval("OPCode") %>' Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Commission">
                                        <ItemTemplate>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCommission" runat="server" Width="50" MaxLength="5" Text='<%# String.IsNullOrEmpty(Convert.ToString(Eval("Commission"))) ? "0" : Eval("Commission") %>'></asp:TextBox>

                                            <asp:CheckBox ID="chkIsCommissionFlat" runat="server" Text="Is Flat" Style="float: left" Checked='<%# Convert.ToBoolean(Eval("IsCommissionFlat")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Surcharge">
                                        <ItemTemplate>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSurcharge" runat="server" Width="50" MaxLength="5" Text='<%# String.IsNullOrEmpty(Convert.ToString(Eval("Surcharge"))) ? "0" : Eval("Surcharge") %>'></asp:TextBox>


                                            <asp:CheckBox ID="chkIsSurchargeFlat" runat="server" Text="Is Flat" Style="float: left" Checked='<%# Convert.ToBoolean(Eval("IsSurchargeFlat")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="RowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerSettings Position="Bottom" />
                            </asp:GridView>
                        </div>
                    </div>
                    </div>
                </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

