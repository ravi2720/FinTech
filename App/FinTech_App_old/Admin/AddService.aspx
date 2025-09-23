<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AddService.aspx.cs" Inherits="Admin_AddService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Service Details</div>
                        <div class="card-body">

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">ServiceName : </label>
                                        <asp:TextBox runat="server" ID="txtServiceName" ToolTip="Enter ServiceName" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">Service URL : </label>
                                        <asp:TextBox runat="server" ID="txtURL" ToolTip="Enter URL" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">Price : </label>
                                        <asp:TextBox runat="server" ID="txtPrice" ToolTip="Enter Price" Text="0" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">Image : </label>
                                        <asp:FileUpload runat="server" ID="flieUpload" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtBranchName">SectionType : </label>
                                        <asp:DropDownList runat="server" ID="ddlSectionType" ToolTip="Select SectionType" class="form-control ValiDationR">
                                            <asp:ListItem Text="Select SectionType" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Recharge" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="BBPS" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Banking" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Travel" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label for="chkActive">Active : </label>
                                        <asp:CheckBox runat="server" Checked="true" ID="chkActive" ToolTip="Active" class="form-control ValiDationR"></asp:CheckBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="chkActive">New : </label>
                                        <asp:CheckBox runat="server" ID="chkIsNew" ToolTip="New Service" class="form-control ValiDationR"></asp:CheckBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="chkActive">Comming Soon : </label>
                                        <asp:CheckBox runat="server" ID="chkSoon" ToolTip="Comming Soon" class="form-control ValiDationR"></asp:CheckBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="chkActive">OnOff Service : </label>
                                        <asp:CheckBox runat="server" ID="chkOnOff" Checked="true" ToolTip="Comming Soon" class="form-control ValiDationR"></asp:CheckBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">txtReason : </label>
                                        <asp:TextBox runat="server" ID="txtReason" ToolTip="Enter Reason" Text="0" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">OnTime : </label>
                                        <asp:TextBox runat="server" ID="txtOnTime" ToolTip="Enter OnTime" Text="0" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">OffTime : </label>
                                        <asp:TextBox runat="server" ID="txtOffTime" ToolTip="Enter OffTime" Text="0" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary m-2" />



                            <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                <thead>
                                    <tr>
                                        <th>Active/DeActive</th>
                                        <th>On/Off</th>
                                        <th>Edit</th>
                                        <th>Service Name</th>
                                        <th>Service URL</th>
                                        <th>SectionType</th>
                                        <th>Price</th>
                                        <th>Image</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="row1">
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgDelete" OnClientClick="javascript:return confirm('Are you sure you want to Active/DeActive this Role?');" CommandName="Active" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("IsActive"))==true ? ConstantsData.ActiveIcon:ConstantsData.DeActiveIcon)  %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="ImageButton1" OnClientClick="javascript:return confirm('Are you sure you want to On/Off this Service?');" CommandName="onoff" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("Onoff"))==true ? ConstantsData.ActiveIcon:ConstantsData.DeActiveIcon)  %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# ConstantsData.EditIcon %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                                <td><span><%# Eval("Name") %></span></td>
                                                <td><span><%# Eval("url") %></span></td>
                                                <td><span><%# Eval("SectionType") %></span></td>
                                                <td><span><%# Eval("Price") %></span></td>
                                                <td>
                                                    <asp:Image runat="server" ID="img" Width="100" Height="100" ImageUrl='<%# "../images/icon/"+Eval("image").ToString().NoneImage() %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>

                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnServiceID" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
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
        function showModal() {
            $('#myModal').modal();
        }


    </script>
</asp:Content>

