<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="PayoutHistory.aspx.cs" Inherits="Admin_PayoutHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetSelectedRow(ID) {
            document.getElementById("<%=hdnIDValue.ClientID %>").value = ID;
            ShowPopup();
            return false;
        }
    </script>
    <script type="text/javascript">
        function ShowPopup() {
            $("#centralModalSuccess").modal("show");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="update">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">
                            Payout History
                            <span class="btn btn-success btn3d">Success <span class="badge" runat="server" id="lblSCount">0</span></span><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="btn btn-success btn3d">Success <span class="badge" runat="server" id="lblSsum">0</span></span>
                            <span class="btn btn-warning btn3d">Pending <span class="badge" runat="server" id="lblPCount">0</span></span><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="btn btn-warning btn3d">Pending <span class="badge" runat="server" id="lblPSum">0</span></span>
                            <span class="btn btn-danger btn3d">Failed <span class="badge" runat="server" id="lblFCount">0</span></span><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="btn btn-danger btn3d">Failed <span class="badge" runat="server" id="lblFSum">0</span></span>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <strong>Search</strong>
                                    <asp:TextBox runat="server" ID="txtSearch" placeholder="Enter Search" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <strong>From Date</strong>

                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                        TargetControlID="txtfromdate">
                                    </cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2">
                                    <strong>To Date</strong>

                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="MM-dd-yyyy" Animated="False"
                                        PopupButtonID="txttodate" TargetControlID="txttodate">
                                    </cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2">
                                    <strong>Status</strong>
                                    <asp:DropDownList runat="server" ID="dllStatus" CssClass="form-control select2">
                                        <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Success" Value="Success"></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                        <asp:ListItem Text="Failed" Value="Failed"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label for="ddlMember">Member ID : </label>
                                    <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member." class="form-control select2"></asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <br />
                                    <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click" CausesValidation="false"
                                        class="btn btn-primary btn3d" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="ibox float-e-margins">
                                        <div class="ibox-content collapse show">
                                            <div class="widgets-container">
                                                <div>
                                                    <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th>S.No</th>
                                                                <th>Action</th>
                                                                <th>Type</th>
                                                                <th>OrderID</th>
                                                                <th>Status</th>
                                                                <th>RRN</th>
                                                                <th>Member ID</th>
                                                                <th>Name</th>
                                                                <th>Amount</th>
                                                                <th>Charge</th>
                                                                <th>Transaction Mode</th>
                                                                <th>AEPS Approve Amount</th>
                                                                <th>Bank Details</th>
                                                                <th>Transaction ID</th>
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
                                                                            <asp:Label ID="lblId" runat="server" Visible="false" Text='<%#Eval("ID") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <div class="dropdown">
                                                                                <button class="dropbtn">Action</button>
                                                                                <div class="dropdown-content">
                                                                                    <a href="#">
                                                                                        <asp:ImageButton runat="server" ToolTip="Force Failed" ID="btnforcefailed" ImageUrl="<%# ConstantsData.DeleteIcon %>" OnClientClick="return confirm('Are you sure for Force Failed')" CommandName="IsDelete" Height="50" Visible='<%# (Eval("RequestStatus").ToString()=="Pending"?true:false) %>' CommandArgument='<%# Eval("ID") %>' />
                                                                                    </a>
                                                                                    <a href="#">
                                                                                        <asp:ImageButton runat="server" ToolTip="Force Success" ID="btnforcesuccess" ImageUrl="<%# ConstantsData.fs %>" OnClientClick="return confirm('Are you sure for Force Success')" CommandName="IsApprove" Height="50" Visible='<%# (Eval("RequestStatus").ToString()=="Pending"?true:false) %>' CommandArgument='<%# Eval("ID") %>' />
                                                                                    </a>
                                                                                    <a href="#">
                                                                                        <asp:ImageButton runat="server" ToolTip="Check Status" ID="btnCheckStatus" ImageUrl="<%# ConstantsData.checkstatus %>" CommandName="CheckStatus" CommandArgument='<%# Eval("ID") %>' Height="50" Visible='<%# (Eval("RequestStatus").ToString()=="Pending"?true:false) %>' />
                                                                                    </a>

                                                                                </div>
                                                                            </div>



                                                                        </td>
                                                                        <td><%#Eval("RequestType") %></td>
                                                                        <td><%#Eval("RequestID") %></td>
                                                                        <td>
                                                                            <span class='<%# (Eval("RequestSTatus").ToString().ToUpper()=="SUCCESS"?"btn btn-primary btn-lg btn3d":"btn btn-danger btn-lg btn3d") %>'><%# Eval("RequestSTatus") %></span>
                                                                        </td>
                                                                        <td><%#Eval("RRN") %></td>
                                                                        <td><%#Eval("LoginID") %></td>
                                                                        <td><%#Eval("NAME") %></td>
                                                                        <td><%#Eval("AMOUNT") %></td>
                                                                        <td><%#Eval("Charge") %></td>
                                                                        <td><%#Eval("TransMode") %></td>
                                                                        <td><%#Eval("AEPSApproveAmount") %></td>
                                                                        <td>
                                                                            <asp:Button runat="server" ID="btnBankDetails" CssClass="btn btn-warning btn3d" data-toggle="modal" data-target="#BankDetails" CommandName="ViewBankDetails" CommandArgument='<%#Eval("BankID") %>' Text="View" />
                                                                        </td>
                                                                        <td><%#Eval("TransactionID") %></td>
                                                                        <td><%#Eval("RequestDate") %></td>
                                                                        <td><%#Eval("PaidDate") %></td>
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
                    <asp:HiddenField ID="hdnIDValue" runat="server" />
                </ContentTemplate>

            </asp:UpdatePanel>
        </section>
    </div>


    <!-- Central Modal Medium Success -->
    <div class="modal fade" id="centralModalSuccess" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-notify modal-success" role="document">
            <!--Content-->
            <div class="modal-content">
                <!--Header-->
                <div class="modal-header">
                    <p class="heading lead">AEPS Withdraw</p>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true" class="white-text">&times;</span>
                    </button>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>

                        <!--Body-->
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="exampleInputPassword1">Bank Refference ID</label>
                                <asp:TextBox ID="txtBenkRefID" runat="server" placeholder="Bank Refference ID" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Bank Refference ID" Font-Bold="True" ForeColor="Red" ControlToValidate="txtBenkRefID" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <!--Footer-->
                        <div class="modal-footer justify-content-center">
                            <asp:Button ID="Button1" runat="server" data-dismiss="modal" CssClass="btn btn-warning" Text="Close" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <!--/.Content-->
        </div>
    </div>
    <!-- Central Modal Medium Success-->

    <!-- Central Modal Medium Success -->
    <div class="modal fade" id="BankDetails" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-notify modal-success" role="document">
            <!--Content-->
            <div class="modal-content">
                <!--Header-->
                <div class="modal-header">
                    <p class="heading lead">Bank Details</p>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true" class="white-text">&times;</span>
                    </button>
                </div>

                <!--Body-->
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                            <asp:Repeater runat="server" ID="rptBankData">
                                <ItemTemplate>
                                    <div class="form-group">
                                        <label for="exampleInputPassword1">Name : </label>
                                        <%# Eval("Name") %>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputPassword1">IFSC</label>
                                        <%# Eval("IFSCCode") %>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputPassword1">AccountNo</label>
                                        <%# Eval("AccountNumber") %>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputPassword1">Acount Holder Name</label>
                                        <%# Eval("AccountHolderName") %>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputPassword1">Branch Name</label>
                                        <%# Eval("BranchName") %>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>

                <!--Footer-->
                <div class="modal-footer justify-content-center">
                    <asp:Button ID="Button4" runat="server" data-dismiss="modal" CssClass="btn btn-warning" Text="Close" />
                </div>
            </div>
            <!--/.Content-->
        </div>

    </div>
    <!-- Central Modal Medium Success-->

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

