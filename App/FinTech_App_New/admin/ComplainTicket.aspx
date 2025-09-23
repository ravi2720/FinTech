<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ComplainTicket.aspx.cs" Inherits="Admin_ComplainTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card panel-primary">
                        <div class="card-header">Support List</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="ibox float-e-margins">
                                        <div class="ibox-content collapse show">
                                            <div class="widgets-container">
                                                <div>
                                                    <table id="example6" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                        <thead>
                                                            <tr>
                                                               <th>Action</th>
                                                                <th>Ticket id</th>
                                                                <th>LoginID</th>
                                                                <th>Name</th>
                                                                <th>Service</th>
                                                                <th>Message</th>
                                                                <th>Date</th>

                                                                <th>Status</th>
                                                                <th>Approve Date</th>
                                                              
                                                            </tr>
                                                        </thead>
                                                        
                                                        <tbody>
                                                            <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr class="row1">
                                                                         <td>
                                                                            <a href='<%# "ComplainChat.aspx?ID="+Eval("ID") %>' style="color:red" class="btn btn-primary" target="_self">Chat</a>
                                                                            <asp:Button ID="btnApprove" CommandName="IsApprove" CssClass="btn btn-primary" Text="Approve" Visible='<%# (Convert.ToBoolean(Eval("IsApprove"))==false && Convert.ToBoolean(Eval("IsDelete"))==false) == true ? true : false %>' runat="server" OnClientClick="javascript:return confirm('Are you sure?')"></asp:Button>
                                                                            <asp:Button ID="btnReject" CommandName="IsReject" CssClass="btn btn-danger" Text="Reject" Visible='<%# (Convert.ToBoolean(Eval("IsDelete"))==false && Convert.ToBoolean(Eval("IsApprove"))==false) == true ? true : false %>' runat="server" OnClientClick="javascript:return confirm('Are you sure?')"></asp:Button></td>
                                                                        <td><span><%# Eval("ID") %></span>
                                                                            <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ID") %>' />
                                                                        </td>
                                                                        <td><span><%# Eval("LoginID") %></span></td>
                                                                        <td><span><%# Eval("UserName") %></span></td>
                                                                        <td><span><%# Eval("ServiceName") %></span></td>
                                                                        <td><span><%# Eval("Message") %></span></td>
                                                                        <td><span><%# Eval("AddDate") %></span></td>

                                                                        <td><span><%# Eval("Status") %></span></td>
                                                                        <td><span><%#Eval("StatusUpdateDate") %><span></td>
                                                                       
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
        </section>
    </div>
     <script>
        $(function () {
            Action(DataObject);
        });

        var DataObject = {
            ID: "example6",
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

