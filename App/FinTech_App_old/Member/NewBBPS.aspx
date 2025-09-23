<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true"
    CodeFile="NewBBPS.aspx.cs" Inherits="Service_NewBBPS" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <link href="https://unikpe.com/css/styles.69798e3a.chunk.css" rel="stylesheet" />
        


        <link rel="preload" href="https://www.freecharge.in/_next/static/css/pages/index.44a618dc.chunk.css"
            as="style" />
        <link rel="stylesheet" href="https://www.freecharge.in/_next/static/css/pages/index.44a618dc.chunk.css"
            data-n-p="" />
        <script src="https://assets.adobedtm.com/extensions/EPbde2f7ca14e540399dcc1f8208860b7b/AppMeasurement.min.js"
            async=""></script>
        <script
            src="https://assets.adobedtm.com/extensions/EPbde2f7ca14e540399dcc1f8208860b7b/AppMeasurement_Module_ActivityMap.min.js"
            async=""></script>
        <script src="https://connect.facebook.net/signals/config/343077906463563?v=2.9.81&amp;r=stable"
            async=""></script>
        <script src="https://connect.facebook.net/signals/plugins/identity.js?v=2.9.81" async=""></script>
        <script src="https://connect.facebook.net/en_US/fbevents.js" async=""></script>
   

        <style data-n-href="https://www.freecharge.in/_next/static/css/pages/dcc.3cea41d3.chunk.css">
            @media (min-width: 1025px) {
                .main-container .main-content {
                    margin: 0 25px;
                }
            }

            .gjZEHA {
                appearance: none;
                background-color: rgb(249, 249, 252);
                border-width: 1px;
                border-style: solid;
                border-color: rgb(216, 216, 216);
                border-radius: 10px;
                color: rgb(34, 34, 34);
                font-family: sspBold;
                font-size: 16px;
                line-height: 14px;
                padding: 14px 0px 14px 13px;
                width: 100%;
            }

            .kjqNrV {
                position: relative;
            }

            .dMWSZh {
                opacity: 0;
                position: absolute;
                right: 0px;
                top: 14px;
                transition: all 0.3s ease-out 0s;
                visibility: hidden;
            }

            .iUhaYg {
                appearance: none;
                background-color: rgb(249, 249, 252);
                border-width: 1px;
                border-style: solid;
                border-color: rgb(227, 113, 77);
                border-radius: 10px;
                color: rgb(34, 34, 34);
                font-family: sspBold;
                font-size: 16px;
                line-height: 14px;
                padding: 14px 0px 14px 13px;
                width: 100%;
            }

            .dMWSZh {
                opacity: 0;
                position: absolute;
                right: 0px;
                top: 14px;
                transition: all 0.3s ease-out 0s;
                visibility: hidden;
            }

            .iKzHuO {
                color: rgb(255, 255, 255);
                cursor: pointer;
                overflow: hidden;
                position: relative;
                width: 100%;
                background: linear-gradient(to right, rgb(246, 105, 63) 0%, rgb(235, 131, 69) 100%);
                border: none;
                border-radius: 10px;
                font-family: sspBold;
                font-size: 16px;
                height: 54px;
            }



            .search-layout .search-layout-section {
                margin-bottom: 10px
            }

                .search-layout .search-layout-section .section-head {
                    color: #5c5c5c;
                    font-family: sspBold;
                    font-size: .857em;
                    margin-bottom: 10px;
                    text-transform: uppercase
                }

            .search-layout .search-layout-field-wrapper {
                align-items: center;
                background: #fff;
                border: 1px solid #d8d8d8;
                border-radius: 2px;
                display: flex;
                height: 40px;
                padding: 11px;
                width: 100%;
            }

                .search-layout .search-layout-field-wrapper .search-layout-field-icon img {
                    width: 14px
                }

                .search-layout .search-layout-field-wrapper .search-layout-input-wrapper {
                    width: 100%
                }

                    .search-layout .search-layout-field-wrapper .search-layout-input-wrapper input {
                        border: 0;
                        font-size: 1em;
                        height: 100%;
                        padding: 0 0 0 13px
                    }

                        .search-layout .search-layout-field-wrapper .search-layout-input-wrapper input:focus {
                            outline: 0
                        }

                        .search-layout .search-layout-field-wrapper .search-layout-input-wrapper input::placeholder {
                            color: #aeaeae;
                            opacity: 1;
                            -webkit-text-fill-color: #aeaeae
                        }

                        .search-layout .search-layout-field-wrapper .search-layout-input-wrapper input:-ms-input-placeholder {
                            color: #aeaeae;
                            -webkit-text-fill-color: #aeaeae
                        }

                        .search-layout .search-layout-field-wrapper .search-layout-input-wrapper input::-ms-input-placeholder {
                            color: #aeaeae;
                            -webkit-text-fill-color: #aeaeae
                        }

            .search-layout .operator-tile {
                align-items: center;
                cursor: pointer;
                display: flex;
                margin-bottom: 20px
            }

                .search-layout .operator-tile.hide {
                    display: none
                }

                .search-layout .operator-tile .logo-container {
                    position: relative
                }

                .search-layout .operator-tile .operator-img {
                    border-radius: 50%;
                    height: 40px;
                    margin-right: 14px;
                    width: 40px
                }

                .search-layout .operator-tile .operator-tag {
                    background-color: #f74;
                    border-radius: 10px;
                    color: #fff;
                    font-size: .571em;
                    padding: 2px 4px;
                    position: absolute;
                    right: 8px;
                    top: 0
                }

                .search-layout .operator-tile .operator-content {
                    display: flex;
                    flex-direction: column;
                    line-height: 1.3
                }

                    .search-layout .operator-tile .operator-content .operator-name {
                        color: #5c5c5c;
                        font-family: sspBold;
                        font-size: .857em
                    }

                    .search-layout .operator-tile .operator-content .operator-subname {
                        color: #aeaeae;
                        font-family: sspRegular;
                        font-size: .714em;
                        margin-top: 5px
                    }

            .search-layout .search-layout-collapsible-section {
                margin-top: 10px;
                max-height: 500px;
                overflow: hidden;
                -webkit-transition: all .5s ease-in-out 0s;
                -moz-transition: all .5s ease-in-out 0s;
                -ms-transition: all .5s ease-in-out 0s;
                -o-transition: all .5s ease-in-out 0s;
                transition: all .5s ease-in-out 0s
            }

            .search-layout .no-operator {
                align-items: center;
                display: flex;
                flex-direction: column;
                justify-content: center;
                margin-top: 75px
            }

                .search-layout .no-operator img {
                    width: 80px
                }

                .search-layout .no-operator label {
                    color: #5c5c5c;
                    font-family: sspRegular;
                    font-size: 1em;
                    margin-top: 25px
                }

            .search-layout .search-layout-section.no-operator {
                margin: 0
            }

                .search-layout .search-layout-section.no-operator .section-head {
                    width: 100%
                }

                .search-layout .search-layout-section.no-operator .no-operator-tile {
                    align-items: center;
                    display: flex;
                    flex-direction: column;
                    justify-content: center;
                    padding: 70px 20px;
                    text-align: center
                }

                    .search-layout .search-layout-section.no-operator .no-operator-tile img {
                        max-width: 160px;
                        width: auto
                    }

                    .search-layout .search-layout-section.no-operator .no-operator-tile .no-result-title {
                        color: #222;
                        font-family: sspBold;
                        font-size: 1.143em;
                        margin-top: 20px
                    }

                    .search-layout .search-layout-section.no-operator .no-operator-tile .no-result-subtitle {
                        color: #666;
                        font-size: 1em;
                        line-height: 20px;
                        margin-top: 10px
                    }



            .search-layout.new-layout .desktop-operators-main-header {
                margin: 15px 0 25px;
                position: relative
            }

                .search-layout.new-layout .desktop-operators-main-header img:not(.heading-image) {
                    max-height: 210px;
                    position: absolute;
                    right: 0;
                    top: 0;
                    width: 200px
                }

                .search-layout.new-layout .desktop-operators-main-header .desktop-heading-image {
                    float: left;
                    height: 45px;
                    margin-right: 15px;
                    margin-top: 5px
                }

                    .search-layout.new-layout .desktop-operators-main-header .desktop-heading-image img.heading-image {
                        max-height: 45px;
                        max-width: 45px
                    }

                .search-layout.new-layout .desktop-operators-main-header .main-heading {
                    color: #222;
                    font-family: sspLight;
                    font-size: 1.286em;
                    font-weight: 300;
                    line-height: 24px
                }

                .search-layout.new-layout .desktop-operators-main-header .sub-heading {
                    color: #222;
                    font-family: sspRegular;
                    font-size: 1.286em;
                    line-height: 28px
                }

            .search-layout.new-layout .search-layout-section {
                margin: 5px 0 10px
            }

                /*.search-layout.new-layout .search-layout-section.search-input-wrapper {
                padding: 10px 20px 25px 0
            }*/

                .search-layout.new-layout .search-layout-section.search-input-wrapper .desktop-search-heading {
                    color: #222;
                    font-family: sspBold;
                    font-size: 1em;
                    line-height: 20px;
                    margin-bottom: 10px
                }

                .search-layout.new-layout .search-layout-section .section-head {
                    color: #222;
                    font-family: sspBold;
                    font-size: 1.143em;
                    margin-bottom: 0;
                    padding-bottom: 30px;
                    text-transform: none
                }

                .search-layout.new-layout .search-layout-section .search-tile-wrapper.hide {
                    display: none
                }

                .search-layout.new-layout .search-layout-section .search-tile-wrapper:nth-of-type(2) .group-head {
                    margin-top: 0
                }

                .search-layout.new-layout .search-layout-section .group-head {
                    color: #666;
                    font-family: sspRegular;
                    font-size: .714em;
                    line-height: 25px;
                    text-transform: capitalize
                }

            .search-layout.new-layout .section-spaced {
                margin-top: 15px
            }

            .search-layout.new-layout .search-layout-field-wrapper {
                background: #f9f9fc;
                border-radius: 10px;
                flex-direction: row-reverse;
                height: 48px;
                padding: 11px 15px
            }

                .search-layout.new-layout .search-layout-field-wrapper .search-layout-field-icon img {
                    width: 18px
                }

                .search-layout.new-layout .search-layout-field-wrapper.focus {
                    border: 1px solid #ec7f51;
                    box-shadow: 0 1px 6px #efd1c5
                }

                .search-layout.new-layout .search-layout-field-wrapper .search-layout-input-wrapper input {
                    background: none;
                    font-family: sspBold;
                    font-size: 1.286em;
                    padding: 0 13px 0 0
                }

                    .search-layout.new-layout .search-layout-field-wrapper .search-layout-input-wrapper input:focus {
                        outline: 0
                    }

                    .search-layout.new-layout .search-layout-field-wrapper .search-layout-input-wrapper input::-webkit-input-placeholder,
                    .search-layout.new-layout .search-layout-field-wrapper .search-layout-input-wrapper input::placeholder {
                        color: #666;
                        font-family: sspRegular !important
                    }

                    .search-layout.new-layout .search-layout-field-wrapper .search-layout-input-wrapper input:-ms-input-placeholder {
                        color: #666;
                        font-family: sspRegular !important
                    }

            .search-layout.new-layout .grouped-operator-tiles {
                display: flex;
                flex-wrap: wrap;
                word-break: break-word
            }

            .search-layout.new-layout .operator-tile {
                align-items: flex-start;
                background: #fff;
                border-radius: 12px;
                cursor: pointer;
                margin-bottom: 15px;
                min-height: 81px;
                min-width: 170px;
                padding: 18px;
                word-break: break-word
            }

                .search-layout.new-layout .operator-tile.flex-center {
                    align-items: center
                }

                .search-layout.new-layout .operator-tile .operator-img {
                    border-radius: 50%;
                    height: auto;
                    max-height: 45px;
                    width: 45px
                }

                .search-layout.new-layout .operator-tile .operator-content .operator-name {
                    color: #222;
                    font-family: sspBold;
                    font-size: 1em
                }

                .search-layout.new-layout .operator-tile .operator-content .operator-subname {
                    color: #666;
                    font-size: .857em
                }

            .search-layout.new-layout .search-layout-collapsible-section.collapsed .search-layout-section {
                margin: 0
            }

            @media (min-width:768px) {
                .search-layout .search-layout-section.no-operator .no-operator-tile {
                    background: #fff;
                    border-radius: 12px;
                    width: 100%
                }

                .search-layout.new-layout .search-layout-section .slider .slide {
                    margin-left: 10px !important;
                    margin-right: 10px !important
                }

                .search-layout.new-layout .search-layout-section .section-head {
                    border-bottom: 1px solid rgba(219, 219, 219, .64);
                    margin: 25px 0 20px;
                    padding-bottom: 15px
                }

                .search-layout.new-layout .search-layout-section .group-head {
                    font-size: 1em;
                    margin: 10px 0
                }

                .search-layout.new-layout .skeleton-field-wrapper {
                    max-width: 350px
                }

                .search-layout.new-layout .search-layout-field-wrapper {
                    background: #fff;
                    max-width: 350px
                }

                .search-layout.new-layout .all-providers-tile {
                    display: flex;
                    flex-wrap: wrap
                }

                .search-layout.new-layout .operator-tile {
                    -webkit-box-shadow: 0 0 9px 0 rgba(0, 0, 0, .05);
                    -moz-box-shadow: 0 0 9px 0 rgba(0, 0, 0, .05);
                    box-shadow: 0 0 9px 0 rgba(0, 0, 0, .05);
                    margin-right: 20px
                }

                    .search-layout.new-layout .operator-tile:last-of-type {
                        margin-right: 0
                    }
            }

            @media (max-width:767px) {
                .search-layout .search-layout-section.sticky {
                    position: sticky;
                    top: 61px;
                    z-index: 2;
                    -webkit-box-shadow: 0 5px 5px 0 rgba(0, 0, 0, .03);
                    -moz-box-shadow: 0 5px 5px 0 rgba(0, 0, 0, .03);
                    box-shadow: 0 5px 5px 0 rgba(0, 0, 0, .03)
                }

                .search-layout .search-layout-section.no-operator .no-operator-tile {
                    padding: 35px
                }

                    .search-layout .search-layout-section.no-operator .no-operator-tile img {
                        width: 115px
                    }

                .search-layout.new-layout .search-layout-section.providers-list {
                    background: #fff;
                    border-radius: 20px;
                    padding: 25px 18px
                }

                .search-layout.new-layout .search-layout-section.search-input-wrapper {
                    background: #fff;
                    border-radius: 0 0 20px 20px;
                    margin-top: 0;
                    padding-left: 20px
                }

                .search-layout.new-layout .search-layout-section .group-head {
                    background: #f2f5fc;
                    margin: 15px -18px;
                    padding: 0 18px
                }

                .search-layout.new-layout .grouped-operator-tiles {
                    flex-direction: column
                }

                .search-layout.new-layout .operator-tile {
                    border: 1px solid #e1ebff;
                    margin-bottom: 10px;
                    width: 100%
                }
            }

            @media (min-width:1025px) {
                .search-layout.new-layout .operator-tile {
                    width: calc(33.33% - 20px)
                }
            }

            @media (min-width:768px) and (max-width:1200px) {
                .search-layout.new-layout .operator-tile {
                    width: calc(50% - 20px)
                }
            }

            .offers-header {
                color: #5c5c5c;
                font-family: sspBold;
                font-size: .857em;
                margin: 14px 0 10px 7px;
                text-transform: uppercase
            }

            .offers-slider-wrapper {
                margin: 0
            }

                .offers-slider-wrapper .offers-slider .slide {
                    display: grid;
                    grid-template-columns: 100%
                }

            .offers-card {
                -webkit-tap-highlight-color: transparent;
                -webkit-touch-callout: none;
                -webkit-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none;
                background-color: #fff;
                border-radius: 12px;
                cursor: pointer;
                flex-direction: column;
                margin-bottom: 5px;
                padding: 20px
            }

                .offers-card,
                .offers-card .offers-image-desc {
                    display: flex;
                    flex-wrap: wrap;
                    justify-content: space-between
                }

                    .offers-card .offers-image-desc {
                        align-items: center;
                        width: 100%
                    }

                    .offers-card .img-div {
                        align-self: flex-start;
                        height: 45px;
                        margin: 3px 15px 0 0;
                        max-width: 45px
                    }

                        .offers-card .img-div img {
                            height: 100%
                        }

                    .offers-card .desc-div {
                        display: flex;
                        flex: 1;
                        flex-direction: column;
                        width: calc(100% - 60px)
                    }

                        .offers-card .desc-div .desc-head {
                            color: #222;
                            font-family: sspBold;
                            font-size: 1em;
                            line-height: 18px;
                            margin-bottom: 5px
                        }

                        .offers-card .desc-div .desc-text {
                            color: #666;
                            font-family: sspRegular;
                            font-size: .857em;
                            line-height: 16px
                        }

                            .offers-card .desc-div .desc-text.trim-text {
                                max-width: 100%;
                                overflow: hidden;
                                text-overflow: ellipsis;
                                white-space: nowrap
                            }

                    .offers-card .code-div {
                        border-top: 1px solid #f4f6f7;
                        display: flex;
                        flex-wrap: nowrap;
                        margin-top: 10px;
                        padding-left: 45px;
                        padding-top: 10px;
                        text-align: center
                    }

                        .offers-card .code-div .code-label {
                            color: #222;
                            font-size: .714em
                        }

                        .offers-card .code-div .code-btn {
                            margin-left: 5px
                        }

                            .offers-card .code-div .code-btn p {
                                color: #e3714d;
                                font-family: sspBold;
                                font-size: .714em;
                                margin: 0
                            }

            .popup-container .offer-popup-container {
                margin: 40px 0 10px;
                max-height: 450px;
                overflow: auto;
                padding: 0 20px
            }

                .popup-container .offer-popup-container::-webkit-scrollbar {
                    display: none
                }

                .popup-container .offer-popup-container .image-div img {
                    width: 100%
                }

                .popup-container .offer-popup-container .popup-desc li {
                    color: #5c5c5c;
                    font-family: sspRegular;
                    font-size: .857em;
                    line-height: 18px;
                    padding-left: 20px;
                    position: relative;
                    text-align: left
                }

                    .popup-container .offer-popup-container .popup-desc li :before {
                        background-color: #ddd;
                        border-radius: 50%;
                        content: "";
                        display: inline-block;
                        height: 6px;
                        left: 0;
                        margin-right: 10px;
                        position: absolute;
                        top: 7px;
                        width: 6px
                    }

                .popup-container .offer-popup-container .redeem-div {
                    margin-bottom: 12px;
                    margin-top: 20px
                }

                    .popup-container .offer-popup-container .redeem-div .popup-head-text {
                        color: #222;
                        margin-bottom: 10px
                    }

                    .popup-container .offer-popup-container .redeem-div .popup-desc li {
                        margin-bottom: 10px
                    }

                        .popup-container .offer-popup-container .redeem-div .popup-desc li:last-child {
                            margin-bottom: 0
                        }

                        .popup-container .offer-popup-container .redeem-div .popup-desc li p {
                            color: #666;
                            line-height: 18px
                        }

                .popup-container .offer-popup-container .tc-div .popup-head-text {
                    align-items: center;
                    color: #e3714d;
                    cursor: pointer;
                    display: inline-flex;
                    font-family: sspRegular;
                    margin-bottom: 10px
                }

                    .popup-container .offer-popup-container .tc-div .popup-head-text .arrow {
                        border: 1px solid #e3714d;
                        border-radius: 50%;
                        box-sizing: border-box;
                        display: inline-block;
                        height: 14px;
                        margin-left: 5px;
                        padding: 2px;
                        position: relative;
                        width: 14px
                    }

                        .popup-container .offer-popup-container .tc-div .popup-head-text .arrow:after {
                            border-radius: 1px;
                            border-color: transparent #e3714d #e3714d transparent;
                            border-style: solid;
                            border-width: 1px;
                            content: "";
                            height: 4px;
                            position: absolute;
                            transform-origin: 65% 65%;
                            width: 4px;
                            -webkit-transform: rotate(45deg);
                            -moz-transform: rotate(45deg);
                            -ms-transform: rotate(45deg);
                            -o-transform: rotate(45deg);
                            transform: rotate(45deg);
                            -webkit-transition: transform .5s ease-in-out 0s;
                            -moz-transition: transform .5s ease-in-out 0s;
                            -ms-transition: transform .5s ease-in-out 0s;
                            -o-transition: transform .5s ease-in-out 0s;
                            transition: transform .5s ease-in-out 0s
                        }

                        .popup-container .offer-popup-container .tc-div .popup-head-text .arrow.arrow-up:after {
                            transform-origin: 67% 67%;
                            -webkit-transform: rotate(-135deg);
                            -moz-transform: rotate(-135deg);
                            -ms-transform: rotate(-135deg);
                            -o-transform: rotate(-135deg);
                            transform: rotate(-135deg)
                        }

                .popup-container .offer-popup-container .tc-div .popup-desc {
                    max-height: 0;
                    overflow: hidden;
                    -webkit-transition: max-height .5s ease-in-out 0s;
                    -moz-transition: max-height .5s ease-in-out 0s;
                    -ms-transition: max-height .5s ease-in-out 0s;
                    -o-transition: max-height .5s ease-in-out 0s;
                    transition: max-height .5s ease-in-out 0s
                }

                    .popup-container .offer-popup-container .tc-div .popup-desc.show-desc {
                        margin-bottom: 15px;
                        margin-top: 5px;
                        max-height: 100%
                    }

                    .popup-container .offer-popup-container .tc-div .popup-desc li {
                        margin-bottom: 10px
                    }

                        .popup-container .offer-popup-container .tc-div .popup-desc li:last-child {
                            margin-bottom: 0
                        }

                        .popup-container .offer-popup-container .tc-div .popup-desc li p {
                            color: #666;
                            line-height: 18px
                        }

                .popup-container .offer-popup-container .offer-button-container button {
                    font-size: 1.143em;
                    height: auto;
                    padding: 17px 0 18px;
                    text-transform: capitalize;
                    -webkit-box-shadow: 0 8px 16px 0 #f1b8a0;
                    -moz-box-shadow: 0 8px 16px 0 #f1b8a0;
                    box-shadow: 0 8px 16px 0 #f1b8a0
                }

            .offer-popup-container {
                margin-top: 25px;
                max-height: 475px;
                overflow-y: scroll;
                padding: 0 16px
            }

                .offer-popup-container::-webkit-scrollbar {
                    display: none
                }

                .offer-popup-container .image-div img {
                    max-width: 100%
                }

                .offer-popup-container .redeem-div {
                    margin-bottom: 30px;
                    margin-top: 8px
                }

                .offer-popup-container .offer-button-container {
                    background-color: #fff;
                    bottom: 0;
                    padding: 16px 0;
                    position: -webkit-sticky;
                    position: sticky
                }

                    .offer-popup-container .offer-button-container .offer-button {
                        text-transform: uppercase
                    }

                .offer-popup-container .popup-head-text {
                    color: #5c5c5c;
                    font-family: sspBold;
                    font-size: 1em;
                    margin-bottom: 20px
                }

            @media (max-width:767px) {
                .offers-slider-wrapper .slider-section .offers-slider .slide:first-of-type .offers-card:not(.single-card) {
                    margin: 0 0 5px 10px
                }

                .offers-slider-wrapper .slider-section .offers-slider .slide:last-of-type .offers-card:not(.single-card) {
                    margin: 0 5px 5px 0
                }

                .offers-slider-wrapper .slider-section .offers-slider .slide .single-card.offers-card {
                    margin: 0 10px 5px
                }
            }

            @media (min-width:768px) {
                .offers-slider-wrapper {
                    margin: 18px 0 25px
                }

                    .offers-slider-wrapper .offers-slider .slide {
                        grid-template-columns: calc(100% - 10px)
                    }

                .offers-card {
                    -webkit-box-shadow: 0 0 9px 0 rgba(0, 0, 0, .05);
                    -moz-box-shadow: 0 0 9px 0 rgba(0, 0, 0, .05);
                    box-shadow: 0 0 9px 0 rgba(0, 0, 0, .05)
                }
            }

            @keyframes shimmer {
                0% {
                    background-position: 100% 0
                }

                to {
                    background-position: -100% 0
                }
            }

            .slider-wrapper {
                box-sizing: border-box;
                display: block;
                margin: 5px;
                position: relative;
                -webkit-tap-highlight-color: transparent;
                -ms-touch-action: pan-y;
                touch-action: pan-y;
                -webkit-touch-callout: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none
            }

                .slider-wrapper :focus {
                    outline: none
                }

                .slider-wrapper.slider-loading {
                    visibility: hidden
                }

                .slider-wrapper.slider-arrows {
                    margin-left: 5px;
                    margin-right: 5px;
                    padding: 0 15px
                }

                    .slider-wrapper.slider-arrows .slider-track {
                        position: relative
                    }

                        .slider-wrapper.slider-arrows .slider-track .arrow {
                            align-items: center;
                            background: transparent;
                            border: none;
                            color: #aeaeae;
                            cursor: pointer;
                            display: flex;
                            padding: 0;
                            position: absolute
                        }

                            .slider-wrapper.slider-arrows .slider-track .arrow:disabled span {
                                cursor: not-allowed;
                                opacity: .5
                            }

                            .slider-wrapper.slider-arrows .slider-track .arrow:focus {
                                outline: none
                            }

                            .slider-wrapper.slider-arrows .slider-track .arrow.left-arrow {
                                bottom: 0;
                                left: -12px;
                                top: 0
                            }

                                .slider-wrapper.slider-arrows .slider-track .arrow.left-arrow span {
                                    -webkit-transform: rotate(135deg);
                                    -moz-transform: rotate(135deg);
                                    -ms-transform: rotate(135deg);
                                    -o-transform: rotate(135deg);
                                    transform: rotate(135deg)
                                }

                            .slider-wrapper.slider-arrows .slider-track .arrow.right-arrow {
                                bottom: 0;
                                right: -12px;
                                top: 0
                            }

                                .slider-wrapper.slider-arrows .slider-track .arrow.right-arrow span {
                                    -webkit-transform: rotate(315deg);
                                    -moz-transform: rotate(315deg);
                                    -ms-transform: rotate(315deg);
                                    -o-transform: rotate(315deg);
                                    transform: rotate(315deg)
                                }

                            .slider-wrapper.slider-arrows .slider-track .arrow span {
                                border-bottom: 3px solid #aeaeae;
                                border-right: 3px solid #aeaeae;
                                display: inline-block;
                                height: 12px;
                                width: 12px
                            }

                .slider-wrapper.slider-indicators-inside .slider-indicators {
                    bottom: 5px;
                    left: 0;
                    position: absolute;
                    right: 0
                }

                .slider-wrapper .slider-track .slider-section {
                    overflow: hidden
                }

                    .slider-wrapper .slider-track .slider-section,
                    .slider-wrapper .slider-track .slider-section .slider {
                        -webkit-transform: translateZ(0);
                        -moz-transform: translateZ(0);
                        -ms-transform: translateZ(0);
                        -o-transform: translateZ(0);
                        transform: translateZ(0)
                    }

                        .slider-wrapper .slider-track .slider-section .slider {
                            display: flex;
                            scrollbar-width: none;
                            width: 100%
                        }

                            .slider-wrapper .slider-track .slider-section .slider::-webkit-scrollbar {
                                display: none
                            }

                            .slider-wrapper .slider-track .slider-section .slider .slide-shimmer {
                                animation-duration: 1s;
                                animation-fill-mode: forwards;
                                animation-iteration-count: infinite;
                                animation-name: shimmer;
                                animation-timing-function: linear;
                                background: linear-gradient(90deg, #eee 2%, #c5c5c5 18%, #eee 33%);
                                background-size: 200%;
                                overflow: hidden;
                                width: 100%
                            }

                            .slider-wrapper .slider-track .slider-section .slider .slide {
                                flex: 0 0 auto
                            }

                                .slider-wrapper .slider-track .slider-section .slider .slide:first-child {
                                    margin-left: 0 !important
                                }

                                .slider-wrapper .slider-track .slider-section .slider .slide:last-child {
                                    margin-right: 0 !important
                                }

                .slider-wrapper .slider-indicators {
                    display: flex;
                    justify-content: center;
                    margin-top: 5px
                }

                    .slider-wrapper .slider-indicators ul {
                        display: flex
                    }

                        .slider-wrapper .slider-indicators ul li {
                            line-height: 6px;
                            margin-right: 5px
                        }

                            .slider-wrapper .slider-indicators ul li:last-child {
                                margin-right: 0
                            }

                            .slider-wrapper .slider-indicators ul li.active span.indicator {
                                background-color: #e3714d
                            }

                            .slider-wrapper .slider-indicators ul li span.indicator {
                                background-color: #d8d8d8;
                                border-radius: 50%;
                                display: inline-block;
                                height: 6px;
                                width: 6px
                            }

            .swipeable-tabs-wrapper {
                background-color: #f2f5fc
            }

                .swipeable-tabs-wrapper .tab-labels {
                    background-color: #fff;
                    display: flex;
                    flex-wrap: nowrap;
                    overflow-x: auto
                }

                    .swipeable-tabs-wrapper .tab-labels .tab-option {
                        border-bottom: 2px solid transparent;
                        color: #9c9c9c;
                        cursor: pointer;
                        font-size: .857em;
                        font-weight: 300;
                        padding: 15px;
                        text-align: center;
                        text-transform: uppercase;
                        width: 100%
                    }

                        .swipeable-tabs-wrapper .tab-labels .tab-option,
                        .swipeable-tabs-wrapper .tab-labels .tab-option.active {
                            -webkit-transition: border-bottom .3s cubic-bezier(1, 1.01, 1, 1) 0s;
                            -moz-transition: border-bottom .3s cubic-bezier(1, 1.01, 1, 1) 0s;
                            -ms-transition: border-bottom .3s cubic-bezier(1, 1.01, 1, 1) 0s;
                            -o-transition: border-bottom .3s cubic-bezier(1, 1.01, 1, 1) 0s;
                            transition: border-bottom .3s cubic-bezier(1, 1.01, 1, 1) 0s
                        }

                            .swipeable-tabs-wrapper .tab-labels .tab-option.active {
                                border-bottom: 2px solid #e3714d;
                                color: #464646;
                                font-family: sspBold
                            }

                            .swipeable-tabs-wrapper .tab-labels .tab-option.border-under-text {
                                margin-left: 15px;
                                margin-right: 15px;
                                padding-left: 0;
                                padding-right: 0;
                                width: auto
                            }

                    .swipeable-tabs-wrapper .tab-labels::-webkit-scrollbar {
                        display: none
                    }

                .swipeable-tabs-wrapper .tab-content-wrapper {
                    overflow-x: hidden
                }

                    .swipeable-tabs-wrapper .tab-content-wrapper .tab-content {
                        display: flex;
                        flex-wrap: nowrap
                    }

                        .swipeable-tabs-wrapper .tab-content-wrapper .tab-content :focus {
                            outline: none
                        }

                        .swipeable-tabs-wrapper .tab-content-wrapper .tab-content > div {
                            flex: 0 0 auto;
                            flex-shrink: 0;
                            max-height: 0;
                            opacity: 0;
                            overflow: hidden;
                            width: 100%
                        }

                            .swipeable-tabs-wrapper .tab-content-wrapper .tab-content > div.active {
                                max-height: 100%;
                                opacity: 1
                            }

            @media (min-width:768px) {
                .swipeable-tabs-wrapper .tab-content.animation-fade > div {
                    position: absolute;
                    z-index: -1;
                    -webkit-transition: opacity .75s ease-in-out 0s;
                    -moz-transition: opacity .75s ease-in-out 0s;
                    -ms-transition: opacity .75s ease-in-out 0s;
                    -o-transition: opacity .75s ease-in-out 0s;
                    transition: opacity .75s ease-in-out 0s
                }

                .swipeable-tabs-wrapper .tab-content.animation-fade > .active {
                    position: static;
                    z-index: 1
                }
            }

            .recents-slider-container {
                background: #fff;
                border-radius: 20px;
                margin: 14px 0 19px;
                padding: 25px 18px 20px;
                -webkit-transition: transform .5s ease-in-out 0s;
                -moz-transition: transform .5s ease-in-out 0s;
                -ms-transition: transform .5s ease-in-out 0s;
                -o-transition: transform .5s ease-in-out 0s;
                transition: transform .5s ease-in-out 0s
            }

                .recents-slider-container .collapsing-text {
                    align-items: center;
                    border-radius: 16px;
                    color: #666;
                    cursor: pointer;
                    display: flex;
                    font-family: sspBold;
                    font-size: .857em;
                    height: 30px;
                    justify-content: center;
                    margin: 15px auto 7px;
                    padding: 10px 12px 10px 15px;
                    text-align: center;
                    width: 110px;
                    -webkit-box-shadow: 0 0 5px 0 rgba(0, 0, 0, .16);
                    -moz-box-shadow: 0 0 5px 0 rgba(0, 0, 0, .16);
                    box-shadow: 0 0 5px 0 rgba(0, 0, 0, .16)
                }

                    .recents-slider-container .collapsing-text:after {
                        background: url(https://pwa-cdn.freecharge.in/pwa-static/pwa/images/icons/angle-down.png) center -3px;
                        background-size: cover;
                        content: "";
                        height: 8px;
                        margin-left: 7px;
                        width: 11px
                    }

                    .recents-slider-container .collapsing-text.expanded:after {
                        -webkit-transform: rotate(180deg);
                        -moz-transform: rotate(180deg);
                        -ms-transform: rotate(180deg);
                        -o-transform: rotate(180deg);
                        transform: rotate(180deg)
                    }

                .recents-slider-container .section-head {
                    color: #222;
                    font-family: sspBold;
                    font-size: 1.143em;
                    margin-bottom: 15px
                }

                .recents-slider-container .recents-slider-section {
                    display: flex;
                    flex-wrap: wrap
                }

                    .recents-slider-container .recents-slider-section .slide-bill-details {
                        align-items: center;
                        display: flex
                    }

                        .recents-slider-container .recents-slider-section .slide-bill-details .amount-date-separator {
                            background: #ccc;
                            border-radius: 5px;
                            height: 4px;
                            margin: 0 10px;
                            width: 4px
                        }

                        .recents-slider-container .recents-slider-section .slide-bill-details .slide-date {
                            color: #7a7a7a;
                            font-size: .857em
                        }

                            .recents-slider-container .recents-slider-section .slide-bill-details .slide-date .date-value {
                                font-family: sspBold;
                                margin-left: 3px
                            }

                        .recents-slider-container .recents-slider-section .slide-bill-details .slide-amount {
                            color: #222;
                            display: flex;
                            flex-wrap: wrap;
                            font-size: .857em;
                            line-height: 13px
                        }

                            .recents-slider-container .recents-slider-section .slide-bill-details .slide-amount .amt-value {
                                align-items: center;
                                display: flex;
                                font-family: sspBold
                            }

                                .recents-slider-container .recents-slider-section .slide-bill-details .slide-amount .amt-value:before {
                                    color: #5c5c5c;
                                    content: "\20B9   ";
                                    font-size: 12px;
                                    display: inline-block;
                                    font-size: 1em;
                                    margin: 0 0 0 3px
                                }

                    .recents-slider-container .recents-slider-section .recents-slide {
                        background: #fff;
                        border: 1px solid #e1ebff;
                        border-radius: 12px;
                        cursor: pointer;
                        display: flex;
                        margin-bottom: 15px;
                        padding: 15px;
                        width: 100%
                    }

                        .recents-slider-container .recents-slider-section .recents-slide .slide-img {
                            height: 45px;
                            margin-right: 12px;
                            width: 45px
                        }

                        .recents-slider-container .recents-slider-section .recents-slide .slide-content {
                            display: flex;
                            flex-direction: column;
                            justify-content: center
                        }

                            .recents-slider-container .recents-slider-section .recents-slide .slide-content .slide-name,
                            .recents-slider-container .recents-slider-section .recents-slide .slide-content .slide-number {
                                color: #222;
                                font-family: sspBold;
                                font-size: 1em;
                                margin-bottom: 5px
                            }

            @media (max-width:767px) {
                .recents-slider-container {
                    margin: 10px 0 14px
                }

                    .recents-slider-container .collapsing-text {
                        margin: 30px auto 5px
                    }

                    .recents-slider-container .recents-slider-section {
                        display: block
                    }

                        .recents-slider-container .recents-slider-section .slide-bill-details .amount-date-separator {
                            margin: 0 5px
                        }
            }

            @media (min-width:768px) {
                .recents-slider-container {
                    border-radius: 12px;
                    padding: 25px 5px 20px 25px;
                    -webkit-box-shadow: 0 0 9px 0 rgba(0, 0, 0, .05);
                    -moz-box-shadow: 0 0 9px 0 rgba(0, 0, 0, .05);
                    box-shadow: 0 0 9px 0 rgba(0, 0, 0, .05)
                }
            }

            @media (min-width:768px) and (max-width:1200px) {
                .recents-slider-container .recents-slider-section .recents-slide {
                    margin-right: 20px;
                    width: calc(50% - 20px)
                }
            }

            @media (min-width:1025px) {
                .recents-slider-container .recents-slider-section .recents-slide {
                    margin-right: 20px;
                    width: calc(33.33% - 20px)
                }
            }
        </style>

        <script>
            function myFunction() {
                location.replace("member/Default.aspx")
            }
        </script>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div class="main-container container-fluid">
            <div class="main-content-body">
                <div class="row row-sm">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-body">
                                
                            <div id="__bbps" class="main-content">
                                <div v-if="!showbil">
                                    <div v-if="!showdiv && !showbil && !showbilError" class="search-layout new-layout">
                                        <div class="desktop-operators-main-header review-bill-header card-header">
                                            <div class="desktop-header-text">
                                                <div class="main-heading">Let's Get Your</div>
                                                <div class="sub-heading">{{GetVal()["SType"]}} Bill Payment Done!</div>
                                            </div>
                                           
                                        </div>
                                        <div class="search-layout-section search-input-wrapper ">
                                            <div class="desktop-search-heading">Search</div>
                                            <div class="search-layout-field-wrapper ">
                                                <div class="search-layout-field-icon">
                                                    <img src="https://pwa-cdn.freecharge.in/pwa-static/pwa/images/icons/search-icon.png"
                                                        alt="search field icon">
                                                </div>
                                                <div
                                                    class="input__InputContainerWrapper-sc-77palv-0 gxoua input-wrapper search-layout-input-wrapper ">
                                                    <div
                                                        class="input__InputContainer-sc-77palv-1 kjqNrV input-container">
                                                        <input type="text" v-model="search" name="search-field"
                                                            placeholder="Search" width="100%" autocomplete="off"
                                                            class="input__InputWrapper-sc-77palv-4 ldAVAW" value="">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="search-layout-collapsible-section  collapsed"
                                            style="max-height: 100%;">
                                            <div class="search-layout-section">
                                                <div class="slider-wrapper offers-slider-wrapper">
                                                    <div class="slider-track">
                                                        <div class="slider-section">
                                                            <div class="slider offers-slider" style="overflow-x: auto;">
                                                                <div data-index="0" class="slide active-slide"
                                                                    tabindex="-1"
                                                                    style="width: 380px; margin: 0px 5px;">
                                                                    <div class="offers-card">
                                                                        <div class="offers-image-desc">
                                                                            <div class="img-div">
                                                                                <img src="https://offers.freecharge.in/inappicons/mob.png"
                                                                                    alt="Offer Logo">
                                                                            </div>
                                                                            <div class="desc-div">
                                                                                <p class="desc-head">Flat ₹10 CB</p>
                                                                                <p class="desc-text">Flat Rs.10 Rs
                                                                                    cashback on min
                                                                                    txn of
                                                                                    Rs.48. Once per user. Use code- FC10
                                                                                </p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div data-index="1" class="slide" tabindex="-1"
                                                                    style="width: 380px; margin: 0px 5px;">
                                                                    <div class="offers-card">
                                                                        <div class="offers-image-desc">
                                                                            <div class="img-div">
                                                                                <img src="https://offers.freecharge.in/inappicons/electricity.png"
                                                                                    alt="Offer Logo">
                                                                            </div>
                                                                            <div class="desc-div">
                                                                                <p class="desc-head">Flat ₹40 CB</p>
                                                                                <p class="desc-text">Flat Rs.40 cashback
                                                                                    on
                                                                                    Electricity Bill
                                                                                    payments on min txn of Rs.200.For
                                                                                    new users
                                                                                    only.Use
                                                                                    code-POWNEW.</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                               
                                            </div>

                                        </div>
                                        <div class="search-layout-section providers-list">
                                            <div class="section-head">All Providers</div>
                                            <div class="search-tile-wrapper ">
                                                <div class="group-head" style="display: none">andhra pradesh</div>
                                                <div class="grouped-operator-tiles">
                                                    <div class="operator-tile  flex-center" @click="asignd(item)"
                                                        tabindex="0" v-for="item in filteredList">
                                                        <div class="logo-container">
                                                            <img v-bind:src="'./images/icon/'+item.Class"
                                                                alt="Operator Logo" class="operator-img" />
                                                        </div>
                                                        <div class="operator-content">
                                                            <div class="operator-name">{{item.Name}}</div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="providerdetails-page" v-if="showdiv && !showbil">
                                        <div class="desktop-operators-main-header">
                                            <div class="desktop-header-text">
                                                <div class="main-heading">Lets Get Your</div>
                                                <div class="sub-heading">{{GetVal()["SType"]}} Bill Payment Done!</div>
                                            </div>
                                            <img src="https://pwa-cdn.freecharge.in/pwa-static/pwa/images/icons/bbps-logo.png"
                                                alt="Bharat Bill Pay System" class="bbps-img" height="32"
                                                width="32"><img class="illustrator"
                                                src="https://pwa-cdn.freecharge.in/pwa-static/pwa/images/operators/electricity.png"
                                                title="Electricity Bill Payment Done!"
                                                alt="Electricity Bill Payment Done!">
                                        </div>
                                        <div id="jj" v-for="item in SingleBBPS">
                                            <div class="accountdetails-head">
                                                <div class="operator-details">
                                                    <div class="operator-details-container">
                                                        <img v-bind:src="'./images/icon/'+item.Class"
                                                            alt="Operator Logo" class="operator-img">
                                                        <div class="operator-content">
                                                            <div class="operator-code">{{item.Name+'('+item.ID+')'}}
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <a class="operator-edit" @click="reset()">EDIT</a>
                                                </div>
                                            </div>
                                            <div class="accountdetails-form">
                                                <div class="accountdetails-field-wrapper" v-for="(item,index) in BBPSf">
                                                    <div
                                                        class="input__InputContainerWrapper-sc-77palv-0 gxoua input-wrapper accountdetails-field error">
                                                        <div
                                                            class="input__InputContainer-sc-77palv-1 kjqNrV input-container">
                                                            <input type="tel" name="Consumer ID"
                                                                v-bind:property="'optiona'+index"
                                                                v-bind:placeholder="item.Labels" width="100%"
                                                                autocomplete="off"
                                                                class="input__InputWrapper-sc-77palv-4 iUhaYg 345authlook"
                                                                v-bind:maxlength="item.FieldMaxLen" value="">
                                                            <p class="input__SubmitWrapper-sc-77palv-8 dMWSZh">
                                                                <img src="https://pwa-cdn.freecharge.in/pwa-static/pwa/images/dcc/clear_field.svg"
                                                                    alt="Clear" title="Clear">
                                                            </p>
                                                        </div>
                                                    </div>
                                                    <div class="field-description">Please Enter Valid
                                                        {{item.Labels}}(Min.
                                                        {{item.FieldMinLen}} to Max. {{item.FieldMaxLen}})</div>
                                                </div>

                                            </div>
                                            <div class="button-wrapper">
                                                <input
                                                    class="button__PrimaryButtonWrapper-ah8457-0 iKzHuO form-submit-btn"
                                                    width="100%" type="button" value="Next" @click="bilfetc(item.ID)" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                               
                                <div class="billdetails-page" style="min-height: 577px;" v-if="showbil"
                                    v-for="itemp in BBPSfillfetch">
                                    <div class="billdetails-wrapper">
                                        <div class="billdetails-head">
                                            <div class="desktop-operators-main-header">
                                                <div class="desktop-header-text">
                                                    <div class="main-heading">Lets Get Your</div>
                                                    <div class="sub-heading">Payment Done</div>
                                                </div>
                                            </div>
                                            <div class="operators-details-edit" v-for="item in SingleBBPS">
                                                <div class="operator-details"><img
                                                        v-bind:src="'./images/icon/'+item.Class"
                                                        alt="service provider logo" class="operator-img">
                                                    <div class="operator-info-block">
                                                        <div class="operator-content">
                                                            <div>
                                                                <div class="operator-billNumber">{{itemp.billnumber}}
                                                                </div>
                                                                <div class="operator-code">{{item.Name}} ({{item.ID}})
                                                                </div>
                                                            </div><a class="operator-edit" @click="billrest">EDIT</a>
                                                        </div>
                                                        <div class="separator"></div>
                                                        <div class="user-details">Customer Name : <span
                                                                class="operator-code">{{itemp.customername}}</span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="bill-details-content" v-for="item in SingleBBPS">
                                            <div class="bill-details-head-wrapper">
                                                <div class="bill-details-head">Bill Details</div><img
                                                    src="https://pwa-cdn.freecharge.in/pwa-static/pwa/images/icons/bbps-logo.png"
                                                    alt="Bharat Bill Pay System" class="bbps-img" height="32"
                                                    width="32">
                                            </div>
                                            <div class="bill-fields-area">
                                                <div class="bill-amount-date">
                                                    <div class="bill-field">
                                                        <div class="bill-field-title">Bill Amount</div>
                                                        <div class="bill-field-value amount">{{itemp.dueamount}}</div>
                                                    </div>
                                                </div>
                                                <div class="details-popup-wrapper">
                                                    <div class="bill-field">
                                                        <div class="bill-field-title">Bill Due Date</div>
                                                        <div class="bill-field-value">{{itemp.duedate}}</div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="note-info">
                                                <div class="note-info-heading"><img
                                                        src="https://pwa-cdn.freecharge.in/pwa-static/pwa/images/paymentPage/note-info-icon.svg"
                                                        alt="Note info icon"><span>Note</span></div>
                                                <p class="note-info-text">Paying this bill allows Rechargeto fetch your
                                                    current and
                                                    future
                                                    bills
                                                    so that you can view and pay them.</p>
                                            </div>
                                            <div class="footer-content">
                                                <input
                                                    class="button__PrimaryButtonWrapper-ah8457-0 iKzHuO proceed-btn"
                                                    width="100%" type="button" value="MAKE PAYMENT"
                                                    v-on:click="myFunction();"
                                                    @click="bilfetcpay(item.ID,itemp.dueamount,itemp.billnumber)" />
                                                
                                            </div>




                                        </div>
                                    </div>
                                </div>
                                <div class="billdetails-page" style="min-height: 577px;"
                                    v-if="!showdiv && !showbil && showbilError">
                                    <div class="billdetails-wrapper">
                                        <div class="billdetails-head">
                                            <div class="desktop-operators-main-header">
                                                <div class="desktop-header-text">
                                                    <div class="main-heading">Lets Get Your</div>
                                                    <div class="sub-heading">Payment Done</div>
                                                </div>
                                            </div>
                                            <div class="operators-details-edit" v-for="item in SingleBBPS">
                                                <div class="operator-details"><img
                                                        v-bind:src="'./images/icon/'+item.Class"
                                                        alt="service provider logo" class="operator-img">
                                                    <div class="operator-info-block">
                                                        <div class="operator-content">
                                                            <div>
                                                                <div class="operator-billNumber">5645</div>
                                                                <div class="operator-code">{{item.Name}} ({{item.ID}})
                                                                </div>
                                                            </div><a class="operator-edit" @click="billrest">EDIT</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="bill-details-content">
                                            <div class="payment-received-wrapper"><img
                                                    src="https://pwa-cdn.freecharge.in/pwa-static/pwa/images/dcc/sorry_not_approved.png"
                                                    title="No Payment Due" alt="No Payment Due">
                                                <p class="no-payment-title">No Payment Due</p>
                                                <p class="no-payment-subtitle">Payment received for the billing period,
                                                    no bills due
                                                </p><input type="button" class="btn btn-danger" width="100%"
                                                    @click="billrest" value="Back" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <script>
                                new Vue({
                                    el: '#__bbps',
                                    data() {
                                        return {
                                            BBPS: [],
                                            SingleBBPS: [],
                                            BBPSf: [],
                                            BBPSfillfetch: [],
                                            search: "",
                                            showdiv: false,
                                            showbil: false,
                                            showbilError: false
                                        }
                                    },
                                    methods: {
                                        BBPSData: function () {
                                            var el = this;
                                            const article = {
                                                "MethodName": el.GetVal()["SType"]
                                            }
                                            axios.post("Services.aspx/GetData", article)
                                                .then(function (response) { el.BBPS = JSON.parse(response.data.d); });

                                        },
                                        BBPSDataF: function (ID) {
                                            var el = this;
                                            const article = {
                                                "MethodName": ID
                                            }
                                            axios.post("Services.aspx/BBPSF", article)
                                                .then(function (response) { el.BBPSf = JSON.parse(response.data.d); });

                                        },
                                        asignd: function (item) {
                                            this.SingleBBPS = [];
                                            this.SingleBBPS.push(item);
                                            this.showdiv = true;
                                            this.BBPSDataF(item.ID);
                                        },
                                        reset: function () {
                                            this.SingleBBPS = [];
                                            this.showdiv = false;
                                            this.showbilError = false;
                                            this.showbil = false;
                                        },
                                        bilfetc: function (ID) {
                                            document.getElementById('load').style.visibility = "visible";
                                            var el = this;
                                            el.BBPSfillfetch = [];
                                            var objs = {
                                                optional0: "",
                                                optional1: "",
                                                optional2: "",
                                                optional3: "",
                                                optional4: "",
                                                optional5: "",
                                                optional6: "",
                                                optional7: "",
                                                optional8: "",
                                                optional9: "",
                                            };
                                            var Flag = true;
                                            $('.345authlook').each(function (i) {
                                                if ($(this).val() == "") {
                                                    Flag = false;
                                                    $(this).css("border", "2px solid red");
                                                }
                                                objs[Object.keys(objs)[i]] = $(this).val();
                                            });
                                            if (Flag == true) {
                                                var el = this;
                                                const article = {
                                                    "OTID": ID,
                                                    "Json": JSON.stringify(objs)
                                                }
                                                axios.post("Services.aspx/BiFetch", article)
                                                    .then(function (response) {
                                                        document.getElementById('load').style.visibility = "hidden";
                                                        var PData = JSON.parse(response.data.d);
                                                        if (PData.code == "TXN") {
                                                            el.BBPSfillfetch.push(PData.data);
                                                            if (el.BBPSfillfetch[0].ResponseCode == "000") {
                                                                el.showbil = true;
                                                                el.showbilError = false;
                                                                el.showdiv = false;
                                                                SuccessMess(PData.mess);
                                                            } else {
                                                                el.showbil = false;
                                                                el.showbilError = false;
                                                                el.showdiv = true;
                                                                ErrorMess(el.BBPSfillfetch[0].ResponseMessage);
                                                            }
                                                        } else {
                                                            el.showbil = false;
                                                            el.showbilError = false;
                                                            el.showdiv = true;
                                                            ErrorMess(PData.mess);
                                                        }
                                                    });

                                            }


                                        },
                                        bilfetcpay: function (ID, Amt, no) {
                                            {
                                                document.getElementById('load').style.visibility = "visible";
                                                var el = this;
                                                var objs = {
                                                    optional0: "",
                                                    optional1: "",
                                                    optional2: "",
                                                    optional3: "",
                                                    optional4: "",
                                                    optional5: "",
                                                    optional6: "",
                                                    optional7: "",
                                                    optional8: "",
                                                    optional9: "",
                                                    Amount: Amt
                                                };

                                                var objsBiGfetc = {
                                                    Name: el.BBPSfillfetch[0].customername,
                                                    Amount: el.BBPSfillfetch[0].dueamount,
                                                    DueDate: el.BBPSfillfetch[0].duedate,
                                                    BillDate: el.BBPSfillfetch[0].billdate
                                                };

                                                var Flag = true;
                                                objs.optional0 = no;
                                               
                                                if (Flag == true) {
                                                    var el = this;
                                                    const article = {
                                                        "OTID": ID,
                                                        "Json": JSON.stringify(objs),
                                                        "BillFetch": JSON.stringify(objsBiGfetc)
                                                    }
                                                    axios.post("Services.aspx/biPay", article)
                                                        .then(function (response) {
                                                            document.getElementById('load').style.visibility = "hidden";
                                                            var JData = JSON.parse(response.data.replace('{"d":null}', ''));
                                                            if (JData.statuscode == "TXN") {
                                                                el.BBPSfillfetch = [];
                                                                el.showbil = false;
                                                                el.showbilError = false;
                                                                el.showdiv = true;
                                                                SuccessMess("Recharge Successfully Done");
                                                            } else {
                                                                el.showbil = true;
                                                                el.showbilError = false;
                                                                el.showdiv = false;
                                                                ErrorMess(JData.status);
                                                            }
                                                        });
                                                }

                                            }

                                        },
                                        billrest: function () {
                                            this.showdiv = true;
                                            this.showbil = false;
                                            this.showbilError = false;
                                        },

                                        GetVal: function () {
                                            var vars = [], hash;
                                            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                                            for (var i = 0; i < hashes.length; i++) {
                                                hash = hashes[i].split('=');
                                                vars.push(hash[0]);
                                                vars[hash[0]] = hash[1];
                                            }
                                            return vars;
                                        },


                                        //goToHome(){
                                        //this.$router.push('/Index'); 
                                        //   }

                                    },
                                    computed: {
                                        filteredList() {
                                            return this.BBPS.filter(post => {
                                                return post.Name.toLowerCase().includes(this.search.toLowerCase())
                                            })
                                        }
                                    },
                                    mounted() {
                                        this.BBPSData();
                                    }
                                });
                            </script>
                                         
                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </asp:Content>