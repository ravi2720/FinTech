<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="UploadData.aspx.cs" Inherits="Admin_UploadData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="btnSend" runat="server" Text="Export" onclick="btnSend_Click"  />
</asp:Content>

