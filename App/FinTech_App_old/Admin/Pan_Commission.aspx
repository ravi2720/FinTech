<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Pan_Commission.aspx.cs" Inherits="Admin_Pan_Commission" %>

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
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <span class="red">*</span>Package 
   
                            <asp:DropDownList ID="ddlPackage" runat="server" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPackage_SelectedIndexChanged">
                            </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPackage" runat="server" ControlToValidate="ddlPackage"
                                            Display="Dynamic" ErrorMessage="Please select Package !" SetFocusOnError="True"
                                            ValidationGroup="v" InitialValue="0">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">

                                        <label for="ddlPANType">PAN Card Type</label>
                                        <asp:DropDownList ID="ddlPANType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPANType_SelectedIndexChanged"></asp:DropDownList>

                                        <asp:HiddenField ID="hdnidd" runat="server" Value="" />
                                    </div>
                                    <div class="form-group">
                                        PAN Fees
                                        <asp:TextBox ID="txtPANFees" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        Commission
                                        <asp:TextBox ID="txtsurcharged" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        Is Flat
                                        <br />
                                        <asp:CheckBox ID="chkflatd" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnadd" runat="server" CssClass="btn btn-success"
                                            OnClick="btnadd_Click" Text="Add / Update" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="SAVE PLAN" ValidationGroup="v" OnClick="btnSubmit_Click" class="btn btn-success" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-primary" />
                                        <asp:ValidationSummary ID="ValidationSummary" runat="server" ClientIDMode="Static"
                                            ValidationGroup="v" />
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                        <thead>
                                            <tr>
                                                <th>Sr No.</th>
                                                <th>PAN Card Fees</th>
                                                <th>PAN Card Type</th>
                                                <th>Commission</th>
                                                <th>Is Flat</th>
                                                <th>Edit</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="gvOperator" runat="server" OnItemCommand="gvOperator_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Container.ItemIndex+1 %>
                                                        </td>
                                                        <td>
                                                            <span><%# Eval("PANFees") %></span>
                                                        </td>
                                                        <td>
                                                            <span><%# Convert.ToBoolean(Eval("PANType")) == false ? "Physical Coupon" : "Digital Coupon" %></span>
                                                        </td>
                                                        <td>
                                                            <span><%# Eval("surcharge") %></span>
                                                        </td>
                                                        <td>
                                                            <%# Convert.ToBoolean(Eval("IsFlat")) == false ? "No" : "Yes" %>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnedit" runat="server" Text="Edit" CommandName="medit" CssClass="btn btn-warning" CommandArgument='<%# Container.ItemIndex+1 %>' />
                                                            <asp:Button ID="btndelete" runat="server" Text="Delete" CommandName="mdelete" CssClass="btn btn-danger" CommandArgument='<%# Container.ItemIndex+1 %>' />

                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>


                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnadd" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlPackage" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>


</asp:Content>

