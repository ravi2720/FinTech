<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="PAN_Report.aspx.cs" Inherits="Admin_PAN_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="box">
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="col-md-12">

                                    <div class="col-md-3">

                                        <strong>From Date</strong>
                                        <div class="input-group date" data-provide="datepicker">
                                            <asp:TextBox ID="txtfromdate" autocomplete="off" Width="121%" runat="server" CssClass="form-control" placeholder="From Date"></asp:TextBox>
                                            <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MMM-yyyy" PopupButtonID="txtfromdate"
                                                TargetControlID="txtfromdate">
                                            </cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <strong>To Date</strong>
                                        <div class="input-group date" data-provide="datepicker">
                                            <asp:TextBox ID="txttodate" autocomplete="off" Width="121%" runat="server" CssClass="form-control" placeholder="To Date"></asp:TextBox>
                                            <cc1:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd-MMM-yyyy" Animated="False"
                                                PopupButtonID="txttodate" TargetControlID="txttodate">
                                            </cc1:CalendarExtender>
                                        </div>
                                    </div>



                                    <div class="col-md-3">
                                        <strong>PSA Status</strong>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem>Select Status</asp:ListItem>
                                            <asp:ListItem>Pending</asp:ListItem>
                                            <asp:ListItem>Rejected</asp:ListItem>
                                            <asp:ListItem>Approved</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <br />
                                        <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click" CausesValidation="false"
                                            class="btn btn-success" />
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
                                                <th>MsrNo</th>
                                                <th>Member Id</th>
                                                <th>Member Name</th>
                                                <th>Email</th>
                                                <th>Mobile</th>
                                                <th>Pan Number</th>
                                                <th>PSAID</th>
                                                <th>PSA Status</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="gvOperator" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex+1  %></td>
                                                        <td><%# Eval("MsrNo") %></td>
                                                        <td><%# Eval("MemberID") %></td>
                                                        <td><%# Eval("Member Name") %></td>
                                                        <td><%# Eval("Email") %></td>
                                                        <td><%# Eval("Mobile") %></td>
                                                        <td><%# Eval("PanNumber") %></td>
                                                        <td><%# Eval("PsaIdPan") %></td>
                                                        <td><%# Eval("panstatus") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>

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

