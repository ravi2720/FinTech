<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AEPSWithdrawRequestShow.aspx.cs" Inherits="Admin_AEPSWithdrawRequestShow" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function GetSelectedRow(ID) {
            debugger;
            document.getElementById("<%=hdnIDValue.ClientID %>").value = ID;
            ShowPopup();
            return false;
        }
    </script>
    <script type="text/javascript">
        function ShowPopup() {
            debugger;
            $("#centralModalSuccess").modal("show");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="update">
                <ContentTemplate>

                    <div class="panel panel-primary">
                        <div class="panel-heading">Fund Request List</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:TextBox runat="server" ID="txtfromdate" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                        TargetControlID="txtfromdate">
                                    </cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox runat="server" ID="txttodate" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="MM-dd-yyyy" Animated="False"
                                        PopupButtonID="txttodate" TargetControlID="txttodate">
                                    </cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2">
                                    <label>Status</label>
                                    <asp:DropDownList runat="server" ID="dllStatus" CssClass="form-control">
                                        <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Success" Value="Processed"></asp:ListItem>                                        
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger" OnClick="btnSearch_Click" Text="Search"></asp:Button>
                                </div>
                            </div>
                            <br />
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
                                                                <th>Status</th>
                                                                <th>Type</th>
                                                                <th>OrderID</th>
                                                                <th>Member ID</th>
                                                                <th>Reason</th>
                                                                <th>Amount</th>
                                                                <th>Charge</th>
                                                                <th>Transaction Mode</th>
                                                                <th>Bank RRN</th>
                                                                <th>Bank Details</th>
                                                                <th>Request ID</th>
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
                                                                            <span class='<%# (Eval("REquestStatus").ToString()=="Success" || Eval("REquestStatus").ToString()=="processed"?"btn btn-success":"btn btn-danger") %>'>
                                                                                <%# (Eval("REquestStatus").ToString()=="Success" || Eval("REquestStatus").ToString()=="processed"?"Success":Eval("REquestStatus").ToString())%>
                                                                            </span>
                                                                        </td>
                                                                        <td><%#Eval("RequestType") %></td>
                                                                        <td><%#Eval("RequestID") %></td>
                                                                        <td><%#Eval("LoginID") %></td>
                                                                        <td><%#Eval("StatusMessage") %></td>
                                                                        <td><%#Eval("AMOUNT") %></td>
                                                                        <td><%#Eval("Charge") %></td>
                                                                        <td><%#Eval("TransMode") %></td>
                                                                        <td><%#Eval("rrn") %></td>
                                                                        <td>
                                                                            <asp:Button runat="server" ID="btnBankDetails" CssClass="btn btn-danger" data-toggle="modal" data-target="#BankDetails" CommandName="ViewBankDetails" CommandArgument='<%#Eval("BankID") %>' Text="View" />
                                                                        </td>
                                                                        <td><%#Eval("RequestID") %></td>
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
    <%--<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#centralModalSuccess">Launch
   modal
</button>--%>

    <!-- Central Modal Medium Success -->
    <div class="modal fade" id="centralModalSuccess" runat="server" visible="false" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
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
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-success" Text="Approve" ValidationGroup="chkaddfund" />
                            <asp:Button ID="Button1" runat="server" data-dismiss="modal" CssClass="btn btn-warning" Text="Close" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
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

