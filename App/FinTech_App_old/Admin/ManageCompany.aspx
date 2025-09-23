<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ManageCompany.aspx.cs" Inherits="Admin_ManageMenu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Manage Company</div>
                        <div class="card-body">
                            <asp:HiddenField ID="hdnID" runat="server" Value="0" />
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">Member : </label>
                                        <asp:DropDownList ID="ddlMember" class="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Company Name</label>
                                        <asp:TextBox ID="txtCompanyName" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtCompanyName" Font-Bold="True" ForeColor="Red" ErrorMessage="*" ValidationGroup="addmenu" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Logo</label>
                                        <asp:FileUpload ID="fuLogo" CssClass="form-control" runat="server" />
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Signature</label>
                                        <asp:FileUpload ID="filesignature" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Favicon </label>
                                        <asp:FileUpload ID="fuicon" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Owner Name</label>
                                        <asp:TextBox ID="txtOwnerName" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtOwnerName" Font-Bold="True" ForeColor="Red" ErrorMessage="*" ValidationGroup="addmenu" />
                                    </div>
                                </div>

                            </div>


                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Email</label>
                                        <asp:TextBox ID="txtEmail" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                            ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            Display="Dynamic" ErrorMessage="Invalid email address" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" Font-Bold="True" ForeColor="Red" ErrorMessage="*" ValidationGroup="addmenu" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Alternate Email</label>
                                        <asp:TextBox ID="txtAlternateEmail" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAlternateEmail"
                                            ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            Display="Dynamic" ErrorMessage="Invalid email address" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Mobile</label>
                                        <asp:TextBox ID="txtMobile" class="form-control m-t-xxs" MaxLength="15" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtMobile" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMobile" Font-Bold="True" ForeColor="Red" ErrorMessage="*" ValidationGroup="addmenu" />
                                    </div>
                                </div>

                            </div>


                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Alternate Mobile</label>
                                        <asp:TextBox ID="txtAlternateMobile" class="form-control m-t-xxs" MaxLength="15" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtAlternateMobile" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Website URL</label>
                                        <asp:TextBox ID="txtWebsiteURL" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Android URL</label>
                                        <asp:TextBox ID="txtAndroidURL" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Address</label>
                                        <asp:TextBox ID="txtAddress" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">&copy;Copyright(Company Name)</label>
                                        <asp:TextBox ID="txtCopyright" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">&copy;FaceBook</label>
                                        <asp:TextBox ID="txtFacebook" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>


                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">&copy;Whatsapp</label>
                                        <asp:TextBox ID="txtwhatsapp" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">&copy;Instagram</label>
                                        <asp:TextBox ID="txtinstagram" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">&copy;Twitter</label>
                                        <asp:TextBox ID="txttwitter" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">&copy;Youtube</label>
                                        <asp:TextBox ID="txtYoutube" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">&copy;Bank Name</label>
                                        <asp:TextBox ID="txtBankName" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">&copy;A/C Name</label>
                                        <asp:TextBox ID="txtACName" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">&copy;A/C Type</label>
                                        <asp:TextBox ID="txtAcType" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">&copy;A/C Number</label>
                                        <asp:TextBox ID="txtACNumber" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">&copy;IFSC</label>
                                        <asp:TextBox ID="txtIFSCCode" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">&copy;MICRCODE</label>
                                        <asp:TextBox ID="txtMicrCode" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">&copy;Profile Amount</label>
                                        <asp:TextBox ID="txtProfileAmount" class="form-control m-t-xxs" Text="0" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">&copy;Header Color</label>
                                        <input type="color" value="#e0ffee" runat="server" id="HeadercolorPicker" />
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-2">
                                        <label for="exampleInputEmail1">&copy;Body Color</label>
                                        <input type="color" value="#e0ffee" runat="server" id="BodycolorPicker" />
                                    </div>
                                    <div class="col-md-2">
                                        <label for="exampleInputEmail1">&copy;Left Color</label>
                                        <input type="color" value="#e0ffee" runat="server" id="LeftcolorPicker" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary mt-2" ValidationGroup="addmenu" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button runat="server" ID="btnDone" Visible="false" CssClass="btn btn-warning center-block mt-2" Style="text-align: center;" Text="Done" OnClick="btnDone_Click" />
                                    </div>
                                </div>
                            </div>

                            <asp:Label ID="lblMessage" runat="server" CssClass="center-block" Style="text-align: center; font-weight: 600" ForeColor="Red" Text=""></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
            </asp:UpdatePanel>

            <hr />
            <div class="container-fluid">
                <asp:UpdatePanel ID="updt1" runat="server">
                    <ContentTemplate>
                        <div class="panel panel-primary">
                            <div class="panel-heading">Company List</div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-content collapse show">
                                                <div class="widgets-container">
                                                    <div>
                                                        <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                            <thead>
                                                                <tr>
                                                                    <th>Action</th>
                                                                    <th>Name</th>
                                                                    <th>Owner Name</th>
                                                                    <th>Email</th>
                                                                    <th>Mobile</th>
                                                                    <th>WebsiteURL</th>
                                                                    <th>AndroidURL</th>
                                                                    <th>Address</th>
                                                                    <th>Logo</th>
                                                                    <th>Add Date</th>
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th>Action</th>
                                                                    <th>Name</th>
                                                                    <th>Owner Name</th>
                                                                    <th>Email</th>
                                                                    <th>Mobile</th>
                                                                    <th>WebsiteURL</th>
                                                                    <th>AndroidURL</th>
                                                                    <th>Address</th>
                                                                    <th>Logo</th>
                                                                    <th>Add Date</th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody>
                                                                <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <tr class="row1">
                                                                            <td>
                                                                                <asp:Button runat="server" ID="imgDelete" CssClass='<%# Convert.ToInt16(Eval("IsActive")) == 1 ? "btn btn-success" : "btn btn-danger" %>' Text='<%# Convert.ToInt16(Eval("IsActive")) == 1 ? "Active" : "Deactive" %>' OnClientClick="javascript:return confirm('Are you sure you want to Active/DeActive this Role?');" CommandName="Active" CommandArgument='<%# Eval("ID") %>' ToolTip="Activate/Deactivate" />
                                                                                <asp:Button runat="server" ID="imgEdit" CssClass="btn btn-info" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ToolTip="Edit" Text="Edit" />
                                                                            </td>
                                                                            <td><span><%# Eval("Name") %></span></td>
                                                                            <td><span><%# Eval("OwnerName") %></span></td>
                                                                            <td><span><%# Eval("Email") %></span></td>
                                                                            <td><span><%# Eval("Mobile") %></span></td>
                                                                            <td><span><%# Eval("WebsiteURL") %></span></td>
                                                                            <td><span><%# Eval("AndroidURL") %></span></td>
                                                                            <td><span><%# Eval("Address") %></span></td>
                                                                            <td><span>
                                                                                <img src='../images/Company/<%# Eval("logo") %>' width="75" /></span></td>
                                                                            <td><span><%# Eval("AddDate") %></span></td>
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
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </section>
    </div>
    <script>
        $(function () {
            Action(DataObject);
        });

        var DataObject = {
            ID: "example1",
            iDisplayLength: 15,
            bPaginate: true,
            bFilter: true,
            bInfo: true,
            bLengthChange: true,
            searching: true
        };
        function LoadData() {

            Action(DataObject);
        }

        var req = Sys.WebForms.PageRequestManager.getInstance();
        req.add_beginRequest(function () {

        });
        req.add_endRequest(function () {
            LoadData();
        });
        function showModal() {
            $('#myModal').modal();
        }


    </script>
</asp:Content>

