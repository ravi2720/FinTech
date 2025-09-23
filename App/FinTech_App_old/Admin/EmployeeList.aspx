<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="EmployeeList.aspx.cs" Inherits="Admin_EmployeeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="updt1" runat="server">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card card-primary">
                            <div class="card-header">Staff List</div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="ibox float-e-margins">
                                            <asp:HiddenField ID="hfname" runat="server" />
                                            <div class="ibox-content collapse show">
                                                <div class="widgets-container">
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <asp:TextBox runat="server" ID="txtSearch" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click1" />
                                                        </div>
                                                    </div>
                                                    <div class="row mt-2">
                                                        <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                            <thead>
                                                                <tr>
                                                                    <th>S.No.</th>
                                                                    <th>Action</th>
                                                                    <th>Memberid</th>
                                                                    <th>Member Name</th>
                                                                    <th>Email</th>
                                                                    <th>Mobile</th>
                                                                    <th>DOJ</th>
                                                                    <th>Password</th>
                                                                    <th>TPIN</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <tr class="row1">
                                                                            <td><%# Container.ItemIndex+1 %></td>
                                                                            <td>
                                                                                <a href='<%# "EmployeeRegistration.aspx?ID="+Eval("ID").ToString() %>' class="btn btn-danger">Edit</a>
                                                                                <asp:Button ID="lbtnApprove" CommandName="Active" CommandArgument='<%# Eval("ID") %>' CssClass='<%# Convert.ToInt16(Eval("IsActive"))==1 ? "btn btn-success":"btn btn-danger" %>' Text='<%# Convert.ToInt16(Eval("IsActive"))==1 ? "Active":"InActive" %>' runat="server" CausesValidation="false"></asp:Button>
                                                                            </td>
                                                                            <td><span><%# Eval("LoginID") %></span></td>

                                                                            <td><span><%# Eval("Name") %></span></td>
                                                                            <td><span><%# Eval("Email") %></span></td>
                                                                            <td><span><%# Eval("Mobile") %></span></td>
                                                                            <td><span><%# Eval("AddDate") %></span></td>
                                                                            <td><span><%# Eval("password") %></span> </td>
                                                                            <td><span><%# Eval("Transactionpassword") %></span> </td>

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
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

