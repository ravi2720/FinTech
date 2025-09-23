<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AEPSPendingTransaction.aspx.cs" Inherits="Admin_AEPSPendingTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">

                <div class="panel panel-primary">
                    <div class="panel-heading">OnBoard Details</div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="box">
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <div class="col-md-12">

                                        <div class="col-md-2">
                                            <strong>From Date</strong>
                                            <div class="input-group date" data-provide="datepicker">
                                                <asp:TextBox ID="txtfromdate" AutoComplete="off" runat="server" CssClass="form-control" MaxLength="10" placeholder="MM/DD/YYYY"></asp:TextBox>
                                                <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="dd-MMM-yyyy" PopupButtonID="txtfromdate"
                                                    TargetControlID="txtfromdate">
                                                </cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <strong>To Date</strong>
                                            <div class="input-group date" data-provide="datepicker">
                                                <asp:TextBox ID="txttodate" AutoComplete="off" runat="server" CssClass="form-control" MaxLength="10" placeholder="MM/DD/YYYY"></asp:TextBox>
                                                <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="dd-MMM-yyyy" Animated="False"
                                                    PopupButtonID="txttodate" TargetControlID="txttodate">
                                                </cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <strong>Search </strong>
                                            <asp:TextBox ID="txtSearch" placeholder="Searching...." runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <br />
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CausesValidation="false"
                                                class="btn btn-primary" />
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
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <div class="col-md-12">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                                <thead>
                                                                    <tr>
                                                                        <th></th>
                                                                        <th>S.No</th>
                                                                        <th>MemberName</th>
                                                                        <th>Member ID</th>
                                                                        <th>AadharNumber</th>

                                                                        <th>Amount</th>
                                                                        <th>Api TransID</th>

                                                                        <th>Status</th>

                                                                        <th>Date (DD/MM/YYYY)</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>

                                                                    <asp:Repeater ID="gvEWalletTransaction" runat="server">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>  <asp:CheckBox ID="chkRow" runat="server" /></td>
                                                                                <td><%# Container.ItemIndex + 1 %></td>
                                                                                <td><%# Eval("Name") %></td>
                                                                                <td><%# Eval("LoginID") %></td>
                                                                                <td><%# Eval("adhaarnumber") %></td>
                                                                                <td><%# Eval("Amount") %></td>
                                                                                <td><%# Eval("TransactionId") %></td>


                                                                                <td><%# Eval("Status") %>


                                                                                    <asp:HiddenField ID="hddMsrNo" runat="server" Value='<%# Eval("MsrNo") %>' />
                                                                                    <asp:HiddenField ID="hdnApiTransID" runat="server" Value='<%# Eval("TransactionId") %>' />
                                                                                  
                                                                                </td>


                                                                                <td><%# Eval("AddDate").ToString().Substring(6,2) + "/" + Eval("AddDate").ToString().Substring(4,2) + "/" + Eval("AddDate").ToString().Substring(0,4)%></td>
                                                                            </tr>
                                                                        </ItemTemplate>



                                                                        <%--<asp:CheckBox ID="chkRow" runat="server" />--%>
                                                                    </asp:Repeater>
                                                                </tbody>

                                                            </table>
                                                            <div class="col-md-12">
                                                                <div class="row">


                                                                    <div class="col-md-2">
                                                                        <asp:Button ID="btnCheckStatus" runat="server" Text="Check Status" OnClick="btnCheckStatus_Click" CssClass="btn btn-success" />
                                                                    </div>
                                                                   
                                                                </div>
                                                            </div>

                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnCheckStatus" EventName="Click" />

                                                        </Triggers>

                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>

                        </div>
                    </div>
                </div>

            </div>

        </section>
    </div>
    <script>
        $(function () {
            action(dataobject);
        });

        var dataobject = {
            id: "example1",
            idisplaylength: 5,
            bpaginate: true,
            bfilter: true,
            binfo: true,
            blengthchange: true,
            searching: true
        };
        function loaddata() {

            action(dataobject);
        }

        var req = sys.webforms.pagerequestmanager.getinstance();
        req.add_beginrequest(function () {

        });
        req.add_endrequest(function () {
            loaddata();
        });
        function showmodal() {
            $('#mymodal').modal();
        }


    </script>
</asp:Content>

