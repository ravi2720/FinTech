<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="MemberList.aspx.cs" Inherits="Admin_Default2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .dropbtn {
            background-color: #4CAF50;
            color: white;
            padding: 16px;
            font-size: 16px;
            border: none;
            cursor: pointer;
        }

        .dropdown {
            position: relative;
            display: inline-block;
        }

        .dropdown-content {
            display: none;
            position: absolute;
            background-color: #f9f9f9;
            min-width: 160px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            z-index: 1;
                top: -30px;
        }

            .dropdown-content a {
                color: black;
                padding: 12px 16px;
                text-decoration: none;
                display: block;
            }

                .dropdown-content a:hover {
                    background-color: #f1f1f1
                }

        .dropdown:hover .dropdown-content {
            display: flex !important;
        }

        .dropdown:hover .dropbtn {
            background-color: #3e8e41;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="updt1" runat="server">
                <ContentTemplate>
                    <div>
                        <div class="card card-primary">
                            <div class="card-header">Member List</div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <label>Select From Date</label>
                                            <asp:TextBox ID="txtfromdate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                            <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                                TargetControlID="txtfromdate">
                                            </cc1:CalendarExtender>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Select To Date</label>
                                            <asp:TextBox ID="txttodate" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                            <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="MM-dd-yyyy" Animated="False"
                                                PopupButtonID="txttodate" TargetControlID="txttodate">
                                            </cc1:CalendarExtender>
                                        </div>
                                        <div class="col-md-2">
                                            <label>Select Role</label>
                                            <asp:DropDownList ID="dllRole" class="form-control ValiDationP" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <label>All Search</label>
                                            <asp:TextBox runat="server" placeholder="Enter Mobile or LoginID" ID="txtCityName" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger mt-4" Text="Search" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-lg-12">
                                        <div class="ibox float-e-margins">
                                            <asp:HiddenField ID="hfname" runat="server" />
                                            <div class="ibox-content collapse show">
                                                <div class="widgets-container">
                                                    <div>
                                                        <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                            <thead>
                                                                <tr>
                                                                    <th>S.No.</th>
                                                                    <th>Action</th>
                                                                    <th>Hold Amount</th>
                                                                    <th>Memberid</th>
                                                                    <th>Member Name</th>
                                                                    <th>City</th>
                                                                    <th>Parent Details</th>
                                                                    <th>Mobile</th>
                                                                    

                                                                    <th>Email ID</th>
                                                                    <th>AlterNative Mobile</th>
                                                                    <th>Shop Name</th>
                                                                    <th>Aadhar No</th>
                                                                    <th>Pan No</th>
                                                                    <th>Date Of Joining</th>

                                                                    <th>Password</th>
                                                                    <th>Login Pin</th>
                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                                <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <tr class="row1">
                                                                            <td><%# Container.ItemIndex+1 %></td>
                                                                            <td>
                                                                                <div class="dropdown">
                                                                                    <button class="dropbtn">Action</button>
                                                                                    <div class="dropdown-content">
                                                                                        <a href="#"><asp:ImageButton ID="btnAV" ToolTip="Member Video KYC ON/Off" CommandName="VideoKYC" CommandArgument='<%# Eval("msrno") %>' Height="50" ImageUrl='<%# Convert.ToInt16(Eval("VideoKYC"))==1 ?ConstantsData.kycA:ConstantsData.kycP %>' runat="server" CausesValidation="false"></asp:ImageButton></a>
                                                                                        <a href="#"><asp:ImageButton ID="lbtnApprove" ToolTip="Member Account Active/DeActive" OnClientClick="return confirm('are you sure do you want to Active/Deactive Account')" CommandName="IsActive" CommandArgument='<%# Eval("msrno") %>' Height="50" ImageUrl='<%# Convert.ToInt16(Eval("IsActive"))==1 ? ConstantsData.ActiveIcon:ConstantsData.DeActiveIcon %>' runat="server" CausesValidation="false"></asp:ImageButton></a>
                                                                                        <a href="#"><asp:ImageButton ID="btnOnHold" ToolTip="Member Account Hold/Un-Hold" CommandName="OnHold" OnClientClick="return confirm('are you sure do you want to hold/Unhold Account')" Height="50" CommandArgument='<%# Eval("msrno") %>' ImageUrl='<%# Convert.ToInt16(Eval("OnHold"))==1 ? ConstantsData.lockM:ConstantsData.unlocked %>' runat="server" CausesValidation="false"></asp:ImageButton></a>
                                                                                        <a href="#"><asp:ImageButton runat="server" ToolTip="Send Mail" ID="ImageButton1" CommandArgument='<%# Eval("msrno") %>' OnClientClick="target ='_blank';" Height="50" ImageUrl="<%# ConstantsData.gmail %>" CommandName="ReSend"  /></a>
                                                                                        <a href="#"><asp:ImageButton runat="server" ToolTip="Send SMS"  ID="btnLogin" CommandArgument='<%# Eval("msrno") %>' OnClientClick="target ='_blank';" Height="50" ImageUrl="<%# ConstantsData.SMS %>" CommandName="ReSend"  /></a>
                                                                                        <a href='EditProfile.aspx?ID=<%# Eval("ID") %>'>
                                                                                    <input type="image" style="height: 50px;" title="Edit Member Details" src="<%= ConstantsData.EditIcon %>" /></a>
                                                                                        <a href="#"><asp:Button runat="server" CommandName="Redirect" CommandArgument='<%# Eval("Msrno") %>' ID="btnPayment" CssClass='<%# (Convert.ToBoolean(Eval("ActiveProfile"))==true?"btn btn-success":"btn btn-danger") %>' Text='<%# (Convert.ToBoolean(Eval("ActiveProfile"))==true?"Paid":"Un-Paid") %>' Enabled='<%# (Convert.ToBoolean(Eval("ActiveProfile"))==true?false:true) %>' /></a>
                                                                                    </div>
                                                                                </div>         

                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox runat="server" ID="txtAmount" CssClass="form-control" Text='<%# Eval("HoldAmt") %>'></asp:TextBox>
                                                                                <asp:Button runat="server" ID="btnHoldAmount" CssClass="btn btn-danger" Text="Hold" CommandName="HoldAmount" CommandArgument='<%# Eval("iD") %>' />
                                                                            </td>
                                                                            <td><span><%# Eval("LoginID") %></span></td>
                                                                            <td><span><%# Eval("Name") %></span></td>
                                                                            <td><span><%# Eval("CityName") %></span></td>
                                                                            <td><span><%# Eval("ParentDetails") %></span></td>
                                                                            <td><span><%# Eval("Mobile") %></span></td>
                                                                            <td><span><%# Eval("Email") %></span></td>
                                                                            <td><span><%# Eval("AlterNativeMobileNumber") %></span></td>
                                                                            <td><span><%# Eval("ShopName") %></span></td>
                                                                            <td><span><%# Eval("Aadhar") %></span></td>
                                                                            <td><span><%# Eval("Pan") %></span></td>
                                                                            <td><span><%# Eval("CreatedDate") %></span></td>
                                                                            <td><span><%# Eval("password") %></span> </td>
                                                                            <td><span><%# Eval("Loginpin") %></span> </td>
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
    <script>
        $(function () {
            Action(DataObject);
        });

        var DataObject = {
            ID: "example1",
            iDisplayLength: 15,
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

