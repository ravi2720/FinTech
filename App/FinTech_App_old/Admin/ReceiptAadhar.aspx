<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ReceiptAadhar.aspx.cs" Inherits="Admin_ReceiptAadhar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="content-wrapper">
        <section class="content">
            <div class="container">
                <div class="row">
                    <div class="col-md-12" style="background-color: white; padding-bottom: 30px" align="center">
                        <asp:Panel ID="Panel1" runat="server">
                           
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                        <ContentTemplate>
                                            
                                                <div class="modal-header" style="text-align: center">
                                                    <div class="col-md-6">
                                                        <img src='<%=  Application["URL"].ToString() %>' height="50" />

                                                    </div>
                                                    <div class="col-md-6">
                                                        <img src="../img/aadharpay.png" height="50" />
                                                    </div>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="row">
                                                        <div class="col-md-12 center-block" style="text-align: center">
                                                            <img runat="server" id="imgSet" height="50" /><br />
                                                            <a style="color: blue;" runat="server" id="Message"></a>
                                                        </div>

                                                        <div class="col-md-12">
                                                            <asp:Repeater runat="server" ID="repSummarry">
                                                                <ItemTemplate>

                                                                    <table class="table table-responsive">
                                                                        <tr>
                                                                            <td><strong>BankName</strong></td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="lblBankName" Text='<%# Eval("bankname") %>'> </asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><strong>Aadhar No</strong></td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="Label3" Text='<%# Eval("adhaarnumber") %>'> </asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><strong>Customer Mobile</strong></td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="CusMobile" Text='<%# Eval("mobilenumber") %>'> </asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><strong>BC Code</strong></td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="lblBcCode" Text='<%# Eval("LoginID") %>'> </asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><strong>BC Name</strong></td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="BCName" Text='<%# Eval("Name") %>'></asp:Label>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><strong>Amount</strong></td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="Label2"  Text='<%# Eval("Amount") %>'></asp:Label>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><strong>Balance</strong></td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="Label1" Text="xxxxx"></asp:Label>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><strong>TransactionID</strong></td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="lblTransID" Text='<%# Eval("referenceno") %>'></asp:Label>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><strong>RRN</strong></td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="lblBankNumber" Text='<%# Eval("rrn") %>'> </asp:Label>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td><strong>Date</strong></td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="lblDate" Text='<%# Eval("Adddate") %>'> </asp:Label>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><strong>Remark</strong></td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="lblMark" Text='<%# Eval("message") %>'> </asp:Label>

                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    Powered By <img src="../img/powerdby.png" style="width:200px;"/>
                                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                    <asp:Button ID="Button1" runat="server" Text="PRINT INVOICE" CssClass="btn btn-info"
                                                        CausesValidation="false" OnClientClick="printDiv('ErrorMessage')" />
                                                </div>
                                            
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                               


                        </asp:Panel>
                       
                    </div>
                </div>
            </div>
        </section>
    </div>
    <script type="text/javascript">
   function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }

    </script>
</asp:Content>

