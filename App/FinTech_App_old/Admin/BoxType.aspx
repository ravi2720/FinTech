<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="BoxType.aspx.cs" Inherits="Admin_BoxType" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link href="../app_css/metro.css" rel="stylesheet" />
    <link href="../app_css/style-product.css" rel="stylesheet" />

    <style>
        #widget_scroll_container {
            width: 2160px;
        }

        div.widget_container {
            width: 100%;
         div.widget div.main > span {
            font-weight:700 !important;
                padding: 0px 5px;
        }
        div.widget {
            box-shadow:none !important;
        }
     
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
     <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div class="container-fluid">

                <div class="row">

                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                        <ItemTemplate>

                            <div class='<%# (Eval("TypeID").ToString()=="3"?"col-md-2":"col-md-3") %>'>
                                <%# MakeBOX(Eval("BoxProperty").ToString(),Eval("TypeID").ToString()) %>
                                <div >
                                    <a href='<%# "addBoxService.aspx?ID="+Eval("ID").ToString() %>' class="btn btn-danger">Edit</a>

                                    <a id="A1" class="btn btn-danger" href='<%# "DynamicBoxField.aspx?ID="+ Eval("ID").ToString() %>' runat="server" visible='<%# (Eval("TypeID").ToString()!="1"?true:false) %>'>Make Form</a>
                                    <asp:Button ID="Button1" CssClass="btn btn-danger" OnClientClick="return confirm('do you want to delete?')" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' runat="server" Text="Delete" />
                                    <asp:Button ID="btnDelete" CssClass="btn btn-danger" OnClientClick="return confirm('do you want to Active/DeActive?')" CommandName="Active" CommandArgument='<%# Eval("ID") %>' runat="server" Text='<%# (Convert.ToBoolean(Eval("IsActive"))==true?"Active":"DeActive") %>' />
                                </div>
                            </div>


                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </div>
            <asp:HiddenField ID="hdnData" ClientIDMode="Static" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dllBoxType" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Modal Header</h4>
                        </div>
                        <div class="modal-body">
                            <asp:Panel runat="server" ID="plnData">
                                <div class="form-group">
                                    <asp:DropDownList runat="server" ID="dllBoxType" OnSelectedIndexChanged="dllBoxType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>

                            </asp:Panel>
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-danger" OnClientClick="return MakeJson()" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </ContentTemplate>

            </asp:UpdatePanel>
        </div>
    </div>
              </section>
         </div>
    <script type="text/javascript">

        function MakeJson() {
            jsonObjData = [];
            var Flag = 0;

            item = {}
            $(".ValData").each(function (i) {
                var id = $(this).attr("data-type");
                var Type = $(this).attr("type");
                var Val = $(this).val();
                if (id == "Description") {
                    var Point = $(this).attr("data-CK");
                    if (Point != null) {
                        Val = $('.cke_show_borders').innerHTML;
                    } else {
                        Val = $(this).val();
                    }

                }

                if (Type == "checkbox") {
                    debugger;
                    Val = $(this).find('input')[0].checked;
                }
                if (i == $(".ValData").length - 1) {
                    Flag = 1;
                }
                item[id] = Val;
            });
            jsonObjData.push(item);
            $("#hdnData").val(JSON.stringify(jsonObjData));
            return Flag;
        }


        function change() {
            var fileUpload = $("#fupload").get(0);
            var files = fileUpload.files;

            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }

            $.ajax({
                url: "http://localhost:3264/website/api/FileUploadHandler.ashx",
                type: "POST",
                data: data,
                contentType: false,
                processData: false,
                success: function (result) { alert(result); },
                error: function (err) {
                    alert(err.statusText)
                }
            });


        }

        function OpenLink(Link) {
            debugger;
           // alert();
            window.open(Link);
        }
    </script>
</asp:Content>

