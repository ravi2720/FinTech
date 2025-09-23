<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="InstantKYCList.aspx.cs" Inherits="Admin_InstantKYCList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card">
                        <h4 class="card-header">Customer History
                            <input type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#numberchange" value="Update Mobile" />
                        </h4>

                        <div class="card-body">
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
                                    <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member." class="form-control select2"></asp:DropDownList>
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

                                                <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>S.No</th>
                                                            <th>Member Name</th>
                                                            <th>OutLet</th>
                                                            <th>Mobile</th>
                                                            <th>Name</th>
                                                            <th>Pan</th>
                                                            <th>Address</th>
                                                            <th>Photo</th>
                                                            <th>AddDate</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%# Container.ItemIndex + 1 %></td>
                                                                    <td><%# Eval("MemberName") %><br />
                                                                        <%# Eval("LOGINID") %></td>
                                                                    <td><%# Eval("OutLetID") %></td>
                                                                    <td><%# Eval("MOBILE") %></td>
                                                                    <td><%# Eval("Name") %></td>
                                                                    <td><%# Eval("Pan") %></td>
                                                                    <td><%# Eval("Address") %></td>
                                                                    <td>
                                                                        <img height="100" width="100" src='<%# "data:image/png;base64, "+Eval("ProfilePic").ToString() %>' alt="Red dot" />
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
                    </div>

                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>

    <div id="numberchange" class="modal fade" role="dialog">
        <div class="modal-dialog modal-md">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Update Kyc Number</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="upchnage">
                        <ContentTemplate>


                            <div class="card">
                                <div class="card-body">
                                    <div class="form-group">
                                        <label>Old Mobile</label>
                                        <asp:TextBox runat="server" ID="txtoldMobile" AutoPostBack="true" OnTextChanged="txtoldMobile_TextChanged" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>New Mobile</label>
                                        <asp:TextBox runat="server" ID="txtNewMobile" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Repeater runat="server" ID="rptDataMemberDtails">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <label>Name</label>
                                                       <p><strong><%# Eval("Name") %></strong></p> 
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label>LoginID</label>
                                                     <p>   <strong><%# Eval("LoginID") %></strong></p>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label>Pan</label>
                                                      <p>  <strong><%# Eval("Pan") %></strong></p>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="form-group" runat="server" id="otpdiv" visible="false">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label>Old Mobile OTP</label>
                                                <asp:TextBox runat="server" ID="txtOldOTP" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label>New Mobile OTP</label>
                                                <asp:TextBox runat="server" ID="txtNewOTP" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button runat="server" ID="btnsubmit" CssClass="btn btn-danger" Text="Send OTP" OnClick="btnsubmit_Click" />

                                    </div>
                                </div>
                            </div>
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





