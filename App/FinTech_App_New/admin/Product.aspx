<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Reatiler_HolidayCategory" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <section class="content">
                    <asp:UpdatePanel runat="server" ID="updateRolePanel">
                        <ContentTemplate>
                            <div class="card card-primary">
                                <div class="card-header">Product Details </div>
                                <div class="card-body">
                                    <asp:HiddenField ID="hdnid" Value="0" runat="server" />

                                    <div class="form-group">
                                        <label for="exampleInputPassword1">Product Name / Title* : </label>
                                        <asp:TextBox ID="txtProductName" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtProductName" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>

                                    </div>

                                    <div>

                                        <label for="ddlCategory">Category Name : </label>
                                        <asp:DropDownList runat="server" ID="ddlCategory" ToolTip="Select Category Name." CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlCategory" InitialValue="0" Font-Bold="True" ForeColor="Red" ErrorMessage="Please select Category" ValidationGroup="chkaddfund" />


                                    </div>

                                    <div class="form-group">
                                        <label for="exampleInputPassword1">Short Description :</label>

                                        <CKEditor:CKEditorControl ID="ckDescShort" runat="server" BasePath="~/CKEditor/"
                                            Height="250px" Width="90%">
                                        </CKEditor:CKEditorControl>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="ckDescShort" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group">
                                        <label for="exampleInputPassword1">Description :</label>

                                        <CKEditor:CKEditorControl ID="ckNewsDesc" runat="server" BasePath="~/CKEditor/"
                                            Height="250px" Width="90%">
                                        </CKEditor:CKEditorControl>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="ckNewsDesc" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                    </div>



                                    <div class="row">
                                        <div class="form-group  col-md-4">
                                            <label for="exampleInputPassword1">HSN / SAC Code</label>
                                            <asp:TextBox ID="txtHSN" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtHSN" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group  col-md-4">
                                            <label for="exampleInputPassword1">Product Code / SKU*</label>
                                            <asp:TextBox ID="txtsku" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtsku" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group  col-md-4">
                                            <label for="exampleInputPassword1">Barcode (e.g. UPC, ISBN,GTIN)</label>
                                            <asp:TextBox ID="txtBarcode" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtBarcode" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="col-md-2">
                                            <label>Origin (e.g. India)</label>
                                            <asp:DropDownList runat="server" ID="ddlcountry" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row">

                                        <div class="form-group col-md-4" style="margin-top: 31px; border: groove">
                                            <asp:RadioButton ID="radio1" runat="server" Text="Video" GroupName="RD" OnCheckedChanged="radio1_CheckedChanged" AutoPostBack="true" />
                                            <asp:RadioButton ID="radio2" runat="server" Text="Images" GroupName="RD" OnCheckedChanged="radio2_CheckedChanged" AutoPostBack="true" />
                                        </div>
                                        <div class="form-group col-md-4" id="divvideo" runat="server" visible="false">
                                            <label for="exampleInputPassword1">Video Code : </label>
                                            <span>(Ex: https://www.youtube.com/embed/XGSy3_Czz8k) </span>
                                            <asp:TextBox ID="txtvideos" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                        </div>




                                        <div class="form-group col-md-4" id="divImage" runat="server" visible="false">
                                            <label for="exampleInputPassword1">Image : </label>
                                            <input type="file" id="FileUpload1" accept=".jpg,.png,.gif" />
                                         <%--   <asp:FileUpload runat="server" ID="FileUpload1" CssClass="form-control" />--%>
                                        </div>


                                        <table border="0" cellpadding="0" cellspacing="5">
                                            <tr>
                                                <td>
                                                    <img id="Image1" src="" alt=""  />
                                                </td>
                                                <td>
                                                    <canvas id="canvas" height="3" width="3"></canvas>
                                                </td>
                                                <td>
                                                         <input type="button" id="btnCrop" value="Crop" style="display: none" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                   
                                        <%--<asp:Button ID="btnUpload" runat="server" Text="Upload" Style="display: none" />--%>
                                        <input type="hidden" name="imgX1" id="imgX1" />
                                        <input type="hidden" name="imgY1" id="imgY1" />
                                        <input type="hidden" name="imgWidth" id="imgWidth" />
                                        <input type="hidden" name="imgHeight" id="imgHeight" />
                                        <input type="hidden" name="imgCropped" id="imgCropped" />









                                        <div class="form-group col-md-4" style="display:none">
                                            <label for="exampleInputPassword1">Upload Images : </label>
                                            <asp:FileUpload runat="server" ID="flieUpload" CssClass="form-control" />
                                        </div>

                                    </div>





                                    <div class="row">


                                        <div class="form-group col-md-6">
                                            <label for="exampleInputPassword1">Weight (Kg. / Pound) :</label>
                                            <asp:TextBox ID="txtWeight" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtWeight" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-md-6">
                                            <label for="exampleInputPassword1">Days allowed to return</label>
                                            <asp:TextBox ID="txtdayreturn" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtdayreturn" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtdayreturn" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>

                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <label for="exampleInputPassword1">Labels :</label>
                                        <asp:TextBox ID="txtLabels" runat="server" class="form-control m-t-xxs" onkeypress="return AllowAlphabet(event)"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtLabels" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group">
                                        <label for="exampleInputPassword1">Price :</label>
                                        <asp:TextBox ID="txtPrice" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                        <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtPrice" />--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtPrice" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group">
                                        <label for="exampleInputPassword1">Quantity</label>
                                        <asp:TextBox ID="txtQuantity" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtQuantity" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtQuantity" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group">
                                        <label for="exampleInputPassword1">Discount :</label>
                                        <asp:TextBox ID="txtDiscount" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                        <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtDiscount" />--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtDiscount" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group">
                                        <label for="chkActive">Is Flat : </label>
                                        <asp:CheckBox runat="server" ID="chkFlate" class="form-control m-t-xxs"></asp:CheckBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputPassword1">GST :</label>
                                        <asp:TextBox ID="txtGST" runat="server" value="0" class="form-control m-t-xxs"></asp:TextBox>
                                        <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtGST" />--%>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtGST" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSubmit_Click" ValidationGroup="chkaddfund" />
                                        <asp:Label ID="lblMessage" CssClass="center-block" Style="text-align: center; font-weight: 600" ForeColor="Red" runat="server" />
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <hr />

                </section>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
             <asp:PostBackTrigger ControlID="radio1" />
            <asp:PostBackTrigger ControlID="radio2" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        $(function () {
            Action(DataObject);
        });

        var DataObject = {
            ID: "example1",
            iDisplayLength: 15,
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

    <script>
        function AllowAlphabet(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45')
                return true;
            else {
                alert('Please Enter Only Character values.');
                return false;
            }
        }
    </script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-jcrop/0.9.9/js/jquery.Jcrop.min.js"></script>
    <script>
    $(function () {
        $('#FileUpload1').change(
            function () {
            $('#Image1').hide();
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#Image1').show();
                $('#Image1').attr("src", e.target.result);
                $('#Image1').Jcrop({
                    onChange: SetCoordinates,
                    onSelect: SetCoordinates
                });
            }
            reader.readAsDataURL($(this)[0].files[0]);
        });

        $('#btnCrop').click(function () {
            var x1 = $('#imgX1').val();
            var y1 = $('#imgY1').val();
            var width = $('#imgWidth').val();
            var height = $('#imgHeight').val();
            var canvas = $("#canvas")[0];
            var context = canvas.getContext('2d');
            var img = new Image();
            img.onload = function () {
                canvas.height = height;
                canvas.width = width;
                context.drawImage(img, x1, y1, width, height, 0, 0, width, height);
                $('#imgCropped').val(canvas.toDataURL());
                //$('[id*=btnUpload]').show();
            };
            img.src = $('#Image1').attr("src");
        });
    });
    function SetCoordinates(c) {
        $('#imgX1').val(c.x);
        $('#imgY1').val(c.y);
        $('#imgWidth').val(c.w);
        $('#imgHeight').val(c.h);
        $('#btnCrop').show();
    };
    </script>

</asp:Content>

