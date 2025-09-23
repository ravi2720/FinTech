﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ComplainChat.aspx.cs" Inherits="Admin_ComplainChat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        h1 {
            font-size: 60px;
            text-align: center;
        }

        .timeline {
            position: relative;
            margin: 50px auto;
            padding: 40px 0;
            width: 1000px;
            box-sizing: border-box;
        }

            .timeline:before {
                content: '';
                position: absolute;
                left: 50%;
                width: 2px;
                height: 100%;
                background: #c5c5c5;
            }

            .timeline ul {
                padding: 0;
                margin: 0;
            }

                .timeline ul li {
                    list-style: none;
                    position: relative;
                    width: 50%;
                    padding: 20px 40px;
                    box-sizing: border-box;
                }

                    .timeline ul li:nth-child(odd) {
                        float: left;
                        text-align: right;
                        clear: both;
                    }

                    .timeline ul li:nth-child(even) {
                        float: right;
                        text-align: left;
                        clear: both;
                    }

        .content {
            padding-bottom: 20px;
            min-height: auto !important;
        }

        .timeline ul li:nth-child(odd):before {
            content: '';
            position: absolute;
            width: 10px;
            height: 10px;
            top: 24px;
            right: -6px;
            background: rgba(233,33,99,1);
            border-radius: 50%;
            box-shadow: 0 0 0 3px rgba(233,33,99,0.2);
        }

        .timeline ul li:nth-child(even):before {
            content: '';
            position: absolute;
            width: 10px;
            height: 10px;
            top: 24px;
            left: -4px;
            background: rgba(233,33,99,1);
            border-radius: 50%;
            box-shadow: 0 0 0 3px rgba(233,33,99,0.2);
        }

        .timeline ul li h3 {
            padding: 0;
            margin: 0;
            color: rgba(233,33,99,1);
            font-weight: 600;
        }

        .timeline ul li p {
            margin: 10px 0 0;
            padding: 0;
        }

        .timeline ul li .time h4 {
            margin: 0;
            padding: 0;
            font-size: 14px;
        }

        .timeline ul li:nth-child(odd) .time {
            position: absolute;
            top: 12px;
            right: -165px;
            margin: 0;
            padding: 8px 16px;
            background: rgba(233,33,99,1);
            color: #fff;
            border-radius: 18px;
            box-shadow: 0 0 0 3px rgba(233,33,99,0.3);
        }

        .timeline ul li:nth-child(even) .time {
            position: absolute;
            top: 12px;
            left: -165px;
            margin: 0;
            padding: 8px 16px;
            background: rgba(233,33,99,1);
            color: #fff;
            border-radius: 18px;
            box-shadow: 0 0 0 3px rgba(233,33,99,0.3);
        }

        @media(max-width:1000px) {
            .timeline {
                width: 100%;
            }
        }

        @media(max-width:767px) {
            .timeline {
                width: 100%;
                padding-bottom: 0;
            }

            h1 {
                font-size: 40px;
                text-align: center;
            }

            .timeline:before {
                left: 20px;
                height: 100%;
            }

            .timeline ul li:nth-child(odd),
            .timeline ul li:nth-child(even) {
                width: 100%;
                text-align: left;
                padding-left: 50px;
                padding-bottom: 50px;
            }

                .timeline ul li:nth-child(odd):before,
                .timeline ul li:nth-child(even):before {
                    top: -18px;
                    left: 16px;
                }

                .timeline ul li:nth-child(odd) .time,
                .timeline ul li:nth-child(even) .time {
                    top: -30px;
                    left: 50px;
                    right: inherit;
                }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">
                            <h3 runat="server" id="ComplainTitle"></h3>
                        </div>
                        <div class="panel-body">

                            <div class="row">

                                <div class="timeline">
                                    <ul>
                                        <li>
                                            <div class="content">
                                                <p>
                                                    <asp:TextBox runat="server" ID="txtMessage" CssClass="form-control"></asp:TextBox></p>
                                            </div>
                                            <div class="time">
                                                <h4>
                                                    <asp:Button runat="server" ID="btnAddMessage"  OnClick="btnAddMessage_Click" Text="Reply" />
                                                </h4>
                                            </div>
                                        </li>
                                        <asp:Repeater runat="server" ID="rptData">
                                            <ItemTemplate>
                                                <li>
                                                    <div class="content">
                                                        <h3><%# Eval("Name") %></h3>
                                                        <p><%# Eval("Message") %></p>
                                                    </div>
                                                    <div class="time">
                                                        <h4><%# Eval("AddDate") %></h4>
                                                    </div>
                                                </li>

                                                <div style="clear: both;"></div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

