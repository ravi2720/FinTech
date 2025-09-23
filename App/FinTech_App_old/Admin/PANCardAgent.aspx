<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="PANCardAgent.aspx.cs" Inherits="Admin_PANCardAgent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="box">
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <strong>Search</strong>
                                        <asp:TextBox ID="txtSearch" runat="server" placeholder="MemberID /PSAID /RequestID" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <br />
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-success" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <asp:HiddenField ID="hfname" runat="server" />
                                <div class="ibox-content collapse show">
                                    <div class="widgets-container">
                                        <div>

                                            <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                <thead>
                                                    <tr>
                                                        <th>S.No</th>
                                                        <th>MsrNo</th>
                                                        <th>Member Id</th>
                                                        <th>Member Name</th>
                                                        <th>Email</th>
                                                        <th>Mobile</th>
                                                        <th>PSA Id</th>
                                                        <th>PSA Status</th>
                                                        <th>Request Id</th>
                                                        <th>CheckStatus</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="gvPANReport" runat="server" OnItemCommand="gvPANReport_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Container.ItemIndex+1 %></td>
                                                                <td><%# Eval("MsrNo") %></td>
                                                                <td><%# Eval("MemberID") %></td>
                                                                <td><%# Eval("Name") %></td>
                                                                <td><%# Eval("Email") %></td>
                                                                <td><%# Eval("Mobile") %></td>
                                                                <td><%# Eval("PsaIdPan") %></td>
                                                                <td><%# Eval("panstatus") %></td>
                                                                <td><%# Eval("RequestId") %></td>
                                                                <td>
                                                                    <asp:HiddenField ID="hdnval" runat="server" Value='<%# Eval("RequestId")%>' />
                                                                    <asp:Button runat="server" ID="btnCheck" Text="Status" CommandName="CheckStatus" class="btn btn-danger btn-sm" CommandArgument='<%# Eval("RequestId") %>' />

                                                                </td>
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
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
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

