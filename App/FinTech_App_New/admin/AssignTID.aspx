<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AssignTID.aspx.cs" Inherits="Admin_AssignTID" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Assign TID</div>
                        <div class="card-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-2">
                                        <label for="ddlPackage"><b>Member : </b></label>
                                        <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member." CssClass="form-control select2"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-2">
                                        <label for="ddlMember">Select Status : </label>
                                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                            <asp:ListItem Text="Select Status" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Accepted" Value="Accepted"></asp:ListItem>
                                            <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-2">
                                        <label for="txtfromd"><b>AEPSID : </b></label>
                                        <asp:TextBox runat="server" ID="txtAEPSID" ToolTip="Enter AEPSID" class="form-control ValiDationR"></asp:TextBox>
                                    </div>

                                    <div class="col-md-2">
                                        <label for="txtfromd"><b>Mobile : </b></label>
                                        <asp:TextBox runat="server" ID="txtMobile" ToolTip="Enter Mobile" value="0" class="form-control ValiDationR"></asp:TextBox>
                                        <asp:HiddenField ID="hdnidd" runat="server" Value="" />
                                    </div>

                                    <div class="col-md-4">
                                        <asp:Button runat="server" ID="btnadd" Text="Add" OnClick="btnSubmit_Click" class="btn btn-primary mt-4" />
                                    </div>
                                </div>
                            </div>


                            <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th>S.No</th>
                                          <th>Action</th>
                                        <th>Member Detail</th>
                                        <th>AEPSID</th>
                                        <th>Bank Name</th>
                                        <th>STATUS</th>
                                        <th>Mobile</th>
                                        <th>AddDate</th>
                                    </tr>
                                </thead>
                                <tfoot>
                                    <tr>
                                        <th>S.No</th>
                                        <th>Action</th>
                                        <th>Member Detail</th>
                                        <th>AEPSID</th>
                                        <th>Bank Name</th>
                                        <th>STATUS</th>
                                        <th>Mobile</th>
                                        <th>AddDate</th>
                                    </tr>
                                </tfoot>
                                <tbody>
                                    <asp:Repeater ID="RptData" runat="server" OnItemCommand="RptData_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Container.ItemIndex+1 %></td>
                                                <td>
                                                    <asp:Button ID="btnDelete" runat="server" OnClientClick="return confirm('Do You Want To Delete Record')" CssClass="btn btn-primary" Text="Delete" CommandArgument='<%#Eval("ID") %>' CommandName="Delete" />
                                                    </td>
                                                <td><%#Eval("membername") %>
                                                    <br />
                                                    <%# Eval("LOGINID") %>
                                                </td>
                                                <td><%#Eval("AEPSID") %></td>
                                                <td><%#Eval("Name") %></td>
                                                <td><%#Eval("STATUS") %></td>
                                                <td><%#Eval("Mobile") %></td>
                                                <td><%#Eval("AddDate") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnRoleID" Value="0" />
                </ContentTemplate>
                <Triggers>
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

