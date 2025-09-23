<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ProductAdd.aspx.cs" Inherits="Admin_ProductAdd" %>


<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">
        <section class="content">

            <div class="panel panel-primary">
                <div class="panel-heading">Manage Product</div>
                <div class="panel-body">
                    <asp:HiddenField ID="hdnid" Value="0" runat="server" />
                    <div class="form-group">
                        <label for="exampleInputPassword1">Select Category</label>
                        <asp:DropDownList ID="ddlCategory" runat="server" class="form-control m-t-xxs"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">Heading Name</label>
                        <asp:TextBox ID="txtHeadingName" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtHeadingName" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">Description</label>
                        <CKEditor:CKEditorControl ID="ckNewsDesc" runat="server" BasePath="~/CKEditor/"
                            Height="250px" Width="90%">
                        </CKEditor:CKEditorControl>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="ckNewsDesc" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Photo</label>
                        <asp:FileUpload runat="server" ID="fileUploadOtherDoc" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label>Amount</label>
                        <asp:TextBox runat="server" ID="txtAmount" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSubmit_Click" ValidationGroup="chkaddfund" />
                        <asp:Label ID="lblMessage" CssClass="center-block" Style="text-align: center; font-weight: 600" ForeColor="Red" runat="server" />
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>

            <hr />
            <div class="container-fluid">
                <div class="panel panel-primary">
                    <div class="panel-heading">News List</div>
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
                                                            <th>S.No</th>
                                                            <th>Name</th>
                                                            <th>Description</th>
                                                            <th>Action</th>
                                                            <th>Add Date</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="repeater1" runat="server" OnItemCommand="repeater1_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%# Container.ItemIndex+1 %></td>
                                                                    <td><%#Eval("Name") %></td>
                                                                    <td><%#Eval("ShortDescription") %></td>
                                                                   
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
                </div>
            </div>
        </section>
    </div>


</asp:Content>

