<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AllRequest.aspx.cs" Inherits="Admin_Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="panel panel-primary">
                <div class="panel-heading">Request Details</div>


                <table class="display nowrap table responsive nowrap table-bordered">
                    <tbody>
                        <asp:Repeater ID="repData" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><strong><%#Eval("AllRechargeRequest") %></strong> Total Recharge Request </td>
                                    <td><strong><%#Eval("PendingRechargeRequest") %></strong>  Pending Recharge Request</td>
                                    <td><strong><%#Eval("SuccessRechargeRequest") %></strong>  Success Recharge Request</td>
                                    <td><strong><%#Eval("FailedRechargeRequest") %></strong> Failed Recharge Request</td>

                                </tr>
                                <tr>
                                    <td><strong><%#Eval("AllDMTRequest") %></strong> Total DMT Request </td>
                                    <td><strong><%#Eval("PendingDMTRequest") %></strong> Pending DMT Request</td>
                                    <td><strong><%#Eval("SuccessDMTRequest") %></strong> Success DMT Request</td>
                                    <td><strong><%#Eval("FailedDMTequest") %></strong> Failed DMT Request</td>

                                </tr>
                                <tr>
                                    <td><strong><%#Eval("AllAEPSRequest") %></strong> Total AEPS Request </td>
                                    <td><strong><%#Eval("PendingAEPSRequest") %></strong>   Pending AEPS Request</td>
                                    <td><strong><%#Eval("SuccessAEPSRequest") %></strong>   Success AEPS Request</td>
                                    <td><strong><%#Eval("FailedAEPSRequest") %></strong>   Failed AEPS Request</td>

                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>

                    </tbody>
                </table>


            </div>
        </section>
    </div>
</asp:Content>

