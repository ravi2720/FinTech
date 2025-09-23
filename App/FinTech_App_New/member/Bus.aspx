<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="Bus.aspx.cs" Inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Buscss/style.css" rel="stylesheet" />
    <link href="../Buscss/jquery.datetimepicker.min.css" rel="stylesheet" />
    <link href="../Buscss/font-awesome.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid" id="his">
        <div class="main-content-body">
            <div class="travel-detail">
                <div class="container">
                    <div class="row">
                        <div class="col-12 col-md-6">
                            <div class="card-header">
                                <h3 class="card-title"><b>Travel Buses & Cabs</b> </h3>
                            </div>


                            <div class="travel-part">
                                <div class="row">
                                    <div class="col-md-12">

                                        <label for="ddlcity">leaving From :</label>
                                        <asp:DropDownList runat="server" ID="ddlcityF" ToolTip="Select City Name." CssClass="form-control"></asp:DropDownList>

                                    </div>
                                    <div class="col-md-12">

                                        <label for="ddlcity">Going to : </label>
                                        <asp:DropDownList runat="server" ID="ddlcityT" ToolTip="Select City Name." CssClass="form-control"></asp:DropDownList>
                                    </div>


                                    <div class="col-md-12">
                                        <label for="ddlcity">Date of Journey :</label>
                                        <input type="date" id="datepicker" />

                                    </div>


                                    <div class="col-md-12">

                                        <asp:Button ID="btnSearch" runat="server" class="btn-primary" Text="Search Buses" OnClick="btnSearch_Click" />


                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
    $('#datepicker').datepicker({
        weekStart: 1,
        daysOfWeekHighlighted: "6,0",
        autoclose: true,
        todayHighlight: true,
    });
    $('#datepicker').datepicker("", new Date());
    </script>

</asp:Content>

