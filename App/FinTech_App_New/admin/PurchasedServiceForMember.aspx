<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="PurchasedServiceForMember.aspx.cs" Inherits="Admin_PurchasedServiceForMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
        .card {
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
            transition: 0.3s;
            padding: 5px;
        }

            .card:hover {
                box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2);
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Active Service</div>

                        <div class="panel-body">
                            <div class="row">
                                <asp:DropDownList runat="server" ID="ddlMember" AutoPostBack="true" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                            <asp:Repeater runat="server" ID="rptadata" OnItemCommand="rptadata_ItemCommand">
                                <ItemTemplate>
                                    <div class="col-md-3">
                                        <div class="card">                                            
                                            <img src='<%# "./images/Service/"+Eval("image").ToString() %>' height="200" style="width: 100%" class="card-img-top" alt="...">
                                            <div class="card-body">
                                                <h5 class="card-title"><strong><%# Eval("Name") %></strong></h5>
                                                <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                                                <span>
                                                    <asp:HiddenField runat="server" ID="hdnActiveOrNot" Value='<%# Eval("ActiveOrNot") %>' />
                                                    <asp:HiddenField runat="server" ID="hdnServiceID" Value='<%# Eval("ID") %>' />
                                                    <a href="#" class="btn btn-success" runat="server" visible='<%# Convert.ToBoolean(Eval("ActiveOrNot")) %>'>Active</a>
                                                    <asp:Button runat="server" CommandName="Purchase" CommandArgument='<%# Eval("ID") %>' ID="btnPurchaseService" OnClientClick="return confirm('Do you want to Purchase this service')" Visible='<%# !Convert.ToBoolean(Eval("ActiveOrNot")) %>' Text="Purchase" CssClass="btn btn-danger" />
                                                </span><span class="btn btn-danger pull-right"><%# Eval("Price") %></span>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

