<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" EnableViewStateMAC="true" AutoEventWireup="true" CodeFile="BBPS.aspx.cs" Inherits="Member_BBPS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="p-4">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>

<%--                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-7">
                                <div class="card-header">
                                    <h4><%= Request.QueryString["SType"].ToString() %> Recharges</h4>
                                </div>
                                <div class="card card-body">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-col">
                                                <label for="operator">Select Operator</label><p class="hide"></p>
                                                <asp:DropDownList runat="server" ID="ddlOperatorELECTRICITY" AutoPostBack="true" OnSelectedIndexChanged="ddlOperatorELECTRICITY_SelectedIndexChanged" CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Repeater runat="server" ID="rptData">
                                            <ItemTemplate>
                                                <div class="col-12 col-lg-6 mt-2">
                                                    <div class="input-group mb-3">
                                                        <asp:TextBox runat="server" ID="txtVal" CssClass="form-control" MinLength='<%# Eval("FieldMinLen") %>' MaxLength='<%# Eval("FieldMaxLen") %>' placeholder='<%# Eval("Labels") %>'></asp:TextBox>
                                                        <span class="input-group-text"><%# Eval("Labels") %></span>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>


                                        <div class="col-12 col-lg-6 mt-2">
                                            <div class="input-group mb-3">
                                                <span class="input-group-text" id="BillPayAmount">₹</span>
                                                <asp:TextBox runat="server" class="form-control" ID="txtAmountELECTRICITY" placeholder="Amount" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" MaxLength="4"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 mt-2">

                                            <asp:Button runat="server" ID="btnBillFetch" OnClick="btnBillFetch_Click" name="btnRecharge" class="btn btn-primary" Text="Bill Fetch" />

                                            <asp:Button runat="server" ID="btnElePay" OnClick="btnElePay_Click" name="btnRecharge" class="btn btn-primary" Text="Pay Now" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="card-header">
                                    <h2>Bill Details</h2>
                                </div>
                                <div class="card card-body">
                                    <div class="row">
                                        <div class="input-group mb-3">
                                            <asp:Label runat="server" ID="lblcustomername" CssClass="form-control"></asp:Label>
                                            <span class="input-group-text">CustomerName</span>
                                        </div>

                                        <div class="input-group mb-3">
                                            <asp:Label runat="server" ID="billdate" CssClass="form-control"></asp:Label>
                                            <span class="input-group-text">Billdate</span>
                                        </div>
                                        <div class="input-group mb-3">
                                            <asp:Label runat="server" ID="duedate" CssClass="form-control"></asp:Label>
                                            <span class="input-group-text">Duedate</span>
                                        </div>

                                        <div class="input-group mb-3">
                                            <span class="input-group-text" id="basic-addon1">₹</span>
                                            <asp:Label runat="server" ID="Billamount" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>--%>

                    
           <div class="main-content-body">
            <div class="row row-sm">
                <div class="col-lg-8">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title"><%= Request.QueryString["SType"].ToString() %> Recharges
                                <a href="#collapseExample" class="font-weight-bold" aria-controls="collapseExample" aria-expanded="false" data-bs-toggle="collapse" role="button">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" class="bi bi-filter" viewBox="0 0 16 16">
                                        <path d="M6 10.5a.5.5 0 0 1 .5-.5h3a.5.5 0 0 1 0 1h-3a.5.5 0 0 1-.5-.5zm-2-3a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7a.5.5 0 0 1-.5-.5zm-2-3a.5.5 0 0 1 .5-.5h11a.5.5 0 0 1 0 1h-11a.5.5 0 0 1-.5-.5z" />
                                    </svg>
                                </a>
                            </h3>
                        </div>
                        <div class="card-body">
                            <asp:UpdatePanel runat="server" ID="uprecharge">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-12 col-lg-4">
                                          
                                            <div class="form-group">
                                                <label for="operator">Select Operator</label><p class="hide"></p>                                               
                                                <asp:DropDownList runat="server" ID="ddlOperatorELECTRICITY" AutoPostBack="true" OnSelectedIndexChanged="ddlOperatorELECTRICITY_SelectedIndexChanged" CssClass="form-control">
                                                </asp:DropDownList>

                                            </div>


                                            <div class="form-group">
                                                  <asp:Repeater runat="server" ID="rptData">
                                            <ItemTemplate>
                                                <div class="col-12 col-lg-6 mt-2">
                                                    <div class="input-group mb-3">
                                                        <asp:TextBox runat="server" ID="txtVal" CssClass="form-control" MinLength='<%# Eval("FieldMinLen") %>' MaxLength='<%# Eval("FieldMaxLen") %>' placeholder='<%# Eval("Labels") %>'></asp:TextBox>
                                                        <span class="input-group-text"><%# Eval("Labels") %></span>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                            </div>




                                          <div class="input-group mb-3">
                                                <span class="input-group-text" id="BillPayAmount">₹</span>
                                                 <asp:TextBox runat="server" class="form-control" Text="0" ID="txtAmountELECTRICITY" placeholder="Amount" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" MaxLength="4"></asp:TextBox>

                                                </div>
                                            
                                   

                                            




                                            <div class="form-group">                                                                                            
                                            <asp:Button runat="server" ID="btnBillFetch" OnClick="btnBillFetch_Click" name="btnRecharge" class="btn btn-primary" Text="Bill Fetch" />
                                            <asp:Button runat="server" ID="btnElePay" OnClick="btnElePay_Click" name="btnRecharge" class="btn btn-primary" Text="Pay Now" />
                                            
                                            </div>




                                        </div>
                                        <div class="col-12 col-lg-8">
                                            <p>
                                            </p>
                                        </div>
                                    </div>


                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                </div>       
                            <div class="col-lg-4">
                                <div class="card-header">                                  
                                    <h3 class="card-title">Bill Details</h3>
                                </div>
                                    <div class="card card-body">
                                    <div class="row">
                                        <div class="input-group mb-3">
                                            <asp:Label runat="server" ID="lblcustomername" CssClass="form-control"></asp:Label>
                                            <span class="input-group-text">CustomerName</span>
                                        </div>

                                        <div class="input-group mb-3">
                                            <asp:Label runat="server" ID="billdate" CssClass="form-control"></asp:Label>
                                            <span class="input-group-text">Billdate</span>
                                        </div>
                                        <div class="input-group mb-3">
                                            <asp:Label runat="server" ID="duedate" CssClass="form-control"></asp:Label>
                                            <span class="input-group-text">Duedate</span>
                                        </div>

                                        <div class="input-group mb-3">
                                            <span class="input-group-text" id="basic-addon1">₹</span>
                                            <asp:Label runat="server" ID="Billamount" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>



            </div>

        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>

</asp:Content>

