<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="Settlement.aspx.cs" Inherits="Member_Settlement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        input[type="radio"] {
            margin: 4px 10px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <div class="main-content-body">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>               
                    <div class="row row-sm">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-header">
                                    <h3 class="card-title">AEPS Withdraw Request
                                          <span style="color: black">
                                              <h4>AEPS request allow b/w <span runat="server" style="display: none" id="spanstartTime"></span>AM to <span runat="server" id="spnendTime"></span>PM</h4>
                                          </span>
                                    </h3>
                                </div>
                                <div class="card-body">
                                    <div class="card card-body">
                                        <asp:HiddenField ID="hdnid" runat="server" />
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="exampleInputPassword1">
                                                        AEPS Wallet Balance
                                                    </label>
                                                    <asp:Label ID="lblBalance" Text="0.00" class="form-control m-t-xxs" runat="server" />
                                                </div>
                                                <div class="col-md-4">
                                                    <label for="exampleInputPassword1">Amount</label>
                                                    <asp:TextBox ID="txtamount" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Amount" Font-Bold="True" ForeColor="Red" ControlToValidate="txtAmount" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" ValidChars="." TargetControlID="txtamount" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="exampleInputPassword1">
                                                        Move To : 
                                                    </label>
                                                    <asp:RadioButtonList ID="rdtype" RepeatLayout="Flow" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdtype_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                        <asp:ListItem Text="Main Wallet" Value="Wallet"></asp:ListItem>
                                                        <asp:ListItem Text="Bank" Selected="True" Value="Bank"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="col-md-4" runat="server" id="divTransactionType" visible="true">
                                                    <label for="exampleInputPassword1">
                                                        Transaction Mode : 
                                                    </label>
                                                    <asp:RadioButtonList ID="rdtTransactionMode" Visible="true" RepeatLayout="Flow" OnSelectedIndexChanged="rdtTransactionMode_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" runat="server">
                                                        <asp:ListItem Text="IMPS" Value="IMPS" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="NEFT" Value="NEFT"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                            <div class="form-group" runat="server" id="divTransactionCharge" visible="true">
                                                <label for="" style="color: red;">
                                                    Bank Details : 
                                                </label>


                                                <div class="table-responsive">
                                                    <table id="file-datatable" class="border-top-0  table table-bordered text-nowrap key-buttons border-bottom">
                                                        <thead>
                                                            <tr>

                                                                <th class="border-bottom-0">Select</th>
                                                                <th class="border-bottom-0">SNO</th>
                                                                <th class="border-bottom-0">IFSC Code </th>
                                                                <th class="border-bottom-0">Account No</th>
                                                                <th class="border-bottom-0">BranchName</th>
                                                                <th class="border-bottom-0">Name</th>
                                                                <th class="border-bottom-0">Bank Name</th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rptBankData" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:HiddenField runat="server" ID="hdnAccount" Value='<%# Eval("AccountNumber") %>' />
                                                                            <asp:CheckBox runat="server" CssClass="ChkOne" onchange="SelectOne('.ChkOne',this);" ID="chk" />
                                                                        </td>
                                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                                        <td><%# Eval("IfscCode") %></td>
                                                                        <td><%# Eval("AccountNumber") %></td>
                                                                        <td><%# Eval("BranchName") %></td>
                                                                        <td><%# Eval("AccountHolderName") %></td>
                                                                        <td><%# Eval("Name") %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <td colspan="7" class="text-center">
                                                                        <a href="BankDetails.aspx" class="btn btn-danger">Add Bank</a>
                                                                    </td>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Remark</label>
                                                <asp:TextBox ID="txtRemark" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Remark" Font-Bold="True" ForeColor="Red" ControlToValidate="txtRemark" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group">
                                                <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Submit" OnClientClick=" if (Page_ClientValidate()){ this.disabled = true; this.value = 'Submitting...';}" UseSubmitBehavior="false" OnClick="btnSubmit_Click" ValidationGroup="chkaddfund" />

                                                <asp:Label ID="lblMessage" CssClass="center-block" Style="text-align: center; font-weight: bold; font-size: x-large;" ForeColor="Red" runat="server" />
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnValue" ClientIDMode="Static" Value="" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
    <script>
        function SelectOne(cls, ObjData, Acc) {
            $(cls).each(function () {
                $(this).find('input')[0].checked = false;
            });
            $(ObjData).find('input')[0].checked = true;
        }
    </script>
        <script>


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

            // Action(DataObject);
        }

        var req = Sys.WebForms.PageRequestManager.getInstance();
        req.add_beginRequest(function () {

        });
        req.add_endRequest(function () {
            var table = $('#file-datatable').DataTable({
                buttons: ['copy', 'excel', 'pdf', 'colvis'],
                language: {
                    searchPlaceholder: 'Search...',
                    scrollX: "100%",
                    sSearch: '',
                }
            });
            table.buttons().container()
                .appendTo('#file-datatable_wrapper .col-md-6:eq(0)');
        });
        function showModal() {
            $('#myModal').modal();
        }


    </script>
</asp:Content>

