<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AssignService.aspx.cs" Inherits="Admin_AssignService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Assign Service</div>
                        <div class="card-body">
                            <asp:HiddenField ID="hdnid" runat="server" />
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="exampleInputPassword1">MemberID</label>
                                        <asp:DropDownList ID="ddlMemberList" ClientIDMode="Static" runat="server" class="form-control m-t-xxs select2" OnSelectedIndexChanged="ddlMemberList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:HiddenField ID="hidMsrNo" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button runat="server" ID="btnAssign" Text="Assign" CssClass="btn btn-warning mt-4" OnClick="btnAssign_Click" />
                                    </div>
                                </div>

                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Repeater runat="server" ID="rptAllService">
                                        <ItemTemplate>
                                            <div class="col-md-2">
                                                <asp:CheckBox runat="server" ID="chkData" CssClass="form-control" /><%# Eval("Name") %>
                                                <asp:HiddenField runat="server" ID="hdnVal" Value='<%# Eval("ID") %>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlMemberList" EventName="SelectedIndexChanged" />
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
                        url: "AssignService.aspx/GetMember",
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
    </script>
</asp:Content>

