<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AEPSNonActiveMember.aspx.cs" Inherits="Admin_AEPSNonActiveMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>                        
                        <div class="row">
                            <div class="box">

                                <div class="box-body">
                                    <div class="col-md-12">

                                        <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                            <thead>
                                                <tr>
                                                    <th>S.No</th>
                                                    <th>Member Id</th>
                                                    <th>Member Name</th>
                                                    <th>MobileNO</th>
                                                    <th>EmailID</th>
                                                    <th>AddDate</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="gvtData" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Container.ItemIndex+1 %></td>
                                                            <td><%# Eval("LoginiD") %></td>
                                                            <td><%# Eval("Name") %></td>
                                                            <td><%# Eval("Mobile") %></td>
                                                            <td><%# Eval("Email") %></td>
                                                            <td><%# Eval("CreatedDate") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>

                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
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

