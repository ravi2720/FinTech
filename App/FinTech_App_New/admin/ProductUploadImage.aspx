<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ProductUploadImage.aspx.cs" Inherits="Admin_ProductUploadImage" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Product Image/Video </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <label>Select Uplaod Type</label>
                                    <asp:DropDownList runat="server" ID="ddlType" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" CssClass="form-control">
                                        <asp:ListItem Text="Image" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Video" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label for="exampleInputPassword1">Upload Images : </label>
                                    <asp:FileUpload runat="server" ID="flieUpload" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                    <label for="exampleInputPassword1">IsActive : </label>
                                    <asp:CheckBox runat="server" ID="chkActive" Checked="true" />
                                </div>
                                <div class="col-md-3 mt-2">
                                    <asp:Button runat="server" ID="btnUplaod" CssClass="btn btn-primary mt-2" OnClick="btnUplaod_Click" Text="Upload" />
                                </div>
                            </div>
                            <div class="row mt-5">
                                <div class="table-responsive">
                                    <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>Action</th>
                                                <th>Type</th>
                                                <th>Status</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Container.ItemIndex+1 %>
                                                        </td>
                                                        <td>
                                                            <asp:Button runat="server" ID="btnDelete" CommandName="Delete"  CommandArgument='<%# Eval("ID") %>' Text="Delete" OnClientClick="return confirm('Do You Want to Dlete')" CssClass="btn btn-danger" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("ImageType") %>
                                                        </td>
                                                        <td>

                                                            <img src='<%# "./images/"+Eval("URL").ToString() %>' style='<%# "display:"+(Eval("ImageType").ToString()=="1"?"": "none") %>' height="400" />

                                                            <video width="320" height="240" controls style='<%# "display:"+(Eval("ImageType").ToString()=="1"?"none": "") %>'>
                                                                <source src='<%# "./images/"+Eval("URL").ToString() %>' type="video/ogg">
                                                            </video>
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
                    <asp:PostBackTrigger ControlID="btnUplaod" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

