<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="TreeBlance.aspx.cs" Inherits="Admin_TreeBlance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <style>
        /*----------------genealogy-scroll----------*/

        .genealogy-scroll::-webkit-scrollbar {
            width: 5px;
            height: 8px;
        }

        .genealogy-scroll::-webkit-scrollbar-track {
            border-radius: 10px;
            background-color: #e4e4e4;
        }

        .genealogy-scroll::-webkit-scrollbar-thumb {
            background: #212121;
            border-radius: 10px;
            transition: 0.5s;
        }

            .genealogy-scroll::-webkit-scrollbar-thumb:hover {
                background: #d5b14c;
                transition: 0.5s;
            }


        /*----------------genealogy-tree----------*/
        .genealogy-body {
            white-space: nowrap;
            overflow-y: hidden;
            padding: 50px;
            min-height: 500px;
            padding-top: 10px;
        }

        .genealogy-tree ul {
            padding-top: 20px;
            position: relative;
            padding-left: 0px;
            display: flex;
        }

        .genealogy-tree li {
            float: left;
            text-align: center;
            list-style-type: none;
            position: relative;
            padding: 20px 5px 0 5px;
        }

            .genealogy-tree li::before, .genealogy-tree li::after {
                content: '';
                position: absolute;
                top: 0;
                right: 50%;
                border-top: 2px solid #ccc;
                width: 50%;
                height: 18px;
            }

            .genealogy-tree li::after {
                right: auto;
                left: 50%;
                border-left: 2px solid #ccc;
            }

            .genealogy-tree li:only-child::after, .genealogy-tree li:only-child::before {
                display: none;
            }

            .genealogy-tree li:only-child {
                padding-top: 0;
            }

            .genealogy-tree li:first-child::before, .genealogy-tree li:last-child::after {
                border: 0 none;
            }

            .genealogy-tree li:last-child::before {
                border-right: 2px solid #ccc;
                border-radius: 0 5px 0 0;
                -webkit-border-radius: 0 5px 0 0;
                -moz-border-radius: 0 5px 0 0;
            }

            .genealogy-tree li:first-child::after {
                border-radius: 5px 0 0 0;
                -webkit-border-radius: 5px 0 0 0;
                -moz-border-radius: 5px 0 0 0;
            }

        .genealogy-tree ul ul::before {
            content: '';
            position: absolute;
            top: 0;
            left: 50%;
            border-left: 2px solid #ccc;
            width: 0;
            height: 20px;
        }

        .genealogy-tree li a {
            text-decoration: none;
            color: #666;
            font-family: arial, verdana, tahoma;
            font-size: 11px;
            display: inline-block;
            border-radius: 5px;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
        }

            .genealogy-tree li a:hover + ul li::after,
            .genealogy-tree li a:hover + ul li::before,
            .genealogy-tree li a:hover + ul::before,
            .genealogy-tree li a:hover + ul ul::before {
                border-color: #fbba00;
            }

        /*--------------memeber-card-design----------*/
        .member-view-box {
            padding: 0px 20px;
            text-align: center;
            border-radius: 4px;
            position: relative;
        }

        .member-image {
            width: 60px;
            position: relative;
        }

            .member-image img {
                width: 60px;
                height: 60px;
                border-radius: 6px;
                background-color: #000;
                z-index: 1;
            }
    </style>
    <script>
        $(function () {
            $('.genealogy-tree ul').hide();
            $('.genealogy-tree>ul').show();
            $('.genealogy-tree ul.active').show();
            $('.genealogy-tree li').on('click', function (e) {
                var children = $(this).find('> ul');
                if (children.is(":visible")) children.hide('fast').removeClass('active');
                else children.show('fast').addClass('active');
                e.stopPropagation();
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="card">
                <div class="card-body">

                    <div class="row">
                        <div class="col-md-2">
                            <label for="ddlMember">Member ID : </label>
                            <asp:TextBox runat="server" ID="txtMemberID"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger mt-3" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row m-2" style="background-color: white; box-shadow: rgba(0, 0, 0, 0.4) 0px 2px 4px, rgba(0, 0, 0, 0.3) 0px 7px 13px -3px, rgba(0, 0, 0, 0.2) 0px -3px 0px inset; border-radius: 20px">
                <div class="body genealogy-body genealogy-scroll">
                    <div class="genealogy-tree">
                        <ul>
                            <li>
                                <a href="javascript:void(0);">
                                    <div class="member-view-box">
                                        <div class="member-image">
                                            <img src="data:image/png;base64,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" alt="Member">
                                            <div class="member-details">
                                                <h3><span runat="server" id="lblName"></span><b>(<span runat="server" id="lblbalance"></span>)</b></h3>
                                            </div>
                                        </div>
                                    </div>
                                </a>
                                <asp:UpdatePanel runat="server" ID="updateRolePanel">
                                    <ContentTemplate>
                                        <ul class="active">
                                            <asp:Repeater runat="server" ID="rptDateChild" OnItemDataBound="rptDateChild_ItemDataBound">
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:HiddenField runat="server" ID="rptmsrno" Value='<%# Eval("Msrno") %>' />
                                                        <a href="javascript:void(0);">
                                                            <div class="member-view-box">
                                                                <div class="member-image">
                                                                    <img src="data:image/png;base64,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" alt="Member">
                                                                    <div class="member-details">
                                                                        <p>
                                                                            <%# Eval("loginid") %><br /><b>
                                                                            (<%# Eval("MainBalance") %>)</b>
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </a>
                                                        <ul class="active">
                                                            <asp:Repeater runat="server" ID="rptinner">
                                                                <ItemTemplate>
                                                                    <li>
                                                                        <a href="javascript:void(0);">
                                                                            <div class="member-view-box">
                                                                                <div class="member-image">
                                                                                    <img src="data:image/png;base64,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" alt="Member">
                                                                                    <div class="member-details">
                                                                                        <p>
                                                                                            <%# Eval("loginid") %><br /><b>
                                                                                            (<%# Eval("MainBalance") %>)</b>
                                                                                        </p>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </a>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ul>
                                                    </li>

                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                            </li>
                        </ul>
                    </div>
                </div>

            </div>


        </section>
    </div>
    <script>
        $(function () {
            Action(DataObject);
        });

        var DataObject = {
            ID: "example1",
            iDisplayLength: 5,
            bPaginate: true,
            bFilter: true,
            bInfo: true,
            bLengthChange: true,
            searching: true
        };
        function LoadData() {

            Action(DataObject);
        }

        var req = Sys.WebForms.PageRequestManager.getInstance();
        req.add_beginRequest(function () {

        });
        req.add_endRequest(function () {
            LoadData();

        });
        function showModal() {
            $('#myModal').modal();
        }


    </script>

</asp:Content>
