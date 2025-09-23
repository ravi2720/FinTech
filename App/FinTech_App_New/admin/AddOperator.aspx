<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AddOperator.aspx.cs" Inherits="Admin_AddOperator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">

            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Operator Details</div>
                        <div class="card-body">
                            <div class="form-group">
                                <label for="txtAccountNo">OperatorName : </label>
                                <asp:TextBox runat="server" ID="txtOperatorName" ToolTip="Enter OperatorName" class="form-control ValiDationR"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="txtAccountNo">OperatorCode : </label>
                                <asp:TextBox runat="server" ID="txtOperatorCode" ToolTip="Enter OperatorCode" class="form-control ValiDationR"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="txtBranchName">Service : </label>
                                <asp:DropDownList runat="server" ID="ddlService" ToolTip="Select SectionType" class="form-control ValiDationR">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label for="chkActive">Active : </label>
                                <asp:CheckBox runat="server" ID="chkActive" ToolTip="Active" class="form-control ValiDationR"></asp:CheckBox>
                            </div>
                            <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary" />


                            <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                <thead>
                                    <tr>
                                        <th>Active/DeActive</th>
                                        <th>Pending</th>
                                        <th>Edit</th>
                                        <th>Operator Name</th>
                                        <th>Service Name</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="row1">
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgDelete" OnClientClick="javascript:return confirm('Are you sure you want to Active/DeActive this Role?');" CommandName="Active" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("IsActive"))==true ? "./images/Active.png":"./images/delete.PNG")  %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                                 <td>
                                                    <asp:ImageButton runat="server" ID="ImageButton1" OnClientClick="javascript:return confirm('Are you sure you want to Pending this Role?');" CommandName="Pending" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("IsPending"))==true ? "./images/Active.png":"./images/delete.PNG")  %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ImageUrl="./images/Edit.PNG" Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                                <td><span><%# Eval("Name") %></span></td>
                                                <td><span><%# Eval("ServiceName") %></span></td>
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

