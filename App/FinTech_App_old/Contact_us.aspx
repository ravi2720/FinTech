<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Contact_us.aspx.cs" Inherits="Contact_us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
            <img src="../assets/frontpage/img/photo/contact2.jpg" class="img-fluid fix-img" alt="Responsive image">
        </div><br><br>
                <!--Contact Section Start--> 
                <section class="pt-100 pb-100 " data-scroll-index="6">
                    <div class="container">
                       <div class="row">
                          <div class="col-lg-12 offset-lg-0">
                              <div class="row">
                                    <div class="col-lg-4">
                                        <div class="contact-info active1">
                                            <div class="contact-info-icon">
                                                <img src="../assets/frontpage/img/icon/phone.gif" style="margin-top: -18px;">
                                            </div>
                                            <div class="contact-info-text">
                                                <h2>Phone Number</h2>
                                                <p>+91 <%=company.Mobile %></p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="contact-info active2">
                                            <div class="contact-info-icon">
                                                <img src="../assets/frontpage/img/icon/mail.gif" style="margin-top: -12px;">
                                            </div>
                                            <div class="contact-info-text">
                                                <h2>Email Address</h2>
                                                <p> helpaadharpay@gmail.com</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="contact-info active3">
                                            <div class="contact-info-icon">
                                                <img src="../assets/frontpage/img/icon/location.gif" style="margin-top: -5px;">
                                            </div>
                                            <div class="contact-info-text">
                                                <h2>Office</h2>
                                                <p style="margin-left: -45px;"><%=company.Address %></p>
                                            </div>
                                        </div>
                                    </div>
                              </div>
                          </div>
                       </div>
                       <div id="contact-form-div" class="row ">
                           <div class="col-lg-6">
                              <div>
                                 <img class="postman" src="../assets/frontpage/img/photo/postman.gif" alt="mail-img" >
                              </div>
                           </div>
                           <div class="col-lg-6 d-flex align-items-center">
                               <div class="contact-form-section">
                                <h6>Get in touch</h6>
                                <h2>We Are Here For you</h2>
                                <form action="javascript:void(0)" method="post" id="contact_form">
                                    
                                    <div class="row">
                                       <div class="col-lg-12">
                                          <div class="form-group">
                                             <input type="text" name="name" class="form-control" id="name" placeholder="Enter Your Name Here" required="required">
                                          </div>
                                       </div>
                                       <div class="col-lg-12">
                                          <div class="form-group">
                                             <input type="email" name="email" class="form-control" id="email" placeholder="Enter Your Email Here" required="required">
                                          </div>
                                       </div>
                                       <div class="col-lg-12">
                                          <div class="form-group">
                                             <input type="mobile" name="mobile" class="form-control" id="mobile" placeholder="Enter Your Mobile Number Here" required="required">
                                          </div>
                                       </div>
                                       <div class="col-md-12">
                                          <div class="form-group">
                                               <textarea rows="6" name="message" class="form-control" id="message" placeholder="Enter Your Message Here" required="required"></textarea>
                                          </div>
                                       </div>
                                       
                                        <div class="col-md-12 text-center">
                                            <!--contact button-->
                                            <div class="form-group">
                                            
                                            <button type="submit" id="contact-submit" class="div-btn" >Send Message</button>
                                            </div>
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
                                </form>
                               </div>
                           </div>
                       </div>
                    </div>
                </section><br><br>
                <!--Contact Section End-->

</asp:Content>

