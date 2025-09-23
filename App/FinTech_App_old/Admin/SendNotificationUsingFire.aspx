<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="SendNotificationUsingFire.aspx.cs" Inherits="Admin_SendNotificationUsingFire" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">
        <section class="content">

            <div class="panel panel-primary">
                <div class="panel-heading">Manage Notification</div>
                <div class="panel-body">
                    <asp:HiddenField ID="hdnid" Value="0" runat="server" />
                    <div class="form-group">
                        <label for="exampleInputPassword1">MemberID(For Single Person Message)</label>
                        <asp:TextBox runat="server" ID="txtMemberID" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">Heading</label>
                        <asp:TextBox ID="txtNewsName" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtNewsName" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <label for="exampleInputPassword1">Description</label>
                        <asp:TextBox runat="server" ID="ckNewsDesc" CssClass="form-control"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="ckNewsDesc" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label for="fileImage">Select Image : </label>
                        <asp:FileUpload runat="server" ID="fileUpload" ToolTip="Upload Image" class="form-control ValiDationP"></asp:FileUpload>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSubmit_Click" ValidationGroup="chkaddfund" />
                        <asp:Label ID="lblMessage" CssClass="center-block" Style="text-align: center; font-weight: 600" ForeColor="Red" runat="server" />
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
        </section>
    </div>

</asp:Content>

