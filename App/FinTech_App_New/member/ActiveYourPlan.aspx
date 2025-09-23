<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="ActiveYourPlan.aspx.cs" Inherits="Member_ActiveYourPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <h4 class="card-title mt-4">Update Your Member Ship</h4>
        <asp:UpdatePanel runat="server" ID="updd">
            <ContentTemplate>
                <!-- row -->
                <div class="row">

                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                        <ItemTemplate>
                            <div class="col-xs-6 col-sm-6 col-lg-6 col-md-6 col-xl-3">
                                <div class="panel price panel-color">
                                    <div class="panel-heading bg-primary p-0 text-center">
                                        <h3><%# Eval("Name") %></h3>
                                    </div>
                                    <div class="panel-body text-center">
                                        <p class="lead"><strong>₹<%# Eval("Amount") %></strong> </p>
                                    </div>
                                    <ul class="list-group list-group-flush text-center">
                                        <li class="list-group-item"><%# Eval("Description") %></li>
                                    </ul>
                                    <div class="panel-footer text-center">
                                        <asp:Button runat="server" ID="btnBugAccount" OnClientClick="return confirm('Do You Wamt to purchase Plan')" Enabled='<%# (Eval("PurchaseOrnot").ToString()=="1"?false:true) %>' CssClass="btn btn-danger" Text='<%# (Eval("PurchaseOrnot").ToString()=="1"?"Actived":"Buy Plan") %>' CommandName="Plan" CommandArgument='<%# Eval("ID") %>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>


                    <!-- COL-END -->
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- /row -->
    </div>
</asp:Content>

