<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="DeviceInfo.aspx.cs" Inherits="Member_DeviceInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <div class="row">
            <div class="col-lg-12">
                <div class="card custom-card">
                    <div class="card-header custom-card-header">
                        <h6 class="card-title mb-0">Login Activity Logs</h6>
                    </div>
                    <div class="card-body">
                        <div class="vtimeline">
                            <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                <ItemTemplate>
                                    <div class="timeline-wrapper timeline-wrapper-primary">
                                        <div class="timeline-badge"></div>
                                        <div class="timeline-panel">
                                            <div class="timeline-heading">
                                                <h6 class="timeline-title"><%# Eval("IP") %></h6>
                                            </div>
                                            <div class="timeline-body">
                                                <p>System Login At <b><%# Eval("Brand") %> Mobile Phone</b> with Model <b><%# Eval("MODEL") %></b></p>
                                            </div>
                                            <div class="timeline-footer d-flex align-items-center flex-wrap">
                                                <span class="ms-auto"><i class="fe fe-calendar text-muted me-1"></i><%# Eval("ADDDATE") %><asp:Button runat="server" OnClientClick="return confirm('Do You want Block/UnBlock Device')" CommandName="active" CommandArgument='<%# Eval("ID") %>' Text='<%# Eval("Name") %>' CssClass="btn btn-danger" /> </span>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

