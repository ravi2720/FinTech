<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Member_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        section.money-transfer {
            margin-top: 50px;
        }

        section.money-transfer {
            margin-top: 100px;
        }

        header .owl-dot {
            font-weight: normal !important;
            display: inline-block;
            counter-increment: slides-num;
            color: rgba(255,255,255,.7) !important;
            /* margin-right: 5px; */
        }

        header .info {
            text-align: center;
            margin-top: 110px;
        }

        header .item {
            width: 100%;
            background-size: cover;
            background-repeat: no-repeat;
            position: relative;
            min-height: 450px;
            padding: 50px 0px;
            background-position: top:
        }

            header .item.cd1 {
                background: url('../images/imgb1.jpg');
                background-size: cover;
                background-repeat: no-repeat;
                background-position: top:
            }

            header .item.cd2 {
                background: url('../images/imgb2.jpg');
                background-size: cover;
                background-repeat: no-repeat;
                background-position: top:
            }

            header .item.cd3 {
                background: url('../images/imgb3.jpg');
                background-size: cover;
                background-repeat: no-repeat;
            }

            header .item.cd4 {
                background: url('../images/imgb4.jpg');
                background-size: cover;
                background-repeat: no-repeat;
                background-position: top:
            }

        header .col-md-12 {
            padding: 0;
        }

        .item {
            position: relative;
            z-index: 1;
            background-size: cover;
            background-repeat: no-repeat;
            background-position: center center;
        }

        header .item:after {
            position: absolute;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: -1;
            background-color: rgb(14 14 14 / 64%);
            content: "";
        }

        .box-shedow-box {
            padding: 0px 0px 00px;
            border-radius: 50px;
            background: #fff;
            box-shadow: 0 0 50px !important;
        }

        .customer-part .container {
            padding: 0 0px 0px 20px;
            border-radius: 50px;
            background: #fff;
            box-shadow: 0 0 50px !important;
        }

        section.customer-part .right-scane-img img {
            height: 450px;
        }

        section.customer-part {
            margin: 30px 0px;
        }

            section.customer-part .col-md-8 {
                padding-top: 20px;
            }

            section.customer-part .col-md-4 {
                padding: 0;
            }

        .customer-part .container {
            padding: 0 0px 0px 0;
        }

        section.page-inner-detail .tab-content.container .box-shedow-box.cdn {
            padding: 40px 0px 100px 20px;
            border-radius: 50px;
            background: #fff;
            box-shadow: 0 0 50px !important;
        }

        section.page-inner-detail .tab-content.container .box-shedow-box .col-md-4 {
            padding: 0;
        }

        section.top-header {
            background: linear-gradient( 60deg,#ffffff 0,#c92727 100%);
            box-shadow: 0 0 20px -10px rgb(0 0 0 / 80%);
        }

        .android-app i {
            font-size: 25px;
            background: #14630a;
            border-radius: 100%;
            /* width: 40px; */
            width: 3.125rem;
            padding-top: 12px;
            height: 3.125rem;
            /* height: 40px; */
            /* padding: 10px; */
            text-align: center;
            color: #ffff;
        }

        i.fa.fa-check-circle {
            font-size: 22px;
            color: #14630a;
        }

        section.page-inner-detail span.marks {
            font-size: 14px;
        }

        .form-group {
            margin-bottom: 0;
            margin-top: 20px;
        }

        .menu h3 {
            font-size: 35px;
        }

        .logo img {
            max-width: 100%;
        }

        footer img {
            max-width: 100%;
        }

        section.tabs-details .col-md-6.col-lg-4.col-12 {
            padding: 7px;
        }

        .top-form .form-group {
            position: relative;
            margin-bottom: 1rem;
        }

        header .container-fluid {
            padding: 0;
        }

        header .row {
            margin: 0;
        }

        header .bg-dark {
            padding: 15px 0px;
        }

        a.navbar-brand img {
            max-width: 250px;
        }

        .header-innner-detail img {
            max-width: 100%;
        }

        header .menu .navbar-dark .navbar-nav .nav-link {
            color: #fff;
            font-size: 17px;
        }

        header .menu li.nav-item {
            padding-left: 30px;
        }

        header .detail {
            position: absolute;
            top: 50%;
            text-align: left;
            color: #fff;
            padding: 0;
            transform: translate(-50%, -50%);
            left: 32%;
            /* width: 1140px; */
        }

        .sub-detail {
            width: 100%;
            height: 500px;
            overflow-y: auto;
        }
        /* .detail {
    right: 0;
    z-index: 999;
    left: 0;
    position: absolute;
    padding-top: 130px;
} */
        header .owl-nav {
            display: none;
        }

        .right-scane-img img {
            max-width: 100%;
            object-fit: cover;
            border-radius: 50px;
            height: 610px;
        }

        section.waves img {
            /* max-width: 100%; */
            height: 280px;
            width: 100%;
            position: relative;
            /* object-fit: cover; */
        }

        section.waves {
            padding-top: 150px;
        }

        .card[_ngcontent-hfk-c12] {
            position: relative;
            margin-bottom: 30px;
            box-shadow: 0 0 1.25rem rgb(31 45 61 / 5%);
            border-radius: 12px;
        }

        .navbar-expand-md .navbar-nav .nav-link {
            padding-right: 0;
            padding-left: 0;
        }

        .card-body.text-center.p-4 {
            bottom: -67px;
            position: absolute;
            width: 100%;
            border-radius: 12px;
            box-shadow: 0 2px 48px 0 rgb(0 0 0 / 8%);
            background: #fff;
        }

        .money-transfer .content {
            padding-left: 20px;
            display: table;
        }

        button.owl-prev {
            background: #232534 !important;
            /* height: 20px; */
            border-radius: 100% !important;
            height: 35px;
            padding: 5px 0px !important;
            width: 35px;
            color: #fff !important;
            outline: inherit !important;
        }

        button.owl-next {
            background: #232534 !important;
            /* height: 20px; */
            border-radius: 100% !important;
            height: 35px;
            padding: 5px 0px !important;
            width: 35px;
            color: #fff !important;
            outline: inherit !important;
        }

        .gradient-half-primary-v1 {
            background-image: linear-gradient( 60deg,#0b564d 0,#96c93d 100%);
            background-repeat: repeat-x;
        }

        .top-bg {
            background-image: url(../images/img3.jpg);
            background-position: top;
            width: 100%;
            object-fit: cover;
            padding: 40px 19px;
            background-repeat: no-repeat;
            color: #fff;
            border-radius: .3125rem !important;
            text-align: left;
            margin: 20px 0px;
        }

            .top-bg h1 {
                font-size: 25px;
                font-weight: 500;
            }

        .department-detail img {
            max-width: 100%;
        }

        .department-detail h1 {
            font-size: 20px;
        }

        .department-detail ul {
            padding: 0;
        }

            .department-detail ul li {
                list-style: none;
            }

            .department-detail ul li {
                display: inline;
                list-style: none;
                padding-right: 20px;
            }

        .customer-detail ul {
            padding: 0;
        }

        .customer-detail li {
            color: #14630a;
            background: rgba(0,201,167,.1);
            list-style: none;
            margin-bottom: 15px;
            font-size: 18px;
            border-radius: .3125rem;
            font-weight: 600;
            padding: 15px 15px;
        }

        .customer-detail span {
            float: right;
        }

        .customer-detail a.btn-primary {
            width: 15% !important;
            text-align: center;
            float: right;
        }

        .services li {
            text-align: center;
            list-style: none;
            display: inline-block;
            padding-right: 40px;
        }

        .wave-6-top-left-0 {
            fill: #EFA02E;
        }

        .fill-warning-darker {
            fill: #ca3737 !important;
        }

        .services li a img {
            padding: 5px;
            background: #fff;
            /* max-width: 27%; */
            border-radius: 50%;
            width: 60px;
            height: 60px;
        }

        .services span {
            color: #fff;
            padding-top: 15px;
            display: block;
            text-align: center;
        }


        .responsive__tabs {
            background: linear-gradient( 60deg,#825151 0,#e62d2e 100%);
        }

            .responsive__tabs ul.scrollable-tabs {
                overflow-x: inherit;
                white-space: nowrap;
                display: flex;
                text-transform: uppercase;
                flex-direction: row;
            }

                .responsive__tabs ul.scrollable-tabs li {
                    list-style-type: none;
                }

                    .responsive__tabs ul.scrollable-tabs li a {
                        display: inline-block;
                        color: white;
                        text-align: center;
                        padding: 14px;
                        text-decoration: none;
                    }


            .responsive__tabs .col-md-12 {
                padding: 0;
            }





        .services i {
            color: #53941b !important;
        }

        /* .services:after {
    z-index: -1;
    content: '';
    position: absolute;
    background: url(../images/bg-pattern-3.png);
    left: 0;
    bottom: -63px;
    width: 100%;
    height: 63px;
    background-repeat: no-repeat;
    display: none;
    background-size: cover;
} */
        .box-detail {
            display: flex;
        }

        .step .step-icon::after {
            position: absolute;
            top: 3.59375rem;
            left: 1.5625rem;
            height: calc(100% - 2.65625rem);
            border-left: .125rem solid #e7eaf3;
            content: "";
        }

        .box-detail {
            display: table;
            clear: both;
            padding-bottom: 15px;
            padding-top: 20px;
        }

        .money-transfer .icon:after {
            content: '';
            position: absolute;
            top: 60px;
            left: 30px;
            height: calc(100% - 2.65625rem);
            border-left: .125rem solid #e7eaf3;
            border-left-style: dashed;
        }

        .money-transfer h1 {
            font-weight: 300;
            font-size: 35px;
            color: #000;
        }

        .money-transfer .title {
            font-size: 21px;
        }

        .money-transfer p {
            color: #77838f;
            line-height: 1.7;
            font-size: 16px;
        }


        .eletriciity-icon img {
            border-radius: 100%;
            max-width: 100%;
            width: 60px;
            height: 60px;
        }


        ul.process {
            padding: 0;
            margin: 0;
        }

            ul.process li {
                list-style: none;
                padding: 0;
                padding-right: 15px;
                display: inline;
                margin: 0;
            }

            ul.process span {
                color: #77838f !important;
            }

        .consumer-detail {
            width: 100%;
        }

            .consumer-detail ul li {
                font-size: 20px;
                border-bottom: 2px solid;
                border-radius: 15px;
                padding-bottom: 2px;
                list-style: none;
                font-weight: 600;
                margin: 0;
                padding: 35px 10px;
                padding-bottom: 0;
            }

            .consumer-detail span {
                float: right;
            }

        .tabs-details .right-detail .box {
            border-color: rgba(55,125,255,.3);
            padding: 1em;
            box-shadow: 0 0 35px rgb(55 125 255 / 13%);
            border: 1px solid #e7eaf3;
        }

            .tabs-details .right-detail .box:hover {
                border-color: rgba(55, 125, 255, .3);
                box-shadow: 0 0 35px rgb(55 125 255 / 13%);
            }

        .money-transfer .title {
            font-weight: 600;
            color: #000;
            padding-bottom: 10px;
        }

        section.testimonials {
            margin: 50px 0px;
        }

        section.money-transfer .icon {
            float: left;
            padding: 6px;
            text-align: center;
            font-size: 16px;
            background: #000;
            width: 35px;
            height: 35px;
            border-radius: 100%;
            color: #fff;
            color: #377dff;
            background-color: rgba(55,125,255,.1);
        }

        .icon {
            display: inline-block;
        }

            .icon img {
                max-width: 27%;
            }

        section.money-transfer img {
            max-width: 70%;
            box-shadow: 0 10px 40px 10px rgba(140,152,164,.175) !important;
            border-radius: .3125rem !important;
        }

        section.money-transfer .col-md-6 {
            margin: auto 0;
        }

        section.money-transfer .box {
            box-shadow: 0 10px 40px 10px rgba(140,152,164,.175) !important;
            text-align: center;
            padding: 30px 0;
            color: #000;
            transition: all 500ms ease;
            margin: 0px 15px;
        }

            section.money-transfer .box:hover {
                -webkit-transform: translateY(-10px);
                transform: translateY(-10px);
                box-shadow: 0 15px 30px rgb(0 0 0 / 10%);
            }

        section.waves .container-fluid {
            padding: 0;
        }

            section.waves .container-fluid .row {
                margin: 0;
            }

                section.waves .container-fluid .row .col-md-6 {
                    padding: 0;
                }








        section.money-transfer .box h1 {
            font-weight: bold;
        }

        section.money-transfer .box p {
            color: #fff;
        }


        section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(1) .box {
            background: linear-gradient( 60deg,#a35151 0,#d73233 100%);
            color: #fff;
        }

            section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(1) .box h1 {
                color: #fff;
            }

            section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(1) .box:hover {
                background: linear-gradient( 60deg,#a35151 0,#d73233 100%);
                color: #fff;
            }

        section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(2) .box:hover {
            background: linear-gradient( 60deg,#a35151 0,#d73233 100%);
            color: #fff;
        }


        section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(3) .box:hover {
            background: linear-gradient( 60deg,#a35151 0,#d73233 100%);
            color: #fff;
        }

        section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(4) .box:hover {
            background: linear-gradient( 60deg,#a35151 0,#d73233 100%);
            color: #fff;
        }

        section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(2), section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(4) {
            margin-bottom: 30px;
        }

        section.money-transfer .col-md-7 {
            margin: auto 0;
        }

        .slider-img {
            background: url('../images/2.png');
            background-size: cover;
            background-repeat: no-repeat;
            width: 100%;
            height: 400px;
        }

        section.top-part .col-md-5 {
            padding: 0;
        }


        section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(2) .box p, section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(3) .box p, section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(4) .box p {
            color: #53941b;
        }

        section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(2) .box p, section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(3) .box p, section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(4) .box p {
            color: #53941b;
        }

        section.app-development .col-md-6 {
            margin: auto 0;
        }

        section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(2) .box:hover:hover p, section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(3) .box:hover p, section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(4) .box:hover p {
            color: #fff !important;
        }

        section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(2) .box:hover:hover h1, section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(3) .box:hover h1, section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(4) .box:hover h1 {
            color: #fff !important;
        }

        section.money-transfer .col-md-6:nth-child(2), .right-part .col-md-6:nth-child(3) .box {
            background: inherit;
            color: #000;
        }


        section.tabs-details .right-detail .col-md-6.col-lg-4.col-6 {
            padding: 10px;
        }

        @-ms-viewport {
            width: device-width;
        }

        section.tabs-details .container-fluid {
            padding: 0;
        }

        section.tabs-details .row {
            margin: 0;
        }

        section.tabs-details .col-md-12 {
            padding: 0;
        }

        .navbar {
            padding: .5rem 0rem;
        }



        header#navbar {
            background: inherit;
        }

        .sticky .navbar-dark .navbar-nav .nav-link {
            color: #000;
        }

        section.top-part {
            position: relative;
            background: linear-gradient( 60deg,#f5e8e8 0,#e62d2e 100%);
        }

        div#navbar .col-md-3 {
            margin: auto;
        }

        .navbar-nav {
            padding-left: 40px;
        }

        .header-innner-detail {
            padding-top: 70px;
        }

        .top .sticky .logo a {
            color: #000 !important;
            font-size: 20px;
        }

        section.middle-banner img {
            max-width: 100%;
        }

        section.middle-banner .col-md-12 {
            padding: 0;
        }

        .sticky + .content {
            padding-top: 80px;
        }

        .bg-dark {
            background: inherit !important;
        }


        .shadow-effect {
            background: #fff;
            padding: 8px;
            border-radius: 4px;
            text-align: center;
            border-top-left-radius: calc(.3125rem - 1px);
            border-top-right-radius: calc(.3125rem - 1px);
        }

        #customers-testimonials .shadow-effect p {
            font-family: inherit;
            font-size: 17px;
            line-height: 1.5;
            margin: 0 0 17px 0;
            font-weight: 300;
        }

        .testimonial-name {
            margin: -17px auto 0;
            display: table;
            width: auto;
            background: #3190E7;
            padding: 9px 35px;
            border-radius: 12px;
            text-align: center;
            color: #fff;
            box-shadow: 0 9px 18px rgba(0,0,0,0.12), 0 5px 7px rgba(0,0,0,0.05);
        }

        #customers-testimonials .item {
            text-align: center;
            /* padding: 10px; */
            /* margin-bottom: 80px; */
            opacity: 1;
            -webkit-transform: scale3d(0.9, 0.9, 1);
            /* transform: scale3d(0.8, 0.8, 1); */
            /* -webkit-transition: all 0.3s ease-in-out; */
            -moz-transition: all 0.3s ease-in-out;
            -webkit-transform: scale3d(0.8, 0.8, 1);
            -webkit-transition: all 0.3s ease-in-out;
            -moz-transition: all 0.3s ease-in-out;
            transition: all 0.3s ease-in-out;
            /* transition: all 0.3s ease-in-out; */
        }

        #customers-testimonials .owl-item.active.center .item {
            opacity: 1;
            -webkit-transform: scale3d(1.0, 1.0, 1);
            transform: scale3d(1.0, 1.0, 1);
        }

        .owl-carousel .owl-item img {
            object-fit: cover;
            margin: 0 auto 17px;
            border-top-left-radius: calc(.3125rem - 1px);
            border-top-right-radius: calc(.3125rem - 1px);
            display: block;
            min-height: 300px;
            width: 100%;
        }

        #customers-testimonials.owl-carousel .owl-dots .owl-dot.active span,
        #customers-testimonials.owl-carousel .owl-dots .owl-dot:hover span {
            background: #3190E7;
            transform: translate3d(0px, -50%, 0px) scale(0.7);
        }

        #customers-testimonials.owl-carousel .owl-dots {
            display: inline-block;
            width: 100%;
            text-align: center;
        }

            #customers-testimonials.owl-carousel .owl-dots .owl-dot {
                display: inline-block;
                outline: inherit !important;
            }

                #customers-testimonials.owl-carousel .owl-dots .owl-dot span {
                    background: #14630a !important;
                    display: inline-block;
                    height: 20px;
                    border: 3px solid transparent;
                    border-radius: 100%;
                    margin: 0 2px 5px;
                    transform: translate3d(0px, -50%, 0px) scale(0.3);
                    transform-origin: 50% 50% 0;
                    transition: all 250ms ease-out 0s;
                    width: 20px;
                }


        section.tabs-details .collapse:not(.show) {
            display: block;
        }

        section.tabs-details button.btn.btn-primary {
            display: none;
        }

        .tabs-details ul.scrollable-tabs li {
            list-style: none;
            display: inline-block;
            padding-right: 40px;
        }

            .tabs-details ul.scrollable-tabs li a img {
                padding: 12px;
                background: #fff;
                /* max-width: 27%; */
                /* max-width: inherit; */
                border-radius: 50%;
                width: 55px;
                height: 55px;
            }

        .tabs-details ul.scrollable-tabs span {
            color: #fff;
            padding-top: 15px;
            text-transform: capitalize;
            display: block;
            text-align: center;
        }

        .services {
            /* position: absolute; */
            background: linear-gradient( 60deg,var(--primary-bg-color) 0,var(--dark-theme) 100%) !important;
            padding: 25px 0px;
            background-color: #293462;
            /* bottom: 60px; */
            border-top-left-radius: 80px !important;
            border-bottom-right-radius: 80px !important;
            /*z-index: 999;*/
            border-left-width: 8px;
            border-left-color: #8bc34a;
            border-right-width: 8px;
            border-right-color: #8bc34a;
            box-shadow: 0 0 10px !important;
            /* text-align: center; */
            /* bottom: -113px; */
        }

        /* .services:before {
    content: '';
    position: absolute;
    background: url(../images/bg-pattern01.png);
    left: 0;
    top: 0;
    z-index: 0;
    width: 100%;
    height: 194px;
    background-repeat: no-repeat;
    background-size: cover;
} */
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <div class="main-content-body">
            <div class="row row-sm">
                <div class="col-lg-12">
                    <div id="app1">
                        <asp:UpdatePanel runat="server" ID="updateRolePanel">
                            <ContentTemplate>
                                <div class="container-fluid">
                                    <strong>
                                        <marquee runat="server" id="news"></marquee>
                                    </strong>
                                    <div class="row">
                                        <div class="card" v-for="(itemAll,index) in ServiceAll" data-intro="itemAll.Name">
                                            <div class="card-header bg-transparent border-success">
                                                <h5 class="">{{itemAll.Name}}</h5>
                                            </div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="services">
                                                        <ul>
                                                            <li v-for="item in JSON.parse(itemAll.ServiceData)" v-if="itemAll.ServiceData!=null">
                                                                <a v-on:click="checkcinform(item)" target="_self">
                                                                    <img v-bind:src="'../images/icon/'+item.Image" class="img-sec">
                                                                    <span>{{item.Name}}</span>
                                                                </a>
                                                            </li>
                                                            <li v-if="itemAll.ServiceData==null" data-intro="Active Your Service">
                                                                <a target="_self" href="ActiveService.aspx">
                                                                    <img src="../images/icon/Service.png" class="img-sec">
                                                                    <span>Active Your Service</span>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <div class="row" style="display: none">
                                                    <div class="col-6 col-sm-4 col-lg-2" v-if="index==0" data-toggle="modal" data-target="#DrCodeUPI">
                                                        <div class="serviceBox">
                                                            <div class="service-icon">
                                                                <span><i class="fa fa-plus"></i></span>
                                                            </div>
                                                            <h3 class="title">Add Balance</h3>

                                                        </div>
                                                    </div>
                                                    <div class="col-6 col-sm-4 col-lg-2" v-if="index==0">
                                                        <div class="serviceBox">
                                                            <a href="FundRequest.aspx">
                                                                <div class="service-icon">
                                                                    <span><i class="fa fa-solid fa-circle-plus fa-beat"></i></span>
                                                                </div>
                                                                <h3 class="title">Fund Request</h3>
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <div class="col-6 col-sm-4 col-lg-2" v-for="item in JSON.parse(itemAll.ServiceData)">
                                                        <div class="serviceBox red">
                                                            <a v-on:click="checkcinform(item)" target="_self">
                                                                <div class="service-icon">
                                                                    <span>
                                                                        <img v-bind:src="'../images/Service/'+item.Image" height="50" /></span>
                                                                    <%--<span><i v-bind:class="item.ICON" class="fa fa-tank"  style="font-size: inherit !important;"></i></span>--%>
                                                                </div>

                                                                <h3 class="title">{{item.Name}}</h3>
                                                            </a>

                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="card">
                                            <div class="card-header bg-transparent border-success">
                                                <h5 class="">Other Service Links</h5>
                                            </div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-xl-3 col-lg-3 col-md-12" v-for="item in OffLine">
                                                        <a v-bind:href="item.Link" target="_blank">
                                                            <div class="card text-center">
                                                                <img height="150" width="263" class="card-img-top w-100" v-bind:src="'../images/Service/'+item.Icon" alt="">
                                                                <div class="card-body">
                                                                    <h4 class="card-title mb-3">{{item.Name}}</h4>
                                                                </div>
                                                            </div>
                                                        </a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <asp:HiddenField runat="server" ID="hdnAuth" ClientIDMode="Static" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div class="modal fade" id="newsdata" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">News</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Repeater ID="repeater1" runat="server">
                        <ItemTemplate>
                            <h4 class="heading-main"><%#Eval("Name") %></h4>
                            <div class="row">
                                <div class="col-12">
                                    <div class="icon card-white">
                                        <a>
                                            <%#Eval("Description") %>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        new Vue({
            el: '#app1',
            data() {
                return {
                    BBPS: [],
                    Service: [],
                    ServiceAll: [],
                    CommingService: [],
                    OffLine: [],
                    Search: ""
                }
            },
            methods: {
                async BBPSData() {

                    const article = {
                        "MethodName": "GetBBPSService"
                    }
                    axios.post("home.aspx/GetData", article)
                        .then(response => this.BBPS = JSON.parse(response.data.d));// .then(function (response) { debugger; this.MainData =response.data.d; });

                },
                async OfflineLink() {

                    const article = {
                        "MethodName": "GetOffLine"
                    }
                    axios.post("home.aspx/GetOffLine", article)
                        .then(response => this.OffLine = JSON.parse(response.data.d));// .then(function (response) { debugger; this.MainData =response.data.d; });

                },
                async ServiceData() {
                    const article = {
                        "MethodName": "ServiceData",
                        "Auth": $("#hdnAuth").val()
                    }
                    axios.post("home.aspx/ServiceData", article)
                        .then(response => this.Service = JSON.parse(response.data.d));// .then(function (response) { debugger; this.MainData =response.data.d; });

                },

                async ServiceDataAll() {
                    const article = {
                        "MethodName": "ServiceData",
                        "Auth": $("#hdnAuth").val()
                    }
                    axios.post("home.aspx/ServiceDataAll", article)
                        .then(response => this.ServiceAll = JSON.parse(response.data.d));// .then(function (response) { debugger; this.MainData =response.data.d; });

                }
                ,
                async CoomingSoonServiceData() {
                    const article = {
                        "MethodName": "ServiceData",
                        "Auth": $("#hdnAuth").val()
                    }
                    axios.post("home.aspx/CommingSoonService", article)
                        .then(response => this.CommingService = JSON.parse(response.data.d));// .then(function (response) { debugger; this.MainData =response.data.d; });

                }
                ,
                checkcinform: function (objData) {
                    if (objData.Onoff == true) {
                        window.open(objData.URL, "_self");
                    } else {
                        alertify
                            .alert(objData.Reason, function () {
                                alertify.message('OK');
                            });
                    }
                }
            },
            mounted() {
                this.BBPSData();
                this.ServiceDataAll();
                this.OfflineLink();
                this.CoomingSoonServiceData();
            }
        });

    </script>
</asp:Content>

