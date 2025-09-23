<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="MemberUpi_Details.aspx.cs" Inherits="Admin_UPIDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div role="tabpanel" class="profile-tab-pane tab-pane " id="UPIID">
                        <div class="card-group padding-20" id="accordion" role="tablist" aria-multiselectable="true">
                            <div class="card card-primary">
                                <div class="card-header">UPI Details  </div>
                                <div class="card-body">
                                    <div class="row">
                                        <table class="table" style="background: #011f3f; color: white">
                                            <tbody>
                                                <asp:Repeater ID="rptUPIDetails" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>UPI Id :</td>
                                                            <td>
                                                                <asp:Label ID="lblUpiID" runat="server" Text='<%#Eval("ICICIUPI") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Name :</td>
                                                            <td>
                                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>PAN :</td>
                                                            <td>
                                                                <asp:Label ID="lblPAN" runat="server" Text='<%#Eval("Pan") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Category :</td>
                                                            <td>
                                                                <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Category") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Status :</td>
                                                            <td>
                                                                <asp:Label ID="lblStatusUPI" runat="server" Text="ACTIVATED"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Address :</td>
                                                            <td>
                                                                <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Created At :</td>
                                                            <td>
                                                                <asp:Label ID="lblAddDate" runat="server" Text='<%#Eval("AddDate") %>'></asp:Label></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="ibox float-e-margins">
                                                <div class="ibox-content collapse show">
                                                    <div class="widgets-container">
                                                        <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th>S.No</th>
                                                                    <th>Txn ID</th>
                                                                    <th>Payer Name</th>
                                                                    <th>Source</th>
                                                                    <th>Mobile</th>
                                                                    <th>Txn Type</th>
                                                                    <th>Bank Ref No</th>
                                                                    <th>Txn Amount</th>
                                                                    <th>Charges</th>
                                                                    <th>Settled Amount</th>
                                                                    <th>Date</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%# Container.ItemIndex + 1 %></td>
                                                                            <td><%# Eval("txn_id") %></td>
                                                                            <td><%# Eval("payer_name") %></td>
                                                                            <td><%# Eval("source") %></td>
                                                                            <td><%# Eval("payer_mobile") %></td>
                                                                            <td><%# Eval("payment_type") %></td>
                                                                            <td><%# Eval("bank_ref_num") %></td>
                                                                            <td><%# Eval("amount") %></td>
                                                                            <td><%# Eval("charges_gst") %></td>
                                                                            <td><%# Eval("settled_amount") %></td>
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
