<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AddAEPSCommission.aspx.cs" Inherits="Admin_AddAEPSCommission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Commission Details</div>
                        <div class="card-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="ddlPackage">Package : </label>
                                        <asp:DropDownList ID="ddlPackage" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPackage_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtfromd">Start Range : </label>
                                        <asp:TextBox runat="server" ID="txtStartVal" ToolTip="Enter Start Range" class="form-control ValiDationR"></asp:TextBox>
                                        <asp:HiddenField ID="hdnidd" runat="server" Value="" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtfromd">End Range : </label>
                                        <asp:TextBox runat="server" ID="txtEndVal" ToolTip="Enter End Range" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="txtfromd">Commission : </label>
                                        <asp:TextBox runat="server" ID="txtsurcharged" ToolTip="Enter Surcharged" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="chkActive">Flat : </label>
                                        <asp:CheckBox runat="server" ID="chkflatd" ToolTip="Active" class="form-control ValiDationR"></asp:CheckBox>
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
                                        <th>StartVal</th>
                                        <th>EndVal</th>
                                        <th>Commission</th>
                                        <th>Flat/Percentage</th>
                                        <th>AddDate</th>
                                    </tr>
                                </thead>
                                <tfoot>
                                    <tr>
                                        <th>S.No</th>
                                        <th>Action</th>
                                        <th>PackageName</th>
                                        <th>StartVal</th>
                                        <th>EndVal</th>
                                        <th>Commission</th>
                                        <th>Flat/Percentage</th>
                                        <th>AddDate</th>
                                    </tr>
                                </tfoot>
                                <tbody>
                                    <asp:Repeater ID="rptAepsCommission" runat="server" OnItemCommand="rptAepsCommission_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Container.ItemIndex+1 %></td>
                                                <td>
                                                    <asp:Button ID="btnDelete" runat="server" OnClientClick="return confirm('Do You Want To Delete Record')" CssClass="btn btn-primary" Text="Delete" CommandArgument='<%#Eval("ID") %>' CommandName="Delete" />
                                                    <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Edit" CommandArgument='<%#Eval("ID") %>' CommandName="Edit" /></td>
                                                <td><%#Eval("PackageName") %></td>
                                                <td><%#Eval("StartVal") %></td>
                                                <td><%#Eval("EndVal") %></td>
                                                <td><%#Eval("Surcharge") %></td>
                                                <td><%#Eval("IsFlat") %></td>
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

