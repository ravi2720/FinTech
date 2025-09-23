<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="SetMinimumBalance.aspx.cs" Inherits="Admin_SetMinimumBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Set balance Limit</div>
                        <div class="panel-body">

                            <div class="form-group">
                                <label for="txtMDMinBalance">MD Min Balance : </label>
                                <asp:TextBox runat="server" ID="txtMDMinBalance" Text="0" ToolTip="Enter MD Minimum Balance" class="form-control ValiDationP"></asp:TextBox>
                            </div>

                             <div class="form-group">
                                <label for="txtMDMinBalance">Partner Min Balance : </label>
                                <asp:TextBox runat="server" ID="txtPartner" Text="0" ToolTip="Enter Partner Minimum Balance" class="form-control ValiDationP"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="txtDistributorMinBalance">Distributor Min Balance : </label>
                                <asp:TextBox runat="server" ID="txtDistributorMinBalance" Text="0" ToolTip="Enter Distributor Minimum Balance" class="form-control ValiDationP"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="txtRetailerMinBalance">Retailer Min Balance : </label>
                                <asp:TextBox runat="server" ID="txtRetailerMinBalance" Text="0" ToolTip="Enter Reatiler Minimum Balance" class="form-control ValiDationP"></asp:TextBox>
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

