<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="FundRequest.aspx.cs" Inherits="Reatiler_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="card card-primary">
                <div class="card-header">Fund Request List</div>
                <div class="card-body">
                    <asp:UpdatePanel runat="server" ID="updatefund">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-3">
                                    <label for="ddlMember">From Date : </label>
                                    <asp:TextBox ID="txtfromdate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                        TargetControlID="txtfromdate">
                                    </cc1:CalendarExtender>

                                </div>
                                <div class="col-md-3">
                                    <label for="ddlMember">To Date : </label>
                                    <asp:TextBox ID="txttodate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="MM-dd-yyyy" Animated="False"
                                        PopupButtonID="txttodate" TargetControlID="txttodate">
                                    </cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2">
                                    <label for="ddlMember">Member ID : </label>
                                    <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member."  class="form-control select2"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger" Text="Search" OnClick="btnSearch_Click" />
                                </div>
                            </div>

                            <div class="row mt-2">
                                <div class="col-lg-12">
                                    <div class="ibox float-e-margins">
                                        <div class="ibox-content collapse show">
                                            <div class="widgets-container">
                                                <div>
                                                    <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>S.No</th>
                                                                <th>Action</th>
                                                                <th>Member ID</th>
                                                                <th>Name</th>
                                                                <th>Amount</th>
                                                                <th>Company Bank Name</th>
                                                                <th>Bank Ref ID</th>
                                                                <th>Payment Date</th>
                                                                <th>Payment Mode</th>
                                                                <th>Remark</th>
                                                                <th>Add Date</th>

                                                                <th>Approve Date</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="repeater1" OnItemCommand="repeater1_ItemCommand" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Container.ItemIndex+1 %>
                                                                            <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ID") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="btnreject" CommandName="IsDelete" CssClass='<%#Eval("BTNCOLOR") %>' Text="Reject" Visible='<%# (Convert.ToInt32(Eval("IsDelete")) == 0 && Convert.ToInt32(Eval("ISAPPROVE")) == 0) ? true : false %>' runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure to Reject?');"></asp:Button>
                                                                            <asp:Button ID="lbtnApprove" CommandName="IsApprove" CssClass='<%#Eval("BTNCOLOR") %>' Text='<%#Eval("STATUS") %>' Enabled='<%# (Convert.ToBoolean(Eval("ISAPPROVE"))==false && Convert.ToBoolean(Eval("IsDelete"))==false) == true ? true : false  %>' runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure to Approve?');"></asp:Button>
                                                                        </td>
                                                                        <td><%#Eval("LoginID") %></td>
                                                                        <td><%#Eval("NAME") %></td>
                                                                        <td><%#Eval("AMOUNT") %></td>
                                                                        <td>
                                                                            <%#Eval("BankName") %>
                                                                            <asp:ImageButton runat="server" ID="imgView" data-toggle="modal" data-target="#myModal" CommandName="ViewBankDetails" Height="30" Width="30" ImageUrl='<%# ConstantsData.EyeOpen %>' CommandArgument='<%# Eval("CompanyBankID") %>' />
                                                                        </td>
                                                                        <td><%#Eval("BANKREFID") %></td>
                                                                        <td><%#Eval("PAYMENTDATE") %></td>
                                                                        <td><%#Eval("PAYMENTMODE") %></td>
                                                                        <td><%#Eval("REMARK") %></td>
                                                                        <td><%#Eval("AddDate") %></td>

                                                                        <td><%#Eval("ApproveDate") %></td>
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
                    </asp:UpdatePanel>
                </div>
            </div>
        </section>
    </div>
    <!-- Modal -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Company Bank Details</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="uptbank">
                        <ContentTemplate>
                            <asp:Repeater runat="server" ID="rptBankData">
                                <ItemTemplate>
                                    <div class="row">
                                        <div class="col-md-3 font-weight-500">Bank Name</div>
                                        <div class="col-md-3"><%# Eval("BankName") %></div>
                                        <div class="col-md-3 font-weight-500">IFSC</div>
                                        <div class="col-md-3"><%# Eval("IFSCCode") %></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3 font-weight-500">Account Holder Name</div>
                                        <div class="col-md-3"><%# Eval("AccountHolderName") %></div>
                                        <div class="col-md-3 font-weight-500">Branch Name</div>
                                        <div class="col-md-3"><%# Eval("BranchName") %></div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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

