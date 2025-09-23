<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Admin_Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.5.0/css/all.css" integrity="sha384-B4dIYHKNBt8Bc12p+WXckhzcICo0wtJAoU8YZTY5qE0Id1GSseTk6S+L3BlXeVIU" crossorigin="anonymous" />
    <style>
        .autocompleteel, .autocompleteel .suggestionlist {
            width: 300px;
        }

            .autocompleteel input[type=text] {
                width: 100%;
                padding: 5px;
            }

        .suggestionlist {
            /*position: absolute;*/
        }

            .suggestionlist ul {
                width: 100%;
                background: whitesmoke;
                list-style: none;
                margin: 0;
                padding: 5px;
                height:300px;
                overflow-y:scroll;
            }

                .suggestionlist ul li {
                    background: white;
                    padding: 4px;
                    margin-bottom: 1px;
                }

            .suggestionlist li:not(:last-child) {
                border-bottom: 1px solid #cecece;
            }

            .suggestionlist ul li:hover {
                cursor: pointer;
                background: whitesmoke;
            }

        @import url("https://fonts.googleapis.com/css?family=Merriweather+Sans:400,400italic");
        @import url("https://fonts.googleapis.com/css?family=Merriweather+Sans:400,400italic");



        @font-face {
            font-family: "San Francisco";
            font-weight: 400;
            src: url("https://applesocial.s3.amazonaws.com/assets/styles/fonts/sanfrancisco/sanfranciscodisplay-regular-webfont.woff");
        }

        @font-face {
            font-family: "San Francisco";
            font-weight: 800;
            src: url("https://applesocial.s3.amazonaws.com/assets/styles/fonts/sanfrancisco/sanfranciscodisplay-bold-webfont.woff");
        }

        body {
            background: url(https://uhdwallpapers.org/uploads/converted/20/06/25/macos-big-sur-wwdc-2560x1440_785884-mm-90.jpg);
        }

        .menu-bar {
            width: 100%;
            height: 30px;
            position: absolute;
            top: 0;
            left: 0;
            display: flex;
            align-items: center;
            justify-content: space-between;
            background: rgba(83, 83, 83, 0.4);
            backdrop-filter: blur(50px);
            -webkit-backdrop-filter: blur(50px);
        }

            .menu-bar .left {
                display: flex;
                align-items: center;
                justify-content: space-evenly;
                width: auto;
                margin-left: 20px;
            }

                .menu-bar .left .apple-logo {
                    transform: scale(1);
                }

                .menu-bar .left .menus {
                    height: 100%;
                    display: flex;
                    align-items: center;
                    margin-left: 20px;
                    color: rgba(255, 255, 255, 0.95);
                    font-size: 14px;
                }

                .menu-bar .left .active {
                    font-weight: bold;
                    color: #fff !important;
                }

            .menu-bar .right {
                display: flex;
                align-items: center;
                justify-content: space-evenly;
                width: 380px;
                margin-right: 20px;
            }

                .menu-bar .right .vol {
                    transform: scale(0.6);
                    margin-right: -10px;
                }

                .menu-bar .right .menu-time {
                    height: 100%;
                    width: auto;
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    color: #fff;
                }

                .menu-bar .right .menu-ico {
                    height: 100%;
                    width: 10px;
                    display: flex;
                    align-items: center;
                    justify-content: center;
                }

                    .menu-bar .right .menu-ico .control-center {
                        -webkit-filter: invert(100%);
                        filter: invert(100%);
                        transform: scale(0.5);
                    }

                    .menu-bar .right .menu-ico .siri {
                        transform: scale(0.7);
                        object-fit: fill;
                    }

                    .menu-bar .right .menu-ico i {
                        display: contents;
                        font-size: 16px;
                        color: #fff;
                    }

        .dock {
            display: flex;
            justify-content: center;
            position: fixed;
            /* bottom: 20px; */
            left: 50%;
            bottom: 0px;
            width: 100% !important;
            transform: translateX(-50%);
            z-index: 99999999999999;
            background: linear-gradient(-29deg,#1064e8 0,rgb(255 255 255) 100%) !important;
        }

            .dock .dock-container {
                padding: 3px;
                width: auto;
                height: 100%;
                display: flex;
                align-items: center;
                justify-content: center;
                border-radius: 16px;
                background: rgba(83, 83, 83, 0.25);
                backdrop-filter: blur(13px);
                -webkit-backdrop-filter: blur(13px);
                border: 1px solid rgba(255, 255, 255, 0.18);
            }

                .dock .dock-container .li-bin {
                    margin-left: 20px;
                    border-left: 1.5px solid rgba(255, 255, 255, 0.4);
                    padding: 0px 10px;
                }

                .dock .dock-container .li-1::after {
                    position: absolute;
                    width: 5px;
                    height: 5px;
                    border-radius: 50%;
                    background: rgba(255, 255, 255, 0.5);
                    content: "";
                    bottom: 2px;
                }

                .dock .dock-container li {
                    list-style: none;
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    width: 50px;
                    height: 50px;
                    vertical-align: bottom;
                    transition: 0.2s;
                    transform-origin: 50% 100%;
                }

                    .dock .dock-container li:hover {
                        margin: 0px 13px 0px 13px;
                    }

                    .dock .dock-container li .name {
                        position: absolute;
                        top: -70px;
                        background: rgba(0, 0, 0, 0.5);
                        color: rgba(255, 255, 255, 0.9);
                        height: 10px;
                        padding: 10px 15px;
                        display: flex;
                        align-items: center;
                        justify-content: center;
                        border-radius: 5px;
                        visibility: hidden;
                    }

                        .dock .dock-container li .name::after {
                            content: "";
                            position: absolute;
                            bottom: -10px;
                            width: 0;
                            height: 0;
                            backdrop-filter: blur(13px);
                            -webkit-backdrop-filter: blur(13px);
                            border-left: 10px solid transparent;
                            border-right: 10px solid transparent;
                            border-top: 10px solid rgba(0, 0, 0, 0.5);
                        }

                    .dock .dock-container li .ico {
                        width: 100%;
                        height: 100%;
                        object-fit: cover;
                        transition: 0.2s;
                    }

                    .dock .dock-container li .ico-bin {
                        width: 94% !important;
                        height: 94% !important;
                        object-fit: cover;
                        transition: 0.2s;
                    }

                        .dock .dock-container li .ico-bin:hover {
                            margin-left: 10px;
                        }

        .li-1:hover .name {
            visibility: visible !important;
        }

        .li-2:hover .name {
            visibility: visible !important;
        }

        .li-3:hover .name {
            visibility: visible !important;
        }

        .li-4:hover .name {
            visibility: visible !important;
        }

        .li-5:hover .name {
            visibility: visible !important;
        }

        .li-6:hover .name {
            visibility: visible !important;
        }

        .li-7:hover .name {
            visibility: visible !important;
        }

        .li-8:hover .name {
            visibility: visible !important;
        }

        .li-9:hover .name {
            visibility: visible !important;
        }

        .li-10:hover .name {
            visibility: visible !important;
        }

        .li-11:hover .name {
            visibility: visible !important;
        }

        .li-12:hover .name {
            visibility: visible !important;
        }

        .li-13:hover .name {
            visibility: visible !important;
        }

        .li-14:hover .name {
            visibility: visible !important;
        }

        .li-15:hover .name {
            visibility: visible !important;
        }

        ul.dots * {
            /* disable border-box from bootstrap */
            box-sizing: content-box;
            list-style-type: none;
            display: flex;
        }

        ul.dots a {
            text-decoration: none;
            font-size: 20px;
            color: #34495e;
        }

            ul.dots a:hover {
                text-decoration: none;
                font-size: 20px;
                color: #bdc3c7;
            }

        ul.dots li {
            display: box;
            position: relative;
            width: 100%;
            padding-left: 20px;
            display: inline-block;
        }

            ul.dots li:hover {
                /*background: #2c3e50;*/
            }

            ul.dots li span {
                display: block;
                -webkit-border-radius: 30px;
                -moz-border-radius: 30px;
                border-radius: 30px;
                background-color: #FFF;
                -webkit-box-shadow: 1px 1px 5px #808080;
                -moz-box-shadow: 1px 1px 5px #808080;
                box-shadow: 1px 1px 5px #808080;
                padding: 10px;
                width: 30px;
                height: 30px;
                margin: 0 auto;
                line-height: 30px;
                text-align: center;
                position: relative;
            }

            ul.dots li mark {
                -webkit-border-radius: 20px;
                -moz-border-radius: 20px;
                border-radius: 20px;
                border: 2px solid #FFF;
                width: 20px;
                height: 20px;
                background-color: #FF6B6B;
                position: absolute;
                top: -5px;
                left: -10px;
                font-size: 10px;
                line-height: 20px;
                font-family: 'Roboto', sans-serif;
                font-weight: 400;
                color: #FFF;
                font-weight: 700;
            }

                ul.dots li mark.big {
                    width: 30px;
                    height: 30px;
                    -webkit-border-radius: 30px;
                    -moz-border-radius: 30px;
                    border-radius: 30px;
                    line-height: 30px;
                    font-size: 16px;
                    top: -10px;
                    left: -15px;
                }

                ul.dots li mark.green {
                    background-color: #27ae60;
                }

                ul.dots li mark.blue {
                    background-color: #3498db;
                }

        ul.dots > li > a > span > mark {
            -webkit-animation-name: bounceIn;
            animation-name: bounceIn;
            -webkit-transform-origin: center bottom;
            -ms-transform-origin: center bottom;
            transform-origin: center bottom;
            -webkit-animation-duration: 1s;
            animation-duration: 1s;
            -webkit-animation-fill-mode: both;
            animation-fill-mode: both;
            -webkit-animation-iteration-count: 1;
            animation-iteration-count: 1;
        }

        ul.dots > li:hover > a > span > mark {
            -webkit-animation-name: bounce;
            animation-name: bounce;
        }

            ul.dots > li:hover > a > span > mark.rubberBand {
                -webkit-animation-name: rubberBand;
                animation-name: rubberBand;
            }

            ul.dots > li:hover > a > span > mark.swing {
                -webkit-transform-origin: top center;
                -ms-transform-origin: top center;
                transform-origin: top center;
                -webkit-animation-name: swing;
                animation-name: swing;
            }

            ul.dots > li:hover > a > span > mark.tada {
                -webkit-animation-name: tada;
                animation-name: tada;
            }

            ul.dots > li:hover > a > span > mark.wobble {
                -webkit-animation-name: wobble;
                animation-name: wobble;
            }

        ul.dots li {
            list-style-type: circle;
        }

        code {
            background: #ecf0f1;
        }

        /* animation keyframes */
        @-webkit-keyframes bounce {
            0%, 20%, 53%, 80%, 100% {
                -webkit-transition-timing-function: cubic-bezier(0.215, 0.610, 0.355, 1.000);
                transition-timing-function: cubic-bezier(0.215, 0.610, 0.355, 1.000);
                -webkit-transform: translate3d(0,0,0);
                transform: translate3d(0,0,0);
            }

            40%, 43% {
                -webkit-transition-timing-function: cubic-bezier(0.755, 0.050, 0.855, 0.060);
                transition-timing-function: cubic-bezier(0.755, 0.050, 0.855, 0.060);
                -webkit-transform: translate3d(0, -30px, 0);
                transform: translate3d(0, -30px, 0);
            }

            70% {
                -webkit-transition-timing-function: cubic-bezier(0.755, 0.050, 0.855, 0.060);
                transition-timing-function: cubic-bezier(0.755, 0.050, 0.855, 0.060);
                -webkit-transform: translate3d(0, -15px, 0);
                transform: translate3d(0, -15px, 0);
            }

            90% {
                -webkit-transform: translate3d(0,-4px,0);
                transform: translate3d(0,-4px,0);
            }
        }

        @keyframes bounce {
            0%, 20%, 53%, 80%, 100% {
                -webkit-transition-timing-function: cubic-bezier(0.215, 0.610, 0.355, 1.000);
                transition-timing-function: cubic-bezier(0.215, 0.610, 0.355, 1.000);
                -webkit-transform: translate3d(0,0,0);
                transform: translate3d(0,0,0);
            }

            40%, 43% {
                -webkit-transition-timing-function: cubic-bezier(0.755, 0.050, 0.855, 0.060);
                transition-timing-function: cubic-bezier(0.755, 0.050, 0.855, 0.060);
                -webkit-transform: translate3d(0, -30px, 0);
                transform: translate3d(0, -30px, 0);
            }

            70% {
                -webkit-transition-timing-function: cubic-bezier(0.755, 0.050, 0.855, 0.060);
                transition-timing-function: cubic-bezier(0.755, 0.050, 0.855, 0.060);
                -webkit-transform: translate3d(0, -15px, 0);
                transform: translate3d(0, -15px, 0);
            }

            90% {
                -webkit-transform: translate3d(0,-4px,0);
                transform: translate3d(0,-4px,0);
            }
        }

        @-webkit-keyframes bounceIn {
            0%, 20%, 40%, 60%, 80%, 100% {
                -webkit-transition-timing-function: cubic-bezier(0.215, 0.610, 0.355, 1.000);
                transition-timing-function: cubic-bezier(0.215, 0.610, 0.355, 1.000);
            }

            0% {
                opacity: 0;
                -webkit-transform: scale3d(.3, .3, .3);
                transform: scale3d(.3, .3, .3);
            }

            20% {
                -webkit-transform: scale3d(1.1, 1.1, 1.1);
                transform: scale3d(1.1, 1.1, 1.1);
            }

            40% {
                -webkit-transform: scale3d(.9, .9, .9);
                transform: scale3d(.9, .9, .9);
            }

            60% {
                opacity: 1;
                -webkit-transform: scale3d(1.03, 1.03, 1.03);
                transform: scale3d(1.03, 1.03, 1.03);
            }

            80% {
                -webkit-transform: scale3d(.97, .97, .97);
                transform: scale3d(.97, .97, .97);
            }

            100% {
                opacity: 1;
                -webkit-transform: scale3d(1, 1, 1);
                transform: scale3d(1, 1, 1);
            }
        }

        @keyframes bounceIn {
            0%, 20%, 40%, 60%, 80%, 100% {
                -webkit-transition-timing-function: cubic-bezier(0.215, 0.610, 0.355, 1.000);
                transition-timing-function: cubic-bezier(0.215, 0.610, 0.355, 1.000);
            }

            0% {
                opacity: 1;
                -webkit-transform: scale3d(.3, .3, .3);
                transform: scale3d(.3, .3, .3);
            }

            20% {
                -webkit-transform: scale3d(1.1, 1.1, 1.1);
                transform: scale3d(1.1, 1.1, 1.1);
            }

            40% {
                -webkit-transform: scale3d(.9, .9, .9);
                transform: scale3d(.9, .9, .9);
            }

            60% {
                opacity: 1;
                -webkit-transform: scale3d(1.03, 1.03, 1.03);
                transform: scale3d(1.03, 1.03, 1.03);
            }

            80% {
                -webkit-transform: scale3d(.97, .97, .97);
                transform: scale3d(.97, .97, .97);
            }

            100% {
                opacity: 1;
                -webkit-transform: scale3d(1, 1, 1);
                transform: scale3d(1, 1, 1);
            }
        }

        @-webkit-keyframes rubberBand {
            0% {
                -webkit-transform: scale3d(1, 1, 1);
                transform: scale3d(1, 1, 1);
            }

            30% {
                -webkit-transform: scale3d(1.25, 0.75, 1);
                transform: scale3d(1.25, 0.75, 1);
            }

            40% {
                -webkit-transform: scale3d(0.75, 1.25, 1);
                transform: scale3d(0.75, 1.25, 1);
            }

            50% {
                -webkit-transform: scale3d(1.15, 0.85, 1);
                transform: scale3d(1.15, 0.85, 1);
            }

            65% {
                -webkit-transform: scale3d(.95, 1.05, 1);
                transform: scale3d(.95, 1.05, 1);
            }

            75% {
                -webkit-transform: scale3d(1.05, .95, 1);
                transform: scale3d(1.05, .95, 1);
            }

            100% {
                -webkit-transform: scale3d(1, 1, 1);
                transform: scale3d(1, 1, 1);
            }
        }

        @keyframes rubberBand {
            0% {
                -webkit-transform: scale3d(1, 1, 1);
                transform: scale3d(1, 1, 1);
            }

            30% {
                -webkit-transform: scale3d(1.25, 0.75, 1);
                transform: scale3d(1.25, 0.75, 1);
            }

            40% {
                -webkit-transform: scale3d(0.75, 1.25, 1);
                transform: scale3d(0.75, 1.25, 1);
            }

            50% {
                -webkit-transform: scale3d(1.15, 0.85, 1);
                transform: scale3d(1.15, 0.85, 1);
            }

            65% {
                -webkit-transform: scale3d(.95, 1.05, 1);
                transform: scale3d(.95, 1.05, 1);
            }

            75% {
                -webkit-transform: scale3d(1.05, .95, 1);
                transform: scale3d(1.05, .95, 1);
            }

            100% {
                -webkit-transform: scale3d(1, 1, 1);
                transform: scale3d(1, 1, 1);
            }
        }

        @-webkit-keyframes swing {
            20% {
                -webkit-transform: rotate3d(0, 0, 1, 15deg);
                transform: rotate3d(0, 0, 1, 15deg);
            }

            40% {
                -webkit-transform: rotate3d(0, 0, 1, -10deg);
                transform: rotate3d(0, 0, 1, -10deg);
            }

            60% {
                -webkit-transform: rotate3d(0, 0, 1, 5deg);
                transform: rotate3d(0, 0, 1, 5deg);
            }

            80% {
                -webkit-transform: rotate3d(0, 0, 1, -5deg);
                transform: rotate3d(0, 0, 1, -5deg);
            }

            100% {
                -webkit-transform: rotate3d(0, 0, 1, 0deg);
                transform: rotate3d(0, 0, 1, 0deg);
            }
        }

        @keyframes swing {
            20% {
                -webkit-transform: rotate3d(0, 0, 1, 15deg);
                transform: rotate3d(0, 0, 1, 15deg);
            }

            40% {
                -webkit-transform: rotate3d(0, 0, 1, -10deg);
                transform: rotate3d(0, 0, 1, -10deg);
            }

            60% {
                -webkit-transform: rotate3d(0, 0, 1, 5deg);
                transform: rotate3d(0, 0, 1, 5deg);
            }

            80% {
                -webkit-transform: rotate3d(0, 0, 1, -5deg);
                transform: rotate3d(0, 0, 1, -5deg);
            }

            100% {
                -webkit-transform: rotate3d(0, 0, 1, 0deg);
                transform: rotate3d(0, 0, 1, 0deg);
            }
        }

        @-webkit-keyframes tada {
            0% {
                -webkit-transform: scale3d(1, 1, 1);
                transform: scale3d(1, 1, 1);
            }

            10%, 20% {
                -webkit-transform: scale3d(.9, .9, .9) rotate3d(0, 0, 1, -3deg);
                transform: scale3d(.9, .9, .9) rotate3d(0, 0, 1, -3deg);
            }

            30%, 50%, 70%, 90% {
                -webkit-transform: scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, 3deg);
                transform: scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, 3deg);
            }

            40%, 60%, 80% {
                -webkit-transform: scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, -3deg);
                transform: scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, -3deg);
            }

            100% {
                -webkit-transform: scale3d(1, 1, 1);
                transform: scale3d(1, 1, 1);
            }
        }

        @keyframes tada {
            0% {
                -webkit-transform: scale3d(1, 1, 1);
                transform: scale3d(1, 1, 1);
            }

            10%, 20% {
                -webkit-transform: scale3d(.9, .9, .9) rotate3d(0, 0, 1, -3deg);
                transform: scale3d(.9, .9, .9) rotate3d(0, 0, 1, -3deg);
            }

            30%, 50%, 70%, 90% {
                -webkit-transform: scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, 3deg);
                transform: scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, 3deg);
            }

            40%, 60%, 80% {
                -webkit-transform: scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, -3deg);
                transform: scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, -3deg);
            }

            100% {
                -webkit-transform: scale3d(1, 1, 1);
                transform: scale3d(1, 1, 1);
            }
        }

        @-webkit-keyframes wobble {
            0% {
                -webkit-transform: none;
                transform: none;
            }

            15% {
                -webkit-transform: translate3d(-25%, 0, 0) rotate3d(0, 0, 1, -5deg);
                transform: translate3d(-25%, 0, 0) rotate3d(0, 0, 1, -5deg);
            }

            30% {
                -webkit-transform: translate3d(20%, 0, 0) rotate3d(0, 0, 1, 3deg);
                transform: translate3d(20%, 0, 0) rotate3d(0, 0, 1, 3deg);
            }

            45% {
                -webkit-transform: translate3d(-15%, 0, 0) rotate3d(0, 0, 1, -3deg);
                transform: translate3d(-15%, 0, 0) rotate3d(0, 0, 1, -3deg);
            }

            60% {
                -webkit-transform: translate3d(10%, 0, 0) rotate3d(0, 0, 1, 2deg);
                transform: translate3d(10%, 0, 0) rotate3d(0, 0, 1, 2deg);
            }

            75% {
                -webkit-transform: translate3d(-5%, 0, 0) rotate3d(0, 0, 1, -1deg);
                transform: translate3d(-5%, 0, 0) rotate3d(0, 0, 1, -1deg);
            }

            100% {
                -webkit-transform: none;
                transform: none;
            }
        }

        @keyframes wobble {
            0% {
                -webkit-transform: none;
                transform: none;
            }

            15% {
                -webkit-transform: translate3d(-25%, 0, 0) rotate3d(0, 0, 1, -5deg);
                transform: translate3d(-25%, 0, 0) rotate3d(0, 0, 1, -5deg);
            }

            30% {
                -webkit-transform: translate3d(20%, 0, 0) rotate3d(0, 0, 1, 3deg);
                transform: translate3d(20%, 0, 0) rotate3d(0, 0, 1, 3deg);
            }

            45% {
                -webkit-transform: translate3d(-15%, 0, 0) rotate3d(0, 0, 1, -3deg);
                transform: translate3d(-15%, 0, 0) rotate3d(0, 0, 1, -3deg);
            }

            60% {
                -webkit-transform: translate3d(10%, 0, 0) rotate3d(0, 0, 1, 2deg);
                transform: translate3d(10%, 0, 0) rotate3d(0, 0, 1, 2deg);
            }

            75% {
                -webkit-transform: translate3d(-5%, 0, 0) rotate3d(0, 0, 1, -1deg);
                transform: translate3d(-5%, 0, 0) rotate3d(0, 0, 1, -1deg);
            }

            100% {
                -webkit-transform: none;
                transform: none;
            }
        }

        .notification {
            background-color: #555;
            color: white;
            text-decoration: none;
            padding: 15px 26px;
            position: relative;
            display: inline-block;
            border-radius: 2px;
        }

            .notification:hover {
                background: red;
            }

            .notification .badge {
                position: absolute;
                top: -10px;
                left: 0px;
                padding: 5px 10px;
                border-radius: 50%;
                background-color: red;
                color: white;
            }
    </style>
    <script type="text/javascript">
        var soundObject = null;
        function PlaySound() {
            if (soundObject != null) {
                document.body.removeChild(soundObject);
                soundObject.removed = true;
                soundObject = null;
            }
            soundObject = document.createElement("embed");
            soundObject.setAttribute("src", "https://unikpe.com/img/WelcomeTone.wav");
            soundObject.setAttribute("hidden", true);
            soundObject.setAttribute("autostart", true);
            document.body.appendChild(soundObject);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="app1">
    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <div class="content-wrapper">

                <div class="row">
                    <div class="col-md-12 mb-4 stretch-card transparent">
                        <asp:Repeater runat="server" ID="rptRoleData">
                            <ItemTemplate>
                                <div class="col-md-3">
                                    <div class="card rounded mb-2">
                                        <div class="card-body p-3">
                                            <div class="media">
                                                <img src="../assets/frontpage/img/icon/teamwork.gif" alt="image" class="img-sm me-3 rounded-circle">
                                                <div class="media-body">
                                                    <h6 class="mb-1"><%# Eval("Name") %></h6>
                                                    <p class="mb-0 text-muted" style="font-size: x-large; font-weight: bold; color: red !important;">
                                                        <%# Eval("CountM") %>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12 mb-4 stretch-card transparent">
                        <div class="col-md-3">
                            <div class="card rounded mb-2">
                                <div class="card-body p-3">
                                    <a href="DayBook.aspx?Type=Recharge">
                                        <div class="media">
                                            <i class="fa fa-book" aria-hidden="true"></i>
                                            <div class="media-body">
                                                <h6 class="mb-1">Recharge Daybook</h6>

                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="card-body p-3">
                                    <a href="DayBook.aspx?Type=AEPS">
                                        <div class="media">
                                            <i class="fa fa-book" aria-hidden="true"></i>
                                            <div class="media-body">
                                                <h6 class="mb-1">AEPS Daybook</h6>

                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="card rounded mb-2">
                                <div class="card-body p-3">
                                    <a href="DayBook.aspx?Type=DMT">
                                        <div class="media">
                                            <i class="fa fa-book" aria-hidden="true"></i>
                                            <div class="media-body">
                                                <h6 class="mb-1">MoneyTransfer Daybook</h6>

                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="card-body p-3">
                                    <a href="DayBook.aspx?Type=Payout">
                                        <div class="media">
                                            <i class="fa fa-book" aria-hidden="true"></i>
                                            <div class="media-body">
                                                <h6 class="mb-1">Payout Daybook</h6>

                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="card rounded mb-2">
                                <div class="card-body p-3">
                                    <a href="DayBook.aspx?Type=MATMs">
                                        <div class="media">
                                            <i class="fa fa-book" aria-hidden="true"></i>
                                            <div class="media-body">
                                                <h6 class="mb-1">MATM Daybook</h6>

                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="card-body p-3">
                                    <a href="DayBook.aspx?Type=Aadhar">
                                        <div class="media">
                                            <i class="fa fa-book" aria-hidden="true"></i>
                                            <div class="media-body">
                                                <h6 class="mb-1">Aadhar Daybook</h6>

                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="card rounded mb-2">
                                <div class="card-body p-3">
                                    <a href="DayBook.aspx?Type=UPILoad">
                                        <div class="media">
                                            <i class="fa fa-book" aria-hidden="true"></i>
                                            <div class="media-body">
                                                <h6 class="mb-1">UPILoad Daybook</h6>

                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="card-body p-3">
                                    <a href="DayBook.aspx?Type=UPITransfer">
                                        <div class="media">
                                            <i class="fa fa-book" aria-hidden="true"></i>
                                            <div class="media-body">
                                                <h6 class="mb-1">UPITransfer Daybook</h6>

                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="card rounded mb-2">
                                <div class="card-body p-3">
                                    <a href="DayBook.aspx?Type=Van">
                                        <div class="media">
                                            <i class="fa fa-book" aria-hidden="true"></i>
                                            <div class="media-body">
                                                <h6 class="mb-1">Van Daybook</h6>

                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="card-body p-3">
                                    <a href="DayBook.aspx?Type=FundRequest">
                                        <div class="media">
                                            <i class="fa fa-book" aria-hidden="true"></i>
                                            <div class="media-body">
                                                <h6 class="mb-1">FundRequest Daybook</h6>

                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>

                <div v-for="post in MainData">
                    <div class="row">
                        <asp:Repeater runat="server" ID="rptDataRequest">
                            <ItemTemplate>
                                <div class="col-md-3 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="d-sm-flex flex-row flex-wrap text-center text-sm-left align-items-center">
                                                <div class="ml-sm-3 ml-md-0 ml-xl-3 mt-2 mt-sm-0 mt-md-2 mt-xl-0">
                                                    <h6 class="mb-0">
                                                        <a href="UserQuery.aspx" class="btn btn-lg btn-custom notification">UserQuery <span class='<%# (Convert.ToInt32(Eval("UserQuery").ToString())==0?"":"badge") %>'><%# (Convert.ToInt32(Eval("UserQuery").ToString())==0?"":Eval("UserQuery")) %></span></a>
                                                    </h6>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="d-sm-flex flex-row flex-wrap text-center text-sm-left align-items-center">
                                                <div class="ml-sm-3 ml-md-0 ml-xl-3 mt-2 mt-sm-0 mt-md-2 mt-xl-0">
                                                    <h6 class="mb-0">
                                                        <a href="AEPSWithdrawRequest.aspx" class="btn btn-lg btn-custom notification">Payout <span class='<%# (Convert.ToInt32(Eval("AEPSREQUEST").ToString())==0?"":"badge") %>'><%# (Convert.ToInt32(Eval("AEPSREQUEST").ToString())==0?"":Eval("AEPSREQUEST")) %></span></a>
                                                    </h6>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="d-sm-flex flex-row flex-wrap text-center text-sm-left align-items-center">
                                                <div class="ml-sm-3 ml-md-0 ml-xl-3 mt-2 mt-sm-0 mt-md-2 mt-xl-0">
                                                    <h6 class="mb-0"><a href="memberlist.aspx" class="btn btn-lg btn-custom notification">SignUp<span class='<%# (Convert.ToInt32(Eval("TotalSignuptoday").ToString())==0?"":"badge") %>'><%# (Convert.ToInt32(Eval("TotalSignuptoday").ToString())==0?"":Eval("TotalSignuptoday")) %></span></a></h6>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="d-sm-flex flex-row flex-wrap text-center text-sm-left align-items-center">
                                                <div class="ml-sm-3 ml-md-0 ml-xl-3 mt-2 mt-sm-0 mt-md-2 mt-xl-0">
                                                    <h6 class="mb-0"><a href="AEPSWithdrawRequest.aspx" class="btn btn-lg btn-custom notification">AEPS Withdraw Request<span class='<%# (Convert.ToInt32(Eval("AEPSREQUEST").ToString())==0?"":"badge") %>'><%# (Convert.ToInt32(Eval("AEPSREQUEST").ToString())==0?"":Eval("AEPSREQUEST")) %></span></a></h6>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card">
                                    <div class="card">

                                        <div class="card-body">
                                            <h4 class="card-title">Select Date</h4>
                                            <div class="d-flex justify-content-between">
                                                <p class="text-muted">
                                                    <input type="date" v-model="Datex" v-on:change="init" class="form-control" />
                                                    <%--<asp:TextBox runat="server" ID="txtFromDateA" OnTextChanged="txtFromDateA_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:TextBox>--%>
                                                </p>
                                            </div>
                                            <div class="progress progress-md">
                                                <div class="progress-bar bg-success w-25" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card">
                                    <div class="card">
                                        <a href="EwalletSummay.aspx">
                                            <div class="card-body">
                                                <h4 class="card-title">E-Wallet Summary</h4>
                                                <div class="d-flex justify-content-between">
                                                    <p class="text-muted">max: </p>
                                                </div>
                                                <div class="progress progress-md">
                                                    <div class="progress-bar bg-success w-25" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card">
                                    <div class="card">
                                        <a href="BalanceTransfer.aspx">
                                            <div class="card-body">
                                                <h4 class="card-title">Balance Transfer</h4>
                                                <div class="d-flex justify-content-between">
                                                    <p class="text-muted">max: </p>
                                                </div>
                                                <div class="progress progress-md">
                                                    <div class="progress-bar bg-success w-25" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card">
                                    <div class="card">
                                        <a href="Registration.aspx">
                                            <div class="card-body">
                                                <h4 class="card-title">Registration</h4>
                                                <div class="d-flex justify-content-between">
                                                    <p class="text-muted">max: </p>
                                                </div>
                                                <div class="progress progress-md">
                                                    <div class="progress-bar bg-danger w-25" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="row" style="display: none">
                        <nav id="sidebar">
                            <ul class="dots" style="display: flex;">
                                <li title="Recharge Success">
                                    <a href="#">
                                        <span class="fas fa-mobile"><mark style="background-color: green">0</mark></span>
                                    </a>
                                    <div><strong>Recharge</strong></div>
                                </li>
                                <li title="AEPS Success">
                                    <a href="#">
                                        <span class="fas fa-fingerprint"><mark style="background-color: green">0</mark></span>
                                    </a>
                                    <div><strong>AEPS</strong></div>
                                </li>
                                <li title="Money Transfer Success">
                                    <a href="#">
                                        <span class="fas fa-bank"><mark style="background-color: green" class="rubberBand">0</mark></span>
                                    </a>
                                    <div><strong>Money Transfer</strong></div>
                                </li>
                                <li title="Payout Success">
                                    <a href="#">
                                        <span class="fas fa-bank"><mark style="background-color: green" class="rubberBand">0</mark></span>
                                    </a>
                                    <div><strong>Payout</strong></div>
                                </li>
                                <li title="MATM Success">
                                    <a href="#">
                                        <span class="fas fa-bank"><mark style="background-color: green" class="rubberBand">0</mark></span>
                                    </a>
                                    <div><strong>MATM</strong></div>
                                </li>
                                <li title="UPI Success">
                                    <a href="#">
                                        <span class="fas fa-barcode"><mark style="background-color: green" class="rubberBand">0</mark></span>
                                    </a>
                                    <div><strong>UPI</strong></div>
                                </li>
                                <li title="AadharPay Success">
                                    <a href="#">
                                        <span class="fas fa-barcode"><mark style="background-color: green" class="rubberBand">0</mark></span>
                                    </a>
                                    <div><strong>AadharPay</strong></div>
                                </li>

                                <li title="Pan Success">
                                    <a href="#">
                                        <span class="fas fa-box"><mark style="background-color: green" class="rubberBand">0</mark></span>
                                    </a>
                                    <div><strong>Pan</strong></div>
                                </li>
                            </ul>
                        </nav>

                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Todays DMT</h4>
                                            <div class="d-flex justify-content-between">

                                                <p class="text-muted">max: {{post.DMT}}</p>
                                                <p class="progress-bar bg-danger w-50 card-title text-white">Sur.: {{post.DMTSurcharge}}</p>
                                            </div>
                                            <div class="progress progress-md">
                                                <div class="progress-bar bg-info w-25" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Total Recharge</h4>
                                            <div class="d-flex justify-content-between">
                                                <p class="text-muted">max: {{post.Recharge}} </p>
                                                <p class="progress-bar bg-success w-50 card-title text-white">Comm: {{post.RechargeCommission}} </p>
                                            </div>
                                            <div class="progress progress-md">
                                                <div class="progress-bar bg-success w-25" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Total AEPS</h4>
                                            <div class="d-flex justify-content-between">
                                                <p class="text-muted">max: {{post.AEPS}}</p>
                                                <p class="progress-bar bg-success w-50 card-title text-white">Comm: {{post.AEPSCommission}}</p>
                                            </div>
                                            <div class="progress progress-md">
                                                <div class="progress-bar bg-danger w-25" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Total Payout</h4>
                                            <div class="d-flex justify-content-between">
                                                <p class="text-muted">max: {{post.Payout}}</p>
                                                <p class="progress-bar bg-danger w-50 card-title text-white">Charge: {{post.PayoutSurcharge}}</p>
                                            </div>
                                            <div class="progress progress-md">
                                                <div class="progress-bar bg-warning w-25" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Todays MATM</h4>
                                            <div class="d-flex justify-content-between">
                                                <p class="text-muted">max: {{post.MATM}}</p>
                                                <p class="progress-bar bg-success w-50 card-title text-white">Comm: {{post.MATMCommission}}</p>
                                            </div>
                                            <div class="progress progress-md">
                                                <div class="progress-bar bg-info w-25" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Total Aadharpay</h4>
                                            <div class="d-flex justify-content-between">
                                                <p class="text-muted">max:  {{post.AadharPay}}</p>
                                                <p class="progress-bar bg-danger w-50 card-title text-white">Surcharge: {{post.AadharPaySurcharge}}</p>
                                            </div>
                                            <div class="progress progress-md">
                                                <div class="progress-bar bg-success w-25" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Total UPI</h4>
                                            <div class="d-flex justify-content-between">
                                                <p class="text-muted">max: {{post.UPI}}</p>
                                                <p class="progress-bar bg-danger w-50 card-title text-white">Charge: {{post.UPISurcharge}}</p>
                                            </div>
                                            <div class="progress progress-md">
                                                <div class="progress-bar bg-danger w-25" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Total Pan</h4>
                                            <div class="d-flex justify-content-between">
                                                <p class="text-muted">Avg. Session</p>
                                                <p class="progress-bar bg-danger w-50 card-title text-white">max: {{post.Pan}}</p>
                                            </div>
                                            <div class="progress progress-md">
                                                <div class="progress-bar bg-warning w-25" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Total Fund Request</h4>
                                            <div class="d-flex justify-content-between">
                                                <p class="text-muted">Avg. Session</p>
                                                <p class="progress-bar bg-danger w-50 card-title text-white">max: {{post.FundRequest}}</p>
                                            </div>
                                            <div class="progress progress-md">
                                                <div class="progress-bar bg-warning w-25" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-6 col-md-6 col-xl-3 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">UPI Transfer</h4>
                                            <div class="d-flex justify-content-between">
                                                <p class="text-muted">max: {{post.UPITransfer}}</p>
                                                <p class="progress-bar bg-danger w-50 card-title text-white">Charge: {{post.UPITransferSurcharge}}</p>
                                            </div>
                                            <div class="progress progress-md">
                                                <div class="progress-bar bg-warning w-25" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="row" style="display: none">
                        <div class="col-md-4 grid-margin">
                            <div class="card bg-facebook d-flex align-items-center">
                                <div class="card-body">
                                    <div class="d-flex flex-row align-items-center">
                                        <i class="text-white icon-md">&#8377;</i>
                                        <div class="ml-3">
                                            <h6 class="text-white">{{post.MainBalance}}</h6>
                                            <p class="mt-2 text-white card-text">Main Wallet</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 grid-margin">
                            <div class="card bg-linkedin d-flex align-items-center">
                                <div class="card-body">
                                    <div class="d-flex flex-row align-items-center">
                                        <i class="text-white icon-md">&#8377;</i>
                                        <div class="ml-3">
                                            <h6 class="text-white">{{post.AEPSBalance}}</h6>
                                            <p class="mt-2 text-white card-text">AEPS Wallet</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 grid-margin">
                            <div class="card bg-twitter d-flex align-items-center">
                                <div class="card-body">
                                    <div class="d-flex flex-row align-items-center">
                                        <i class="text-white icon-md">&#8377;</i>
                                        <div class="ml-3">
                                            <h6 class="text-white">0</h6>
                                            <p class="mt-2 text-white card-text">Commission</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="dock">
                        <div class="col-md-2 grid-margin">
                            <div class="card bg-facebook d-flex align-items-center">
                                <div class="mt-2">
                                    <div class="d-flex flex-row align-items-center">
                                        <i class="text-white icon-md">&#8377;</i>
                                        <div class="ml-3">
                                            <h6 class="text-white">{{post.MainBalance}}</h6>
                                            <p class="mt-2 text-white card-text">Main Wallet</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="dock-container">

                            <li class="li-1">
                                <div class="name">Finder</div>
                                <img class="ico" src="https://uploads-ssl.webflow.com/5f7081c044fb7b3321ac260e/5f70853981255cc36b3a37af_finder.png" alt="" style="transform: scale(1) translateY(0px);">
                            </li>
                            <li class="li-2">
                                <div class="name">Siri</div>
                                <img class="ico" src="https://uploads-ssl.webflow.com/5f7081c044fb7b3321ac260e/5f70853ff3bafbac60495771_siri.png" alt="" style="transform: scale(1) translateY(0px);">
                            </li>
                            <li class="li-3">
                                <div class="name">LaunchPad</div>
                                <img class="ico" src="https://uploads-ssl.webflow.com/5f7081c044fb7b3321ac260e/5f70853943597517f128b9b4_launchpad.png" alt="" style="transform: scale(1) translateY(0px);">
                            </li>
                            <li class="li-4">
                                <div class="name">Contacts</div>
                                <img class="ico" src="https://uploads-ssl.webflow.com/5f7081c044fb7b3321ac260e/5f70853743597518c528b9b3_contacts.png" alt="" style="transform: scale(1) translateY(0px);">
                            </li>
                            <li class="li-5">
                                <div class="name">Notes</div>
                                <img class="ico" src="https://uploads-ssl.webflow.com/5f7081c044fb7b3321ac260e/5f70853c849ec3735b52cef9_notes.png" alt="" style="transform: scale(1) translateY(0px);">
                            </li>
                            <li class="li-6">
                                <div class="name">Reminders</div>
                                <img class="ico" src="https://uploads-ssl.webflow.com/5f7081c044fb7b3321ac260e/5f70853d44d99641ce69afeb_reminders.png" alt="" style="transform: scale(1) translateY(0px);">
                            </li>
                            <li class="li-7">
                                <div class="name">Photos</div>
                                <img class="ico" src="https://uploads-ssl.webflow.com/5f7081c044fb7b3321ac260e/5f70853c55558a2e1192ee09_photos.png" alt="" style="transform: scale(1) translateY(0px);">
                            </li>
                            <li class="li-8" data-toggle="modal" data-target="#MemberSearch">
                                <div class="name">Search Member</div>
                                <img class="ico" src='<%= ConstantsData.checkstatus %>' alt="" style="transform: scale(1) translateY(0px);">
                            </li>
                            <li class="li-9">
                                <div class="name">FaceTime</div>
                                <img class="ico" src="https://uploads-ssl.webflow.com/5f7081c044fb7b3321ac260e/5f708537f18e2cb27247c904_facetime.png" alt="" style="transform: scale(1) translateY(0px);">
                            </li>
                            <li class="li-10">
                                <div class="name">Music</div>
                                <img class="ico" src="https://uploads-ssl.webflow.com/5f7081c044fb7b3321ac260e/5f70853ba0782d6ff2aca6b3_music.png" alt="" style="transform: scale(1) translateY(0px);">
                            </li>
                            <li class="li-11">
                                <div class="name">Podcasts</div>
                                <img class="ico" src="https://uploads-ssl.webflow.com/5f7081c044fb7b3321ac260e/5f70853cc718ba9ede6888f9_podcasts.png" alt="" style="transform: scale(1) translateY(0px);">
                            </li>
                            <li class="li-12">
                                <div class="name">TV</div>
                                <img class="ico" src="https://uploads-ssl.webflow.com/5f7081c044fb7b3321ac260e/5f708540dd82638d7b8eda70_tv.png" alt="" style="transform: scale(1) translateY(0px);">
                            </li>
                            <li class="li-12">
                                <div class="name">App Store</div>
                                <img class="ico" src="https://uploads-ssl.webflow.com/5f7081c044fb7b3321ac260e/5f70853270b5e2ccfd795b49_appstore.png" alt="" style="transform: scale(1) translateY(0px);">
                            </li>
                            <li class="li-14">
                                <div class="name">Safari</div>
                                <img class="ico" src="https://uploads-ssl.webflow.com/5f7081c044fb7b3321ac260e/5f70853ddd826358438eda6d_safari.png" alt="" style="transform: scale(1) translateY(0px);">
                            </li>
                            <li class="li-bin li-15">
                                <div class="name">Bin</div>
                                <img class="ico ico-bin" src="https://findicons.com/files/icons/569/longhorn_objects/128/trash.png" alt="" style="transform: scale(1) translateY(0px);">
                            </li>

                        </div>
                        <div class="col-md-2 grid-margin">
                            <div class="card bg-linkedin d-flex align-items-center">
                                <div class="mt-2">
                                    <div class="d-flex flex-row align-items-center">
                                        <i class="text-white icon-md">&#8377;</i>
                                        <div class="ml-3">
                                            <h6 class="text-white">{{post.AEPSBalance}}</h6>
                                            <p class="mt-2 text-white card-text">AEPS Wallet</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    ...
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="MemberSearch" tabindex="-1" role="dialog" aria-labelledby="MemberSearch" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-body table table-responsive">
                    <div class="row">
                            <div class="col-md-6">
                                <label>Mobile/LoginID</label>
                                <vueauto-complete v-model="Search"  @input="handleInput1()"></vueauto-complete>
                            </div>
                            <div class="col-md-6">
                            <input type="button" @click="SearchData" value="Search" class="btn btn-danger mt-4" />
                                </div>
                        </div>
                       <div class="form-group" id="rowdata">
                           <table class="table table-responsive" id="tbrow"></table>
                       </div>
                    </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
        </div>
    <script>

        new Vue({
            el: '#app1',
            data() {
                return {
                    MainData: [],
                    MainDataCount: [],
                    MemberDetails: [],
                    Search: "",
                    Datex: ""
                }
            },
            methods: {
                Getdate() {
                    var today = new Date();
                    var dd = String(today.getDate()).padStart(2, '0');
                    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
                    var yyyy = today.getFullYear();

                    today = yyyy + '-' + mm + '-' + dd;
                    return today;
                },
                init() {
                    var el = this;
                    const article = {
                        "MethodName": "getflight",
                        "Data": el.Datex
                    }
                    axios.post("Dashboard.aspx/GetData", article)
                        .then(response => {
                            debugger;
                            this.MainData = response.data.d
                            this.initCount();
                        });// .then(function (response) { debugger; this.MainData =response.data.d; });
                },
                SearchData() {
                    var el = this;
                    el.MemberDetails = [];
                    const article = {
                        "Data": el.Search
                    }
                    axios.post("Dashboard.aspx/MemberDetails", article)
                        .then(response => {
                            el.MemberDetails = (response.data.d == '[]' ? [] : JSON.parse(response.data.d));
                            JSONToHTMLTable(el.MemberDetails, "rowdata")
                        });// .then(function (response) { debugger; this.MainData =response.data.d; });
                },
                initCount() {
                    var el = this;
                    const article = {
                        "MethodName": "getflight",
                        "Data": el.Datex
                    }
                    axios.post("Dashboard.aspx/GetDataCount", article)
                        .then(response => {
                            this.MainDataCount = JSON.parse(response.data.d);
                        });// .then(function (response) { debugger; this.MainData =response.data.d; });
                }

            },
            mounted() {
                this.Datex = this.Getdate();
                this.init();

            },


        });

        function JSONToHTMLTable(jsonData, elementToBind) {

            //This Code gets all columns for header   and stored in array col
            var col = [];
            for (var i = 0; i < jsonData.length; i++) {
                for (var key in jsonData[i]) {
                    if (col.indexOf(key) === -1) {
                        col.push(key);
                    }
                }
            }

            //This Code creates HTML table
            var table = document.getElementById("tbrow");
            $("#tbrow tr").remove();
            //This Code getsrows for header creader above.
            var tr = table.insertRow(-1);

            for (var i = 0; i < col.length; i++) {
                var th = document.createElement("th");
                th.innerHTML = col[i];
                tr.appendChild(th);
            }

            //This Code adds data to table as rows
            for (var i = 0; i < jsonData.length; i++) {

                tr = table.insertRow(-1);

                for (var j = 0; j < col.length; j++) {
                    var tabCell = tr.insertCell(-1);
                    tabCell.innerHTML = jsonData[i][col[j]];
                }
            }

            //This Code gets the all columns for header
            var divContainer = document.getElementById(elementToBind);
            divContainer.innerHTML = "";
            divContainer.appendChild(table);
        }

        let icons = document.querySelectorAll(".ico");
        let length = icons.length;

        icons.forEach((item, index) => {
            item.addEventListener("mouseover", (e) => {
                focus(e.target, index);
            });
            item.addEventListener("mouseleave", (e) => {
                icons.forEach((item) => {
                    item.style.transform = "scale(1)  translateY(0px)";
                });
            });
        });

        const focus = (elem, index) => {
            let previous = index - 1;
            let previous1 = index - 2;
            let next = index + 1;
            let next2 = index + 2;

            if (previous == -1) {
                console.log("first element");
                elem.style.transform = "scale(1.5)  translateY(-10px)";
            } else if (next == icons.length) {
                elem.style.transform = "scale(1.5)  translateY(-10px)";
                console.log("last element");
            } else {
                elem.style.transform = "scale(1.5)  translateY(-10px)";
                icons[previous].style.transform = "scale(1.2) translateY(-6px)";
                icons[previous1].style.transform = "scale(1.1)";
                icons[next].style.transform = "scale(1.2) translateY(-6px)";
                icons[next2].style.transform = "scale(1.1)";
            }
        };

        Vue.component('vueauto-complete', {
            data: function () {
                return {
                    searchText: '',
                    suggestiondata: []
                }
            },
            template: `<div class='autocompleteel' > 
	  		<div >
		  		<input type='text' @keyup='loadSuggestions' placeholder='Enter some text' 
		  			v-model='searchText' 
      			>	<br> 
		  		<div class='suggestionlist' v-if="suggestiondata.length" >
		  		<ul > 
		  			<li v-for= '(item,index) in suggestiondata' @click='itemSelected(index)' > 
		  				{{ item.LoginID }} ({{ item.Name }})
		  			</li>  
		  		</ul>
		  		</div>  
	  		</div>
	  	</div>`,
            methods: {
                loadSuggestions: function (e) {
                    var el = this;
                    this.suggestiondata = [];

                    var el = this;
                    el.MemberDetails = [];
                    const article = {
                        "Data": el.searchText
                    }
                    axios.post("Dashboard.aspx/MemberSearch", article)
                        .then(response => {
                            el.suggestiondata = (response.data.d == '[]' ? [] : JSON.parse(response.data.d));
                        });

                    //if (this.searchText != '') {
                    //    axios.get('ajaxfile.php', {
                    //        params: {
                    //            search: this.searchText
                    //        }
                    //    })
                    //        .then(function (response) {
                    //            el.suggestiondata = response.data;
                    //        });
                    //}

                },
                itemSelected: function (index) {
                    var id = this.suggestiondata[index].Msrno;
                    var name = this.suggestiondata[index].Name;
                    var LoginID = this.suggestiondata[index].LoginID;

                    this.searchText = LoginID;
                    this.suggestiondata = [];

                    this.$emit("input", LoginID);

                }

            },
        })
    </script>
</asp:Content>

