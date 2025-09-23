<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AddBoxService.aspx.cs" Inherits="Admin_AddBoxService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link href="../../css/metro.css" rel="stylesheet" />
    <link href="../../css/style-product.css" rel="stylesheet" />
    <style>
        table {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            table td, #customers th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            table tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            table tr:hover {
                background-color: #ddd;
            }

            table th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #4CAF50;
                color: white;
            }
    </style>
    <script type="text/javascript" language="javascript">

        function LoadData() {
            try {
                debugger;
                $.ajax({
                    type: "POST",
                    url: "AddBoxService.aspx/SubmitSuccessfully",
                    contentType: "application/json;",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            } catch (err) {
            }
        }



        function OnSuccess(response) {
            try {
                var Data = JSON.parse(response.d);
                var MainData = $("#selectElement");
                var ParentData = Data.filter(function (d) { return d.Link == "#" })
                for (var i = 0; i < ParentData.length; i++) {
                    var optgroup = $('<optgroup>');
                    optgroup.attr('label', ParentData[i]["Name"]);
                    optgroup.attr('id', "optgroup_" + ParentData[i]["ID"]);
                    var ChildData = Data.filter(function (d) { return d.ParentID == ParentData[i]["ID"] })
                    var ss = $("#optgroup_" + ParentData[i]["ID"]);
                    var j = 0;
                    for (j = 0; j < ChildData.length; j++) {

                        var CheckParent = ChildData.filter(function (d) { return ChildData[j]["Link"] == "#" });
                        var sss = $("#optgroup_" + ChildData[j]["ID"]);
                        //if (ss.length == 0) {
                        //    ss = optgroup;
                        //}
                        if (CheckParent.length > 0 && sss.length == 0) {
                            var optgroup1 = $('<optgroup>');
                            optgroup1.attr('label', ChildData[j]["Name"]);
                            optgroup1.attr('id', "optgroup_" + ChildData[j]["ID"]);

                            optgroup.append(optgroup1);
                        } else {
                            if (ss.length == 0) {
                                var option = $("<option></option>");
                                option.val(ChildData[j]["Name"]);
                                option.text(ChildData[j]["Name"]);
                                option.attr('id', "optgroup_" + ChildData[j]["ID"]);
                                optgroup.append(option);
                            }
                            else {
                                var option = $("<option></option>");
                                option.val(ChildData[j]["Name"]);
                                option.text(ChildData[j]["Name"]);
                                option.attr('id', "optgroup_" + ChildData[j]["ID"]);
                                optgroup.append(option);
                            }

                        }


                    }
                    //if (ss.length == 0) {
                    //var sssss = $("#" + $(ss)[0].id);
                    //if (sssss.length == 0) {
                    $('#selectElement').append(optgroup);
                    //}
                    //}
                }
            } catch (err) {
            }
        }
       
     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">        
        <section class="content">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container" style="background-color: white">
                <asp:Panel runat="server" ID="plnData">
                    <div class="form-group">
                        <asp:DropDownList runat="server" ID="dllBoxType" OnSelectedIndexChanged="dllBoxType_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Select Size</label>
                       <asp:DropDownList runat="server" ID="dllSize"  onchange="GetSize(this)" ClientIDMode="Static"  CssClass="form-control">
                           
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
                <div id="box"></div>
                <div class="form-group">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-danger" OnClientClick="return MakeJson()" OnClick="btnSubmit_Click" />
                    <input type="button" value="Set Variants" runat="server" id="btnVariants" visible="false" name="Set Variants" onclick="MakeVarintBox()" />
                    <input type="button" value="Save Set Variants Price"  runat="server" id="btnVariantsPrice" visible="false" name="Save Set Variants Price" onclick="Lock()" />

                </div>
                <asp:HiddenField ID="hdnEditData" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="hdnData" ClientIDMode="Static" runat="server" />
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
            </section>
         </div>
    <script type="text/javascript">
        //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        //function EndRequestHandler(sender, args) {
        //    if (args.get_error() != undefined) {
        //        args.set_errorHandled(true);
        //    }
        //}
        function GetSize(ObjData) {
            $("#SizeData").val($(ObjData).val());
        }

        function MakeJson() {
            jsonObjData = [];
            var Flag = true;

            item = {}
            $(".ValData").each(function (i) {
                var id = $(this).attr("data-type");
                var Man = $(this).attr("data-man");
                //data-man="True"
                var Type = $(this).attr("type");
                var Val = $(this).val();

                if (id == "Description") {
                    try {
                        var Point = CKEDITOR.instances["Description"];
                        if (Point != null) {
                            Val = Point.getData();
                        } else {
                            Val = $(this).val();
                        }
                    } catch (err) {
                        Val = $(this).val();

                    }

                }
                if (id == "Specification") {
                    try {
                        var Point = CKEDITOR.instances["Specification"];
                        if (Point != null) {
                            Val = Point.getData();
                        } else {
                            Val = $(this).val();
                        }
                    } catch (err) {
                        Val = $(this).val();

                    }

                }


                if (id == "Category") {
                    Val = $('#selectElement option').attr('selected', 'selected')[0].id.split('_')[1];
                    //Val = $('#selectElement option:eq(1)').attr('selected', 'selected')[0].id.split('_')[1];
                }



                if (Type == "checkbox") {
                    debugger;
                    Val = $(this).find('input')[0].checked;
                }

                if (Type == "file") {
                    debugger;
                    if ($("#imgData").length > 0) {
                        if ($("#imgData").attr('FileName') == "") {
                            Val = $(this).val().replace(/\s/g, '');
                        } else {
                            Val = $("#imgData").attr('FileName');
                        }
                    } else {

                        Val = $(this).val().replace(/\s/g, '');
                    }
                }



                if (Val != "" || Man == undefined) {
                    item[id] = Val;
                } else {
                    if (Man != "False") {
                        $(this).css("border", "1px solid red");
                        Flag = false;
                    }
                }
            });
            try {
                if ($("#dllBoxType").val() == "3") {
                    if (jsonObjDataColorSize.length > 0) {
                        item["SizeColorDetails"] = JSON.stringify(jsonObjDataColorSize);
                    }
                }
            } catch (err) {
            }
            jsonObjData.push(item);
            $("#hdnData").val(JSON.stringify(jsonObjData));

            return Flag;
        }


        function change() {
            var fileUpload = $("#fupload").get(0);
            var files = fileUpload.files;

            var data = new FormData();
            var Extension = files[0].name.split('.')[1].toString().toUpperCase();
            var Size = files[0].size / 1000;

            if (Extension == "PNG" || Extension == "JPEG" || Extension == "PDF" || Extension == "GIF" || Extension == "JPG") {
                if (Size >= 10 && Size <= 1024) {
                    data.append(files[0].name, files[0]);
                    $.ajax({
                        url: "FileUploadHandler.ashx",
                        type: "POST",
                        data: data,
                        contentType: false,
                        processData: false,
                        success: function (result) {
                            debugger;
                            alert(result);
                            if ($("#imgData").length > 0) {
                                $("#imgData").attr('FileName', $("#fupload").val().replace(/\s/g, ''));
                            }

                        },
                        error: function (err) {
                            alert(err.statusText)

                        }
                    });
                }
                else {
                    $("#fupload").val('');
                    alert("File Size Should be b/w 100 kb to 1 MB");
                }

            } else {
                $("#fupload").val('');
                alert("Only JPEG, PNG, GIF, PDF Format Allow");
            }





        }



        function SetEditTime(Data) {
            //var size = $('.Size').val();
            //var color = $('.Color').val();
            //jsonObjDataColorSize = [];
            //var SizeObject = size.split(',');
            //var ColorObject = color.split(',');

            jsonObjDataColorSize = JSON.parse(Data)

            var html = "<table border='1|1' id='details'>";
            for (var i = 0; i < jsonObjDataColorSize.length; i++) {
                html += "<tr>";
                for (var j = 0; j < Object.keys(jsonObjDataColorSize[i]).length; j++) {
                    if (Object.keys(jsonObjDataColorSize[i])[j].toString() != "Price" && Object.keys(jsonObjDataColorSize[i])[j].toString() != "DisCount" && Object.keys(jsonObjDataColorSize[i])[j].toString() != "IsFlat") {
                        html += "<td>" + jsonObjDataColorSize[i][Object.keys(jsonObjDataColorSize[i])[j]] + "</td>";
                    }
                    else if (Object.keys(jsonObjDataColorSize[i])[j].toString() == "IsFlat") {
                        html += "<td><input type='checkbox'/></td>";
                    } else {
                        html += "<td><input type='text' value='" + jsonObjDataColorSize[i][Object.keys(jsonObjDataColorSize[i])[j]] + "'/></td>";
                    }

                }
                html += "</tr>";
            }
            html += "</table>";
            document.getElementById("box").innerHTML = html;
            LoadData();
        }


        function OpenLink(Link) {
            debugger;
            alert();
            window.open(Link);
        }

        function MakeVarintBox() {
            var size = $('.Size').val();
            var color = $('.Color').val();
            jsonObjDataColorSize = [];
            var SizeObject = size.split(',');
            var ColorObject = color.split(',');
            if (SizeObject.length > 0 && ColorObject.length > 0) {
                for (var i = 0; i < SizeObject.length; i++) {
                    for (var j = 0; j < ColorObject.length; j++) {
                        item = {};
                        item["Size"] = SizeObject[i];
                        item["Color"] = ColorObject[j];
                        item["Price"] = "0";
                        item["DisCount"] = "0";
                        item["IsFlat"] = "0";
                        jsonObjDataColorSize.push(item);
                    }

                }
            } else if (ColorObject.length > 0 && SizeObject.length == 0) {

                for (var j = 0; j < ColorObject.length; j++) {
                    item = {};
                    item["Color"] = ColorObject[j];
                    item["Price"] = "0";
                    item["DisCount"] = "0";
                    item["IsFlat"] = "0";
                    jsonObjDataColorSize.push(item);
                }
            }
            else if (SizeObject.length > 0 && ColorObject.length == 0) {
                for (var j = 0; j < SizeObject.length; j++) {
                    item = {};
                    item["Size"] = ColorObject[j];
                    item["Price"] = "0";
                    item["DisCount"] = "0";
                    item["IsFlat"] = "0";
                    jsonObjDataColorSize.push(item);
                }
            }
            debugger;
            var html = "<table border='1|1' id='details'>";
            for (var i = 0; i < jsonObjDataColorSize.length; i++) {
                html += "<tr>";
                for (var j = 0; j < Object.keys(jsonObjDataColorSize[i]).length; j++) {
                    if (Object.keys(jsonObjDataColorSize[i])[j].toString() != "Price" && Object.keys(jsonObjDataColorSize[i])[j].toString() != "DisCount" && Object.keys(jsonObjDataColorSize[i])[j].toString() != "IsFlat") {
                        html += "<td>" + jsonObjDataColorSize[i][Object.keys(jsonObjDataColorSize[i])[j]] + "</td>";
                    }
                    else if (Object.keys(jsonObjDataColorSize[i])[j].toString() == "IsFlat") {
                        html += "<td><input type='checkbox'/></td>";
                    } else {
                        html += "<td><input type='text' value='" + jsonObjDataColorSize[i][Object.keys(jsonObjDataColorSize[i])[j]] + "' /></td>";
                    }

                }
                html += "</tr>";
            }
            html += "</table>";
            document.getElementById("box").innerHTML = html;

        }




        function Lock() {
            var size = $('.Size').val();
            var color = $('.Color').val();
            var SizeObject = size.split(',');
            var ColorObject = color.split(',');
            var convertedIntoArray = [];
            $("table#details tr").each(function (j) {
                var OuterJoin = j;
                var rowDataArray = [];
                var actualData = $(this).find('td');
                if (actualData.length > 0) {
                    actualData.each(function (i) {
                        if (SizeObject.length > 0 && ColorObject.length > 0) {
                            if (i == 2) {
                                jsonObjDataColorSize[OuterJoin]["Price"] = $(this).find('input')[0].value
                            }
                            if (i == 3) {
                                jsonObjDataColorSize[OuterJoin]["DisCount"] = $(this).find('input')[0].value
                            }
                            if (i == 4) {
                                jsonObjDataColorSize[OuterJoin]["IsFlat"] = $(this).find('input')[0].checked
                            }
                        }
                        else if (ColorObject.length > 0 && SizeObject.length == 0) {
                            if (i == 1) {
                                jsonObjDataColorSize[OuterJoin]["Price"] = $(this).find('input')[0].value
                            }
                            if (i == 2) {
                                jsonObjDataColorSize[OuterJoin]["DisCount"] = $(this).find('input')[0].value
                            }
                            if (i == 3) {
                                jsonObjDataColorSize[OuterJoin]["IsFlat"] = $(this).find('input')[0].checked
                            }
                        }
                        else if (SizeObject.length > 0 && ColorObject.length == 0) {
                            if (i == 1) {
                                jsonObjDataColorSize[OuterJoin]["Price"] = $(this).find('input')[0].value
                            }
                            if (i == 2) {
                                jsonObjDataColorSize[OuterJoin]["DisCount"] = $(this).find('input')[0].value
                            }
                            if (i == 3) {
                                jsonObjDataColorSize[OuterJoin]["IsFlat"] = $(this).find('input')[0].checked
                            }
                        }

                    });
                }
            });
            debugger;
        }


    </script>

</asp:Content>

