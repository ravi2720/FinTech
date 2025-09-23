<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ListComplain.aspx.cs" Inherits="Admin_ListComplain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>

                    <div class="panel panel-primary">
                        <div class="panel-heading">Complain List</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <label for="txtFrom">From Date:</label>
                                    <asp:TextBox runat="server" ID="txtFrom" ToolTip="Select From Date." class="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label for="txtTo">To Date:</label>
                                    <asp:TextBox runat="server" ID="txtTo" ToolTip="Select To Date." class="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <input type="submit" name="btnSearch" id="btnSearch" value="Search" class="btn btn-primary" title="Click to search.">
                                </div>
                            </div>
                            <div class="row">

                                <div class="table-responsive">
                                    <table class="table table-hover">
                                        <thead>
                                            <tr>
                                                <th>Complain Id</th>
                                                <th>Complain Date</th>
                                                <th>Business Name</th>
                                                <th>Message</th>
                                                <th>User Type</th>
                                                <th>Status</th>
                                                <th>Response Message</th>
                                                <th>Actions</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rptData">
                                                <ItemTemplate>
                                                    <tr class="row1">
                                                        <td>28                </td>

                                                        <td>2020-03-31                </td>
                                                        <td>Haibat Laskar                </td>
                                                        <td>erer                </td>
                                                        <td>Agent                </td>

                                                        <td>
                                                            <span class="btn btn-success">Solved</span>                  </td>
                                                        <td>ok                </td>
                                                        <td>
                                                            <select class="form-control" id="action_28" onchange="ActionSubmit('28','retailer')">
                                                                <option value="Select">Select</option>
                                                                <option value="Solved">Solved</option>
                                                                <option value="Unsolved">Unsolved</option>
                                                            </select>
                                                        </td>

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
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

