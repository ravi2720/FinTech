<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="faq.aspx.cs" Inherits="Member_faq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <div class="main-content-body">

            <!-- row -->
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <asp:Repeater runat="server" ID="RptFaq">
                            <ItemTemplate>
                                <div class="card-body">
                                    <h4 class="font-weight-semibold tx-15"><%# Eval("FaqSubject") %></h4>
                                    <p class="text-muted mb-0 tx-13"><%# Eval("Description") %></p>
                                </div>

                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <!-- row closed -->
        </div>
    </div>
</asp:Content>

