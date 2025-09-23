<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Master_Distributor.aspx.cs" Inherits="Master_Distributor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <div>
            <img src="../assets/frontpage/img/photo/mdist.png" class="img-fluid" alt="Responsive image" style="width: 100%;">
        </div><br>
            <div class="container">
              <div class="row">
                <div class="col-lg-12">
                  <div class="content-box benefits">
                    <div>
                        <h2 align="center"><b>Hightest Commissions - Master Distributors</b></h2>
                    </div>
                          <p style="margin-left:0in; margin-right:0in; text-align:justify"><span style="font-size:16px;"><%=company.Name %> distributorship empowers our Channel Partners to hire number of Retailers / Agents who will sell <%=company.Name %> cashless services like Wallet to Wallet Transfer, Micro ATM, NSDL & UTI Pan Service Recharges &amp; to their customers.</span></p>
          
                          <p style="margin-left:0in; margin-right:0in; text-align:justify"><span style="font-size:16px;">Under a <%=company.Name %> distributor model you as a distributor, will earn commissions by transferring the trading balance to Agents / Retailers in your own area. As the agents increase, the transactions will increase resulting more commissions.</span></p>
          
                          <p style="margin-left:0in; margin-right:0in; text-align:justify"><span style="font-size:16px;">At <%=company.Name %>, our distributors not only receive the highest commissions through a convenient distributor portal but also get continuous support, encouragement and involvement from our side.</span></p><br>
          
                            <ul>
                                <li><h4>Attractive Commission.</h4></li>
                                <li><h4>Instant and real time Balance Upload.</h4></li>
                                <li><h4>Real time Retailer creation.</h4></li>
                                <li><h4>Most convenient and hassle free Services.</h4></li>
                                <li><h4>Easy to manage retailers.</h4></li>
                                <li><h4>Dedicated Support team.</h4></li>
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

