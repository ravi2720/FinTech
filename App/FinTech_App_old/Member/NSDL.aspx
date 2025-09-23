<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="NSDL.aspx.cs" Inherits="Member_NSDL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <div class="main-container container-fluid">
        <div class="main-content-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="">
                                <div class="card-header">
                                    <h4>NSDL Pan</h4>
                                </div>
                                <div class="card card-body">
                                    <div class="row">
                                        <asp:MultiView runat="server" ID="mult" ActiveViewIndex="0">
                                            <asp:View runat="server" ID="view">
                                                <div class="col-md-3">
                                                    <label>Title *</label>
                                                    <asp:DropDownList runat="server" ID="dllTitle" CssClass="form-control">
                                                        <asp:ListItem Text="Select Title" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Mr/Shri" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Mrs, Shrimati" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>First Name</label>
                                                    <asp:TextBox runat="server" ID="txtFisrtName" Text="" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>Middle Name</label>
                                                    <asp:TextBox runat="server" ID="txtMName" Text="" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>Last Name *</label>
                                                    <asp:TextBox runat="server" ID="txtLastName" Text="" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>Select Pan</label>
                                                    <asp:RadioButtonList runat="server" CssClass="form-control" ID="rdolist" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Physical Pan" Selected="True" Value="P"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>Mobile *</label>
                                                    <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>EmailID *</label>
                                                    <asp:TextBox runat="server" ID="txtEmailID" Text="" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3 mt-4">
                                                    <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-danger" />
                                                </div>
                                            </asp:View>
                                            <asp:View runat="server" ID="view1">
                                                <form action="https://api.paysprint.in/api/v1/service/pan/V2/validateurl">
                                                    <div class="col-md-12">
                                                        <textarea name="encdata" style="display: none">BY5au57KXTLPCOzKohpPjQBCAoV3cBAs97I+KxQXSsGvQnzMdDnZ+xCzWCqu5g4oObe7mxdrGd5dC49vZT2a8G1cNAwGDG\/QkcZnTu0OlPlagXHYiuGeM54yx667DVG8vNa5XceGMfhrHyLmvYTZBJJp4jbl+Dud7vtvRtjelwi6zbHTy\/\/5KUEYgEj\/cYJMX4+WeVaBc3j3tddjxZ91\/9LgssWeio03GFGS6tjQwk53+s4b95TevpVWyF\/uo5bp71KqWewVjpf2xSr0DJ5h1OKNBUfryrlYsBoUkhSCAH860zEz53Ku84jdWvudE1bMkE3BDe0gsm90A45eRtb\/Q9WIWbFvZWlwnczUzRBkOIA=</textarea>
                                                        <input type="button" onclick="Action()" value="Submit" />
                                                    </div>
                                                </form>



                                            </asp:View>
                                        </asp:MultiView>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="form-group">
                                            <div class="alert alert-heading alert-danger">
                                                Note Guide Line
                                                <asp:Label CssClass="btn btn-danger" runat="server" ID="lblMessage"></asp:Label>
                                            </div>
                                            <div class="alert alert-danger">
                                                <ul>
                                                    <li>(a)	Applicant will fill PAN Change Request Form online and submit the form.</li>
                                                    <li>(b)	If the data submitted fails in any format level validation, a response indicating the error(s) will be displayed on the screen.</li>
                                                    <li>(c)	The applicant shall rectify the error(s) and re-submit the form.</li>
                                                    <li>(d)	If there are no format level error(s) a confirmation screen with data filled by the applicant will be displayed.</li>
                                                    <li>(e)	If the applicant requires any amendment to this data, it can choose the edit option, else it shall choose the confirm option.</li>
                                                    <li>(f)	For Changes or Correction in PAN data, fill all mandatory fields (marked with *) of the Form and select the corresponding box on left margin of appropriate field where correction is required.</li>
                                                    <li>(g)	If the application is for re-issuance of a PAN card without any changes in PAN related data of the applicant, fill all fields in the Form but do not select any box on left margin.</li>
                                                    <li>(h)	In case of either a request for Change or Correction in PAN data or request for re-issuance of a PAN Card without any changes in PAN data, the address for communication will be updated in the ITD database using address for communication provided in the application.</li>
                                                    <li>(i)	For Cancellation of PAN, fill all mandatory fields in the Form, enter PAN to be cancelled in Item No.11 of the Form and select the check box on left margin. PAN to be cancelled should not be same as PAN (the one currently used) mentioned at the top of the Form.</li>
                                                    <li>(j)	On confirmation, an acknowledgement will be displayed. The acknowledgement will contain a unique 15-digit acknowledgement number.</li>
                                                    <li>(k)	The applicant is requested to save this acknowledgement.</li>
                                                    <li>(l)	This facility can be used by PAN applicants having a valid Digital Signature Certificate (DSC) issued to them by authorized Certifying Authority (CA) in India.</li>
                                                    <li>(m)	Only valid class II or III DSC will be accepted.</li>

                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField runat="server" ID="hdnVal" ClientIDMode="Static" />
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

        function Action() {

            var settings = {
                "url": "https://api.paysprint.in/api/v1/service/pan/V2/validateurl",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/x-www-form-urlencoded",
                },
                "data": {
                    "encdata": $("#hdnVal").val()
                }
            };
            $.ajax(settings).done(function (response) {
                var newWindow = window.open();
                newWindow.document.write(response);
            });


        }
    </script>
</asp:Content>

