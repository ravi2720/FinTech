<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="GSTDetails.aspx.cs" Inherits="Member_GSTDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid" id="prepaid">
        <!-- main-content-body -->
        <div class="main-content-body">
            <div class="row row-sm">
                <div class="col-lg-12">
                    <asp:UpdatePanel runat="server" ID="updateRolePanel">
                        <ContentTemplate>
                            <div class="card card-primary">
                                <div class="card-header">Update Your GST</div>
                                <div class="card card-body">
                                    <asp:HiddenField ID="hdnid" Value="0" runat="server" />

                                    <div class="row">
                                        <div class="form-group col-md-4">
                                            <label for="ddlcomp_tyoe">State : </label>
                                            <asp:DropDownList runat="server" ID="ddlState" ToolTip="Select State" class="form-control ValiDationR">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlState" InitialValue="0" Font-Bold="True" ForeColor="Red" ErrorMessage="Please select State" ValidationGroup="chkaddfund" />
                                        </div>

                                        <div class="form-group col-md-4">
                                            <label for="exampleInputPassword1">Firm Name :</label>
                                            <asp:TextBox ID="txtfirmname" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtfirmname" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>

                                        </div>


                                        <div class="form-group col-md-4">
                                            <label for="exampleInputPassword1">GSTNo. :</label>
                                            <asp:TextBox ID="txtGSTNo" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtGSTNo" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group">
                                            <label for="txtMessage">Address :</label>
                                            <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" Rows="5" Columns="5" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtAddress" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <label for="txtMessage">Company Types :</label>
                                            <asp:DropDownList runat="server" ID="dllType" CssClass="form-control">
                                                <asp:ListItem Text="Select Company" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Composition " Value="Composition"></asp:ListItem>
                                                <asp:ListItem Text="Regular" Value="Regular"></asp:ListItem>
                                                <asp:ListItem Text="Non Register" Value="Non Register"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSubmit_Click" ValidationGroup="chkaddfund" />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="ibox float-e-margins">

                                                <div class="ibox-content collapse show">
                                                    <div class="widgets-container">
                                                        <div>

                                                            <table id="example6" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                                <thead>
                                                                    <tr>
                                                                        <th>S.No</th>
                                                                        <th>Firm Name</th>
                                                                        <th>GST No.</th>
                                                                        <th>Address</th>
                                                                        <th>Type</th>
                                                                        <th>Status</th>
                                                                        <th>Action</th>
                                                                        <th>Add Date</th>

                                                                    </tr>
                                                                </thead>
                                                                <tfoot>
                                                                    <tr>
                                                                        <th>S.No</th>
                                                                        <th>Firm Name</th>
                                                                        <th>GST No.</th>
                                                                        <th>Address</th>
                                                                        <th>Type</th>
                                                                        <th>Status</th>
                                                                        <th>Action</th>
                                                                        <th>Add Date</th>

                                                                    </tr>
                                                                </tfoot>
                                                                <tbody>
                                                                    <asp:Repeater ID="repeater1" runat="server" OnItemCommand="repeater1_ItemCommand">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td><%# Container.ItemIndex+1 %></td>
                                                                                <td><%#Eval("FirmName") %></td>
                                                                                <td><%#Eval("GSTNo") %></td>
                                                                                <td><%#Eval("Address") %></td>
                                                                                <td><%#Eval("CompanyType") %></td>
                                                                                <td>
                                                                                    <asp:Button ID="btnStatus" runat="server" CssClass='<%# Convert.ToInt16(Eval("IsActive")) == 1 ? "btn btn-success" : "btn btn-warning" %>' Text='<%#Eval("Status") %>' CommandArgument='<%#Eval("ID") %>' CommandName="Active" />
                                                                                <td>
                                                                                    <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Edit" CommandArgument='<%#Eval("ID") %>' CommandName="Edit" /></td>
                                                                                <td><%#Eval("AddDate") %></td>

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
                                <div class="clearfix"></div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

