<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="EKYC.aspx.cs" Inherits="Admin_EKYC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
               
                        <asp:MultiView ID="multiview1" ActiveViewIndex="0" runat="server">
                            <asp:View ID="View1" runat="server">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">Select Member</div>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <label for="txtAccountNo">Member : </label>
                                            <asp:TextBox ID="txtMember" class="form-control" AutoPostBack="true" OnTextChanged="txtMember_TextChanged" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                             <table class="table table-hover">
                                                <tbody>
                                                    <tr>
                                                       <td>
                                                           <div class="form-group">
                                                               <label>Name:-</label>
                                                               <asp:Label runat="server" ID="tblName"></asp:Label>
                                                           </div>
                                                       </td>
                                                        <td>
                                                           <div class="form-group">
                                                               <label>BCID:-</label>
                                                               <asp:Label runat="server" ID="lblBCID"></asp:Label>
                                                           </div>
                                                       </td>
                                                        <td>
                                                           <div class="form-group">
                                                               <label>Mobile:-</label>
                                                               <asp:Label runat="server" ID="lblMobile"></asp:Label>
                                                           </div>
                                                       </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Upload KYC</td>
                                                        <td>
                                                            <asp:FileUpload ID="fileKYC" runat="server" /></td>
                                                        <td>
                                                            <asp:Button runat="server" ID="btnkycUpload" Text="Upload" CssClass="btn btn-danger" OnClick="btnkycUpload_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblKYCStatus"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Upload Address KYC</td>
                                                        <td>
                                                            <asp:FileUpload ID="fileAddressKYC" runat="server" /></td>
                                                        <td>
                                                            <asp:Button runat="server" ID="btnAddressKYC" Text="Upload" CssClass="btn btn-danger" OnClick="btnAddressKYC_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblAddressKYC"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Upload ShopPhoto KYC</td>
                                                        <td>
                                                            <asp:FileUpload ID="fileShop" runat="server" /></td>
                                                        <td>
                                                            <asp:Button runat="server" ID="btnShop" Text="Upload" CssClass="btn btn-danger" OnClick="btnShop_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblEShop"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Upload PassPortSize Photo</td>
                                                        <td>
                                                            <asp:FileUpload ID="filePhoto" runat="server" /></td>
                                                        <td>
                                                            <asp:Button runat="server" ID="btnPhoto" Text="Upload" CssClass="btn btn-danger" OnClick="btnPhoto_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblPhoto"></asp:Label>
                                                        </td>
                                                    </tr>

                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                           
                                
                            
                        </asp:MultiView>
                   
                    
            </div>
        </section>
    </div>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>

