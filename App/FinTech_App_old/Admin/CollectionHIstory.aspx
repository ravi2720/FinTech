<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="CollectionHIstory.aspx.cs" Inherits="Admin_CollectionHIstory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">History</div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="box">

                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtfromdate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                                    TargetControlID="txtfromdate">
                                                </cc1:CalendarExtender>

                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txttodate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="MM-dd-yyyy" Animated="False"
                                                    PopupButtonID="txttodate" TargetControlID="txttodate">
                                                </cc1:CalendarExtender>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger" Text="Search" OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                        <br />
                                        <br />

                                        <div class="col-md-12">

                                          <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                <thead>
                                                    <tr>
                                                        <th>S.No</th>                                                        
                                                        <th>Member Name</th>
                                                        <th>UTR</th>
                                                        <th>CustomerAccountNo</th>
                                                        <th>Amount</th>
                                                        <th>VirtualACCode</th>                                                        
                                                        <th>Status</th>
                                                        <th>Date</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                              
                                                                <td><%# Container.ItemIndex + 1 %></td>
                                                                <td><%# Eval("NAME") %><br /><%# Eval("LOGINID") %></td>
                                                                <td><%# Eval("UTR") %></td>
                                                                <td><%# Eval("CustomerAccountNo") %></td>
                                                                <td><%# Eval("AMT") %></td>
                                                                <td><%# Eval("VirtualACCode") %></td>                                                                
                                                                <td>
                                                                   <span class="btn btn-success">Success</span>
                                                                    <asp:HiddenField runat="server" ID="hdnRefNumber" Value='<%# Eval("UTR") %>' />
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

