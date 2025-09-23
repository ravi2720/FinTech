<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AutoSwitch.aspx.cs" Inherits="Admin_AutoSwitch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #customers {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #customers td, #customers th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            #customers tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #customers tr:hover {
                background-color: #ddd;
            }

            #customers th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #4CAF50;
                color: white;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="card card-primary">
                <div class="card-header">Change API</div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-xs-4 col-sm-4">
                            <asp:DropDownList runat="server" ID="ddlPackage" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlPackage_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-xs-4 col-sm-4">
                            <asp:Button ID="Button1" runat="server" Text="Save All" CssClass="btn btn-success" OnClick="Button1_Click" />
                        </div>
                        <div class="col-xs-2 col-sm-2">
                            <a href="DashBoard.aspx" class="btn btn-danger">Back</a>
                        </div>

                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <td><strong>Opeartor Details</strong></td>
                                        <td><strong>Api 1</strong></td>
                                        <td><strong>Api 2</strong></td>
                                        <td><strong>Api 3</strong></td>
                                        <td><strong>Api 4</strong></td>
                                        <td></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptOperator" OnItemDataBound="rptOperator_ItemDataBound" OnItemCommand="rptOperator_ItemCommand">
                                        <ItemTemplate>
                                            <tr class='<%#"row_"+Container.ItemIndex+1 %>'>
                                                <td>
                                                    <p>
                                                        <%# Eval("Data") %>
                                                        <asp:HiddenField runat="server" ID="hdnCommisionVal" Value='<%# Eval("CommissionID") %>' />
                                                        <asp:HiddenField runat="server" ID="hdnOperatorID" Value='<%# Eval("OperatorID") %>' />
                                                        <asp:HiddenField runat="server" ID="hdnSaveAutoVal" Value='<%# Eval("Auto_System_APID") %>' />
                                                    </p>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAPI1" onchange="OnDataSelect(this,'strAPI1_',1,'strCommAPI1_','strAPIhdnOp1_')" runat="server" CssClass="form-control" Width="200px">
                                                    </asp:DropDownList>
                                                    <asp:Label runat="server" ID="lblCommAPI1" Font-Bold="true" CssClass="lblComCss1"></asp:Label>
                                                    <strong id='<%#"strCommAPI1_"+Container.ItemIndex+1%>'></strong>
                                                    <strong id='<%#"strAPI1_"+Container.ItemIndex+1%>'></strong>
                                                    <input type="hidden" id='<%#"strAPIhdnOp1_"+Container.ItemIndex+1%>' value='<%# Eval("OperatorID") %>' />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAPI2" runat="server" onchange="OnDataSelect(this,'strAPI2_',2,'strCommAPI2_','strAPIhdnOp1_')" CssClass="form-control" Width="200px">
                                                    </asp:DropDownList>
                                                    <asp:Label runat="server" ID="lblCommAPI2" Font-Bold="true" CssClass="lblComCss2"></asp:Label>
                                                    <asp:Label runat="server" ID="lblComm2"></asp:Label>
                                                    <strong id='<%#"strAPI2_"+Container.ItemIndex+1%>'></strong>
                                                    <strong id='<%#"strCommAPI2_"+Container.ItemIndex+1%>'></strong>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAPI3" runat="server" onchange="OnDataSelect(this,'strAPI3_',3,'strCommAPI3_','strAPIhdnOp1_')" CssClass="form-control" Width="200px">
                                                    </asp:DropDownList>
                                                    <asp:Label runat="server" ID="lblCommAPI3" Font-Bold="true" CssClass="lblComCss3"></asp:Label>
                                                    <asp:Label runat="server" ID="lblComm3"></asp:Label>
                                                    <strong id='<%#"strAPI3_"+Container.ItemIndex+1%>'></strong>
                                                    <strong id='<%#"strCommAPI3_"+Container.ItemIndex+1%>'></strong>
                                                </td>
                                                <td>

                                                    <asp:DropDownList ID="ddlAPI4" runat="server" onchange="OnDataSelect(this,'strAPI4_',4,'strCommAPI4_','strAPIhdnOp1_')" CssClass="form-control" Width="200px">
                                                    </asp:DropDownList>
                                                    <asp:Label runat="server" ID="lblCommAPI4" Font-Bold="true" CssClass="lblComCss3"></asp:Label>
                                                    <asp:Label runat="server" ID="lblComm4"></asp:Label>
                                                    <strong id='<%#"strAPI4_"+Container.ItemIndex+1%>'></strong>
                                                    <strong id='<%#"strCommAPI4_"+Container.ItemIndex+1%>'></strong>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-danger" CommandName="Save" CommandArgument='<%# Eval("CommissionID") %>' Text="Save"></asp:Button>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
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

