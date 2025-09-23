<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="UPITransfer.aspx.cs" Inherits="Member_UPITransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <div class="main-content-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                   
                    <div class="main-content-body">
                        <div class="row row-sm">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header">
                                        <h3 class="card-title">UPI Transfer
                                <a href="#collapseExample" class="font-weight-bold" aria-controls="collapseExample" aria-expanded="false" data-bs-toggle="collapse" role="button">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" class="bi bi-filter" viewBox="0 0 16 16">
                                        <path d="M6 10.5a.5.5 0 0 1 .5-.5h3a.5.5 0 0 1 0 1h-3a.5.5 0 0 1-.5-.5zm-2-3a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7a.5.5 0 0 1-.5-.5zm-2-3a.5.5 0 0 1 .5-.5h11a.5.5 0 0 1 0 1h-11a.5.5 0 0 1-.5-.5z" />
                                    </svg>
                                </a>
                                        </h3>
                                    </div>
                                    <div class="card-body">



                                        <div class="row">
                                            <div class="box">
                                                <div class="box-body">
                                                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                                        <asp:View ID="View1" runat="server">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblLoginMessage" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <div class="col-md-8">
                                                                                <strong>Enter Customer Mobile Number</strong>
                                                                                <asp:TextBox ID="txtUPI" runat="server" ClientIDMode="Static" CssClass="form-control ValCheckS" MaxLength="20"
                                                                                    AutoCompleteType="None"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                <asp:Button runat="server" ID="btnVerification" Text="Verify" OnClick="btnVerification_Click" CssClass="btn btn-success mt-4" />
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                    <div class="form-group">
                                                                        <strong>Name</strong>
                                                                        <asp:TextBox ID="txtUPIName" runat="server" ClientIDMode="Static" CssClass="form-control ValCheckS" MaxLength="20"
                                                                            AutoCompleteType="None"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <strong>Enter Amount</strong>
                                                                        <asp:TextBox ID="txtAmount" TextMode="Number" runat="server" ClientIDMode="Static" onkeypress="return isNumber(event)" CssClass="form-control ValCheckS" MaxLength="5"
                                                                            AutoCompleteType="None"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <strong>UPI Tag</strong>
                                                                        <div class="row cursor-pointer" style="padding: 20px; cursor: pointer">
                                                                            <div class="col-md-3" onclick="AssignUPI('@paytm')">
                                                                                <span>@paytm</span>
                                                                            </div>
                                                                            <div class="col-md-3" onclick="AssignUPI('@okicici')">
                                                                                <span>@okicici</span>
                                                                            </div>
                                                                            <div class="col-md-3" onclick="AssignUPI('@ybl')">
                                                                                <span>@ybl</span>
                                                                            </div>
                                                                            <div class="col-md-3" onclick="AssignUPI('@ybl')">
                                                                                <span>@okhdfcbank</span>
                                                                            </div>
                                                                            <div class="col-md-3" onclick="AssignUPI('@okaxis')">
                                                                                <span>@okaxis</span>
                                                                            </div>
                                                                            <div class="col-md-3" onclick="AssignUPI('@oksbi')">
                                                                                <span>@oksbi</span>
                                                                            </div>

                                                                            <div class="col-md-3" onclick="AssignUPI('@upi')">
                                                                                <span>@upi</span>
                                                                            </div>
                                                                            <div class="col-md-3" onclick="AssignUPI('@icici')">
                                                                                <span>@icici</span>
                                                                            </div>
                                                                            <div class="col-md-3" onclick="AssignUPI('@apl')">
                                                                                <span>@apl</span>
                                                                            </div>
                                                                            <div class="col-md-3" onclick="AssignUPI('@kotak')">
                                                                                <span>@kotak</span>
                                                                            </div>
                                                                            <div class="col-md-3" onclick="AssignUPI('@pingpay')">
                                                                                <span>@pingpay</span>
                                                                            </div>
                                                                            <div class="col-md-3" onclick="AssignUPI('@axisb')">
                                                                                <span>@axisb</span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <asp:Button ID="btnSearch" OnClientClick="return CheckValD('.ValCheckS')" runat="server" CssClass="btn btn-primary" Text="Submit"
                                                                            ValidationGroup="v" OnClick="btnSearch_Click" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <img src="./images/UPI.png" style="width: 100%" />
                                                                </div>
                                                            </div>
                                                        </asp:View>
                                                    </asp:MultiView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
    <script async="async">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function AssignUPI(Up) {
            var Doc = document.getElementById("txtUPI");
            if (Doc.value.trim() != "") {
                Doc.value = Doc.value.split('@')[0] + Up;
            }
        }
        function CheckValD(cl) {
            try {
                var flag = true;
                $(cl).each(function () {
                    debugger;
                    var lookName = (this.getAttribute("lookName") == null ? "" : this.getAttribute("lookName"));
                    if (this.value == "" || this.value == "Select District" || this.value == "Select State" || this.value == "Select TypeofShop" || this.value == "Select location" || this.value == "Select Population" || this.value == "Select Qualification") {
                        this.style.border = "1px solid red";
                        flag = false;
                    } else {
                        if (lookName == "PMobile") {
                            if (this.value.length < 10) {
                                this.style.border = "1px solid red";
                                this.focus();
                                flag = false;
                            }
                        } else if (lookName == "Email") {
                            if (checkEmail(this) == false) {
                                this.style.border = "1px solid red";
                                flag = false;
                            }
                        }
                        else {
                            this.style.border = "1px solid lightgrey";
                        }
                    }
                });
            } catch (err) {
            }
            return flag;
        }
    </script>
</asp:Content>

