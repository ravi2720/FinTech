<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Retailer.aspx.cs" Inherits="Retailer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <div>
            <img src="../assets/frontpage/img/photo/retailer.png" class="img-fluid" alt="Responsive image" style="width: 100%;">
        </div><br>
            <div class="container">
              <div class="row">
                <div class="col-lg-12">
                  <div class="content-box benefits">
                    <div>
                        <h2 align="center"><b>Hightest Commissions - Retailer</b></h2>
                    </div>
                          <p><span style="font-size:16px;"><%=company.Name %> services enable our channel partners earn extra money. Our state of the art technology enables A through a very safe channel to almost all banks across India. It enables the walk &ndash; in customers even without a bank account for Wallet transfers to any PNK Infotech account anywhere in India. Wallet Transfer is now easy, fast and safe with <%=company.Name %>.</span></p>
          
                          <p><span style="font-size:16px;">Cash withdrawal and Balance Enquiry with AEPS enables a <%=company.Name %> Outlet work like a Micro ATM. Anyone can make Cash withdrawal and Balance Enquiry from their Bank account using their Aadhaar no.</span></p>
          
                          <p><span style="font-size:16px;"><%=company.Name %> fuels Entrepreneurship and enables to start your own business and become self reliant. With PNK Infotech, earnings mean only sky is the limit. Install the fully loaded PNK Infotech kiosks at your office, shop or any work place, sell our wide range of Digital Services and start earning immediately.</span></p><br>
          
                          <p><span style="font-size:16px"><span><strong>Advantages:</strong></span></span></p>
                            <ul>
                                <li><h4>Supplement your income with extra earnings by installing <%=company.Name %> Kiosk.</h4></li>
                                <li><h4>Increase your customers by offering them our wide range of Banking &amp; Digital services - Wallet to Wallet Transfer, AEPS, UTI & NSDL Pan, PMJAY, Prepaid Cards, DTH & Mobile Recharges etc.</h4></li>
                                <li><h4>Become a <%=company.Name %> retailer and start earning immediately.</h4></li>
                                <li><h4>Get equipped with a wide range of Cashless, digital services.</h4></li>
                                <li><h4>Be partners with the leaders in technology and payment systems.</h4></li>
                                <li><h4>Sell our digital services and earn lucrative commissions.</h4></li>
                                <li><h4>Accept and send electronic payments.</h4></li>
                                <li><h4>Shops will now earn extra by installing our Kiosks.</h4></li>
                              
                            </ul>
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
    </div><br><br>
</asp:Content>

