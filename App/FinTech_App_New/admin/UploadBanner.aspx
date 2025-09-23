<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="UploadBanner.aspx.cs" Inherits="Admin_UploadBanner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Banner Image Details</div>
                        <div class="card-body">

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label for="dllBannerType">Banner Type : </label>
                                        <asp:DropDownList runat="server" ID="ddlImage" ToolTip="Enter Package" class="form-control ValiDationP"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="fileImage">Select Image : </label>
                                        <asp:FileUpload runat="server" ID="fileUpload" ToolTip="Upload Image" class="form-control ValiDationP"></asp:FileUpload>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="chkActive">Active : </label>
                                        <asp:CheckBox runat="server" Checked="true" ID="chkActive" ToolTip="Active" class="form-control ValiDationR"></asp:CheckBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary mt-4" />
                                    </div>
                                </div>

                            </div>
                            <table class="table table-hover">
                                <tbody>
                                    <tr>
                                        <th>Active/DeActive</th>
                                        <th>Edit</th>
                                        <th>Banner Type Name</th>
                                        <th>Image</th>
                                        <th>Delete</th>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="row1">
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgDelete" OnClientClick="javascript:return confirm('Are you sure you want to Active/DeActive this Role?');" CommandName="Active" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("IsActive"))==true ? "./images/Active.png":"./images/delete.PNG")  %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ImageUrl="./images/Edit.PNG" Height="20" Width="20" ToolTip="Edit Row" />
                                                </td>
                                                <td><span><%# Eval("Name") %></span></td>
                                                <td>
                                                    <img src='<%# "./images/Banner/"+ Eval("ImagePath").ToString()%>' width="100" height="100" />
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnDelete" CssClass="btn btn-danger" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnImageID" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

