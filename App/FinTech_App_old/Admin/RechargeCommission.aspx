<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="RechargeCommission.aspx.cs" Inherits="Admin_RechargeCommission" %>

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
                                        <label>Select Package</label>
                                        <asp:DropDownList ID="ddlPackage" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlPackage_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Select Service</label>
                                        <asp:DropDownList ID="ddlService" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlService_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success mt-4" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" />
                                    </div>
                                </div>

                            </div>

                            <br />
                            <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th>S No.</th>
                                        <th>
                                            <input type="checkbox" id="chkAll" onchange="ClickAll(this)" />
                                        <th>Operator Name</th>
                                        <th>Commission / Surcharge</th>
                                        <th>Is Surcharge</th>
                                        <th>Is Flat</th>
                                        <th>Service Type</th>
                                        <th>API Name</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <asp:Repeater ID="gvMemberMaster" runat="server" OnItemCommand="gvMemberMaster_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Container.ItemIndex+1 %></td>
                                                <td>
                                                    <asp:CheckBox ID="Chk" ClientIDMode="Static" runat="server" CssClass="chkData" />
                                                    <asp:HiddenField ID="hidOpid" Value='<%# Eval("OperatorID")%>' runat="server" />
                                                </td>
                                                <td><span><%# Eval("OperatorName") %></span></td>
                                                <td><span>
                                                    <asp:TextBox ID="txtCommission" CssClass="form-control" runat="server" Text='<%# Eval("Commission") %>'></asp:TextBox></span></td>
                                                <td><span>
                                                    <asp:CheckBox ID="chkSurcharge" runat="server" Checked='<%# Eval("IsSurcharge") %>' /></span></td>
                                                <td><span>
                                                    <asp:CheckBox ID="chkFlat" runat="server" Checked='<%# Eval("IsFlat") %>' /></span></td>
                                                <td><span><%# Eval("ServiceType") %></span></td>

                                                <td>
                                                    <asp:HiddenField ID="hdnActiveAPI" Value='<%# Eval("ActiveAPI")%>' runat="server" />
                                                    <span><%# Eval("APIName") %></span></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
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

        function ClickAll(Obj) {
            if ($(Obj).is(':checked'))
                $('table [id*=Chk]').prop('checked', true)
            else
                $('table [id*=Chk]').prop('checked', false)
        }
       


    </script>
</asp:Content>

