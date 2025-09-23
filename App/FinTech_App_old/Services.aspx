<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Services.aspx.cs" Inherits="Services" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
            <img src="../assets/frontpage/img/photo/service2.jpg" class="img-fluid" alt="Responsive image" style="width: 100%;">
        </div>
        <!-- cta Wrapper Start -->
    <div class="cta-wrapper">
        <div class="container-fluid">
            <div class="row get_servico_row">
                <div class="col-sm-5 col-xs-12 get_servico_col">
                    <div class="cta-inner cta-text">We improves productivity and profitability of organizations by providing realtime services Support for recharge AEPS,Micro ATM and Security service.</div>
                </div>
                <div class="col-sm-4 col-xs-12 get_servico_col">
                    <div class="cta-inner cta">
                        <img src="../assets/frontpage/img/icon/phonelink-ring_200_transparent.gif" height="100px" width="50px" alt="">
                        <h3>Customer Care<br><span>+91 <%=company.Mobile %></span></h3>
                    </div>
                </div>
                <div class="col-sm-3 col-xs-12 get_servico_col text-center">
                    <div class="cta-inner cta-btn"><a href="Contact_us.aspx">Contact Us<i class="fa fa-arrow-right"></i></a> </div>
                </div>
            </div>
        </div>
    </div>
    <!-- cta Wrapper End -->
    <!-- Our Services Start -->
    <section class="our-services-wrapper">
        <div class="container">
            <div class="title">
                <h2>Our Services</h2>
                <span class="title-border-color"><i class="fa fa-bolt" aria-hidden="true"></i></span>
            </div>
            <div class="row">
                <div class="col-lg-4 col-md-6 col-sm-6">
                    <div class="single-service">
                        <div class="single-services">
                            <div class="services-inner">
                                <div class="our-services-icon"> <span><img src="../assets/frontpage/img/icon/no-mobile-phones_transparent.gif" alt=""></span></div>
                                <div class="our-services-text">
                                    <h3>Prepaid Recharge</h3>
                                    <p>We provides recharge service in India for the mobile networks like Airtel, Idea, Vodafone, Jio, BSNL. Our online recharge service is convenient and fast that facilitates recharge of prepaid mobile at earn highest commission.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6 col-sm-6">
                    <div class="single-service">
                        <div class="single-services">
                            <div class="services-inner">
                                <div class="our-services-icon"> <span><img src="../assets/frontpage/img/icon/computer-display_transparent.gif" alt=""></span></div>
                                <div class="our-services-text">
                                    <h3>DTH Recharge</h3>
                                    <p>We provide online DTH recharge service provider. Our DTH service providers like Airtel DTH, Tata Sky, Dish TV, Sun Direct, Videocon D2H. All you need to have is a Web & App with online recharge of your prepaid DTH account is just few clicks away.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6 col-sm-6">
                    <div class="single-service">
                        <div class="single-services">
                            <div class="services-inner">
                                <div class="our-services-icon"> <span><img src="../assets/frontpage/img/icon/thumb_transparent.gif" alt=""></span></div>
                                <div class="our-services-text">
                                    <h3>Aeps Services</h3>
                                    <p>To build the foundation for a full range of Aadhaar enabled Banking services. Services Offered by Micro ATM & AePS. Cash Withdrawal, Balance Enquiry, Mini Statement,  Aadhaar Pay (Cash Withrawal Only).</p>
                                </div>
                                <!-- GetButton.io widget -->
<script type="text/javascript">
    (function () {
        var options = {
            call: "+919983123568", // Call phone number
            whatsapp: "9983123568", // WhatsApp number
            call_to_action: "Contact us", // Call to action
            button_color: "#A8CE50", // Color of button
            position: "left", // Position may be 'right' or 'left'
            order: "whatsapp,call", // Order of buttons
            pre_filled_message: "Hello, how may we help you? Just send us a message", // WhatsApp pre-filled message
        };
        var proto = 'https:', host = "getbutton.io", url = proto + '//static.' + host;
        var s = document.createElement('script'); s.type = 'text/javascript'; s.async = true; s.src = url + '/widget-send-button/js/init.js';
        s.onload = function () { WhWidgetSendButton.init(host, proto, options); };
        var x = document.getElementsByTagName('script')[0]; x.parentNode.insertBefore(s, x);
    })();
</script>
<!-- /GetButton.io widget -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- Our Services End -->
</asp:Content>

