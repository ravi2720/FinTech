<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <!-- Slider -->
        <div id="wowslider-container2">




<%--            <div class="ws_images">
                <ul>
                    <li><img src="assets/img/frontend/slide.png" alt="default_slider" title="slide1" id="wows2_0" /></li>

                </ul>
            </div>--%>




                <div class="ws_images">
                    <ul>
                        
              <asp:Repeater runat="server" ID="rptDashBoardBanner">


            <ItemTemplate>

                        <li>
                            <img src='<%# "../images/Banner/"+ Eval("ImagePath").ToString()%>'" alt="default_slider" title="slide1" id="wows2_0" /></li>
                        
            </ItemTemplate>
        </asp:Repeater>

                    </ul>
                </div>




        </div>
        <script type="text/javascript" src="assets/frontpage/slider/engine2/wowslider.js"></script>
        <script type="text/javascript" src="assets/frontpage/slider/engine2/script.js"></script>
        <!-- End Slider -->
        <div role="main" class="main">
            <div class="home-intro home-intro-quaternary" id="home-intro">
                <div class="container">

                    <div class="row text-center">
                        <div class="col">
                            <p class="mb-0">
                                The fastest way to grow your business with the leader in <span class="highlighted-word highlighted-word-animation-1 highlighted-word-animation-1-light text-color-light font-weight-semibold text-5">Banking Services</span>
                                <span>Check out our options and features included.</span>
                            </p>
                        </div>
                    </div>

                </div>
            </div>
            <section class="shadow-md pt-4 pb-3" style="background-color:#dce3ea; margin-top: -60px; margin-bottom: -30px;">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <h2><b>Benefits of Using <%=company.Name %> Services</b></h2>
                            <p class="section-subtitle">We are providing the market’s best banking and online mobile recharge services.</p>
                        </div>
                        <div class="col-md-4">
                            <div class="main-services">
                                <img src="assets/frontpage/img/photo/1.png" class="width-100 img-responsive" width="290" height="205" alt="pic">
                                <h4>Earn Extra Profit</h4>
                                <p style="font-size: 13px;">You can earn extra profit on each and every transaction of your customers.</p>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="main-services">
                                <img src="assets/frontpage/img/photo/2.png" class="width-100 img-responsive" width="290" height="205" alt="pic">
                                <h4>One Stop Solution</h4>
                                <p style="font-size: 13px;">You can provide one stop solution of all related services to your customers. </p>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="main-services">
                                <img src="assets/frontpage/img/photo/3.png" class="width-100 img-responsive" width="290" height="205" alt="pic">
                                <h4>Attract More Customers</h4>
                                <p style="font-size: 13px;">You can attract more customers to your shop by providing fast and secure services.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <section class="section bg-color-grey-scale-1 section-height-3 section-no-border appear-animation mycolor" data-appear-animation="fadeIn" style="background-repeat: no-repeat; background-size: cover;">
                <div class="container">
                    <div class="row">
                        <div class="col text-center appear-animation" data-appear-animation="fadeInUpShorter" data-appear-animation-delay="200">
                            <h2 class="font-weight-normal text-6 mb-5 text-white">Our <strong class="font-weight-extra-bold text-white">Services</strong></h2>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-4 mb-4 appear-animation" data-appear-animation="fadeInLeftShorter" data-appear-animation-delay="400">
                            <div class="feature-box feature-box-secondary feature-box-style-4">
                                <div class="feature-box-icon">
                                    <img src="assets/frontpage/img/icon/mobile.png" height="70" width="60">
                                </div>
                                <div class="feature-box-info">
                                    <h4 class="mb-2 font-weight-bold">Recharge Services</h4>
                                    <p class="text-white">We provide Online/Offline Mobile, DTH and Data Recharge Services in India for all the Leading Operators.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 mb-4 appear-animation" data-appear-animation="fadeIn" data-appear-animation-delay="200">
                            <div class="feature-box feature-box-secondary feature-box-style-4">
                                <div class="feature-box-icon">
                                    <img src="assets/frontpage/img/icon/it.png" height="90" width="100">
                                </div>
                                <div class="feature-box-info">
                                    <h4 class="mb-2 font-weight-bold">IT Services</h4>
                                    <p class="text-white">If you are ready to do business on internet then you need a good website that will work on internet as your shop.</p>

                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 mb-4 appear-animation" data-appear-animation="fadeInRightShorter" data-appear-animation-delay="400">
                            <div class="feature-box feature-box-secondary feature-box-style-4">
                                <div class="feature-box-icon">
                                    <img src="assets/frontpage/img/icon/tool.png" height="60" width="60">
                                </div>
                                <div class="feature-box-info">
                                    <h4 class="mb-2 font-weight-bold">Fintech</h4>
                                    <p class="text-white">Fintech is a portmanteau of monetary innovation that portrays a developing money related administrations area in the 21st century.</p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row mt-lg-3">
                        <div class="col-lg-4 mb-4 mb-lg-0 appear-animation" data-appear-animation="fadeInLeftShorter" data-appear-animation-delay="400">
                            <div class="feature-box feature-box-secondary feature-box-style-4">
                                <div class="feature-box-icon">
                                    <img src="assets/frontpage/img/icon/bill.png" height="60" width="60">
                                </div>
                                <div class="feature-box-info">
                                    <h4 class="mb-2 font-weight-bold">Instant Invoice </h4>
                                    <p class="text-white">helps you reach your customers and vendors wherever they are. Share invoices and purchase orders on WhatsApp and Email.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 mb-4 mb-lg-0 appear-animation" data-appear-animation="fadeIn" data-appear-animation-delay="200">
                            <div class="feature-box feature-box-secondary feature-box-style-4">
                                <div class="feature-box-icon">
                                    <img src="assets/frontpage/img/icon/vas1.png" height="60" width="60">
                                </div>
                                <div class="feature-box-info">
                                    <h4 class="mb-2 font-weight-bold">VAS Services</h4>
                                    <p class="text-white">In addition, we offer world-class customer support to answer all your queries.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 appear-animation" data-appear-animation="fadeInRightShorter" data-appear-animation-delay="400">
                            <div class="feature-box feature-box-secondary feature-box-style-4">
                                <div class="feature-box-icon">
                                    <img src="assets/frontpage/img/icon/web.png" height="60" width="60">
                                </div>
                                <div class="feature-box-info">
                                    <h4 class="mb-2 font-weight-bold">SEO</h4>
                                    <p class="text-white">SEO Auditing, In-House Team Training, On-Page Optimization and Modern Link Building strategies.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>


            <section class="pricing-one" id="pricing">
                <div class="container">
                    <div class="block-title text-center">
                        <h3>Choose Pricing <span>Plans</span> Suits You</h3>
                        <p>Register With Us And Feel The Experience Of Expertise.</p>
                    </div><!-- /.block-title text-center -->
                    <div class="tabed-content">
                        <div id="month">
                            <div class="row">
                                <div class="col-lg-4 animated fadeInLeft">
                                    <div class="pricing-one__single">
                                        <div class="pricing-one__inner">
                                            <i class="fas fa-user"></i>
                                            <p>Retailer</p>
                                            <b>Life Time</b>
                                            <ul class="list-unstyled pricing-one__list" align="left">
                                                <li><i class="fa fa-check"></i>Mobile & DTH Recharge</li>
                                                <li><i class="fa fa-check"></i>MicroATM AePS & More</li>
                                                <li><i class="fa fa-check"></i>Retailer Panel</li>
                                                <li><i class="fa fa-check"></i>Dedicated Support</li>
                                            </ul><!-- /.list-unstyled pricing-one__list -->
                                            <a href="https://checkout.razorpay.com/v1/payment-button.js" data-payment_button_id="pl_LFcHv2qPvnIbgR" class="thm-btn pricing-one__btn"><span>Choose Plan</span></a>
                                            <!-- /.thm-btn -->
                                        </div><!-- /.pricing-one__inner -->
                                    </div><!-- /.pricing-one__single -->
                                </div><!-- /.col-lg-4 -->
                                <div class="col-lg-4 animated fadeInUp">
                                    <div class="pricing-one__single">
                                        <div class="pricing-one__inner">
                                            <i class="fas fa-users"></i>
                                            <p>Distributor</p>
                                            <b>Life Time</b>
                                            <ul class="list-unstyled pricing-one__list" align="left">
                                                <li><i class="fa fa-check"></i>Distributor Panel</li>
                                                <li><i class="fa fa-check"></i>Retailer Manager</li>
                                                <li><i class="fa fa-check"></i>Balance Transfer</li>
                                                <li><i class="fa fa-check"></i>Various Reports</li>
                                            </ul><!-- /.list-unstyled pricing-one__list -->
                                            <a href="#" class="thm-btn pricing-one__btn"><span>Choose Plan</span></a>
                                            <!-- /.thm-btn -->
                                        </div><!-- /.pricing-one__inner -->
                                    </div><!-- /.pricing-one__single -->
                                </div><!-- /.col-lg-4 -->
                                <div class="col-lg-4 animated fadeInRight">
                                    <div class="pricing-one__single">
                                        <div class="pricing-one__inner">
                                            <i class="fas fa-paper-plane"></i>
                                            <p>MD</p>
                                            <b>Life Time</b>
                                            <ul class="list-unstyled pricing-one__list" align="left">
                                                <li><i class="fa fa-check"></i>Distributor Panel</li>
                                                <li><i class="fa fa-check"></i>Retailer Manager</li>
                                                <li><i class="fa fa-check"></i>Balance Transfer</li>
                                                <li><i class="fa fa-check"></i>Dedicated Support</li>
                                            </ul><!-- /.list-unstyled pricing-one__list -->
                                            <a href="#" class="thm-btn pricing-one__btn"><span>Choose Plan</span></a>
                                            <!-- /.thm-btn -->
                                        </div><!-- /.pricing-one__inner -->
                                    </div><!-- /.pricing-one__single -->
                                </div><!-- /.col-lg-4 -->
                            </div><!-- /.row -->
                        </div><!-- /#month -->
                    </div><!-- /.tabed-content -->

                </div><!-- /.container -->
            </section><!-- /.pricing-one -->

            <div align="center"><img src="assets/frontpage/img/photo/line.png" class="img-fluid" alt="Responsive image" height="50px" width="80%"></div>

            <section class="service-one" id="features">
                <div class="container">
                    <div class="block-title text-center">
                        <h3><%=company.Name %><span></span>Features Providing You</h3>
                        <p> Best Multi Services single app solutions.</p>
                    </div><!-- /.block-title -->
                    <div class="row">
                        <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12 wow fadeInUp" data-wow-duration="1500ms" data-wow-delay="0ms">
                            <div class="service-one__single">
                                <h3>Prepaid Recharge</h3>
                                <!--<p>Lorem ipsum is are many variations of pass.</p>-->
                                <div class="service-one__icon">
                                    <img src="assets/frontpage/img/icon/smartphone.png" width="70px" height="70px">
                                </div><!-- /.service-one__icon -->
                            </div><!-- /.service-one__single -->
                        </div><!-- /.col-lg-3 col-md-6 col-sm-12 -->
                        <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12 wow fadeInUp" data-wow-duration="1500ms" data-wow-delay="100ms">
                            <div class="service-one__single">
                               
                                <h3>DTH Recharge</h3>
                                <!--<p>Lorem ipsum is are many variations of pass.</p>-->
                                <div class="service-one__icon">
                                    <img src="assets/frontpage/img/icon/satellite.png" width="70px" height="70px">
                                </div><!-- /.service-one__icon -->
                            </div><!-- /.service-one__single -->
                        </div><!-- /.col-lg-3 col-md-6 col-sm-12 -->
                        <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12 wow fadeInUp" data-wow-duration="1500ms" data-wow-delay="300ms">
                            <div class="service-one__single">
                                
                                <h3>Aeps Services</h3>
                                <!--<p>Lorem ipsum is are many variations of pass.</p>-->
                                <div class="service-one__icon">
                                    <img src="assets/frontpage/img/icon/fingerprint.png" width="70px" height="70px">
                                </div><!-- /.service-one__icon -->
                            </div><!-- /.service-one__single -->
                        </div><!-- /.col-lg-3 col-md-6 col-sm-12 -->
                        <div class="col-xl-3 col-lg-6 col-md-6 col-sm-12 wow fadeInUp" data-wow-duration="1500ms" data-wow-delay="200ms">
                            <div class="service-one__single">
                                
                                <h3>MD Panel</h3>
                                <!--<p>Lorem ipsum is are many variations of pass.</p>-->
                                <div class="service-one__icon">
                                    <img src="assets/frontpage/img/icon/apiicon.jpg" width="70px" height="70px">
                                </div><!-- /.service-one__icon -->
                            </div><!-- /.service-one__single -->
                        </div><!-- /.col-lg-3 col-md-6 col-sm-12 -->
                    </div><!-- /.row -->

                </div><!-- /.container -->
            </section><!-- /.service-one -->

            <section class="process section-padding bg-img bg-fixed pos-re text-center" data-overlay-dark="7" data-background="assets/frontpage/img/bg8.jpg" style="background-image:url(assets/frontpage/img/bg8.jpg);">
                <div class="container">
                    <div class="row">
                        <div class="section-head offset-md-2 col-md-8 offset-lg-3 col-lg-6">
                            <h4><span>Our</span> Process</h4>
                            <p>We are a passionate digital design agency that specializes in beautiful and easy-to-use digital design &amp; web development services.</p>
                        </div>
                        <div class="full-width clearfix"></div>
                        <div class="col-lg-3 col-md-6">
                            <div class="item first mb-md50">
                                <img src="assets/frontpage/img/arrow.png" class="tobotm" alt="">
                                <span class="icon far fa-lightbulb"></span>
                                <div class="cont">
                                    <h3>01</h3><br>
                                    <h6>Discussion For Plans With Member</h6>
                                    <p>I think of a conversation plan as a written, step-by-step guide designed to generate a meaningful discussion and achieve a specific goal.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="item odd mb-md50">
                                <img src="assets/frontpage/img/arrow.png" alt="">
                                <span class="icon fa fa-book"></span>
                                <div class="cont">
                                    <h3>02</h3><br>
                                    <h6>KYC Agreement</h6>
                                    <p>KYC laws were introduced in 2001 as part of the Patriot Act, which was passed after 9/11 to provide a variety of means to deter terrorist behavior.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="item first mb-md50">
                                <img src="assets/frontpage/img/arrow.png" class="tobotm" alt="">
                                <span class="icon fa fa-wrench"></span>
                                <div class="cont">
                                    <h3>03</h3><br>
                                    <h6>KYC</h6>
                                    <p>The Service can be work start,after ekyc all service activate afert Verification kyc.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="item odd">
                                <span class="icon fa fa-laptop-code"></span>
                                <div class="cont">
                                    <h3>04</h3><br>
                                    <h6>Start Transaction</h6>
                                    <p>It means you have completed all of the required information to start transaction and secure your Membership ID. Start your full transaction with us.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="curve curve-gray-t curve-top"></div>
                <div class="curve curve-bottom"></div>
            </section>

            <div class="container">
                <div class="row mt-4">
                    <div class="col-lg-12 text-center appear-animation" data-appear-animation="fadeInUpShorter" data-appear-animation-delay="400">
                        <h2 class="font-weight-normal text-6 mt-5 mb-4">Our <strong class="font-weight-extra-bold">Partners Network </strong></h2>
                    </div>
                </div>
                <div class="row my-3">
                    <div class="col my-3">

                        <div class="row text-center my-3">
                            <div class="owl-carousel owl-theme" data-plugin-options="{'responsive': {'0': {'items': 1}, '476': {'items': 1}, '768': {'items': 5}, '992': {'items': 7}, '1200': {'items': 7}}, 'autoplay': true, 'autoplayTimeout': 3000, 'dots': false}">
                                <div>
                                    <img class="img-fluid" src="assets/frontpage/img/partners/1.png" alt="">
                                </div>
                                <div>
                                    <img class="img-fluid" src="assets/frontpage/img/partners/2.png" alt="">
                                </div>
                                <div>
                                    <img class="img-fluid" src="assets/frontpage/img/partners/3.jpg" alt="">
                                </div>
                                <div>
                                    <img class="img-fluid" src="assets/frontpage/img/partners/4.png" alt="">
                                </div>
                                <div>
                                    <img class="img-fluid" src="assets/frontpage/img/partners/5.png" alt="">
                                </div>
                                <div>
                                    <img class="img-fluid" src="assets/frontpage/img/partners/6.jpg" alt="">
                                </div>
                                <div>
                                    <img class="img-fluid" src="assets/frontpage/img/partners/7.jpg" alt="">
                                </div>
                                <div>
                                    <img class="img-fluid" src="assets/frontpage/img/partners/8.jpg" alt="">
                                </div>
                                <div>
                                    <img class="img-fluid" src="assets/frontpage/img/partners/9.jpg" alt="">
                                </div>
                                <div>
                                    <img class="img-fluid" src="assets/frontpage/img/partners/10.png" alt="">
                                </div>
                                <div>
                                    <img class="img-fluid" src="assets/frontpage/img/partners/11.jpg" alt="">
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>



            <section style="background-color: #ffcb36;">
                <div class="container py-4">

                    <div class="row featured-boxes featured-boxes-style-4 mb-3">
                        <div class="col-md-6 col-lg-3 my-2">
                            <div class="m-0 featured-box featured-box-primary featured-box-effect-4 appear-animation animated fadeInLeftShorter appear-animation-visible" data-appear-animation="fadeInLeftShorter" data-appear-animation-delay="1600" style="animation-delay: 1600ms; height: 141px;">
                                <div class="box-content p-0 text-left">
                                    <img src="assets/frontpage/img/icon/avatar.png" height="50px" width="50px">
                                    <h4 class="font-weight-bold text-custom">24X7 Support</h4>
                                    <p class="mb-0 text-custom">Need help? Click here. You can also talk to us on <b>+91 <%= company.Mobile %></b> our Team is available round the clock to resolve your query.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-3 my-2">
                            <div class="m-0 featured-box featured-box-primary featured-box-effect-4 appear-animation animated fadeInLeftShorter appear-animation-visible" data-appear-animation="fadeInLeftShorter" data-appear-animation-delay="1400" style="animation-delay: 1400ms; height: 141px;">
                                <div class="box-content p-0 text-left">
                                    <img src="assets/frontpage/img/icon/technology.png" height="50px" width="50px">
                                    <h4 class="font-weight-bold text-custom"><%=company.Name %>Trust</h4>
                                    <p class="mb-0 text-custom"><%=company.Name %>’s brand Personality and transparency are the two cornerstones of its trust building approach.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-3 my-2">
                            <div class="m-0 featured-box featured-box-primary featured-box-effect-4 appear-animation animated fadeInRightShorter appear-animation-visible" data-appear-animation="fadeInRightShorter" data-appear-animation-delay="1400" style="animation-delay: 1400ms; height: 141px;">
                                <div class="box-content p-0 text-left">
                                    <img src="assets/frontpage/img/icon/success.png" height="50px" width="50px">
                                    <h4 class="font-weight-bold text-custom">100% Quality Assurance</h4>
                                    <p class="mb-0 text-custom">Quality is characterized through joint efforts with our customers. While certain parts of value are steady, others are customer particular.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-3 my-2">
                            <div class="m-0 featured-box featured-box-primary featured-box-effect-4 appear-animation animated fadeInRightShorter appear-animation-visible" data-appear-animation="fadeInRightShorter" data-appear-animation-delay="1600" style="animation-delay: 1600ms; height: 141px;">
                                <div class="box-content p-0 text-left">
                                    <img src="assets/frontpage/img/icon/business.png" height="50px" width="50px">
                                    <h4 class="font-weight-bold text-custom">Our Promise</h4>
                                    <p class="mb-0 text-custom">We promise to deliberately join forces with each customer. we are managed the chance to work with requirements .</p>
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
            </section>


        </div>
</asp:Content>

