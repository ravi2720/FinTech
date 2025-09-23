<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AssignPackageRoleWise.aspx.cs" Inherits="Admin_AssignPackageRoleWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <style>
        #mytable {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #mytable td, #customers th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            #mytable tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #mytable tr:hover {
                background-color: #ddd;
            }

            #mytable th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #4CAF50;
                color: white;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>


                    <div class="card card-primary">
                        <div class="card-header">Assign Package</div>
                        <div class="card-body">
                            <asp:HiddenField ID="hdnid" runat="server" />
                            <div class="form-group">
                                <label for="exampleInputPassword1">
                                    Select Role
                                </label>
                                <asp:DropDownList ID="dllRole" ClientIDMode="Static" runat="server" class="form-control m-t-xxs" OnSelectedIndexChanged="dllRole_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>                                
                                <asp:Label ID="lblMemberName" runat="server" CssClass="green"></asp:Label>
                                <asp:HiddenField ID="hidMsrNo" runat="server" />
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Repeater runat="server" ID="rptAllPackage">
                                        <ItemTemplate>
                                            <div class="col-md-2">
                                                <asp:CheckBox runat="server" ID="chkData" CssClass="form-control" /><%# Eval("Name") %>
                                                <asp:HiddenField runat="server" ID="hdnVal" Value='<%# Eval("ID") %>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Button runat="server" ID="btnAssign" Text="Assign" CssClass="btn btn-warning" OnClick="btnAssign_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dllRole" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>

  
</asp:Content>

