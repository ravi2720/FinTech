<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ManageOrderMenu.aspx.cs" Inherits="Admin_ManageOrderMenu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.10.4/jquery-ui.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper" id="menuupdate">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>

                    <div class="card card-primary">
                        <div class="card-header">
                            Menu List 

                            <input type="button" id="btnSave" @click="UpdateMenuPosition" value="Update" />
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="ibox float-e-margins">

                                        <div class="ibox-content collapse show">
                                            <div class="widgets-container">
                                                <div>
                                                    <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th>Active/DeActive</th>
                                                                <th>Name</th>
                                                                <th>Position</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater runat="server" ID="rptData">
                                                                <ItemTemplate>
                                                                    <tr class="row1 ui-sortable-handle" title='<%# Eval("ID") %>'>
                                                                        <td>
                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hndID" Value='<%# Eval("ID") %>' />
                                                                            <asp:ImageButton runat="server" ID="imgDelete" OnClientClick="javascript:return confirm('Are you sure you want to Active/DeActive this Role?');" CommandName="Active" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("IsActive"))==true ? ConstantsData.ActiveIcon:ConstantsData.DeActiveIcon)  %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                                        </td>
                                                                        <td><span><%# Eval("Name") %></span></td>
                                                                        <td><span><%# Eval("ShowPosition") %></span></td>
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

        //$("tbody").sortable({
        //    cursor: 'row-resize',
        //    placeholder: 'ui-state-highlight',
        //    opacity: '0.55',
        //    items: '.ui-sortable-handle',
        //    update: function () {
        //        debugger;
        //        fnSubmit(submit[0].checked); //.checked return boolean
        //    }
        //}).disableSelection();




        new Vue({
            el: '#menuupdate',
            data() {
                return {

                }
            },
            methods: {
                async UpdateMenuPosition() {
                     document.getElementById('load').style.visibility = "visible";
                    $("table tbody").children().each(function (index) {
                        
                        //$(this).find('td').last().html(index + 1)

                        const article = {
                            "ID": $(this).find('td')[0].children[0].value,
                            "Position": $(this).find('td').last()[0].innerHTML
                        }
                        axios.post("ManageOrderMenu.aspx/UpdateMenu", article)
                            .then(function (response) {
                                alertify.success('Success');
                                 document.getElementById('load').style.visibility = "hidden";
                            });
                    });
                    
                }
            },
            mounted() {
                $("table tbody").sortable({
                    update: function (event, ui) {
                        $(this).children().each(function (index) {
                            $(this).find('td').last().html(index + 1)
                        });
                    }
                });
            }
        });


    </script>
</asp:Content>

