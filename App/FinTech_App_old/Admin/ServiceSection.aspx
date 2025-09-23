<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ServiceSection.aspx.cs" Inherits="Admin_ServiceSection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Create Service Section</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <label>Name</label>
                                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Repeater runat="server" ID="rptAllService">
                                    <ItemTemplate>
                                        <div class="col-md-2 mt-2">
                                            <asp:CheckBox runat="server" ID="chkData" CssClass="form-control" /><%# Eval("Name") %>
                                            <asp:HiddenField runat="server" ID="hdnVal" Value='<%# Eval("ID") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <div class="col-md-4">
                                    <asp:Button runat="server" ID="btnAssign" Text="Assign" CssClass="btn btn-warning mt-4" OnClick="btnAssign_Click" />
                                </div>
                            </div>
                        </div>

                        <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                            <thead>
                                <tr>
                                    <th>S.No</th>
                                    <th>Action</th>
                                    <th>Name</th>
                                    <th>Service</th>
                                </tr>
                            </thead>

                            <tbody>
                                <asp:Repeater ID="rptdata" runat="server" OnItemCommand="rptdata_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex + 1 %></td>
                                            <td>
                                                <asp:ImageButton runat="server" ID="imgDelete" OnClientClick="javascript:return confirm('Are you sure you want to Active/DeActive this Role?');" CommandName="Active" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("IsActive"))==true ? ConstantsData.ActiveIcon:ConstantsData.DeActiveIcon)  %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                <asp:ImageButton runat="server" ID="btnEdit" CommandName="Edit" ImageUrl='<%# ConstantsData.EditIcon %>' CommandArgument='<%# Eval("ID") %>' />
                                            </td>
                                            <td><%# Eval("Name") %></td>
                                            <td><%# Eval("ServiceID") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>

                        </table>
                    </div>

                    <asp:HiddenField runat="server" ID="hdnIDVal" Value="0" />
                </ContentTemplate>

            </asp:UpdatePanel>
        </section>
    </div>
    <script>


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
        $(function () {
            Action(DataObject);
        });
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

