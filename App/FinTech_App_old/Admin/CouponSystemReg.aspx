<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="CouponSystemReg.aspx.cs" Inherits="Admin_CouponSystemReg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Assign Coupon System</div>
                        <div class="panel-body">
                            <asp:HiddenField ID="hdnid" runat="server" />
                            <div class="form-group">
                                <label for="exampleInputPassword1">
                                    Select Member
                                </label>
                                <asp:DropDownList ID="ddlMemberList" ClientIDMode="Static" runat="server" class="form-control m-t-xxs"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label for="exampleInputPassword1">Select Role</label>
                                <asp:DropDownList ID="dllRole" ClientIDMode="Static" runat="server" class="form-control m-t-xxs"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Count Registration</label>
                                <asp:TextBox runat="server" ID="txtCount" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Amount</label>
                                <asp:TextBox runat="server" ID="txtAmount" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button runat="server" ID="btnSubmit" OnClick="btnAssign_Click" Text="Submit" CssClass="btn btn-danger mt-2" />
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <table class="table table-responsive">
                                        <thead>
                                            <th>Sno</th>
                                            <th>Parent MemberID</th>
                                            <th>Child MemberID</th>
                                            <th>Role Name</th>
                                            <th>Registration Count</th>
                                        </thead>

                                        <asp:Repeater runat="server" ID="rptAllPackage">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.ItemIndex+1 %></td>
                                                    <td><%# Eval("ParentLoginID") %></td>
                                                    <td><%# Eval("ChildLoginID") %></td>
                                                    <td><%# Eval("RoleName") %></td>
                                                    <td><%# Eval("RoleRegCount") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="panel panel-primary">
                        <div class="panel-body">
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dllRole" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>

</asp:Content>

