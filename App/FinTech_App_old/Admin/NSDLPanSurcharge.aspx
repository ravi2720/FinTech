<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="NSDLPanSurcharge.aspx.cs" Inherits="Admin_NSDLPanSurcharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">NSDL Pan Surcharge Details</div>
                        <div class="card-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="ddlPackage">Package : </label>
                                        <asp:DropDownList ID="ddlPackage" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPackage_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtfromd">Surcharge : </label>
                                        <asp:TextBox runat="server" TextMode="Number" ID="txtSurcharge" ToolTip="Enter Surcharge" class="form-control ValiDationR"></asp:TextBox>
                                        <asp:HiddenField ID="hdnidd" runat="server" Value="" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtfromd">Commission : </label>
                                        <asp:TextBox runat="server"  ID="txtCommission" ToolTip="Enter Commission" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="chkActive">Flat : </label>
                                        <asp:CheckBox runat="server" ID="chkflatd" ToolTip="Active" class="form-control ValiDationR"></asp:CheckBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtfromd">Coupon Type : </label>
                                        <asp:DropDownList runat="server" ID="dllType" ToolTip="Enter Surcharged" class="form-control ValiDationR">
                                            <asp:ListItem Text="Select Type" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Physical Pan" Value="P"></asp:ListItem>
                                            <asp:ListItem Text="Electronic Pan" Value="E"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-4">
                                        <asp:Button runat="server" ID="btnadd" Text="Add / Update" OnClick="btnSubmit_Click" class="btn btn-primary mt-2" />
                                    </div>
                                </div>
                            </div>

                            <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th>S.No</th>
                                        <th>Action</th>
                                        <th>PackageName</th>
                                        <th>Surcharge</th>
                                        <th>Commission</th>
                                        <th>Flat/Percentage</th>
                                        <th>Name</th>
                                        <th>AddDate</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptAepsCommission" runat="server" OnItemCommand="rptAepsCommission_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Container.ItemIndex+1 %></td>
                                                <td>
                                                    <asp:Button ID="btnDelete" runat="server" OnClientClick="return confirm('Do You Want To Delete Record')" CssClass="btn btn-primary" Text="Delete" CommandArgument='<%#Eval("ID") %>' CommandName="Delete" />
                                                    <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Edit" CommandArgument='<%#Eval("ID") %>' CommandName="Edit" /></td>
                                                <td><%#Eval("PackageName") %></td>
                                                <td><%#Eval("Surcharge") %></td>
                                                <td><%#Eval("Commission") %></td>
                                                <td><%#Eval("IsFlat") %></td>
                                                <td><%#Eval("Name") %></td>
                                                <td><%#Eval("AddDate") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnRoleID" Value="0" />
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>

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
</asp:Content>





