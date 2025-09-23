<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="OffLineServiceTransactionReport.aspx.cs" Inherits="Admin_OffLineServiceTransactionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        table {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            table td, #customers th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            table tr {
                background-color: #f2f2f2;
            }

                table tr:hover {
                    background-color: #ddd;
                }

            table th {
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
            <div class="container-fluid">
                <div class="row">
                    <table class="table table-response">
                        <tr>
                            <th><strong>SNO</strong></th>
                            <th><strong>Member Details</strong></th>
                            <th><strong>Status</strong></th>
                            <th><strong>Request Form</strong></th>
                            <th><strong>Request</strong></th>
                            <th><strong>TransID</strong></th>
                        </tr>
                        <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound" OnItemCommand="rptData_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Container.ItemIndex+1 %>
                                    </td>
                                    <td>
                                        <%# Eval("Name") %>
                                        <br />
                                        <%# Eval("MemberID") %>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dllStatus" CssClass="form-control">
                                            <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                            <asp:ListItem Text="Success" Value="Success"></asp:ListItem>
                                            <asp:ListItem Text="Failed" Value="Failed"></asp:ListItem>
                                            <asp:ListItem Text="Refund" Value="Refund"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Button runat="server" ID="btnUpdate" Visible='<%# (Eval("status").ToString()=="Success"?false:true) %>' CommandArgument='<%# Eval("ID") %>' Text="Update" CommandName="UpdateStatus" />
                                        <asp:HiddenField runat="server" ID="hdnstatus" ClientIDMode="Static" Value='<%# Eval("status") %>' />
                                    </td>
                                    <td>
                                        <%--<asp:HiddenField runat="server" ID="hdnRequestData" Value='<%# Eval("BoxProperty") %>' />
                                <asp:GridView runat="server" ID="grdRequestData" AutoGenerateColumns="true">
                                </asp:GridView>--%>
                                    </td>
                                    <td>
                                        <asp:HiddenField runat="server" ID="hdnData" Value='<%# Eval("RequestData") %>' />
                                        <asp:GridView runat="server" ID="grdData" OnRowDataBound="grdData_RowDataBound" AutoGenerateColumns="true">
                                        </asp:GridView>
                                    </td>
                                    <td>
                                        <%# Eval("TransID") %>
                                    </td>

                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </section>
    </div>
    <script>
        function CreateTableFromJSON(data) {
            var myBooks = JSON.parse(data);

            // EXTRACT VALUE FOR HTML HEADER. 
            // ('Book ID', 'Book Name', 'Category' and 'Price')
            var col = [];
            for (var i = 0; i < myBooks.length; i++) {
                for (var key in myBooks[i]) {
                    if (col.indexOf(key) === -1) {
                        col.push(key);
                    }
                }
            }

            // CREATE DYNAMIC TABLE.
            var table = document.createElement("table");

            // CREATE HTML TABLE HEADER ROW USING THE EXTRACTED HEADERS ABOVE.

            var tr = table.insertRow(-1);                   // TABLE ROW.

            for (var i = 0; i < col.length; i++) {
                var th = document.createElement("th");      // TABLE HEADER.
                th.innerHTML = col[i];
                tr.appendChild(th);
            }

            // ADD JSON DATA TO THE TABLE AS ROWS.
            for (var i = 0; i < myBooks.length; i++) {

                tr = table.insertRow(-1);

                for (var j = 0; j < col.length; j++) {
                    var tabCell = tr.insertCell(-1);
                    tabCell.innerHTML = myBooks[i][col[j]];
                }
            }

            // FINALLY ADD THE NEWLY CREATED TABLE WITH JSON DATA TO A CONTAINER.
            var divContainer = document.getElementById("showData");
            divContainer.innerHTML = "";
            divContainer.appendChild(table);
        }
    </script>
</asp:Content>

