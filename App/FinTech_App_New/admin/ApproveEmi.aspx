<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ApproveEmi.aspx.cs" Inherits="Admin_ApproveEmi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-body bootstrap-select-1">

                                    <h4 class="mt-0 header-title" style="color: blue;"><b>EMI Approvel</b></h4>
                                    <br />
                                    <div class="row">
                                        <table class="table table-hover mb-0">
                                            <tbody>
                                                <tr>
                                                    <th scope="row">From Date</th>
                                                    <td>
                                                        <asp:TextBox ID="txtfromdate" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                    <th scope="row">To Date</th>
                                                    <td>
                                                        <asp:TextBox ID="txttodate" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                    <th scope="row">Application No</th>
                                                    <td>
                                                        <asp:TextBox ID="txtapplicationno" CssClass="form-control" runat="server"></asp:TextBox></td>

                                                </tr>
                                                <tr>
                                                    <th scope="row">Loan Account No</th>
                                                    <td>
                                                        <asp:TextBox ID="txtloanaccno" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                    <th scope="row">Pay Mode</th>
                                                    <td>
                                                        <asp:DropDownList ID="ddlpaymode" CssClass="form-control" runat="server"></asp:DropDownList></td>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-primary" Text="Search>>" /></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- end col -->
                    </div>
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="mt-0 header-title"><b></b></h4>
                                    <div class="table-responsive">
                                        <table class="table table-hover mb-0">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2">Total Records:</td>
                                                    <td>
                                                        <asp:Label ID="lblrecords" runat="server" Text=""></asp:Label></td>
                                                    <td colspan="11"></td>
                                                </tr>
                                                <tr>
                                                    <th>Sr No</th>
                                                    <th>Amount</th>
                                                    <th>Pay Mode</th>
                                                    <th>Paid Date	</th>
                                                    <th>Name</th>
                                                    <th>Loan Account no</th>
                                                    <th>Receipt No.</th>
                                                    <th>Action</th>
                                                </tr>

                                                <asp:Repeater ID="rptdata" runat="server" OnItemCommand="rptdata_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%#Container.ItemIndex+1%>.</td>
                                                            <td><%#Eval("Amount") %></td>
                                                            <td><%#Eval("Pay_mode") %></td>
                                                            <td><%#Eval("Paid_Date") %></td>
                                                            <td><%#Eval("Name") %></td>
                                                            <td><%#Eval("LoanAccountNo") %></td>
                                                            <td><%#Eval("receipt_no") %></td>
                                                            <td>
                                                                <asp:Button ID="btnapprove" CommandName="Approve" CommandArgument='<%#Eval("transaction_id_temp") %>' runat="server" CssClass="btn btn-primary" Text="Approve" />
                                                                <asp:Button ID="btnreject" CommandName="Reject" CommandArgument='<%#Eval("transaction_id_temp") %>' runat="server" CssClass="btn btn-primary" Text="Reject" />
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
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

