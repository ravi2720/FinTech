<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ContentMasterFLoan.aspx.cs" Inherits="Admin_ContentMasterFLoan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                       </div>
                  <div class="panel panel-primary">
                      <div class="panel panel-primary">
                          <div class="panel-heading">Content Master</div>
                          <div class="panel-body">
                              <div class="row">
                                        <table class="table table-hover mb-0">
                                            <tbody>
                                                <tr>
                                                    <th scope="row" style="text-align: right;">Title</th>
                                                    <td>
                                                        <asp:TextBox ID="txttitle" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <th scope="row" style="text-align: right; padding-top: 15%;">Content</th>
                                                    <td>
                                                        <asp:TextBox ID="txtcontent" TextMode="MultiLine" CssClass="form-control" runat="server" Rows="10"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <th scope="row" style="text-align: right;">Photo</th>
                                                    <td>
                                                        <asp:FileUpload ID="flupld" CssClass="form-control" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="btnsearch" runat="server" OnClick="btnsearch_Click" CssClass="btn btn-primary" Text="Submit" /></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- end col -->
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnsearch" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

