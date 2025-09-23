<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="invoice.aspx.cs" Inherits="Member_invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-container container-fluid">
        <div class="main-content-body">
            <!-- container -->
            <div class="main-container container-fluid">
                <!-- row -->
                <div class="row row-sm">
                    <div class="col-md-12 col-xl-4">
                        <div class=" main-content-body-invoice">
                            <div class="card card-invoice">
                                <div class="card-header ps-3 pe-3 pt-3 pb-0">
                                    <h2 class="card-title">My Invoices</h2>
                                </div>
                                <div class="p-0">
                                    <div class="main-invoice-list" id="mainInvoiceList">
                                        <div class="media">
                                            <div class="media-icon">
                                                <i class="far fa-file-alt"></i>
                                            </div>
                                            <div class="media-body">
                                                <h6><span>Invoice002300</span> <span>$25</span></h6>
                                                <div>
                                                    <p><span>Date:</span> Oct 25</p>
                                                    <p><span>Product:</span> 921021</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="media selected">
                                            <div class="media-icon">
                                                <i class="far fa-file-alt"></i>
                                            </div>
                                            <div class="media-body">
                                                <h6><span>Invoice002299</span> <span>$16</span></h6>
                                                <div>
                                                    <p><span>Date:</span> Oct 25</p>
                                                    <p><span>Product:</span> 435423</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="media">
                                            <div class="media-icon">
                                                <i class="far fa-file-alt"></i>
                                            </div>
                                            <div class="media-body">
                                                <h6><span>Invoice002300</span> <span>$32</span></h6>
                                                <div>
                                                    <p><span>Date:</span> Oct 25</p>
                                                    <p><span>Product:</span> 921021</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="media">
                                            <div class="media-icon">
                                                <i class="far fa-file-alt"></i>
                                            </div>
                                            <div class="media-body">
                                                <h6><span>Invoice002300</span> <span>$18</span></h6>
                                                <div>
                                                    <p><span>Date:</span> Oct 25</p>
                                                    <p><span>Product:</span> 921021</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="media">
                                            <div class="media-icon">
                                                <i class="far fa-file-alt"></i>
                                            </div>
                                            <div class="media-body">
                                                <h6><span>Invoice002300</span> <span>$25</span></h6>
                                                <div>
                                                    <p><span>Date:</span> Oct 25</p>
                                                    <p><span>Product:</span> 921021</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="media">
                                            <div class="media-icon">
                                                <i class="far fa-file-alt"></i>
                                            </div>
                                            <div class="media-body">
                                                <h6><span>Invoice002299</span> <span>$16</span></h6>
                                                <div>
                                                    <p><span>Date:</span> Oct 25</p>
                                                    <p><span>Product:</span> 435423</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="media">
                                            <div class="media-icon">
                                                <i class="far fa-file-alt"></i>
                                            </div>
                                            <div class="media-body">
                                                <h6><span>Invoice002300</span> <span>$32</span></h6>
                                                <div>
                                                    <p><span>Date:</span> Oct 25</p>
                                                    <p><span>Product:</span> 921021</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="media">
                                            <div class="media-icon">
                                                <i class="far fa-file-alt"></i>
                                            </div>
                                            <div class="media-body">
                                                <h6><span>Invoice002300</span> <span>$18</span></h6>
                                                <div>
                                                    <p><span>Date:</span> Oct 25</p>
                                                    <p><span>Product:</span> 921021</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="media">
                                            <div class="media-icon">
                                                <i class="far fa-file-alt"></i>
                                            </div>
                                            <div class="media-body">
                                                <h6><span>Invoice002300</span> <span>$25</span></h6>
                                                <div>
                                                    <p><span>Date:</span> Oct 25</p>
                                                    <p><span>Product:</span> 921021</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="media">
                                            <div class="media-icon">
                                                <i class="far fa-file-alt"></i>
                                            </div>
                                            <div class="media-body">
                                                <h6><span>Invoice002299</span> <span>$16</span></h6>
                                                <div>
                                                    <p><span>Date:</span> Oct 25</p>
                                                    <p><span>Product:</span> 435423</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="media">
                                            <div class="media-icon">
                                                <i class="far fa-file-alt"></i>
                                            </div>
                                            <div class="media-body">
                                                <h6><span>Invoice002300</span> <span>$32</span></h6>
                                                <div>
                                                    <p><span>Date:</span> Oct 25</p>
                                                    <p><span>Product:</span> 921021</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="media">
                                            <div class="media-icon">
                                                <i class="far fa-file-alt"></i>
                                            </div>
                                            <div class="media-body">
                                                <h6><span>Invoice002300</span> <span>$18</span></h6>
                                                <div>
                                                    <p><span>Date:</span> Oct 25</p>
                                                    <p><span>Product:</span> 921021</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- main-invoice-list -->
                    <div class="col-md-12 col-xl-8">
                        <asp:UpdatePanel runat="server" ID="updateRolePanel">
                            <ContentTemplate>
                                <div class=" main-content-body-invoice" id="content2">
                                    <div class="card card-invoice">
                                        <div class="card-body">
                                            <div class="invoice-header">
                                                <h1 class="invoice-title">Invoice</h1>

                                                <asp:Repeater runat="server" ID="rptdata">
                                                    <ItemTemplate>
                                                        <div class="billed-from">



                                                            <h6> <%# Eval("Name") %></h6>
                                                            <p>
                                                               <%# Eval("OwnerName") %><br>
                                                                Address :<%# Eval("Address") %><br>
                                                                Company Type:  <%# Eval("CompanyType") %><br>
                                                                Pan:  <%# Eval("Pan") %><br>
                                                                GST: <%# Eval("GSTNo") %><br>
                                                                CIN: <%# Eval("CIN") %><br>
                                                                State: <%# Eval("StateNAME") %>
                                                            </p>
                                                        </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                
                                                <asp:Repeater runat="server" ID="rptDataRecharge">
                                                    <ItemTemplate>
                                                        <!-- billed-from -->
                                                        </div><!-- invoice-header -->
                                                        <div class="row mg-t-20">
                                                            <div class="col-md">
                                                                <label class="tx-gray-600">Billed To</label>
                                                                <div class="billed-to">
                                                                    <h6><%# Eval("FirmName") %></h6>
                                                                    <p>
                                                                        <%# Eval("Address") %><br>
                                                                        Tel No:<%# Eval("Mobile") %><br>
                                                                        Email: <%# Eval("Email") %><br>
                                                                        GST/Pan: <%# Eval("GSTNO") %><br>
                                                                        State: <%# Eval("StateName") %>
                                                                    </p>
                                                                </div>
                                                            </div>
                                                            <div class="col-md">
                                                                <label class="tx-gray-600">Invoice Information</label>
                                                                <p class="invoice-info-row"><span>Invoice No</span> <span id="billNo"></span></p>
                                                                <p class="invoice-info-row"><span>Project ID</span> <span><%# Eval("LoginID") %></span></p>
                                                                <p class="invoice-info-row"><span>Issue Date:</span> <span>November 21, 2017</span></p>
                                                                <p class="invoice-info-row"><span>Due Date:</span> <span>November 30, 2017</span></p>
                                                            </div>
                                                        </div>
                                                        <div class="table-responsive mg-t-40">
                                                            <table class="table table-invoice border text-md-nowrap mb-0">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="wd-20p">Item</th>
                                                                        <th class="wd-40p">Hsn/Sac Code</th>
                                                                        <th class="tx-center">Basic Value</th>
                                                                        <th class="tx-right">GST(18%)</th>
                                                                        <th class="tx-right">Total</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>E-Recharge</td>
                                                                        <td class="tx-12"></td>
                                                                        <td class="tx-center"><%# Math.Round((Convert.ToDecimal(Eval("Recharge").ToString())/1.18M)) %></td>
                                                                        <td class="tx-right"><%# (Math.Round((Convert.ToDecimal(Eval("Recharge").ToString())/1.18M))*18)/100 %></td>
                                                                        <td class="tx-right"><%# Eval("Recharge") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Settlement</td>
                                                                         <td class="tx-12"></td>
                                                                        <td class="tx-center"><%# Convert.ToDecimal(Eval("DMTSurharge"))+Convert.ToDecimal(Eval("Payout"))+Convert.ToDecimal(Eval("UPITransfer")) %></td>
                                                                        <td class="tx-right"><%# (((Convert.ToDecimal(Eval("DMTSurharge"))+Convert.ToDecimal(Eval("Payout"))+Convert.ToDecimal(Eval("UPITransfer")))*18)/100) %></td>
                                                                        <td class="tx-right"><%# ((((Convert.ToDecimal(Eval("DMTSurharge"))+Convert.ToDecimal(Eval("Payout"))+Convert.ToDecimal(Eval("UPITransfer")))*18)/100)+(Convert.ToDecimal(Eval("DMTSurharge"))+Convert.ToDecimal(Eval("Payout"))+Convert.ToDecimal(Eval("UPITransfer")))) %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Pan Surcharge</td>
                                                                        <td class="tx-12"></td>
                                                                        <td class="tx-center"><%# Eval("PanCharge") %></td>
                                                                        <td class="tx-right"><%# ((Convert.ToDecimal(Eval("PanCharge").ToString())*18)/100) %></td>
                                                                        <td class="tx-right"><%# ((Convert.ToDecimal(Eval("PanCharge").ToString())*18)/100)+Convert.ToDecimal(Eval("PanCharge")) %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>AEPS OnBoard</td>
                                                                        <td class="tx-12"></td>
                                                                        <td class="tx-center"><%# Math.Round((Convert.ToDecimal(Eval("AEPSOnBoard").ToString())/1.18M)) %></td>
                                                                        <td class="tx-right"><%# (Math.Round((Convert.ToDecimal(Eval("AEPSOnBoard").ToString())/1.18M))*18)/100 %></td>
                                                                        <td class="tx-right"><%# Eval("AEPSOnBoard") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="valign-middle" colspan="2" rowspan="4">
                                                                            <div class="invoice-notes">
                                                                                <label class="main-content-label tx-13">Notes</label>
                                                                                <p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.</p>
                                                                            </div>
                                                                            <!-- invoice-notes -->
                                                                        </td>
                                                                        <td class="tx-right">Sub-Total</td>
                                                                        <td class="tx-right" colspan="2"><%# Eval("SubTotal") %></td>
                                                                    </tr>
                                                                    <tr style='<%# "display:"+(Eval("stateid").ToString()=="1657"?"": "none") %>'>
                                                                        <td class="tx-right">CGST (9%)</td>
                                                                        <td class="tx-right" colspan="2"><%# Convert.ToDecimal(Eval("SubTotal").ToString())*9/100 %></td>
                                                                    </tr>
                                                                    <tr style='<%# "display:"+(Eval("stateid").ToString()=="1657"?"": "none") %>'>

                                                                        <td class="tx-right">SGST (9%)</td>
                                                                        <td class="tx-right" colspan="2"><%# Convert.ToDecimal(Eval("SubTotal").ToString())*9/100 %></td>
                                                                    </tr>

                                                                     <tr '<%# "display:"+(Eval("stateid").ToString()!="1657"?"": "none") %>'>

                                                                        <td class="tx-right">IGST (18%)</td>
                                                                        <td class="tx-right" colspan="2"><%# Convert.ToDecimal(Eval("SubTotal").ToString())*18/100 %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="tx-right tx-uppercase tx-bold tx-inverse">Total Due</td>
                                                                        <td class="tx-right" colspan="2">
                                                                            <h4 class="tx-primary tx-bold"><%# (Convert.ToDecimal(Eval("SubTotal").ToString())*18/100)+Convert.ToDecimal(Eval("SubTotal").ToString()) %></h4>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>

                                                    </ItemTemplate>
                                                </asp:Repeater>


                                               <%-- <span id="mmbri" runat="server"></span>--%>
                                                <hr class="mg-b-40">
                                                <a class="btn btn-primary btn-block" href="">Pay Now</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- COL-END -->

                            </ContentTemplate>
                        </asp:UpdatePanel>


                    </div>
                    <!-- row closed -->
                </div>
            </div>
        </div>

        </div>
        <script>
        function createPDF() {

            $.ajax({
                type: "POST",
                url: "Invoice.aspx/GetInvoiceNumber",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ 'Auth': getUrlVars()["AuthID"], 'InvoiceMonth': getUrlVars()["Month"] }),
                success: function (data) {
                    if (data.d != "") {
                        $("#billNo").text(data.d);
                        var element = document.getElementById('content2');
                        html2pdf(element, {
                            margin: 1,
                            padding: 0,
                            filename: data.d+"("+$("#ContentPlaceHolder1_mmbri").text()+")",
                            image: { type: 'jpeg', quality: 1 },
                            html2canvas: { scale: 2, logging: true },
                            jsPDF: { unit: 'in', format: 'A2', orientation: 'P' },
                            class: createPDF
                        });
                    } else {
                        alert("Invoice Alreday Download");
                    }
                }
            });


        };

        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        </script>
</asp:Content>

