<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="inner.aspx.cs" Inherits="inner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <link href="../Buscss/style.css" rel="stylesheet" />
    <link href="../Buscss/font-awesome.min.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/js/bootstrap-datepicker.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <link href="../Buscss/jquery.seat-charts.css" rel="stylesheet" />
    <link href="../Buscss/booking.css" rel="stylesheet" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid" id="his">
        <div class="main-content-body">
            <div class="container">
                <div class="travel-detail">
                    <div class="row">
                        <div class="col-12 col-md-6">
                            <div class="panel">
                                <h3><strong id="sc">Chandigarh</strong>&nbsp;➠&nbsp;<strong id="dc">Noida</strong><i> <span id="dj" class="com_dt">30&nbsp;Jul&nbsp;2021,&nbsp;Friday</span></i></h3>
                            </div>
                        </div>

                        <div class="col-md-5">
                            <div class="menu-button">
                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">
                                    Filter
                                </button>
                            </div>
                            <!-- The Modal -->
                            <div class="modal" id="myModal">
                                <div class="modal-dialog">
                                    <div class="modal-content">

                                        <!-- Modal Header -->
                                        <div class="modal-header">
                                            <div class="container mt-3">
                                                <ul class="nav nav-tabs">
                                                    <li class="nav-item">
                                                        <a class="nav-link active" data-toggle="tab" href="#home">Operator |</a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" data-toggle="tab" href="#menu1">Type |</a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" data-toggle="tab" href="#menu2">Boarding |</a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" data-toggle="tab" href="#menu3">Dropping |</a>
                                                    </li>
                                                </ul>
                                                <!-- Tab panes -->
                                                <div class="tab-content">
                                                    <div id="home" class="container tab-pane active">
                                                        <br>
                                                        <div class="form-group">
                                                            <input type="text" class="form-control" id="usr" placeholder="Search Operator">
                                                        </div>
                                                    </div>
                                                    <div id="menu1" class="container tab-pane fade">
                                                        <br>
                                                        <div class="form-group">
                                                            <input type="text" class="form-control" id="usr" placeholder="Search Type">
                                                            <div class="form-check">
                                                                <input type="checkbox" class="form-check-input" id="exampleCheck1">
                                                                <label class="form-check-label" for="exampleCheck1">Cab</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="menu2" class="container tab-pane fade">
                                                        <br>
                                                        <div class="form-group">
                                                            <input type="text" class="form-control" id="usr" placeholder="Search Boarding Point">
                                                        </div>
                                                    </div>
                                                    <div id="menu3" class="container tab-pane fade">
                                                        <br>
                                                        <div class="form-group">
                                                            <input type="text" class="form-control" id="usr" placeholder="Search Dropping Point">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-primary" data-dismiss="modal">Reset</button>
                                            <button type="button" class="btn btn-danger" data-dismiss="modal">Done</button>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-12">
                            <div class="bs-example">
                                <div class="accordion" id="accordionExample">
                                    <div class="card-header" id="headingOne">
                                        <button type="button" class="btn btn-link" data-toggle="collapse" data-target="#collapseOne">Modify <i class="fa fa-plus"></i></button>
                                    </div>
                                    <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#accordionExample">
                                        <div class="card-body container">
                                            <div class="travel-part">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label for="ddlcity">leaving From :</label>
                                                        <asp:DropDownList runat="server" ID="ddlcityF" ToolTip="Select City Name." CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label for="ddlcity">Going to : </label>
                                                        <asp:DropDownList runat="server" ID="ddlcityT" ToolTip="Select City Name." CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3">
                                                     <label for="ddlcity">Date of Journey</label>
                                                        <input type="date" id="datepicker" />
                                                        
                                                    </div>
                                                    <div class="col-md-3">
                                                        <a href="inner.aspx">
                                                            <button class="btn-primary">
                                                                Search Buses
                                                            </button>
                                                        </a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <hr>
                            <ul class="menu-table">
                                <li>
                                    <a href="javascript:void(0)">Departure
                  <i class="fa fa-sort"></i>
                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)">Duration
                  <i class="fa fa-sort"></i>
                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)">Arrival
                  <i class="fa fa-sort"></i>
                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)">Fare
                  <i class="fa fa-sort"></i>
                                    </a>
                                </li>
                            </ul>
                            <div class="table-responsive">
                                <table class="table table-striped">
                                    <tbody>
                                        <tr>
                                            <td><b>Jakhar Travels And Cargo</b>
                                                <p>
                                                    NON A/C Seater / Sleeper (2+1)
                                                </p>
                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal1">
                                                    Cancellation Policy
                                                </button>
                                                <div class="modal fade" id="myModal1">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">

                                                            <!-- Modal Header -->
                                                            <div class="modal-header">
                                                                <h4 class="modal-title">Cancellation Policy</h4>
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                            </div>

                                                            <!-- Modal body -->
                                                            <div class="modal-body">
                                                                <div class="table-responsive">
                                                                    <table class="table table-striped">
                                                                        <thead>
                                                                            <tr>
                                                                                <th scope="col">Description </th>
                                                                                <th scope="col">Charges</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr>
                                                                                <th scope="row">After 31 Jul, 2021 10:30 AM </th>
                                                                                <td>100 %
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <th scope="row">Between 30 Jul, 2021 10:30 PM and 31 Jul, 2021 10:30 AM </th>
                                                                                <td>50 %
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <th scope="row">Between 28 Jul, 2021 10:30 PM and 30 Jul, 2021 10:30 PM </th>
                                                                                <td>10 %
                                                                                </td>

                                                                            </tr>
                                                                            <tr>
                                                                                <th scope="row">Between 24 Jul, 2021 10:30 PM and 28 Jul, 2021 10:30 PM  </th>
                                                                                <td>10 %
                                                                                </td>

                                                                            </tr>
                                                                            <tr>
                                                                                <th scope="row">Before 24 Jul, 2021 10:30 PM   </th>
                                                                                <td>10 %
                                                                                </td>

                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <span class="danger" style="color: #dc3545;">* Partial Cancellation Not Allowed
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <!-- Modal footer -->

                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>31 Jul, 21<br>
                                                <p>
                                                    10:30 PM
                                                </p>
                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal2">
                                                    Boarding Points
                                                </button>
                                                <div class="modal fade" id="myModal2">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">

                                                            <!-- Modal Header -->
                                                            <div class="modal-header">
                                                                <h4 class="modal-title">Boarding Points</h4>
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                            </div>

                                                            <!-- Modal body -->
                                                            <div class="modal-body">
                                                                <div class="table-responsive">
                                                                    <table class="table table-striped">
                                                                        <thead>
                                                                            <tr>
                                                                                <th scope="col">Time   </th>
                                                                                <th scope="col">Location</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr>
                                                                                <th scope="row">31 Jul 2021, 10:30 PM  </th>
                                                                                <td>Sindhi Camp
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <th scope="row">31 Jul 2021, 10:50 PM </th>
                                                                                <td>Others
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td><strong>
                                                <center><i class="fa fa-clock-o"></i><br>8h 5m</center>
                                            </strong></td>
                                            <td>31 Jul, 21<br>
                                                <p>
                                                    10:30 PM
                                                </p>
                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal3">
                                                    Dropping Points
                                                </button>
                                                <div class="table-responsive">
                                                    <div class="modal fade" id="myModal3">
                                                        <div class="modal-dialog">
                                                            <div class="modal-content">

                                                                <!-- Modal Header -->
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title">Boarding Points</h4>
                                                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                </div>

                                                                <!-- Modal body -->
                                                                <div class="modal-body">
                                                                    <div class="table-responsive">
                                                                        <table class="table table-striped">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th scope="col">Time   </th>
                                                                                    <th scope="col">Location</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <th scope="row">301 Aug 2021, 06:35 AM  </th>
                                                                                    <td>DalleKhan Chaki Opp IIT Acdmy Pal Road 9530068484 9530068282
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <th scope="row">01 Aug 2021, 06:35 AM </th>
                                                                                    <td>Near DalleKhan Ki Chakki Opp I.I.T. AcademyPalRoad9530068484
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <th scope="row">01 Aug 2021, 06:35 AM </th>
                                                                                    <td>Mandore Road Pasta 9530068282 95300684884
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>

                                                                    <!-- Modal footer -->


                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>Starting from
                                            </td>
                                            <td><b>31 Jul, 21</b>
                                                <p>
                                                    INR 300
                                                </p>
                                                <button type="button" class="btn btn-primary cdn" data-toggle="modal" data-target="#myModal4">
                                                    View Seats 
                                                </button>
                                                <div class="modal fade" id="myModal4">
                                                    <div class="modal-dialog modal-lg">
                                                        <div class="modal-content">

                                                            <!-- Modal Header -->
                                                            <div class="modal-header">
                                                                <select class="custom-select" id="gender2">
                                                                    <option selected="">Boarding Point</option>
                                                                    <option value="1">Male</option>
                                                                    <option value="2">Female</option>
                                                                </select>
                                                                <select class="custom-select" id="gender1">
                                                                    <option selected="">Dropping Point</option>
                                                                    <option value="1">Male</option>
                                                                    <option value="2">Female</option>
                                                                </select>
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                            </div>

                                                            <!-- Modal body -->
                                                            <div class="modal-body">

                                                                <div class="wrapper">
                                                                    <div class="container">

                                                                        <div class="row">
                                                                            <div class="col-md-3">
                                                                            </div>
                                                                            <div class="col-md-6">
                                                                                <div class="seat-box">
                                                                                    <div class="uper-box">
                                                                                        <h5>Upper</h5>
                                                                                        <div class="row">
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row" style="margin-top: 25px;">
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-3">
                                                                            </div>
                                                                            <div class="col-md-6">
                                                                                <div class="seat-box">
                                                                                    <h5>Lower</h5>
                                                                                    <div class="row">
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>

                                                                                    </div>

                                                                                    <div class="row">
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1 col-3">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>

                                                                                    </div>

                                                                                    <div class="row" style="margin-top: 25px;">
                                                                                        <div class="col-md-2 col-4">
                                                                                            <div class="img-sec">
                                                                                               <img src="./images/seat-big.png" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-2 col-4">
                                                                                            <div class="img-sec">
                                                                                               <img src="./images/seat-big.png" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-2 col-4">
                                                                                            <div class="img-sec">
                                                                                               <img src="./images/seat-big.png" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-2 col-4">
                                                                                            <div class="img-sec">
                                                                                               <img src="./images/seat-big.png" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-2 col-4">
                                                                                            <div class="img-sec">
                                                                                               <img src="./images/seat-big.png" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-2 col-4">
                                                                                            <div class="img-sec">
                                                                                               <img src="./images/seat-big.png" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>


                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                        </div>
                                                                    </div>

                                                                    <div class="select-box">
                                                                        <div class="row">
                                                                            <div class="col-md-3"></div>
                                                                            <div class="col-md-6">
                                                                                <div class="row">
                                                                                    <div class="col-md-3 col-3">
                                                                                        <div class="img-sec">
                                                                                            <img src="./images/select-1.png">
                                                                                        </div>
                                                                                        <p>Booked</p>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-3">
                                                                                        <div class="img-sec">
                                                                                            <img src="./images/select-2.png">
                                                                                        </div>
                                                                                        <p>Available</p>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-3">
                                                                                        <div class="img-sec">
                                                                                            <img src="./images/select-3.png">
                                                                                        </div>
                                                                                        <p>Selected</p>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-3">
                                                                                        <div class="img-sec">
                                                                                            <img src="./images/select-4.png">
                                                                                        </div>
                                                                                        <p>ladies</p>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <!-- Modal footer -->
                                                            <div class="modal-footer">
                                                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Next</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td><b>Jakhar Travels And Cargo</b>
                                                <p>
                                                    NON A/C Seater / Sleeper (2+1)
                                                </p>
                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal1">
                                                    Cancellation Policy
                                                </button>
                                                <div class="modal fade" id="myModal1">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">

                                                            <!-- Modal Header -->
                                                            <div class="modal-header">
                                                                <h4 class="modal-title">Cancellation Policy</h4>
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                            </div>

                                                            <!-- Modal body -->
                                                            <div class="modal-body">
                                                                <div class="table-responsive">
                                                                    <table class="table table-striped">
                                                                        <thead>
                                                                            <tr>
                                                                                <th scope="col">Description </th>
                                                                                <th scope="col">Charges</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr>
                                                                                <th scope="row">After 31 Jul, 2021 10:30 AM </th>
                                                                                <td>100 %
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <th scope="row">Between 30 Jul, 2021 10:30 PM and 31 Jul, 2021 10:30 AM </th>
                                                                                <td>50 %
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <th scope="row">Between 28 Jul, 2021 10:30 PM and 30 Jul, 2021 10:30 PM </th>
                                                                                <td>10 %
                                                                                </td>

                                                                            </tr>
                                                                            <tr>
                                                                                <th scope="row">Between 24 Jul, 2021 10:30 PM and 28 Jul, 2021 10:30 PM  </th>
                                                                                <td>10 %
                                                                                </td>

                                                                            </tr>
                                                                            <tr>
                                                                                <th scope="row">Before 24 Jul, 2021 10:30 PM   </th>
                                                                                <td>10 %
                                                                                </td>

                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <span class="danger" style="color: #dc3545;">* Partial Cancellation Not Allowed
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <!-- Modal footer -->

                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>31 Jul, 21<br>
                                                <p>
                                                    10:30 PM
                                                </p>
                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal2">
                                                    Boarding Points
                                                </button>
                                                <div class="modal fade" id="myModal2">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">

                                                            <!-- Modal Header -->
                                                            <div class="modal-header">
                                                                <h4 class="modal-title">Boarding Points</h4>
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                            </div>

                                                            <!-- Modal body -->
                                                            <div class="modal-body">
                                                                <div class="table-responsive">
                                                                    <table class="table table-striped">
                                                                        <thead>
                                                                            <tr>
                                                                                <th scope="col">Time   </th>
                                                                                <th scope="col">Location</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr>
                                                                                <th scope="row">31 Jul 2021, 10:30 PM  </th>
                                                                                <td>Sindhi Camp
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <th scope="row">31 Jul 2021, 10:50 PM </th>
                                                                                <td>Others
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td><strong>
                                                <center><i class="fa fa-clock-o"></i><br>8h 5m</center>
                                            </strong></td>
                                            <td>31 Jul, 21<br>
                                                <p>
                                                    10:30 PM
                                                </p>
                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal3">
                                                    Dropping Points
                                                </button>
                                                <div class="table-responsive">
                                                    <div class="modal fade" id="myModal3">
                                                        <div class="modal-dialog">
                                                            <div class="modal-content">

                                                                <!-- Modal Header -->
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title">Boarding Points</h4>
                                                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                </div>

                                                                <!-- Modal body -->
                                                                <div class="modal-body">
                                                                    <div class="table-responsive">
                                                                        <table class="table table-striped">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th scope="col">Time   </th>
                                                                                    <th scope="col">Location</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <th scope="row">301 Aug 2021, 06:35 AM  </th>
                                                                                    <td>DalleKhan Chaki Opp IIT Acdmy Pal Road 9530068484 9530068282
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <th scope="row">01 Aug 2021, 06:35 AM </th>
                                                                                    <td>Near DalleKhan Ki Chakki Opp I.I.T. AcademyPalRoad9530068484
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <th scope="row">01 Aug 2021, 06:35 AM </th>
                                                                                    <td>Mandore Road Pasta 9530068282 95300684884
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>

                                                                    <!-- Modal footer -->


                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>Starting from
                                            </td>
                                            <td><b>31 Jul, 21</b>
                                                <p>
                                                    INR 300
                                                </p>
                                                <button type="button" class="btn btn-primary cdn" data-toggle="modal" data-target="#myModal4">
                                                    View Seats 
                                                </button>
                                                <div class="modal fade" id="myModal4">
                                                    <div class="modal-dialog modal-lg">
                                                        <div class="modal-content">

                                                            <!-- Modal Header -->
                                                            <div class="modal-header">
                                                                <select class="custom-select" id="gender2">
                                                                    <option selected="">Boarding Point</option>
                                                                    <option value="1">Male</option>
                                                                    <option value="2">Female</option>
                                                                </select>
                                                                <select class="custom-select" id="gender1">
                                                                    <option selected="">Dropping Point</option>
                                                                    <option value="1">Male</option>
                                                                    <option value="2">Female</option>
                                                                </select>
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                            </div>

                                                            <!-- Modal body -->
                                                            <div class="modal-body">

                                                                <div class="wrapper">
                                                                    <div class="container">

                                                                        <div class="row">
                                                                            <div class="col-md-3">
                                                                            </div>
                                                                            <div class="col-md-6">
                                                                                <div class="seat-box">
                                                                                    <div class="uper-box">
                                                                                        <h5>Upper</h5>
                                                                                        <div class="row">
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row" style="margin-top: 25px;">
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2 col-4">
                                                                                                <div class="img-sec">
                                                                                                   <img src="./images/seat-big.png" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-3">
                                                                            </div>
                                                                            <div class="col-md-6">
                                                                                <div class="seat-box">
                                                                                    <h5>Lower</h5>
                                                                                    <div class="row">
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>

                                                                                    </div>

                                                                                    <div class="row">
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <div class="img-sec">
                                                                                                <img src="images/seat-small.png">
                                                                                            </div>
                                                                                        </div>

                                                                                    </div>

                                                                                    <div class="row" style="margin-top: 25px;">
                                                                                        <div class="col-md-2 col-4">
                                                                                            <div class="img-sec">
                                                                                               <img src="./images/seat-big.png" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-2 col-4">
                                                                                            <div class="img-sec">
                                                                                               <img src="./images/seat-big.png" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-2 col-4">
                                                                                            <div class="img-sec">
                                                                                               <img src="./images/seat-big.png" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-2 col-4">
                                                                                            <div class="img-sec">
                                                                                               <img src="./images/seat-big.png" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-2 col-4">
                                                                                            <div class="img-sec">
                                                                                               <img src="./images/seat-big.png" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-2 col-4">
                                                                                            <div class="img-sec">
                                                                                               <img src="./images/seat-big.png" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>


                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                        </div>
                                                                    </div>

                                                                    <div class="select-box">
                                                                        <div class="row">
                                                                            <div class="col-md-3"></div>
                                                                            <div class="col-md-6">
                                                                                <div class="row">
                                                                                    <div class="col-md-3">
                                                                                        <div class="img-sec">
                                                                                            <img src="images/select-1.png">
                                                                                        </div>
                                                                                        <p>Booked</p>
                                                                                    </div>
                                                                                    <div class="col-md-3">
                                                                                        <div class="img-sec">
                                                                                            <img src="images/select-2.png">
                                                                                        </div>
                                                                                        <p>Available</p>
                                                                                    </div>
                                                                                    <div class="col-md-3">
                                                                                        <div class="img-sec">
                                                                                            <img src="images/select-3.png">
                                                                                        </div>
                                                                                        <p>Selected</p>
                                                                                    </div>
                                                                                    <div class="col-md-3">
                                                                                        <div class="img-sec">
                                                                                            <img src="images/select-4.png">
                                                                                        </div>
                                                                                        <p>ladies</p>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <!-- Modal footer -->
                                                            <div class="modal-footer">
                                                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Next</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
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
$(document).ready(function(){
// Add minus icon for collapse element which is open by default
$(".collapse.show").each(function(){
$(this).prev(".card-header").find(".fa").addClass("fa-minus").removeClass("fa-plus");
});
// Toggle plus minus icon on show hide of collapse element
$(".collapse").on('show.bs.collapse', function(){
$(this).prev(".card-header").find(".fa").removeClass("fa-plus").addClass("Hide ↑ fa-minus");
}).on('hide.bs.collapse', function(){
$(this).prev(".card-header").find(".fa").removeClass("fa-minus").addClass("fa-plus");
});
});
    </script>
</asp:Content>

