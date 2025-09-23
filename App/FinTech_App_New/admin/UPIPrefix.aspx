<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="UPIPrefix.aspx.cs" Inherits="Admin_UPIPrefix" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">UPI Details</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="box">
                                    <!-- /.box-header -->
                                    <div class="box-header">
                                        <label style="font-weight:bold;color:white;" class="btn btn-danger">prefix: required - Only lowercase alphanumeric characters</label>
                                    </div>
                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="col-md-9">
                                                <strong>PreFix</strong>
                                                <asp:TextBox ID="txtPreFix" runat="server" placeholder="PreFix" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <br />
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Search &gt;&gt;" OnClick="btnSearch_Click"
                                                    ValidationGroup="vg1" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="box">
                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                <thead>
                                                    <tr>
                                                        <th>S.No</th>
                                                        <th>Name</th>
                                                        <th>Status</th>
                                                        <th>Check Status</th>
                                                        <th>AddDate</th>                                                        
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptPreFixed" runat="server" OnItemCommand="rptPreFixed_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Container.ItemIndex + 1 %></td>
                                                                <td><%# Eval("Name") %></td>
                                                                <td><%# Eval("Status") %></td>                                                                
                                                                <td>
                                                                    <asp:Button runat="server" ID="btnCheckStatus" Text="CheckStatus" CommandArgument='<%# Eval("Name") %>' CommandName="CheckStatus" CssClass="btn btn-danger" />
                                                                </td>
                                                                <td><%# Eval("AddDate") %></td>                                                                
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
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


    </script>
</asp:Content>

