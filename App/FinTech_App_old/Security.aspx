<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Security.aspx.cs" Inherits="Security" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main class="page-wrapper">
        <!-- Start Team Area  -->
        <div class="main-content">
            <!--Start tab Style Two  -->
            <div id="desktoptc" class="rwt-tab-area rn-section-gap">
                <div class="container">
                    <asp:Repeater runat="server" ID="rptDataT">
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-lg-12">
                                    <h1 class="title theme-gradient"><%# Eval("Name") %></h1>
                                </div>
                            </div>
                            <div class="row row row--30 align-items-center">
                                <div class="col-lg-12 order-1 order-lg-2">
                                    <div class="rn-tab-content tab-content">
                                        <div class="tab-pane fade row box-layout box-shadow show active" id="introducton"
                                            role="tabpanel" aria-labelledby="introducton-tab">
                                            <p>
                                                <%# Eval("Description") %>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="rbt-separator-mid">
                <div class="container">
                    <hr class="rbt-separator m-0">
                </div>
            </div>
        </div>
    </main>
</asp:Content>



