<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ContactHereReport.aspx.cs" Inherits="Admin_ContactHereReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="updateRolePanel">
        <ContentTemplate>
            <div class="content-wrapper">
                <section class="content">
                    <div class="card card-primary">
                        <div class="card-header">Contact Here List</div>
                        <div class="card-body">
                            <%--  <asp:HiddenField ID="hdnid" Value="0" runat="server" />--%>
                            <div class="row">
                              
                                <div class="col-md-3">
                                    <label>From Date</label>
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                        TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                </div>
                                <div class="col-md-3">
                                    <label>To Date</label>
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="MM-dd-yyyy" Animated="False"
                                        PopupButtonID="txttodate" TargetControlID="txttodate">
                                    </cc1:CalendarExtender>

                                </div>

                                <div class="col-md-2">
                                    <label>Search</label>
                                    <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" CssClass="form-control"></asp:Button>
                                </div>
                                <br />

                                <div class="table-responsive">
                                   <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                        <thead>
                                            <tr>
                                            </tr>
                                            <tr>
                                                <th>S.No</th>
                                                <th>Name</th>
                                                <th>Email</th>
                                                <th>Mobile</th>
                                                <th>Message</th>                                                
                                                <th>Add Date</th>
                                                

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rptDataRecharge" OnItemCommand="rptDataRecharge_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex+1 %></td>                                                         
                                                        <td><%#Eval("Name") %></td>
                                                        <td><%#Eval("Email") %></td>                                                    
                                                        <td><%#Eval("Mobile") %></td>
                                                        <td><%#Eval("Message") %></td>
                                                        <td><%#Eval("AddDate") %></td>
                                                      
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                </section>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    
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

