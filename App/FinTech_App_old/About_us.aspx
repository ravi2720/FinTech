<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="About_us.aspx.cs" Inherits="About_us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div>
            <img src="../assets/frontpage/img/photo/about_us.gif" class="img-fluid" alt="Responsive image" style="width: 100%;">
        </div>
        <div class="container" style="margin-top: 50px;">
            <div class="row">
                <div class="col-lg-5">
                    <div class="content-box benefits">
                        <img src="../assets/frontpage/img/photo/writer-work.gif" height="300px" width="400px">
                    </div>
                </div>
                <div class="col-lg-7">
                    <div class="content-box benefits">
                        <div>
                            <h2><b>Who We Are</b></h2>
                        </div>
                        <p style="margin-left:0in; margin-right:0in; text-align:justify">
                            <span style="font-size:16px;">
                                <%=company.Name %>, Have a strong backend support & Database through our Best services. No need of Any investment to start online recharges service. AePS Micro ATM  Service Providding & Boom in telecom industry favour's promising results. We offer simple, Fast And Quick mobile recharge facility
                                recharge service is provided by us like Airtel Digital TV, Dish TV, Videocon D2H, Tata Play and Sun Direct, With Direct Operator Transaction ID.
                            </span>
                        </p>

                        <p style="margin-left:0in; margin-right:0in; text-align:justify"><span style="font-size:16px;">Our aim to develop applications that meet best with the customer's satisfaction at all levels. We provide you with complete assurance about quality, capital and timely execution of the projects.</span></p>
                    </div>
                </div>
            </div>
        </div><div align="center"><hr style="height:1px; width: 80%; background-color:#fb494f"></div>

        <section class="team bg-shape-two" id="team">
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <div class="title">
                            <h2>Our Creative Team</h2>
                            <span class="title-border-color"><i class="fa fa-bolt" aria-hidden="true"></i></span>
                        </div>
						

                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        

        </div>
    </div>
</section>
<!-- Team -->
                    </div>
                </div>
                <div class="row">
                    <!-- Start of Team Row -->

                </div> <!-- End of Team Row -->
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
        </section><br><br><br>
</asp:Content>

