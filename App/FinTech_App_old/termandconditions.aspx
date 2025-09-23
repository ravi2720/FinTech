<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="termandconditions.aspx.cs" Inherits="about_us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <!-- about -->
    <div class="about-inner py-5 mt-5">
        <div class="container pb-xl-5 pb-lg-3">

            
                    <asp:Repeater runat="server" ID="rptDataT">
                           <ItemTemplate>
            <div class="row py-xl-4">
                <div class="col-lg-8 about-right-faq pr-5 .w3ls_banner_txt">
                    <h1 class="mt-4 mb-3 w3ls_pvt-title">
                        
                       <%# Eval("Name") %>
                    </h1>
                    <p>
                             <%# Eval("Description") %>
                    </p>
                    <!-- <p class="mt-2">Suspendisse porta erat sit amet eros sagittis, quis hendrerit libero aliquam. Fusce semper augue
                    ac dolor
                    efficitur.</p> -->
                    <!-- <a href="about.html" class="btn button-style mt-sm-5 mt-4">Read More</a> -->
                </div>
                <div class="col-lg-4 welcome-right text-center mt-lg-0 mt-5">
                    <img src="public/images/index/about.png" alt="" class="img-fluid" />
                </div>
            </div>
        </div>

        </ItemTemplate>
                        </asp:Repeater>

    </div>
        </div>
    <!-- //about -->

</asp:Content>

