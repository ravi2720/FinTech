<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="Services.aspx.cs" Inherits="ab_part" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .modal-dialog {
            position: absolute;
            top: 0px;
            right: 0px;
            bottom: 0;
            /* left: 0; */
            z-index: 10040;
            overflow: auto;
            height: 100vh;
            overflow-y: scroll !important;
        }

        .modal-body {
            overflow-y: scroll;
        }

        .modal-content {
            height: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="tabs-details" id="prepaid">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="responsive__tabs" style="margin-top: -50px;">
                        <ul class="container scrollable-tabs ">

                            <li>
                                <a href="prepaid.aspx">
                                    <img src="images/service-icon-1.PNG" class="img-sec">
                                    <span>Mobile</span>
                                </a>
                            </li>
                            <li>
                                <a href="Services.aspx?SType=ELECTRICITY">
                                    <img src="images/service-icon-2.PNG" class="img-sec">
                                    <span>Electricity</span>
                                </a>
                            </li>


                            <li>
                                <a href="DTH.aspx">
                                    <img src="images/service-icon-3.PNG" class="img-sec">
                                    <span>Dth</span>
                                </a>
                            </li>
                            <li>
                                <a href="Services.aspx?SType=Gas%20Booking">
                                    <img src="images/service-icon-4.PNG" class="img-sec">
                                    <span>Gas</span>
                                </a>
                            </li>
                            <li><a href="#">
                                <img src="images/service-icon-5.PNG" class="img-sec"><span>Emi Pay</span></a></li>
                            <li><a href="Services.aspx?SType=WATER">
                                <img src="images/service-icon-6.PNG" class="img-sec"><span>Water</span></a></li>
                            <li><a href="Services.aspx?SType=INTERNET">
                                <img src="images/service-icon-8.PNG" class="img-sec"><span>Internet</span></a></li>
                            <li><a href="Services.aspx?SType=LANDLINE">
                                <img src="images/service-icon-8.PNG" class="img-sec"><span>Landline</span></a></li>
                            <li><a href="Services.aspx?SType=INSURANCE">
                                <img src="images/service-icon-9.PNG" class="img-sec"><span>Insurance</span></a></li>
                            <li><a href="Services.aspx?SType=FASTAG">
                                <img src="images/Fastag.PNG" class="img-sec"><span>Fastag</span></a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <section class="page-inner-detail">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="tab-content container">
                            <div id="home" class="container tab-pane active">
                                <div class="box-shedow-box cdn">

                                    <div class="row">


                                        <!-- Tab panes -->
                                        <div class="col-md-8">
                                            <div class="tab-content">
                                                <div id="home12" class="container tab-pane  active">

                                                    <%--      <form action="/action_page.php">--%>
                                                    <div class="row">
                                                        <div class="card-header">

                                                            <h4><%= Request.QueryString["SType"].ToString() %> Recharges</h4>

                                                        </div>

                                                        <div class="card card-body">
                                                            <div class="row">
                                                                <div class="col-lg-6">
                                                                    <div class="form-col">
                                                                        <label for="operator">Select Operator</label><p class="hide"></p>
                                                                        <asp:DropDownList runat="server" ID="ddlOperatorELECTRICITY" AutoPostBack="true" OnSelectedIndexChanged="ddlOperatorELECTRICITY_SelectedIndexChanged" CssClass="form-control select2">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <asp:Image runat="server" ID="imgop" Height="100" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <asp:Repeater runat="server" ID="rptData">
                                                                    <ItemTemplate>
                                                                        <div class="col-12 col-lg-6 mt-2">
                                                                            <div class="form-group">
                                                                                <label><%# Eval("Labels") %></label>
                                                                                <asp:TextBox runat="server" ID="txtVal" CssClass="form-control" MinLength='<%# Eval("FieldMinLen") %>' MaxLength='<%# Eval("FieldMaxLen") %>' placeholder='<%# Eval("Labels") %>'></asp:TextBox>

                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>


                                                                <div class="col-12 col-lg-6 mt-2">
                                                                    <div class="input-group mb-3">
                                                                        <span class="input-group-text" id="BillPayAmount">₹</span>
                                                                        <asp:TextBox runat="server" class="form-control" ID="txtAmountELECTRICITY" placeholder="Amount" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" MaxLength="4"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-12 mt-2">



                                                                    <%-- <asp:Button runat="server" ID="btnElePay" OnClick="btnElePay_Click" name="btnRecharge" class="btn btn-primary" Text="Pay Now" />--%>
                                                                </div>
                                                            </div>
                                                        </div>




                                                        <div class="col-lg-12">
                                                            <asp:Button runat="server" ID="btnBillFetch" OnClick="btnBillFetch_Click" name="btnRecharge" class="btn btn-primary" Text="Bill Fetch" />
                                                        </div>

                                                    </div>
                                                    <%--</form>--%>
                                                </div>

                                                <%--                                                    <div id="menu12" class="container tab-pane fade">
                                                        <br>

                                                        <form action="/action_page.php">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <label for="email">Mobile Number</label>
                                                                        <input type="email" class="form-control" placeholder="Mobile Number" id="email">
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <label for="email">Operator</label>
                                                                        <select>
                                                                            <option value="">Operator</option>
                                                                            <option value="">A</option>
                                                                            <option value="">B</option>
                                                                            <option value="">C</option>
                                                                            ...
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <label for="email">Circle</label>
                                                                        <select class="form-control">
                                                                            <option value="">Circle</option>
                                                                            <option value="">A</option>
                                                                            <option value="">B</option>
                                                                            <option value="">C</option>
                                                                            ...
                                                                        </select>
                                                                    </div>
                                                                </div>


                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <label for="email">Amount</label>
                                                                        <select class="form-control">
                                                                            <option value="">Amount</option>
                                                                            <option value="">A</option>
                                                                            <option value="">B</option>
                                                                            <option value="">C</option>
                                                                            ...
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 col-12">
                                                                    <div class="form-group">
                                                                        <a href="javascript:void(0)" class="btn-primary">Proceed  ></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </form>
                                                    </div>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="card-header">
                                                <h2>Bill Details</h2>
                                            </div>
                                            <div class="card card-body">
                                                <div class="row">
                                                    <div class="input-group mb-3">
                                                        <asp:Label runat="server" ID="lblcustomername" CssClass="form-control"></asp:Label>
                                                        <span class="input-group-text">CustomerName</span>
                                                    </div>

                                                    <div class="input-group mb-3">
                                                        <asp:Label runat="server" ID="billdate" CssClass="form-control"></asp:Label>
                                                        <span class="input-group-text">Billdate</span>
                                                    </div>
                                                    <div class="input-group mb-3">
                                                        <asp:Label runat="server" ID="duedate" CssClass="form-control"></asp:Label>
                                                        <span class="input-group-text">Duedate</span>
                                                    </div>

                                                    <div class="input-group mb-3">
                                                        <span class="input-group-text" id="basic-addon1">₹</span>
                                                        <asp:Label runat="server" ID="Billamount" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>



                                </div>

                            </div>
                            <section class="other-recharge-detail">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <!--   <ul>
    <li><img src="images/ATP.png">
    </li>
        <li><img src="images/BGP.png">
    </li>
        <li><img src="images/BGP.png">
    </li>
        <li><img src="images/IDP.png">
    </li>
        <li><img src="images/BGP.png">
    </li>
          <li><img src="images/MMP.png">
    </li>
          <li><img src="images/MMP.png">
    </li>
              <li><img src="images/RJP.png">
    </li>
              <li><img src="images/vfp.png">
    </li>
  </ul> -->
                                            <div class="inner-recharge">
                                                <h1>How to recharge prepaid mobile online, Instant &amp; Safe!
                                                </h1>
                                                <p>
                                                    Mobile phones are an intrinsic part of our lives and with association, so is online mobile recharge. Online recharge gives you the liberty to recharge your mobile phone number anytime and from anywhere - be it from your home, office, restaurant or vacation, all you need is an internet connection. When you think of easy online recharge options,  is the best platform to turn to.

                                                </p>
                                                <p>
                                                    You can now carry out your prepaid online recharge and bill payments for any number, for your friends and family using . Online recharges can be done through Net Banking, Debit Card, Credit Card, Visa or Mastercard and  wallet. HDFC, SBI, CITI &amp; ICICI banks and a host of other banks support Net Banking online recharge with ease.


                                                </p>
                                                <p>
                                                    The process of online recharge is also as simple as it sounds. All you have to do is :-

                                                </p>
                                                <p>
                                                    Enter the phone number you want to carry out the online recharge for.
                                                </p>
                                                <p>
                                                    Select the option from Prepaid/ Postpaid
                                                </p>
                                                <p>
                                                    Select your mobile operator
                                                </p>

                                                <p>
                                                    Select the circle you belong to.
                                                </p>
                                                <p>
                                                    Check the online recharge plans designed exclusively for you or fill in the amount you want to recharge
                                                </p>
                                                <p>
                                                    Enter your mobile number again and put the received OTP
                                                </p>
                                                <p>
                                                    Make the online recharge payment and Voila!
                                                </p>
                                                <p>
                                                    Your online mobile recharge is all done.


                                                </p>
                                                <h1>Why Choose  for Prepaid Recharge?
                                                </h1>
                                                <p>
                                                    is India's No. 1 site for online easy recharges like Airtel Recharge, BSNL Recharge, Idea Recharge, Jio Recharge, MTNL Recharge, Tata Docomo Recharge, Tata Indicom Recharge, Vodafone Recharge and more.


                                                </p>
                                                ,h1&gt;Free Mobile Recharge
                                            </div>
                                            <p>
                                                does not charge you over any online recharges i.e on , enables free mobile recharge.

                                            </p>
                                            <h1>offers easy mobile recharge
                                            </h1>
                                            <p>
                                                Helps you save your time, effort and money. Besides enabling free online mobile recharges, it also ensures easy recharge online.

                                            </p>
                                            <p>
                                                The  wallet can be used across a host of payments services, including bill recharges, online prepaid mobile recharges, utility bill payments, cab bookings, ticket bookings, payments for groceries, as well as payments to online digital and offline retail stores.
                                            </p>
                                            <p>
                                                bridges the digital divide between consumers and companies via easy and quick recharges and bill payments, free of cost, for its online transactions.
                                            </p>
                                            <p>
                                                is India's largest issuer-independent digital financial services platform, leveraging a sophisticated product and merchant acquisition capabilities. It is the undisputed No.2 player in the mobile wallet space in India and amongst the top 3 players in the payment gateway industry in the country. It has a network of over 3 million direct merchants, 140+ billers and 107 million-plus users. It records over 1 million transactions/ day.

                                            </p>
                                            <p>
                                                has grown by leaps and bounds over the past few years. The company has clocked a 4x growth in transactions on YoY basis. The brand vision is to enable a billion Indians with one tap access to digital payments, loans, investments and insurance by the year 2022.

                                            </p>
                                            <h1>Online Mobile Bill Payment | Pay Postpaid Bills at .com
                                            </h1>
                                            <p>
                                                With the present advancement in technology, the internet has made our lives simple and  made it simpler. Experience the trouble-free process of paying the postpaid bill for various operators online. Forget the discomfort of rushing to the nearest retailer for mobile bill payment, it is the most convenient task with us.  presents an instant service to recharge and pay postpaid bill online. .com and  app have the aim of bridging the mobile bill payment, we offer the easiest option for your mobile phones.
                                            </p>
                                            <p>
                                                has the best mobile bill payment service that allows you to pay your postpaid bill from anywhere and anytime whether you use Vodafone, Airtel, Idea, Jio, BSNL, MTNL or any other postpaid service. Just log in to .com and leave all the worries behind for postpaid bill payment to us.

                                            </p>
                                            <h1>Latest Postpaid Bill Payment Offers, Secure Payment Methods &amp; More
                                            </h1>
                                            <p>
                                                Grab a huge range of cashback &amp; discount offers on every bill payment you make at our portal. Save more with , avail exciting deals &amp; cashback offers while paying postpaid bill online. Enjoy the immediate, unfussy &amp; protected service and pay bill online effortlessly. No need to spend hours searching for mobile bill payment stores across the city, expediently make bill payment online at .com. Here, you also get the benefit of choosing from various call plans, Local call plans, night call plan, local &amp; national SMS packs, 3G &amp; 4G services and many more.

                                            </p>
                                            <h1>24*7 Bill Payment Service
                                            </h1>
                                            <p>
                                                Last date to pay your postpaid bill nearby or has it already passed? Don’t worry, our postpaid bill payment service is available for you 24*7 so that you can pay your postpaid bills anytime and from anywhere.

                                            </p>
                                            <h1>Flexible Payment Options
                                            </h1>
                                            <p>
                                                presents a time-saving and convenient way of paying postpaid bills online. Pay through your mobile, desktop or laptops from anywhere and do away with the hassles of going to a bill payment centre. You can make your postpaid payment with your debit card, credit card,  wallet, UPI and  Postpaid (UPI and  Postpaid are only available on  mobile app).

                                            </p>
                                            <h1>How to Make Online Bill Payment?
                                            </h1>
                                            <p>
                                                Indulge in the demanding online bill payment service at our platform. Now pay postpaid mobile bill effortlessly online in just a few easy clicks. Just log in to .com and fill in the following information:
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </section>

                        </div>






                    </div>
                </div>




                <%--  <div class="col-md-4">
                        <div class="right-scane-img">
                            <img src="images/scane.jpg">
                        </div>
                    </div>--%>
            </div>
            </div>

            <section class="Free-payments" style="background: inherit;">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <h1>Simple, fast and Hassle Free payments
                            </h1>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="wow fadeInUp inner" style="visibility: visible; animation-name: fadeInUp;">
                                <div class="icon">
                                    <div class="img-sec">
                                        <img src="images/payment-icon-6.PNG">
                                    </div>
                                    <div class="title">
                                        Phone Recharge &amp; DTH
                                    </div>
                                    <p>
                                        With money loaded in your  wallet, it takes seconds to make phone and DTH recharges!.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="wow fadeInUp inner" style="visibility: visible; animation-name: fadeInUp;">
                                <div class="icon">
                                    <div class="img-sec">
                                        <img src="images/payment-icon-5.PNG">
                                    </div>
                                    <div class="title">
                                        Bill Payments
                                    </div>
                                    <p>
                                        Pay all your bills across categories via  in seconds and avoid late payment charges..
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="wow fadeInUp inner" style="visibility: visible; animation-name: fadeInUp;">
                                <div class="icon">
                                    <div class="img-sec">
                                        <img src="images/payment-icon-4.PNG">
                                    </div>
                                    <div class="title">
                                        Bus Tickets
                                    </div>
                                    <p>
                                        Book instant bus tickets across India and get the best offers.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="wow fadeInUp inner" style="visibility: visible; animation-name: fadeInUp;">
                                <div class="icon">
                                    <div class="img-sec">
                                        <img src="images/payment-icon-3.PNG">
                                    </div>
                                    <div class="title">
                                        Transfer money to Bank
                                    </div>
                                    <p>
                                        With money loaded in your  wallet, it takes seconds to make phone and DTH recharges!.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="wow fadeInUp inner" style="visibility: visible; animation-name: fadeInUp;">
                                <div class="icon">
                                    <div class="img-sec">
                                        <img src="images/payment-icon-2.PNG">
                                    </div>
                                    <div class="title">
                                        Boost your finances with !
                                    </div>
                                    <p>
                                        Get an instant ₹1,00,000 credit in your wallet in less than 5 Minutes.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="wow fadeInUp inner" style="visibility: visible; animation-name: fadeInUp;">
                                <div class="icon">
                                    <div class="img-sec">
                                        <img src="images/payment-icon-1.PNG">
                                    </div>
                                    <div class="title">
                                        Boost your finances with !
                                    </div>
                                    <p>
                                        More than 200 brand's Hot Deals, Offers and Special Products.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>


            --%>

               <%--     </div>
                </div>
            </div>
            </div>--%>
        </section>
    </section>
    <script>
        window.onscroll = function () { myFunction() };
        var navbar = document.getElementById("navbar");
        var sticky = navbar.offsetTop;
        function myFunction() {
            if (window.pageYOffset >= sticky) {
                navbar.classList.add("sticky")
            } else {
                navbar.classList.remove("sticky");
            }
        }
        $(document).on('click', '.dropdown-menu', function (e) {
            e.stopPropagation();
        });
        // make it as accordion for smaller screens
        if ($(window).width() < 992) {
            $('.dropdown-menu a').click(function (e) {
                e.preventDefault();
                if ($(this).next('.submenu').length) {
                    $(this).next('.submenu').toggle();
                }
                $('.dropdown').on('hide.bs.dropdown', function () {
                    $(this).find('.submenu').hide();
                })
            });
        }
    </script>
    <script>
        // values to keep track of the number of letters typed, which quote to use. etc. Don't change these values.
        var i = 0,
            a = 0,
            isBackspacing = false,
            isParagraph = false;
        // Typerwrite text content. Use a pipe to indicate the start of the second line "|".
        var textArray = [
            " Recharge Your Prepaid Mobile",
            "Fastest Online Mobile Recharge",
            "Auto Update Mobile Recharge Plan",
            "Auto Fetch Your Operator",
            "Mobile Recharge API Provider India",
            "Auto Update Mobile Recharge Plan",
        ];
        // Speed (in milliseconds) of typing.
        var speedForward = 100, //Typing Speed
            speedWait = 1000, // Wait between typing and backspacing
            speedBetweenLines = 1000, //Wait between first and second lines
            speedBackspace = 25; //Backspace Speed
        typeWriter("output", textArray);
        function typeWriter(id, ar) {
            var element = $("#" + id),
                aString = ar[a],
                eHeader = element.children("h1"),
                eParagraph = element.children("p");

            if (!isBackspacing) {

                if (i < aString.length) {

                    if (aString.charAt(i) == "|") {
                        isParagraph = true;
                        eHeader.removeClass("cursor");
                        eParagraph.addClass("cursor");
                        i++;
                        setTimeout(function () { typeWriter(id, ar); }, speedBetweenLines);

                    } else {
                        if (!isParagraph) {
                            eHeader.text(eHeader.text() + aString.charAt(i));
                        } else {
                            eParagraph.text(eParagraph.text() + aString.charAt(i));
                        }
                        i++;
                        setTimeout(function () { typeWriter(id, ar); }, speedForward);
                    }

                } else if (i == aString.length) {

                    isBackspacing = true;
                    setTimeout(function () { typeWriter(id, ar); }, speedWait);

                }

            } else {

                if (eHeader.text().length > 0 || eParagraph.text().length > 0) {

                    if (eParagraph.text().length > 0) {
                        eParagraph.text(eParagraph.text().substring(0, eParagraph.text().length - 1));
                    } else if (eHeader.text().length > 0) {
                        eParagraph.removeClass("cursor");
                        eHeader.addClass("cursor");
                        eHeader.text(eHeader.text().substring(0, eHeader.text().length - 1));
                    }
                    setTimeout(function () { typeWriter(id, ar); }, speedBackspace);

                } else {

                    isBackspacing = false;
                    i = 0;
                    isParagraph = false;
                    a = (a + 1) % ar.length;
                    setTimeout(function () { typeWriter(id, ar); }, 50);

                }
            }
        }
        $('body').on('click', '.scrollable-tabs li', function () {
            $('.scrollable-tabs li a.active').removeClass('active');
            $(this).addClass('active');
        });
    </script>
    <script src="js/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="js/wow.min.js"></script>
    <script src="js/owl.carousel.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script>
        new WOW().init();
    </script>
    <script>

        new Vue({
            el: '#prepaid',
            data() {
                return {
                    PlanData: [],
                    PlanDataType: [],
                    RofferData: [],
                    Number: "",
                    Amt: 0,
                    Com: 0,
                    Plan: "",
                    isLoading: false
                }
            },
            methods: {
                GetPrepaidVal: function () {
                    if ($("#txtMobileNo").val().length == 10) {
                        document.getElementById('load').style.visibility = "visible";
                        var el = this;
                        el.Amt = 0;
                        el.Plan = "";
                        const article = {
                            "searchTerm": $("#txtMobileNo").val()
                        }
                        axios.post("PrePaid.aspx/GetPrepaid", article)
                            .then(response => {
                                var str = response.data.d.split(',');
                                $("#ddlOperator").val(str[0]);
                                $('#ddlOperator').trigger("chosen:updated");
                                el.GetPlan($("#ddlOperator option:selected").val());
                                el.GetRoffer();
                            });
                    }
                },
                GetRoffer: function () {
                    if ($("#txtMobileNo").val().length == 10) {
                        document.getElementById('load').style.visibility = "visible";
                        var el = this;
                        el.isLoading = true;

                        el.Amt = 0;
                        el.Plan = "";
                        const article = {
                            "OPID": $("#ddlOperator option:selected").val(),
                            "Number": $("#txtMobileNo").val()
                        }
                        axios.post("PrePaid.aspx/GetRoffer", article)
                            .then(response => {
                                el.RofferData = JSON.parse(response.data.d).records;
                                document.getElementById('load').style.visibility = "hidden";
                                el.isLoading = false;

                            });
                    }
                },
                GetPlan: function (ID) {
                    var el = this;
                    if ($("#ddlOperator option:selected").val() != "0") {
                        const article = {
                            "searchTerm": $("#ddlOperator option:selected").val()
                        }
                        axios.post("PrePaid.aspx/GetPrePaidPlan", article)
                            .then(response => {
                                el.PlanData = JSON.parse(response.data.d);
                                el.PlanDataType = Object.keys(el.PlanData.records)
                                el.GetRoffer();
                                document.getElementById('load').style.visibility = "hidden";
                                $('#viewplans').modal('toggle');
                            });
                    } else {
                        document.getElementById('load').style.visibility = "hidden";
                    }
                },
                GetAmt: function (Amt, Plan) {
                    var el = this;
                    el.Amt = Amt;
                    el.Plan = Plan;
                    alertify.success("Amount Applied")
                    el.GetCommission();
                },
                GetCommission: function () {
                    var el = this;
                    const article = {
                        "Amt": el.Amt,
                        "Op": $("#ddlOperator option:selected").val()
                    }
                    axios.post("PrePaid.aspx/GetCommission", article)
                        .then(response => {
                            el.Com = response.data.d;
                        });
                }
            },
            mounted() {

            },
        });
    </script>
</asp:Content>

