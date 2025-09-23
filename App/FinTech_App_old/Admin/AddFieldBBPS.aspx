<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AddFieldBBPS.aspx.cs" Inherits="Admin_AddFieldBBPS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Add Field BBPS</div>
                        <div class="card-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">Service : </label>
                                        <asp:DropDownList runat="server" ID="dllService" AutoPostBack="true" OnSelectedIndexChanged="dllService_SelectedIndexChanged" ToolTip="Enter Service" class="form-control select2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">Operator Code: </label>
                                        <asp:DropDownList runat="server" ID="dllOpeator" AutoPostBack="true" OnSelectedIndexChanged="dllOpeator_SelectedIndexChanged" ToolTip="Enter Service" class="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label for="txtAccountNo">OperatorCode : </label>
                                        <asp:TextBox runat="server" ID="txtOperatorCode"  ToolTip="Enter OperatorCode" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="txtBranchName">Index : </label>
                                        <asp:TextBox runat="server" ID="txtIndex" Text="0" ToolTip="Select Index" class="form-control ValiDationR">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label for="txtBranchName">Label : </label>
                                        <asp:TextBox runat="server" ID="txtLabel" ToolTip="Select Index" class="form-control ValiDationR">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="txtBranchName">Min Length : </label>
                                        <asp:TextBox runat="server" ID="txtminLength" Text="0" ToolTip="Select Min Length" class="form-control ValiDationR">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="txtBranchName">Max Length : </label>
                                        <asp:TextBox runat="server" ID="txtmaxLength" Text="0" ToolTip="Select Max Length" class="form-control ValiDationR">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary" />


                            <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                <thead>
                                    <tr>                                        
                                        <th>Operator Name</th>
                                        <th>Service Name</th>
                                        <th>Label</th>
                                        <th>FieldMinLen</th>
                                        <th>FieldMaxLen</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptData">
                                        <ItemTemplate>
                                            <tr class="row1">
                                                <td><span><%# Eval("Operator") %></span></td>
                                                <td><span><%# Eval("ServiceType") %></span></td>
                                                <td><span><%# Eval("Labels") %></span></td>
                                                <td><span><%# Eval("FieldMinLen") %></span></td>
                                                <td><span><%# Eval("FieldMaxLen") %></span></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnOperatorID" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
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

