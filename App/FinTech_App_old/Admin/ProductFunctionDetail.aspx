<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ProductFunctionDetail.aspx.cs" Inherits="Admin_RechargeHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="updateRolePanel">
        <ContentTemplate>
            <div class="content-wrapper">
                <section class="content">
                    <div class="card card-primary">
                        <div class="card-header">Product List</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <label>Category</label>
                                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label>From Date</label>
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                        TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2">
                                    <label>To Date</label>
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="MM-dd-yyyy" Animated="False"
                                        PopupButtonID="txttodate" TargetControlID="txttodate">
                                    </cc1:CalendarExtender>

                                </div>

                                <div class="col-md-2">
                                    <label>Search</label>
                                    <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" CssClass="form-control"></asp:Button>
                                </div>
                            </div>
                            <hr />
                            <div class="row mt-2">

                                <div class="table-responsive">
                                    <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                        <thead>
                                            <tr>
                                                <th></th>
                                                <th>S.No</th>
                                                <th>Status</th>
                                                <th>Action</th>
                                                <th>Category</th>
                                                <th>Product Name</th>
                                                <th>Photo </th>
                                                <th>Short Description </th>
                                                <th>Description</th>
                                                <th>HSN/SAC Code </th>
                                                <th>Product Code / SKU* </th>
                                                <th>Barcode </th>
                                                <th>Video Code </th>
                                                <th>Weight  </th>
                                                <th>Days allowed to return </th>
                                                <th>Labels</th>
                                                <th>Price </th>
                                                <th>Quantity </th>
                                                <th>Discount </th>
                                                <th>GST </th>
                                                <th>NetAmount </th>
                                                <th>Add Date</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rptDataRecharge" OnItemCommand="rptDataRecharge_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <input type="checkbox" name="example[]" value='<%# Eval("ID") %>' onchange="CheckData(this)" />
                                                        </td>
                                                        <td><%# Container.ItemIndex+1 %></td>
                                                        <td>

                                                            <asp:Button ID="btnStatus" runat="server" CssClass='<%# Convert.ToInt16(Eval("IsActive")) == 1 ? "btn btn-success" : "btn btn-warning" %>' Text='<%#Eval("Status") %>' CommandArgument='<%#Eval("ID") %>' CommandName="Active" />
                                                        <td>
                                                            <a href='Product.aspx?ID=<%# Eval("ID") %>' class="btn btn-primary" title="Edit Profile">Edit</a>
                                                        <td><%#Eval("ProductCategoryName") %></td>
                                                        <td><%#Eval("ProductName") %></td>
                                                        <td>
                                                            <asp:Image runat="server" ID="img" Width="100" Height="100" ImageUrl='<%# "../images/"+Eval("Image").ToString() %>' />
                                                        </td>
                                                        <td><%#Eval("ShortDescription") %></td>
                                                        <td><%#Eval("Description") %></td>
                                                        <td><%#Eval("HSNCode") %></td>
                                                        <td><%#Eval("SKU") %></td>
                                                        <td><%#Eval("Barcode") %></td>
                                                        <td><%#Eval("ImageType") %></td>
                                                        <td><%#Eval("Weight") %></td>
                                                        <td><%#Eval("DaysReturn") %></td>
                                                        <td><%#Eval("Labels") %></td>
                                                        <td><%#Eval("Price") %></td>
                                                        <td><%#Eval("Quantity") %></td>
                                                        <td><%#Eval("Discount") %></td>
                                                        <td><%#Eval("GST") %></td>
                                                        <td><%#Eval("NetAmount") %></td>
                                                        <td><%#Eval("CreatedDate") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <td>
                                                <asp:Button runat="server" ID="btnSave" CssClass="btn-lg btn-danger" OnClick="btnSave_Click" Text="Submit" />
                                            </td>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>


                        </div>
                </section>
            </div>
            <asp:HiddenField runat="server" ID="hdnValues" ClientIDMode="Static" Value="" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
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

        function CheckData(ObjData) {
            var output = $.map($(':checkbox[name=example\\[\\]]:checked'), function (n, i) {
                return n.value;
            }).join(',');
            $("#hdnValues").val(output);
        }

    </script>
</asp:Content>

