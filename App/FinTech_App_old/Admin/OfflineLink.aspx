<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="OfflineLink.aspx.cs" Inherits="Admin_OfflineLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">OFFLine Service Details</div>
                        <div class="card-body">

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">ServiceName : </label>
                                        <asp:TextBox runat="server" ID="txtServiceName" ToolTip="Enter ServiceName" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">Service URL : </label>
                                        <asp:TextBox runat="server" ID="txtURL" ToolTip="Enter URL" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">Image : </label>
                                        <asp:FileUpload runat="server" ID="flieUpload" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="chkActive">Active : </label>
                                        <asp:CheckBox runat="server" ID="chkActive" Checked="true" ToolTip="Active" class="form-control ValiDationR"></asp:CheckBox>
                                    </div>
                                </div>
                            </div>

                            <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary m-2" />


                            <table class="table table-hover">
                                <tbody>
                                    <tr>
                                        <th>Active/DeActive</th>
                                        <th>Edit</th>
                                        <th>Service Name</th>
                                        <th>Service URL</th>
                                        <th>Image</th>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="row1">
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgDelete" OnClientClick="javascript:return confirm('Are you sure you want to Active/DeActive this Role?');" CommandName="Active" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("IsActive"))==true ? ConstantsData.ActiveIcon:ConstantsData.DeActiveIcon)  %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# ConstantsData.EditIcon %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                                <td><span><%# Eval("Name") %></span></td>
                                                <td><span><%# Eval("Link") %></span></td>
                                                <td>
                                                    <asp:Image runat="server" ID="img" Width="100" Height="100" ImageUrl='<%# "../images/Service/"+Eval("Icon").ToString() %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnServiceID" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>

</asp:Content>


