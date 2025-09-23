<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="PAN_ListPending.aspx.cs" Inherits="Admin_PAN_ListPending" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function showModal() {
            document.getElementById("btnfailurePopup").click();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="box">

                            <div class="box-body">
                                <div class="col-md-12">

                                    <table id="customers">
                                        <tr>
                                            <td>
                                                <span style="color: red">*</span> Search
                                      <asp:TextBox runat="server" ID="txtsearch" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <br />
                                                <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Search" CssClass="btn btn-default" CausesValidation="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="box">

                            <div class="box-body table-responsive">
                                <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                    <thead>
                                        <tr>
                                            <th>S.No</th>
                                            <th>Application Date</th>
                                            <th>MemberID</th>
                                            <th>MemberName</th>
                                            <th>Old Pan Number</th>
                                            <th>TransactionID</th>
                                            <th>PAN Applicant Info</th>
                                            <th>MobileNumber</th>
                                            <th>State</th>
                                            <th>Status</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>

                                        <asp:Repeater ID="dgmember" runat="server" OnItemCommand="dgmember_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <span><%# Container.ItemIndex + 1 %></span>
                                                    </td>
                                                    <td>
                                                        <span><%# Eval("AddDate") %></span>
                                                    </td>
                                                    <td>
                                                        <span><%# Eval("MemberID") %></span>
                                                    </td>
                                                    <td>
                                                        <span><%# Eval("MemberName") %></span>
                                                    </td>
                                                    <td>
                                                        <span><%# Eval("OldPancard") %></span>
                                                    </td>
                                                    <td>
                                                        <span><%# Eval("TransactionID") %></span>
                                                    </td>
                                                    <td>
                                                        <span><%# Eval("ApplicantFirstName") %>&nbsp;  <%# Eval("ApplicantLastName") %>
                                                            <br />
                                                            (Aadhar No. <%# Eval("AadharNumber") %>)


                                                        </span>
                                                    </td>
                                                    <td>
                                                        <span><%# Eval("MobileNumber") %></span>
                                                    </td>
                                                    <td>
                                                        <span><%# Eval("CommunicationState") %></span>
                                                    </td>
                                                    <td>
                                                        <span><%# Eval("Status") %></span>
                                                    </td>
                                                    <td>
                                                        <span><a href="AdminMemberPan.aspx?panid=<%# Eval("id") %>" title="Edit this record">
                                                            <img src="../images/icon/edit_16x16.png" alt="Edit" />
                                                        </a></span>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Button ID="btnFfailed" runat="server" Text="Reject" CausesValidation="false" CommandArgument='<%# Eval("ID") + "|" +Eval("RegistrationNo") %>' CommandName="Fail" OnClientClick="return confirm('Are you sure to reject ?')" /></span>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnForceFailed" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>



    <button id="btnfailurePopup" type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal" style="display: none">Open Modal</button>

    <!-- Modal -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Write Reason of Failure</h4>
                </div>
                <div class="modal-body">

                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="10" Columns="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtRemark" runat="server" ErrorMessage="Remark Required"
                        ControlToValidate="txtRemark" Display="Dynamic" SetFocusOnError="true" ValidationGroup="fv"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <center>
                            <asp:Button ID="btnForceFailed" CssClass="btn btn-primary" runat="server" Text="Fail & Refund" ValidationGroup="fv" OnClick="btnForceFailed_Click" /></center>
                    <asp:HiddenField ID="hddtransID" runat="server" />

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

