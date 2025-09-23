<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="privacy-policy.aspx.cs" Inherits="privacy_policy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Start Page Title Area -->
    <div class="page-title-area item-bg1">
        <div class="d-table">
            <div class="d-table-cell">
                <div class="container">
                    <div class="page-title-content">
                        <h2>Privacy Policy</h2>
                        <ul>
                            <li><a href="index.aspx">Home</a>
                            </li>
                            <li>Privacy Policy</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Page Title Area -->

    <!-- Start Privacy Policy Area -->
    <section class="privacy-policy ptb-100">
        <div class="container">
            <div class="row">
                <asp:Repeater runat="server" ID="rptDataPrivacy">
                    <ItemTemplate>
                        <p>
                            <%# Eval("Description") %>
                        </p>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </div>
    </section>
    <!-- End Privacy Policy Area -->
</asp:Content>

