<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="EmailTemplate.aspx.cs" Inherits="Admin_EmailTemplate" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="app-content content ">
        <div class="content-overlay"></div>
        <div class="header-navbar-shadow"></div>
        <div class="content-wrapper container-xxl p-0" id="appbranch">
            <div class="content-body">
                <section id="ApiKeyPage">
                    <div class="card">
                        <div class="card-header pb-0">
                            <h4 class="card-title">Manage SMSTemplate</h4>
                        </div>
                        <div class="card-body">
                            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label for="txtAccountNo">SMS Template : </label>
                                                <asp:DropDownList runat="server" ID="dllSMSCategory" ToolTip="Enter SMSCategory" class="form-control ValiDationR"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label for="txtAccountNo">Template ID : </label>
                                                <asp:TextBox runat="server" ID="txtTemplateID" ToolTip="Enter Template ID" class="form-control ValiDationR"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <label for="chkActive">Active : </label>
                                                <asp:CheckBox runat="server" Checked="true" ID="chkActive" ToolTip="Active" class="form-control ValiDationR"></asp:CheckBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary me-1 mt-4 waves-effect waves-float waves-light" />
                                            </div>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-12">
                                                
                                                <CKEditor:CKEditorControl ID="txtTemplateName" runat="server" BasePath="~/CKEditor/"
                                                    Height="250px" Width="90%">
                                                </CKEditor:CKEditorControl>
                                            </div>
                                        </div>

                                    </div>

                                    <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                        <thead>

                                            <tr>
                                                <th>S.No</th>
                                                <th>Category Name</th>
                                                <th>Template Name</th>
                                                <th>Template ID</th>
                                                <th>Edit</th>
                                                <th>Add Date</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptData" runat="server" OnItemCommand="repeater1_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex+1 %></td>
                                                        <td><%#Eval("CategoryName") %></td>
                                                        <td><%#Eval("Template") %></td>
                                                        <td><%#Eval("TemplateID") %></td>
                                                        <td>
                                                            <asp:Button runat="server" ID="btnEdit" Text="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-danger" />
                                                        </td>
                                                        <td><%#Eval("AddDate") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>

                                    <asp:HiddenField runat="server" ID="hdnRoleID" Value="0" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </div>
    <script>
        $(function () {
            Action(DataObject);
        });

        var DataObject = {
            ID: "example1",
            iDisplayLength: 5,
            bPaginate: true,
            bFilter: true,
            bInfo: true,
            bLengthChange: true,
            searching: true
        };
        function LoadData() {

            Action(DataObject);
        }

        var req = Sys.WebForms.PageRequestManager.getInstance();
        req.add_beginRequest(function () {

        });
        req.add_endRequest(function () {
            LoadData();

        });

    </script>
</asp:Content>



