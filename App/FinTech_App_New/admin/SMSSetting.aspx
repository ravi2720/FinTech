<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="SMSSetting.aspx.cs" Inherits="Admin_SMSSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="app-content content ">
        <div class="content-overlay"></div>
        <div class="header-navbar-shadow"></div>
        <div class="content-wrapper container-xxl p-0" id="appbranch">
            <div class="content-body">
                <section id="ApiKeyPage">
                    <div class="card">
                        <div class="card-header pb-0">
                            <h4 class="card-title">Manage SMSSetting</h4>
                        </div>
                        <div class="card-body">
                            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label for="txtAccountNo">URL : </label>
                                                <asp:TextBox runat="server" ID="txtURL" ToolTip="Enter URL" class="form-control ValiDationR"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label for="txtBranchName">Sender Text : </label>
                                                <asp:TextBox runat="server" ID="txtSenderText" ToolTip="Enter Sender Text" class="form-control ValiDationR"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="txtBranchName">Sender Value : </label>
                                                <asp:TextBox runat="server" ID="txtSender" ToolTip="Enter Sender Value" class="form-control ValiDationR"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="txtBranchName">CountryText : </label>
                                                <asp:TextBox runat="server" ID="txtCountryText" ToolTip="Enter Country Text" class="form-control ValiDationR"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="txtBranchName">Country : </label>
                                                <asp:TextBox runat="server" ID="txtCountry" ToolTip="Enter Country Value" class="form-control ValiDationR"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="txtBranchName">Route Text : </label>
                                                <asp:TextBox runat="server" ID="txtRouteText" ToolTip="Enter route Text" class="form-control ValiDationR"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="txtBranchName">Route : </label>
                                                <asp:TextBox runat="server" ID="txtroute" ToolTip="Enter route" class="form-control ValiDationR"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="txtBranchName">Param1Text : </label>
                                                <asp:TextBox runat="server" ID="txtParam1Text" ToolTip="Enter Param1Text" class="form-control ValiDationR"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="txtBranchName">Param1Val : </label>
                                                <asp:TextBox runat="server" ID="txtParam1Val" ToolTip="Enter Param1Val" class="form-control ValiDationR"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="txtBranchName">Param2Text : </label>
                                                <asp:TextBox runat="server" ID="txtParam2Text" ToolTip="Enter Param2Text" class="form-control ValiDationR"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="txtBranchName">Param2Val : </label>
                                                <asp:TextBox runat="server" ID="txtParam2Val" ToolTip="Enter Param2Val" class="form-control ValiDationR"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="txtBranchName">Mobile : </label>
                                                <asp:TextBox runat="server" ID="txtParam3Text" ToolTip="Enter Param3Text" class="form-control ValiDationR"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="txtBranchName">Message : </label>
                                                <asp:TextBox runat="server" ID="txtParam3Val" ToolTip="Enter Param3Val" class="form-control ValiDationR"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="txtBranchName">DltText : </label>
                                                <asp:TextBox runat="server" ID="txtDltText" ToolTip="Enter DLT Text" class="form-control ValiDationR"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="chkActive">Active : </label>
                                                <asp:CheckBox runat="server" Checked="true" ID="chkActive" ToolTip="Active" class="form-control ValiDationR"></asp:CheckBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary me-1 mt-4 waves-effect waves-float waves-light" />
                                            </div>
                                        </div>

                                    </div>


                                    <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                        <thead>

                                            <tr>
                                                <th>S.No</th>
                                                <th>URL</th>
                                                <th>Sender Text</th>
                                                <th>Sender</th>
                                                <th>Country Text</th>
                                                <th>country</th>
                                                <th>Route Text</th>
                                                <th>route</th>
                                                <th>Param1Text</th>
                                                <th>Param1Val</th>
                                                <th>Param2Text</th>
                                                <th>Param2Val</th>
                                                <th>Mobile</th>
                                                <th>Message</th>
                                                <th>DLT Text</th>
                                                <th>Edit</th>
                                                <th>Add Date</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="repeater1" runat="server" OnItemCommand="repeater1_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex+1 %></td>
                                                        <td><%#Eval("URL") %></td>
                                                        <td><%#Eval("SenderText") %></td>
                                                        <td><%#Eval("sender") %></td>
                                                        <td><%#Eval("CountryText") %></td>
                                                        <td><%#Eval("country") %></td>
                                                        <td><%#Eval("RouteText") %></td>
                                                        <td><%#Eval("route") %></td>
                                                        <td><%#Eval("Param1Text") %></td>
                                                        <td><%#Eval("Param1Val") %></td>
                                                        <td><%#Eval("Param2Text") %></td>
                                                        <td><%#Eval("Param2Val") %></td>
                                                        <td><%#Eval("Param3Text") %></td>
                                                        <td><%#Eval("DltText") %></td>
                                                        <td><%#Eval("Param3Val") %></td>
                                                        <td>
                                                            <asp:Button runat="server" ID="btnEdit" Text="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-danger" />
                                                        </td>
                                                        <td><%#Eval("AddDate") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>

                                    <asp:HiddenField runat="server" ID="hdnRoleID" Value="0" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </div>
    <script>
        $(function () {
            Action(DataObject);
        });

        var DataObject = {
            ID: "example1",
            iDisplayLength: 5,
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

    </script>
</asp:Content>

