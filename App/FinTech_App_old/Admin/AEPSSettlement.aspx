<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AEPSSettlement.aspx.cs" Inherits="Admin_AEPSSettlement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Role Details</div>
                        <div class="panel-body">

                            <div class="form-group">
                                <label for="txtAccountNo">StartTime : </label>
                                <asp:TextBox runat="server" ID="txtStartTime" ToolTip="Enter StartTime" class="form-control ValiDationR"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="txtAccountNo">End Time : </label>
                                <asp:TextBox runat="server" ID="txtEndTime" ToolTip="Enter End Time" class="form-control ValiDationR"></asp:TextBox>
                            </div>

                            <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary" />
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

