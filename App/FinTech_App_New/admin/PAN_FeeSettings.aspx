<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="PAN_FeeSettings.aspx.cs" Inherits="Admin_PAN_FeeSettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updatepanel">
                <ContentTemplate>
                    <div class="row">
                        <div class="box">
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <span style="color: red">*</span> New Pancard Fee
                                         <asp:TextBox ID="txtNewPanAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_txtNewPanAmount" runat="server" ErrorMessage="Please Enter New Pancard Fee"
                                                ForeColor="Red" ValidationGroup="rv" ControlToValidate="txtNewPanAmount" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                ValidChars="0123456789." TargetControlID="txtNewPanAmount">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group">
                                            <span style="color: red">*</span> Existing PAN Correction Fee
                                        <asp:TextBox ID="txtCorrectPanAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_txtCorrectPanAmount" runat="server" ErrorMessage="Please Enter Existing PAN Correction Fee"
                                                ForeColor="Red" ValidationGroup="rv" ControlToValidate="txtCorrectPanAmount" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                ValidChars="0123456789." TargetControlID="txtCorrectPanAmount">
                                            </cc1:FilteredTextBoxExtender>

                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="rv" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="rv" runat="server" />
                                            <asp:HiddenField ID="hddExistingID" runat="server" Value="0" />
                                        </div>
                                    </div>
                                </div>
                            </div>
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

