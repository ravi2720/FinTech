<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UPISendReceipt.aspx.cs" Inherits="UPISendReceipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="app_js/alertifyjs/css/alertify.min.css" rel="stylesheet" />
    <script src="app_js/alertifyjs/alertify.min.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css">
    <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="p-4" id="aeps">
            <div class="content-wrapper">
                <section class="content">
                    <div class="container">
                        <div class="card card-primary">
                            <div class="card-header">UPI Receipt</div>
                            <div class="card card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">

                                            <h3>
                                                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label><br />
                                                UPI - RECEIPT</h3>

                                            <img src="../img/watch.gif" height="100" runat="server" id="imgShow" visible="false" />
                                        </div>
                                        <asp:Repeater ID="rptTransactionDetails" runat="server">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <label>UPI ID</label>
                                                    <span class="form-control"><%# Eval("UPIID") %></span>
                                                </div>
                                                <div class="form-group">
                                                    <label>TransID</label>
                                                    <span class="form-control"><%# Eval("OrderID") %></span>
                                                </div>
                                                <div class="form-group">
                                                    <label>Reference Id</label>
                                                    <span class="form-control"><%# Eval("VendorID") %></span>
                                                </div>
                                                <div class="form-group">
                                                    <label>Amount</label>
                                                    <span class="form-control"><%# Eval("Amount") %></span>
                                                </div>
                                                <div class="form-group">
                                                    <label>Surcharge</label>
                                                    <span class="form-control"><%# Eval("Surcharge") %></span>

                                                </div>
                                                <div class="form-group">
                                                    <label>UTR</label>
                                                    <span class="form-control"><%# Eval("rrn") %></span>

                                                </div>

                                                <div class="form-group">
                                                    <label>Date</label>
                                                    <span class="form-control"><%# Eval("AddDate") %></span>

                                                </div>


                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            </section>
        </div>
        </div>
    </form>
</body>
</html>
