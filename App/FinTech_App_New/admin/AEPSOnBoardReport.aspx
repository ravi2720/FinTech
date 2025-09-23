<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AEPSOnBoardReport.aspx.cs" Inherits="Admin_AEPSOnBoardReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card panel-primary">
                        <div class="card-header">OnBoard Details</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="box">
                                    <!-- /.box-header -->
                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="col-md-9">
                                                <strong>Search</strong>
                                                <asp:TextBox ID="txtSearch" runat="server" placeholder="Member Id/BcRegistration Id/Mobile" CssClass="form-control"></asp:TextBox>
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
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-content collapse show">
                                        <div class="widgets-container">
                                            <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                <thead>
                                                    <tr>
                                                        <th>S.No</th>
                                                        <th>Member Id</th>
                                                        <th>Member Name</th>
                                                        <th>BC RegistrationId(Mahagram)</th>
                                                        <th>OutLet(InstantPay)</th>
                                                        <th>Email</th>
                                                        <th>Mobile</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="gvTransactionHistory" runat="server" OnItemCommand="gvTransactionHistory_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Container.ItemIndex + 1 %></td>
                                                                <td><%# Eval("Name") %></td>
                                                                <td><%# Eval("LoginID") %></td>
                                                                <td>
                                                                    <span class='<%# Eval("BCIDStatus") %>'>
                                                                        <%# Eval("BCID") %>
                                                                    </span>
                                                                    <asp:Button runat="server" CssClass="btn btn-danger" data-toggle="modal" data-target="#bcid"  ID="btnBCID" Text="Update" CommandName="BCID" CommandArgument='<%# Eval("Mobile") %>' />
                                                                </td>
                                                                <td>
                                                                    <span class='<%# Eval("OutLetIDStatus") %>'>
                                                                        <%# Eval("OutLetID") %>
                                                                    </span>
                                                                </td>
                                                                <td><%# Eval("Email") %></td>
                                                                <td><%# Eval("Mobile") %></td>
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
    <div class="modal upi-modal" id="bcid" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">BC ID Update</h5>
                    <button type="button" class="btn-close" data-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="mrmvs">
                        <ContentTemplate>
                            <div class="row">
                                <asp:Repeater runat="server" ID="rptDatamem">
                                    <ItemTemplate>
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <label>Mobile</label>
                                                <asp:Label runat="server" ID="lblm" Text='<%# Eval("Mobile") %>' CssClass="form-control"></asp:Label>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Email</label>
                                                <asp:Label runat="server" ID="lblEmail" Text='<%# Eval("Email") %>' CssClass="form-control"></asp:Label>
                                            </div>
                                            <div class="col-md-3">
                                                <label>BCID</label>
                                                <asp:Label runat="server" ID="BCID" Text='<%# Eval("BCRegistrationID") %>' CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <label>Enter BCID</label>
                                    <asp:TextBox runat="server" ID="txtBCID" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3 mt-4">
                                    <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" CssClass="form-control btn btn-danger"></asp:Button>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
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

