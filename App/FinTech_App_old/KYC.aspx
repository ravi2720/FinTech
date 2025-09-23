<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KYC.aspx.cs" Inherits="KYC" %>

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
    <style>
         #load {
            width: 100%;
            height: 100%;
            position: fixed;
            z-index: 9999;
            background: url("img/Loading.gif") no-repeat center center rgba(0,0,0,0.25);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
          <div id="load" style="visibility: hidden;"></div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="p-4" id="aeps">
            <asp:UpdatePanel runat="server" ID="updateAEPSBE">
                <ContentTemplate>
                    <div class="content-wrapper">
                        <section class="content">
                            <div class="container">
                                <div class="card card-primary">
                                    <div class="card-header">KYC Verification</div>
                                    <div class="card card-body">
                                        <asp:MultiView runat="server" ID="mul" ActiveViewIndex="0">
                                            <asp:View runat="server" ID="view1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Name</label>
                                                            <asp:Label runat="server" ID="lblName" CssClass="form-control"></asp:Label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label>Pan</label>
                                                            <asp:Label runat="server" ID="lblPan" CssClass="form-control"></asp:Label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label>Email</label>
                                                            <asp:Label runat="server" ID="lblEmail" CssClass="form-control"></asp:Label>
                                                        </div>

                                                        <div class="form-group">
                                                            <asp:Button runat="server" ID="btnProccess" CssClass="btn btn-danger mt-4" OnClick="btnProccess_Click" Text="Process" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:View>
                                            <asp:View runat="server" ID="view2">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label>OTP</label>
                                                        <asp:TextBox runat="server" ID="txtOTP" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Button runat="server" ID="btnVerify" CssClass="btn btn-danger mt-4" OnClick="btnpross_Click" Text="Verify" />
                                                    </div>
                                                </div>
                                            </asp:View>
                                            <asp:View runat="server" ID="view3">
                                                <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                    <ItemTemplate>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <label>Member Name</label>
                                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtName" ReadOnly="false" Text='<%# Eval("Name").ToString() %>'></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <label>Mobile</label>
                                                                <asp:TextBox runat="server" CssClass="form-control" ID="TextBox1" ReadOnly="false" Text='<%# Eval("Mobile").ToString() %>'></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <label>Address</label>
                                                                <asp:TextBox runat="server" CssClass="form-control" ID="TextBox2" ReadOnly="false" Text='<%# Eval("Address").ToString() %>'></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row mt-4">
                                                            <div class="col-md-4">
                                                                <img height="100" width="100" src='<%# "data:image/png;base64, "+Eval("ProfilePic").ToString() %>' alt="Red dot" />
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </asp:View>
                                        </asp:MultiView>

                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnHash" />
                    <asp:HiddenField runat="server" ID="hdnop" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
    <script>
 var req = Sys.WebForms.PageRequestManager.getInstance();
        req.add_beginRequest(function () {
            try {
                document.getElementById('load').style.visibility = "visible";
            } catch (err) {

            }
        });
        req.add_endRequest(function () {
            try {
                
                document.getElementById('load').style.visibility = "hidden";

            } catch (err) {
                document.getElementById('load').style.visibility = "hidden";
            }

        });
    </script>
</body>
</html>
