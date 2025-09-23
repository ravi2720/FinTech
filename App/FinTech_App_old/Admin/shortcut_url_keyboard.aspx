<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="shortcut_url_keyboard.aspx.cs" Inherits="Admin_shortcut_url_keyboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="card">
                    <div class="card-body">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="col-md-12">
                                    <div class="heading">
                                        <h2>Keyboard URL Shortcut</h2>
                                    </div>
                                    <span>
                                        <asp:Button runat="server" data-toggle="modal" data-target="#myPDCModel" data-backdrop="static" ID="btnViewShortCut" Text="ViewKeyBordShortCode" CssClass="btn btn-info btn-sm pull-left m" />

                                    </span>
                                    <span>
                                        <asp:TextBox runat="server" ID="txtKeyCode" placeholder="Search letter for shortcut" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtKeyCode_TextChanged"></asp:TextBox><br />
                                        <asp:Label runat="server" CssClass="form-control" ID="lblKeyCode" Text=""></asp:Label>
                                    </span>

                                    <br />
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <table class="table pge_tbl Design">
                                            <thead>
                                                <tr>
                                                    <th>Sl</th>
                                                    <th>Shortcut</th>
                                                    <th>URL</th>
                                                    <th>SHORTCUT KEY VALUE(View At Popup)</th>
                                                    <th>
                                                        <asp:Button runat="server" Text="Add" OnClick="Unnamed_Click" ID="btnAdd" /></th>
                                                    <th>Remove</th>
                                                    <th>Active/Deactive</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <asp:Repeater runat="server" ID="rptShortkData" OnItemCommand="rptShortkData_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Container.ItemIndex+1 %></td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox2" runat="server" placeholder="Keyboard Shortcut" Font-Bold="true" Text='<%# Eval("SHORTCUTNAME") %>' CssClass="form-control" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox1" runat="server" placeholder="Page URL" Font-Bold="true" Text='<%# Eval("PAGEURL") %>' CssClass="form-control" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox3" runat="server" placeholder="Keyboard KeyCode" Font-Bold="true" Text='<%# Eval("SHORTCUTKETVAL") %>' CssClass="form-control" />
                                                            </td>
                                                            <td>
                                                                <asp:Button runat="server" Visible='<%# ((Convert.ToInt32(Eval("ID"))==1 || Convert.ToInt32(Eval("ISACTIVE"))==1) ==true?false:true) %>' ID="Button1" Text="Save" CssClass="btn btn-info btn-sm pull-left m" CommandName="Save" CommandArgument='<%#Eval("ID") %>' />
                                                                <asp:Button runat="server" Visible='<%# ((Convert.ToInt32(Eval("ID"))!=1 && Convert.ToInt32(Eval("ISACTIVE"))==1) ==true?true:false) %>' ID="btnUpdate" Text="Update" CssClass="btn btn-info btn-sm pull-left m" CommandName="Update" CommandArgument='<%#Eval("ID") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Button runat="server" ID="btnRemove" Visible='<%# (Convert.ToInt32(Eval("ID"))==1?false:true) %>' Text="Remove" CssClass="btn btn-danger btn-sm pull-left m" CommandName="Remove" CommandArgument='<%#Eval("ID") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Button runat="server" ID="btnADctive" Text='<%# (Convert.ToInt32(Eval("ISACTIVE"))==1?"Active":"Deactive") %>' CssClass='<%# "btn " + (Convert.ToInt32(Eval("isactive"))==1?"btn-success":"btn-danger")+" btn-sm pull-left m"%>' CommandName="ADbtn" CommandArgument='<%#Eval("ID") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>


                                    </div>
                                    <br />
                                    <br />
                                    <div class="note">
                                        <h5>Note : </h5>
                                        <p>
                                            If any page permission not assigned to any user then shortcut will not applicable, a alert showing this shortcut not applicable to you as no page found.<br />
                                            <strong>For KeyCode ShortCut View At Below URL :</strong><span style="color: blue"><a href="https://css-tricks.com/snippets/javascript/javascript-keycodes/" target="_blank"> https://css-tricks.com/snippets/javascript/javascript-keycodes/ </a></span>
                                        </p>
                                    </div>
                                </div>
                                <div id="myPDCModel" class="modal fade model_bg" role="dialog">
                                    <div class="modal-dialog modal-width">

                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-header modal_header_copy">
                                                <button type="button" class="close close-copy btn-danger" data-dismiss="modal">&times;</button>
                                                <h3 style="margin: 0px; float: left;">Keybord ShortCut Keyword List</h3>
                                            </div>
                                            <div class="modal-body modal-body-over">
                                                <div class="outer">
                                                    <div class="inner">
                                                        <table id="example" class="table table-bordered table-responsive table-mm">
                                                            <thead>
                                                                <tr>
                                                                    <th>Sl</th>
                                                                    <th>KeysName</th>
                                                                    <th>KeyCode</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater runat="server" ID="rptPDFReadyData">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%# Container.ItemIndex+1%></td>
                                                                            <td><%#Eval("keyw")%></td>
                                                                            <td><%#Eval("codew")%></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>

                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="txtKeyCode" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="btnViewShortCut" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
        </section>
    </div>

    <script>

        var DataObject = {
            ID: "example",
            iDisplayLength: 20,
            bPaginate: true,
            bFilter: true,
            bInfo: true,
            bLengthChange: true,
            searching: true
        };
        function LoadData() {

            Action(DataObject);
        }
        //$(document).ready(function () {
        //    Action(DataObject);

        //});
        var req = Sys.WebForms.PageRequestManager.getInstance();
        req.add_endRequest(function () {
            LoadData();
        });


    </script>
</asp:Content>

