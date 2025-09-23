<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Package.aspx.cs" Inherits="Admin_Package" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Package Details</div>
                        <div class="card-body">
                            <div class="form-group">
                                <label for="txtPrice">Role : </label>
                                <asp:DropDownList ID="dllRole" class="form-control ValiDationP" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="txtPackageName">Package : </label>
                                <asp:TextBox runat="server" ID="txtPackageName" ToolTip="Enter Package" class="form-control ValiDationP"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="txtPrice">Price : </label>
                                <asp:TextBox runat="server" ID="txtPrice" Text="0" ToolTip="Enter Price" class="form-control ValiDationP"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="txtPrice">Capping : </label>
                                <asp:TextBox runat="server" ID="txtCapping" Text="0" ToolTip="Enter Price" class="form-control ValiDationP"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="chkActive">Active : </label>
                                <asp:CheckBox runat="server" Checked="true" ID="chkActive" ToolTip="Active" class="form-control ValiDationR"></asp:CheckBox>
                            </div>
                            <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary" />

                            <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th>Active/DeActive</th>
                                        <th>Edit</th>
                                        <th>Package Name</th>
                                        <th>Price</th>
                                        <th>Capping</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="row1">
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgDelete" OnClientClick="javascript:return confirm('Are you sure you want to Active/DeActive this Role?');" CommandName="Active" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("IsActive"))==true ? ConstantsData.ActiveIcon:ConstantsData.DeActiveIcon)  %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ImageUrl="../images/Edit.PNG" Height="20" Width="20" ToolTip="Edit Row" />
                                                </td>
                                                <td><span><%# Eval("Name") %></span></td>
                                                <td><span><%# Eval("Price") %></span></td>
                                                <td><span><%# Eval("Capping") %></span></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnPackageID" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
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

