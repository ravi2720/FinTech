<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="PANCardTokenType.aspx.cs" Inherits="Admin_PANCardTokenType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updatepanel">
                <ContentTemplate>
                    <div class="row">
                        <div class="box">
                            <!-- /.box-header -->
                            <div class="box-body">

                                <div class="form-group">
                                    <label for="ddlPANType">PAN Card Type</label>
                                    <asp:DropDownList ID="ddlPANType" runat="server" OnSelectedIndexChanged="ddlPANType_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Text="Select Coupon Type" Value="2">Select Coupon Type</asp:ListItem>
                                        <asp:ListItem Text="Physical Coupon" Value="0">Physical Coupon</asp:ListItem>
                                        <asp:ListItem Text="Digital Coupon" Value="1">Digital Coupon</asp:ListItem>

                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ddlPANType" runat="server" ControlToValidate="ddlPANType"
                                        Display="Dynamic" ErrorMessage="Please Select PAN Card Type !" ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtFees">PAN Card Fees</label>
                                    <asp:TextBox ID="txtFees" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFees"
                                        Display="Dynamic" ErrorMessage="Please Enter PAN Card Fees !" ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
                                    &nbsp;&nbsp;&nbsp;
                    
                            <asp:ValidationSummary ID="ValidationSummary" runat="server" ClientIDMode="Static"
                                ValidationGroup="v" />


                                </div>
                                <div class="form-group">
                                    <table class="table table-responsive" id="Cutomers">
                                        <thead>
                                            <th>Sno</th>
                                            <th>TokenType</th>
                                            <th>Fees</th>
                                        </thead>
                                        <asp:Repeater runat="server" ID="rptData">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.ItemIndex+1 %></td>
                                                    <td><%# Eval("PANCARDType") %></td>
                                                    <td><%# Eval("PANFees") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />

                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

