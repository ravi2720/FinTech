<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="DirectTree.aspx.cs" Inherits="Admin_Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        a, a:link, a:visited, a:hover, a:active {
            color: #000000 !important;
        }
    </style>
    <style>
        #mytable {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #mytable td, #customers th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            #mytable tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #mytable tr:hover {
                background-color: #ddd;
            }

            #mytable th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #4CAF50;
                color: white;
            }
    </style>
    <script src="../app_js/jquery-3.1.0.min.js"></script>
    <link href="../app_js/TreeView/screen.css" rel="stylesheet" />
    <link href="../app_js/TreeView/jquery.tooltip.css" rel="stylesheet" />
    <script src="../app_js/TreeView/chili-1.7.pack.js"></script>
    <script src="../app_js/TreeView/jquery.tooltip.js"></script>

    <script type="text/javascript">
        $(function () {
            $('#TreeView1Div a').tooltip({
                track: true,
                delay: 0,
                showURL: false,
                showBody: " - ",
                fade: 250
            });
            $('#TreeView1Div span').tooltip({
                track: true,
                delay: 0,
                showURL: false,
                showBody: " - ",
                fade: 250
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>


                    <div class="panel panel-primary">
                        <div class="panel-heading">Direct Tree</div>
                        <div class="panel-body">
                            <asp:HiddenField ID="hdnid" runat="server" />
                            <div class="form-group">
                                <label for="exampleInputPassword1">
                                    MemberID
                                </label>
                                <asp:TextBox ID="txtMemberID" ClientIDMode="Static" runat="server" class="form-control m-t-xxs" OnTextChanged="txtMemberID_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMemberID" Font-Bold="True" ForeColor="Red" ErrorMessage="*" ValidationGroup="v" />
                                <asp:Label ID="lblMemberName" runat="server" CssClass="green"></asp:Label>
                                <asp:HiddenField ID="hidMsrNo" runat="server" />
                            </div>


                        </div>
                    </div>

                    <div id="divdata" visible="false" runat="server" class="panel panel-primary">

                        <div class="panel-body">


                            <div id="TreeView1Div">

                                <asp:TreeView ID="TreeView1" runat="server" OnTreeNodePopulate="TreeView1_TreeNodePopulate" AfterClientCheck="CheckChildNodes();"
                                    align="left" EnableClientScript="true" PopulateNodesFromClient="true"
                                    ExpandImageToolTip="Expand This" ShowLines="True" data-toggle="tooltip" data-html="true" data-placement="top">
                                </asp:TreeView>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtMemberID" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
        <script type="text/javascript">  
            $(document).ready(function () {
                SearchText();
            });
            function SearchText() {
                $("#txtMemberID").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "DirectTree.aspx/GetMember",
                            data: "{'Search':'" + document.getElementById('txtMemberID').value + "'}",
                            dataType: "json",
                            success: function (data) {
                                response(data.d);
                            },
                            error: function (result) {
                                alert("No Match");
                            }
                        });
                    }
                });
            }

            var req = sys.webforms.pagerequestmanager.getinstance();
            req.add_beginrequest(function () {

            });
            req.add_endrequest(function () {
                $('#TreeView1Div a').tooltip({
                    track: true,
                    delay: 0,
                    showURL: false,
                    showBody: " - ",
                    fade: 250
                });
                $('#TreeView1Div span').tooltip({
                    track: true,
                    delay: 0,
                    showURL: false,
                    showBody: " - ",
                    fade: 250
                });
            });


        </script>
</asp:Content>

