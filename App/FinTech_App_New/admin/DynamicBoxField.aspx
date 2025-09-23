<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="DynamicBoxField.aspx.cs" Inherits="Admin_DynamicBoxField" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <%-- <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>--%>
    <script>
        function ReFormControl(data) {

            var Restr = data;
            jsonObj = JSON.parse(Restr);
            debugger;
            for (var i = 0; i < JSON.parse(Restr).length; i++) {
                var parent = $("#plnData");
                var Control;
                var TypeVal = JSON.parse(Restr)[i]["Type"];
                if (TypeVal == "text") {
                    Control = $('<input type="textbox" data-type="' + JSON.parse(Restr)[i].Name + '" />').addClass('form-control ValData');
                }
                else if (TypeVal == "dropdown") {
                    Control = $('<option data-type="' + JSON.parse(Restr)[i].Name + '">Select</option>').addClass('form-control ValData');
                }
                else if (TypeVal == "img") {
                    Control = $('<input data-type="' + JSON.parse(Restr)[i].Name + '" type="file"/>').addClass('form-control ValData');
                }
                else if (TypeVal == "checkbox") {
                    Control = $('<input data-type="' + JSON.parse(Restr)[i].Name + '" type="checkbox"/>').addClass('form-control ValData');
                }

                var ss = $("<div class='col-md-4' style='color:white;'>" + JSON.parse(Restr)[i].Name + "</div>");
                ss.append(Control)
                .append($('<button/>').addClass('remove').text('Remove').css("color","black"));
                $(parent).append(ss);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">
        <section class="content">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-md-2">
                        <img src="../img/add.png" data-toggle="modal" data-target="#myModal" height="50" />
                        <asp:Button ID="btnSaveRequestionJSON" runat="server" Text="Save" OnClientClick="return MakeJson()" OnClick="btnSaveRequestionJSON_Click" />
                    </div>
                </div>
                <div id="plnData" class="row">
                </div>
                <asp:HiddenField ID="hdnReform" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="hdnData" ClientIDMode="Static" runat="server" />

            </div>

        </ContentTemplate>

    </asp:UpdatePanel>
            </section>
         </div>
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Modal Header</h4>

                </div>
                <div class="modal-body">
                    <asp:Panel runat="server" ID="Panel1">
                        <div class="form-group">
                            <asp:DropDownList runat="server" ClientIDMode="Static" data-type="Type" ID="dllType" CssClass="form-control AddVal">
                                <asp:ListItem Text="text" Value="text"></asp:ListItem>
                                <asp:ListItem Text="img" Value="img"></asp:ListItem>
                                <asp:ListItem Text="checkbox" Value="checkbox"></asp:ListItem>
                                <asp:ListItem Text="dropdown" Value="dropdown"></asp:ListItem>
                                <asp:ListItem Text="file" Value="file"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <strong>Name</strong>
                            <asp:TextBox runat="server" ID="txtName" data-type="Name" data-placeholder="Name" ClientIDMode="Static" CssClass="form-control AddVal"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <strong>Default Value</strong>
                            <asp:TextBox runat="server" ID="txtDefaultValue" data-type="DefaultValue" ClientIDMode="Static" CssClass="form-control AddVal"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <strong>Max Length</strong>
                            <asp:TextBox runat="server" ID="txtMaxLength" Text="100" data-type="Max" ClientIDMode="Static" CssClass="form-control AddVal"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <strong>Min Length</strong>
                            <asp:TextBox runat="server" ID="txtMinLength" Text="100" data-type="Min" ClientIDMode="Static" CssClass="form-control AddVal"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <strong>Mandatory</strong>
                            <asp:CheckBox runat="server" ID="chkMandatory" data-type="Mandatory" ClientIDMode="Static" CssClass="form-control AddVal"></asp:CheckBox>
                        </div>
                        <div class="form-group">
                            <input type="button" id="btnAdd" onclick="AddControl();" class="btn btn-danger" value="Add" />
                        </div>
                    </asp:Panel>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>


        </div>
    </div>

    <script type="text/javascript">
        jsonObj = [];
        function AddControl() {

            item = {}
            $(".AddVal").each(function (i) {
                var id = $(this).attr("data-type");

                var Val = $(this).val();

                if (id == "Mandatory") {
                    debugger;
                    Val = $(this).find('input')[0].checked;
                }
                item[id] = Val;

            });
            jsonObj.push(item);

            var parent = $("#plnData");
            var Control;
            var TypeVal = $("#dllType").val();
            if (TypeVal == "text") {
                Control = $('<input type="textbox" data-type="' + $("#txtName").val() + '" />').addClass('form-control ValData');
            }
            else if (TypeVal == "dropdown") {
                Control = $('<option data-type="' + $("#txtName").val() + '">Select</option>').addClass('form-control ValData');
            }
            else if (TypeVal == "img") {
                Control = $('<input data-type="' + $("#txtName").val() + '" type="file"/>').addClass('form-control ValData');
            }
            else if (TypeVal == "checkbox") {
                Control = $('<input data-type="' + $("#txtName").val() + '" type="checkbox"/>').addClass('form-control ValData');
            }

            var ss = $("<div class='col-md-4'>" + $("#txtName").val() + "</div>");
            ss.append(Control)
                .append($('<button/>').addClass('remove').text('Remove'));
            $(parent).append(ss);
        }




        $(document).on('click', 'button.remove', function (e) {
            e.preventDefault();
            debugger;
            $(this).parent().remove();

            var TypeData = $($($(this).parent()).find('input')).attr('data-type');
            jsonObj = jsonObj.filter(function (d) { return d.Name != TypeData });
        });
    </script>


    <script type="text/javascript">
        jsonObjData = [];
        function MakeJson() {
            var Flag = 0;

            item = {}
            //$(".ValData").each(function (i) {
            //    var id = $(this).attr("data-type");
            //    var Val = $(this).val();
            //    item[id] = Val;
            //    if (i == $(".ValData").length - 1) {
            //        Flag = 1;
            //    }
            //});
            //jsonObjData.push(item);
            $("#hdnData").val(JSON.stringify(jsonObj));
            return Flag;
        }
    </script>
</asp:Content>

