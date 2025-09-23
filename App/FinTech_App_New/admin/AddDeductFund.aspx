<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AddDeductFund.aspx.cs" Inherits="Admin_AddDeductFund" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Add Deduct History</div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="box">

                                    <div class="box-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtfromdate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:calendarextender runat="server" id="txtfromdate_ce" format="MM-dd-yyyy" popupbuttonid="txtfromdate"
                                                    targetcontrolid="txtfromdate">
                                                </cc1:calendarextender>

                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txttodate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:calendarextender runat="server" id="txttodate_ce" format="MM-dd-yyyy" animated="False"
                                                    popupbuttonid="txttodate" targetcontrolid="txttodate">
                                                </cc1:calendarextender>
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
                                                        <th>AddDate</th>
                                                        <th>Amount</th>
                                                        <th>Factor</th>
                                                        <th>Remark</th>
                                                        <th>WalletType</th>                                                        
                                                        <th>By</th>                                                        
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Container.ItemIndex + 1 %></td>
                                                                <td><%# Eval("NAME") %><br />
                                                                    <%# Eval("LOGINID") %></td>
                                                                <td><%# Eval("AddDate") %></td>
                                                                <td><%# Eval("Amount") %></td>
                                                                <td>
                                                               <span class='<%# (Eval("factor").ToString()=="cr"?"btn btn-success":"btn btn-danger") %>'>     <%# Eval("Factor") %></span>

                                                                </td>
                                                                <td><%# Eval("Remark") %></td>                                                               
                                                                <td><%# Eval("WalletType") %></td>                                                               
                                                                <td><%# Eval("By") %></td>                                                               
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

