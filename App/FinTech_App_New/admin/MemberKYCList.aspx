<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="MemberKYCList.aspx.cs" Inherits="Admin_BankDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <hr />
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="updt1" runat="server">
                <ContentTemplate>
                    <div class="card">
                        <div class="card-header">
                            <h3>KYC List</h3>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                    <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                                    <asp:ListItem Text="Pending" Selected="True" Value="Pending"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="ibox float-e-margins">
                                        <asp:HiddenField ID="hfname" runat="server" />
                                        <div class="ibox-content collapse show">
                                            <div class="widgets-container">
                                                <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                    <thead>
                                                        <tr>
                                                            <th>S.No.</th>
                                                            <th>Action</th>
                                                            <th>Reason</th>
                                                            <th>Status</th>

                                                            <th>MemberID</th>
                                                            <th>Name</th>
                                                            <th>Document</th>
                                                            <th>Document Number</th>
                                                            <th>Image</th>
                                                            <th>Back Image</th>
                                                            <th>AddDate</th>
                                                        </tr>
                                                    </thead>
                                                    <tfoot>
                                                        <tr>
                                                            <th>S.No.</th>
                                                            <th>Action</th>
                                                            <th>Reason</th>
                                                            <th>Status</th>
                                                            <th>MemberID</th>
                                                            <th>Name</th>
                                                            <th>Document</th>
                                                            <th>Document Number</th>
                                                            <th>Image</th>
                                                            <th>Back Image</th>
                                                            <th>AddDate</th>
                                                        </tr>
                                                    </tfoot>
                                                    <tbody>
                                                        <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr class="row1">
                                                                    <td><%# Container.ItemIndex+1 %>
                                                                    </td>
                                                                    <td>
                                                                        <asp:HiddenField runat="server" ID="hdnmsrno" Value='<%# Eval("msrno") %>' />
                                                                        <asp:Button ID="btnActive" runat="server" CommandName="IsApprove" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-success" Visible='<%# Eval("Status").ToString() != "Pending" ? false : true  %>' Text="Approve" OnClientClick="if (!confirm('Are you sure do you want to Approve it?')) return false" />
                                                                        <asp:Button ID="Button1" runat="server" CommandName="IsReject" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-danger" Visible='<%# Eval("Status").ToString() != "Pending" ? false : true  %>' Text="Reject" OnClientClick="if (!confirm('Are you sure do you want to Reject it?')) return false" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtReason" CssClass="form-control" Text='<%# Eval("Reason") %>'></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("LoginID") %>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("Name") %>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblDocName" runat="server" Text='<%# Eval("DocName") %>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("DocNumber") %>'></asp:Label></td>
                                                                    <td>
                                                                        <img src='./images/KYC/<%# Eval("DocImage") %>' onclick="downloadIt('<%# Eval("DocImage") %>','<%# "20176783-"+Eval("LoginID").ToString()+".jpeg" %>')" width="100" style="cursor: pointer" /></td>
                                                                    <td>
                                                                        <img src='./images/KYC/<%# Eval("DocImageBack") %>' onclick="downloadIt('<%# Eval("DocImageBack") %>','<%# "20176783-"+Eval("LoginID").ToString()+".jpeg" %>')" width="100" style="cursor: pointer" /></td>
                                                                    <td>
                                                                        <asp:Label ID="lblAdddate" runat="server" Text='<%# Eval("Adddate") %>'></asp:Label></td>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>

    <div class="modal fade" id="imagemodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="myModalLabel">Image preview</h4>
                </div>
                <div class="modal-body">
                    <a href="" download id="dd">
                        <img src="" id="imagepreview" style="width: 400px; height: 264px;">
                    </a>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        function downloadIt(Name, url) {
            var name = Name;
            if (name && name !== '') {
                var link = document.createElement('a');
                link.download = url;
                link.href = "./images/KYC/" + Name;
                link.click();
            }
        }
        function DownloadImage(Obj) {
            var myDiv = document.getElementById('fullsized_image_holder');
            var myImage = myDiv.children[0];
            linkElement.href = myImage.src;
        }
        function OpenImg(data) {
            document.getElementById("dd").href = "./images/KYC/" + data;
            document.getElementById("imagepreview").src = "./images/KYC/" + data;
            captionText.innerHTML = this.alt;

        }
    </script>
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

