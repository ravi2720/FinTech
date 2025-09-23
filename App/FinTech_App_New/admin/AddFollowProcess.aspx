<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AddFollowProcess.aspx.cs" Inherits="Reatiler_ManageNews" %>



<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div class="content-wrapper">
                <section class="content">
                    <asp:UpdatePanel runat="server" ID="updateRolePanel">
                        <ContentTemplate>
                            <div class="panel panel-primary">
                                <div class="panel-heading">Manage Process</div>
                                <div class="panel-body">
                                    <asp:HiddenField ID="hdnid" Value="0" runat="server" />

                                    <div id="messagediv" class="form-group">
                                        <label for="ddlcomp_tyoe">Subject : </label>
                                        <asp:DropDownList runat="server" ID="dllRole" ToolTip="Select Role" class="form-control ValiDationR">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="dllRole" InitialValue="0" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtMessage">Name :</label>
                                        <asp:TextBox runat="server" ID="txtMessage" TextMode="MultiLine" Rows="5" Columns="5" ToolTip="Enter Message Regarding Your Complain" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtMessage" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group">
                                        <label for="exampleInputPassword1">Description</label>

                                        <CKEditor:CKEditorControl ID="ckNewsDesc" runat="server" BasePath="~/CKEditor/"
                                            Height="250px" Width="90%">
                                        </CKEditor:CKEditorControl>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="ckNewsDesc" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSubmit_Click" ValidationGroup="chkaddfund" />
                                        <asp:Label ID="lblMessage" CssClass="center-block" Style="text-align: center; font-weight: 600" ForeColor="Red" runat="server" />
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <hr />

                    <div class="panel panel-primary">
                        <div class="panel-heading">Process List</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="ibox float-e-margins">

                                        <div class="ibox-content collapse show">
                                            <div class="widgets-container">
                                                <div>

                                                    <table id="example6" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th>S. No.</th>
                                                                <th>Role Name</th>
                                                                <th>Subject</th>
                                                                <th>Message</th>
                                                                <th>Date</th>
                                                                <th>Action</th>

                                                            </tr>
                                                        </thead>
                                                        <tfoot>
                                                            <tr>
                                                                <th>S. No.</th>
                                                                <th>Role Name</th>
                                                                <th>Subject</th>
                                                                <th>Message</th>
                                                                <th>Date</th>
                                                                <th>Action</th>

                                                            </tr>
                                                        </tfoot>
                                                        <tbody>
                                                            <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr class="row1">
                                                                        <td><%# Container.ItemIndex+1 %></td>
                                                                        <td><%# Eval("RoleName") %></td>
                                                                        <td><span><%# Eval("Name") %></span></td>
                                                                        <td><span><%# Eval("DESCRIPTION") %></span></td>
                                                                        <td><span><%# Eval("AddDate") %></span></td>
                                                                        <td>
                                                                            <asp:ImageButton runat="server" ID="imgDelete" OnClientClick="javascript:return confirm('Are you sure you want to Active/DeActive this Role?');" CommandName="Active" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("IsActive"))==true ? "./images/Active.png":"./images/delete.PNG")  %>' Height="20" Width="20" ToolTip="Delete Row" />

                                                                            <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Edit" CommandArgument='<%#Eval("ID") %>' CommandName="Edit" /></td>

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


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

