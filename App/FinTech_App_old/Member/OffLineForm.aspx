<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="OffLineForm.aspx.cs" Inherits="Member_OffLineForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <div class="main-container container-fluid">
        <div class="main-content-body">
        <div class="content-wrapper">
            <section class="content">
                <div class="container-fluid">
                    <div class="card">
                        <h4 class="card-header" id="divHeading" runat="server">Offline Service Details</h4>
                        <div class="card-body">
                            <table id="tbldynamicform" class="table table-responsive">
                                <tr>
                                    <td>
                                        <asp:Panel ID="placeholder" runat="server">
                                            <asp:Table runat="server" CssClass="table table-responsive" ID="tbldynamic"></asp:Table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Amount</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblAmount" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </section>
        </div>
            </div>
    </div>
    <script>
        function ValidateForm() {
            var Flag = true;
            $('.Man').each(function () {
                if ($(this).val().trim() == "") {
                    Flag = false;
                    $(this).css("border", "1px solid red");
                } else {
                    $(this).css("border", "1px solid green");
                }
            });
            return Flag;
        }
    </script>
</asp:Content>

