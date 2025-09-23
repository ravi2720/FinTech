<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="Member_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="./Component/resetpass.js"></script>
    <script src="./Component/resetpin.js"></script>
    <script src="./Component/validate.js"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- main-content -->
    <!-- template -->
    <script type="text/template" id="admin--validate">
            <div class="main-pattern-login fixed" style="z-index:101" :class="{ 'fade-out': success }"  ref="touchArea">
    <div class="loading" style="height:0.25em">
      <div v-if="isLoading" style="height:0.25em;width:20%;" class="animation-loading"></div>
    </div>
    <main draggable="false">
      <svg id="pattern-login" ref="svg" viewBox="0 0 30 30" draggable="false" ondragstart="return false;" :class="{ 'success': success, 'error': error }">
        <defs>
          <filter id="glow" width="1.5" height="1.5" x="-.25" y="-.25">
            <feGaussianBlur stdDeviation="0.25" result="coloredBlur"/>
            <feMerge>
                <feMergeNode in="coloredBlur"/>
                <feMergeNode in="SourceGraphic"/>
            </feMerge>
          </filter>
        </defs>
        <path style="-moz-user-select: none;-webkit-user-select: none;-ms-user-select: none;user-select: none;-webkit-user-drag: none;user-drag: none;" draggable="false" ondragstart="return false;" ref="indicator" id="indicator" :d="pathToSvg" stroke-linecap="round" stroke-linejoin="round"></path>
        <circle style="-moz-user-select: none;-webkit-user-select: none;-ms-user-select: none;user-select: none;-webkit-user-drag: none;user-drag: none;" draggable="false" ondragstart="return false;" v-for="coord, i in matrix" :key="i" ref="circle" :cx="coord[0]" :cy="coord[1]" r="2" :class="{ 'glow': (glowMatrix[i]) }" :data-key="i"></circle>
      </svg>
      <form id="login-form" method="post" action="phpApi/login.json" style="margin-top:-9999px">
        <input id="pattern-input" name="pattern" type="number" min="0" max="876543210" :value="pathToString">
        <button type="submit">Log in</button>
      </form>

    </main>
</div>
    </script>

        <!-- container -->
        <div class="main-container container-fluid" id="appprofile">

            <!-- row -->
            <div class="row row-sm">
                <div class="col-lg-3">
                    <div class="card mg-b-20">
                        <div class="card-body">
                            <div class="ps-0">
                                <div class="main-profile-overview">
                                    <div class="main-img-user profile-user">
                                        <img alt="" id="imgpic" src='<%= "./images/icon/"+dtMember.Rows[0]["Pic"].ToString() %>' class="userpic"><label for="pic" ><a   class="fas fa-camera profile-edit"  >  </a></label><input accept="image/*" id="pic" onchange="dddchange(this)" style="opacity:0;"   type="file" />
                                        
                                    </div>
                                    <div class="d-flex justify-content-between mg-b-20">
                                        <div>
                                            <h5 class="main-profile-name"><%= dtMember.Rows[0]["LoginID"] %></h5>
                                            <p class="main-profile-name-text"><%= dtMember.Rows[0]["RoleName"] %></p>
                                        </div>
                                    </div>
                                    <div id="#statustxt">0%</div>


                                </div>
                                <!-- main-profile-overview -->
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-8">

                    <div class="main-content-body main-content-body-profile">
                        <%--<validate v-if="!$store.state.ShowDiv"></validate>--%>
                        
                        <%--v-if="$store.state.ShowDiv"--%>
                        <div class="" >
                            <div class="wideget-user-tab">
                                <div class="tab-menu-heading">
                                    <div class="tabs-menu1">
                                        <ul class="nav">
                                            <li class=""><a href="#tab-51" class="active show" data-bs-toggle="tab">Profile</a></li>
                                            <li><a href="#tab-rpass" data-bs-toggle="tab" class="">Reset Password</a></li>
                                            <li><a href="#tab-rpin" data-bs-toggle="tab" class="">Reset Pin</a></li>
                                            <li><a href="#tab-kyc" data-bs-toggle="tab" class="">KYC</a></li>
                                            <li><a href="#tab-Account" data-bs-toggle="tab" class="">Account Details</a></li>
                                            <li><a href="#tab-71" data-bs-toggle="tab" class="">Gallery</a></li>
                                            <li><a href="#tab-81" data-bs-toggle="tab" class="">My Service</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--v-if="$store.state.ShowDiv"--%>
                        <div class="tab-content" >
                            <div class="tab-pane active show" id="tab-51">
                                <div id="profile-log-switch">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="card-header">
                                                <div class="media">
                                                    <div class="media-user me-2">
                                                        <div class="main-img-user avatar-md">
                                                            <img alt="" class="rounded-circle" src='<%= "./images/icon/"+dtMember.Rows[0]["Pic"].ToString() %>'>
                                                        </div>
                                                    </div>
                                                    <div class="media-body">
                                                        <h6 class="mb-0 mg-t-9"><%= dtMember.Rows[0]["name"] %></h6>
                                                        <span class="text-primary">Online</span>
                                                    </div>
                                                    <div class="ms-auto">
                                                        <div class="dropdown show">
                                                            <a class="new" data-bs-toggle="dropdown" href="JavaScript:void(0);"><i class="fas fa-ellipsis-v"></i></a>
                                                            <div class="dropdown-menu">
                                                                <a class="dropdown-item" href="#">Edit Post</a> <a class="dropdown-item" href="#">Delete Post</a> <a class="dropdown-item" href="#">Personal Settings</a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                          
                                      
 
                                                 <table class="table  table-bordered table-striped">
                                        <tr>
                                            <th>Name</th>
                                            <td><%= dtMember.Rows[0]["name"] %></td>
                                           
                                        </tr>
                                    
                                        <tr>
                                            <th>Address</th>
                                            <td><%= dtMember.Rows[0]["address"] %></td>
                                        </tr>
                                        <tr>
                                            <th>Mobile Number</th>                                            
                                             <td><%= dtMember.Rows[0]["mobile"] %></td>
                                        </tr>
                                        <tr>
                                            <th>Email-id</th>                                            
                                               <td><%= dtMember.Rows[0]["email"] %></td>
                                        </tr>
                                        <tr>
                                            <th>Aadhar</th>
                                           <td><%= dtMember.Rows[0]["aadhar"] %></td>
                                        </tr>
                                         <tr>
                                            <th>Pan</th>
                                           <td><%= dtMember.Rows[0]["Pan"] %></td>
                                        </tr>
                                        
                                       
                                    </table>
                                                                                       
                                        </div>
                                    </div>
                                  
                                </div>
                            </div>
                            <div class="tab-pane" id="tab-rpass">
                                <ul class="widget-users row ps-0 mb-5">
                                    <li class="col-xl-12 col-lg-12  col-md-12 col-sm-12 col-12">
                                        <div class="card border p-0">
                                            <div class="card-header font-weight-bold">
                                                Reset Password
                                            </div>
                                            <div class="card-body">
                                                <reset-password inline-template auth="2" title="Reset Password">
                                                    <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="control-group form-group">
                                                            <label class="form-label">Old Password</label>
                                                            <input type="password" class="form-control required" @change="CheckPass" autocomplete="off" v-model="oldPass" placeholder="Enter Old Password" />
                                                        </div>
                                                        <div class="control-group form-group">
                                                            <label class="form-label">New Password</label>
                                                            <input id="password" @keyup="keyup" v-model="NewPass" class="form-control input-group"
                                                                name="password" type="password"
                                                                placeholder="Enter your password" />
                                                        </div>
                                                        <div class="control-group form-group">
                                                            <input type="button" class="btn btn-primary" v-on:click="ChangePass" v-bind:disabled="disabled" value="Change Password" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">

                                                        <div class="form-group">
                                                            <div id="popover-password">
                                                                <p><span id="result"></span></p>
                                                                <div class="progress">
                                                                    <div id="password-strength"
                                                                        class="progress-bar"
                                                                        role="progressbar"
                                                                        aria-valuenow="40"
                                                                        aria-valuemin="0"
                                                                        aria-valuemax="100"
                                                                        style="width: 0%">
                                                                    </div>
                                                                </div>
                                                                <ul class="list-unstyled">
                                                                    <li class="">
                                                                        <span class="low-upper-case">
                                                                            <i class="fas fa-circle" aria-hidden="true"></i>
                                                                            &nbsp;Lowercase &amp; Uppercase
                                                                        </span>
                                                                    </li>
                                                                    <li class="">
                                                                        <span class="one-number">
                                                                            <i class="fas fa-circle" aria-hidden="true"></i>
                                                                            &nbsp;Number (0-9)
                                                                        </span>
                                                                    </li>
                                                                    <li class="">
                                                                        <span class="one-special-char">
                                                                            <i class="fas fa-circle" aria-hidden="true"></i>
                                                                            &nbsp;Special Character (!@#$%^&*)
                                                                        </span>
                                                                    </li>
                                                                    <li class="">
                                                                        <span class="eight-character">
                                                                            <i class="fas fa-circle" aria-hidden="true"></i>
                                                                            &nbsp;Atleast 8 Character
                                                                        </span>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </reset-password>
                                            </div>
                                            <div class="card-footer text-center">
                                                <div class="row user-social-detail">
                                                    <div class="alert alert-danger mg-b-0" role="alert">
                                                        <strong>Oh snap!</strong> Change a few things up and try submitting again.
                                                   
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="tab-pane" id="tab-rpin">
                                <ul class="widget-users row ps-0 mb-5">
                                    <li class="col-xl-12 col-lg-12  col-md-12 col-sm-12 col-12">
                                        <div class="card border p-0">
                                            <div class="card-header font-weight-bold">
                                                Reset Pin
                                            </div>
                                            <div class="card-body">
                                                <reset-pin inline-template auth="2" title="Reset Password">
                                                    <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="control-group form-group">
                                                            <label class="form-label">Old Pin</label>
                                                            <input type="password" @change="CheckPin" class="form-control required" v-model="oldPin" placeholder="Enter Old Pin" />
                                                        </div>
                                                        <div class="control-group form-group">
                                                            <label class="form-label">New Pin</label>
                                                            <input  id="newpin"  v-model="NewPin" class="form-control input-group"
                                                                name="password" type="password"
                                                                placeholder="Enter your New PIN" />
                                                        </div>
                                                        <div class="control-group form-group">
                                                            <input type="button" v-on:click="ChangePin" v-bind:disabled="disabledPin" class="btn btn-primary" value="Verify" />
                                                        </div>
                                                    </div>
                                                </div>
                                                </reset-pin>
                                            </div>
                                            <div class="card-footer text-center">
                                                <div class="row user-social-detail">
                                                    <div class="alert alert-danger mg-b-0" role="alert">
                                                        <strong>Oh snap!</strong> Change a few things up and try submitting again.
                                                   
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="tab-pane" id="tab-kyc">
                                <ul class="widget-users row ps-0 mb-5">

                                    <li class="col-xl-4 col-lg-6  col-md-6 col-sm-12 col-12"  v-for="itm in KYCList">
                                        <div class="card border p-0 mb-0">
                                            <div class="card-body text-center">
                                                <img class="aavatar brround avatar-xxl me-3 cover-image" v-bind:src="'./images/KYC/'+itm.DocImage" />
                                                <a href="profile.html">
                                                    <h5 class="fs-16 mb-0 mt-3 text-dark fw-semibold">{{itm.DocName}}</h5>
                                                </a>
                                                <span class="text-muted">{{itm.DocNumber}}</span>
                                            </div>
                                            <div class="card-footer text-center">
                                                <div class="row user-social-detail">
                                                    <a href="javascript:void(0);" class="social-profile me-4 rounded text-center bg-primary-transparent">
                                                        <i class="fab fa-google"></i>
                                                    </a>
                                                    <a href="javascript:void(0);" class="social-profile me-4 rounded text-center bg-primary-transparent">
                                                        <i class="fab fa-facebook"></i>
                                                    </a>
                                                    <a href="javascript:void(0);" class="social-profile  rounded text-center bg-primary-transparent">
                                                        <i class="fab fa-twitter"></i>
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="tab-pane" id="tab-Account">
                                <ul class="widget-users row ps-0 mb-5">

                                    <li class="col-xl-4 col-lg-6  col-md-6 col-sm-12 col-12" v-for="item in AccountList">
                                        <div class="card border p-0 mb-0">
                                            <div class="card-body text-center">
                                                <img class="aavatar brround avatar-xxl me-3 cover-image" src="./images/icon/Bank.png" />
                                                <a href="profile.html">
                                                    <h5 class="fs-16 mb-0 mt-3 text-dark fw-semibold">{{item.Name}}</h5>
                                                </a>
                                                <span class="text-muted">{{item.AccountNumber}}({{item.IFSCCode}})</span>
                                            </div>
                                            <div class="card-footer text-center">
                                                <div class="row user-social-detail">
                                                    <a href="javascript:void(0);" class="me-4  text-center bg-primary-transparent">
                                                       {{item.AccountHolderName}}
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </li>
                                    <li class="col-xl-4 col-lg-6  col-md-6 col-sm-12 col-12">
                                        <div class="card border p-0 mb-0">
                                            <a href="BankDetails.aspx">
                                            <div class="card-body text-center">
                                                <span class="aavatar brround avatar-xxl me-3 cover-image"><i class="fe fe-plus"></i></span>
                                                
                                                    <h5 class="fs-16 mb-0 mt-3 text-dark fw-semibold">Bank Details</h5>
                                                
                                                <span class="text-muted"></span>
                                            </div>
                                           </a>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="tab-pane" id="tab-71">
                                <div class="row  mb-5 img-gallery" id="lightgallery">
                                    <div class="col-lg-3 col-md-6" data-responsive="../assets/img/media/1.jpg" data-src="../assets/img/media/1.jpg">
                                        <a href="javascript:void(0);">
                                            <img class="img-fluid mb-2 br-7" src="./assets/img/media/1.jpg " alt="banner image"></a>
                                    </div>
                                    <div class="col-lg-3 col-md-6" data-responsive="./assets/img/media/2.jpg" data-src="../assets/img/media/2.jpg">
                                        <a href="javascript:void(0);">
                                            <img class="img-fluid mb-2 br-7" src="./assets/img/media/2.jpg" alt="banner image "></a>
                                    </div>
                                    <div class="col-lg-3 col-md-6" data-responsive="./assets/img/media/3.jpg" data-src="../assets/img/media/3.jpg">
                                        <a href="javascript:void(0);">
                                            <img class="img-fluid  mb-2 br-7" src="./assets/img/media/3.jpg" alt="banner image "></a>
                                    </div>
                                    <div class="col-lg-3 col-md-6" data-responsive="./assets/img/media/4.jpg" data-src="../assets/img/media/4.jpg">
                                        <a href="javascript:void(0);">
                                            <img class="img-fluid  mb-2 br-7" src="./assets/img/media/4.jpg" alt="banner image "></a>
                                    </div>
                                    <div class="col-lg-3 col-md-6" data-responsive="./assets/img/media/5.jpg" data-src="../assets/img/media/5.jpg">
                                        <a href="javascript:void(0);">
                                            <img class="img-fluid mb-2 br-7" src="./assets/img/media/5.jpg " alt="banner image"></a>
                                    </div>
                                    <div class="col-lg-3 col-md-6" data-responsive="./assets/img/media/6.jpg" data-src="../assets/img/media/6.jpg">
                                        <a href="javascript:void(0);">
                                            <img class="img-fluid mb-2 br-7" src="./assets/img/media/6.jpg" alt="banner image "></a>
                                    </div>
                                    <div class="col-lg-3 col-md-6" data-responsive="./assets/img/media/7.jpg" data-src="../assets/img/media/7.jpg">
                                        <a href="javascript:void(0);">
                                            <img class="img-fluid  mb-2 br-7" src="./assets/img/media/7.jpg" alt="banner image "></a>
                                    </div>
                                    <div class="col-lg-3 col-md-6" data-responsive="./assets/img/media/1.jpg" data-src="../assets/img/media/1.jpg">
                                        <a href="javascript:void(0);">
                                            <img class="img-fluid  mb-2 br-7" src="./assets/img/media/1.jpg" alt="banner image "></a>
                                    </div>
                                    <div class="col-lg-3 col-md-6" data-responsive="./assets/img/media/8.jpg" data-src="../assets/img/media/8.jpg">
                                        <a href="javascript:void(0);">
                                            <img class="img-fluid mb-2 br-7" src="./assets/img/media/8.jpg " alt="banner image"></a>
                                    </div>
                                    <div class="col-lg-3 col-md-6" data-responsive="./assets/img/media/3.jpg" data-src="../assets/img/media/3.jpg">
                                        <a href="javascript:void(0);">
                                            <img class="img-fluid mb-2 br-7" src="./assets/img/media/3.jpg" alt="banner image "></a>
                                    </div>
                                    <div class="col-lg-3 col-md-6" data-responsive="./assets/img/media/4.jpg" data-src="../assets/img/media/4.jpg">
                                        <a href="javascript:void(0);">
                                            <img class="img-fluid  mb-2 br-7" src="./assets/img/media/4.jpg" alt="banner image "></a>
                                    </div>
                                    <div class="col-lg-3 col-md-6" data-responsive="./assets/img/media/2.jpg" data-src="../assets/img/media/2.jpg">
                                        <a href="javascript:void(0);">
                                            <img class="img-fluid  mb-2 br-7" src="./assets/img/media/2.jpg" alt="banner image "></a>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="tab-71">
                                <div class="row  mb-5">
                                    <div class="col-xl-6 col-lg-12 col-md-12">
                                        <div class="card border p-0 over-flow-hidden">
                                            <div class="media card-body media-xs overflow-visible ">
                                                <img class="avatar brround avatar-md me-3" src="../assets/img/faces/10.jpg" alt="avatar-img">
                                                <div class="media-body valign-middle">
                                                    <a href="" class=" fw-semibold text-dark">John Paige</a>
                                                    <p class="text-muted mb-0">johan@gmail.com</p>
                                                </div>
                                                <div class="media-body valign-middle text-end overflow-visible mt-2">
                                                    <button class="btn btn-primary" type="button">Follow</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                </div>
                            </div>
                            <div class="tab-pane" id="tab-81">
                                <div class="row">

                                    <div class="col-xl-6 col-lg-12 col-md-12" v-for="item in ServiceList">
                                        <div class="card border p-0 over-flow-hidden">
                                            <div class="media card-body media-xs overflow-visible ">
                                                <img class=" brround avatar-md me-3" v-bind:src="'./images/icon/'+item.Image" alt="avatar-img" />
                                                <div class="media-body valign-middle">
                                                    <a href="" class=" fw-semibold text-dark">{{item.Name}}</a>
                                                </div>
                                                <div class="media-body valign-middle text-end overflow-visible mt-2">
                                                    <button class="btn btn-primary" type="button">{{(item.ActiveOrNot==0?"Follow":"Following")}}</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                   
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- row closed -->
        </div>
        <!-- Container closed -->


    <script>
        Vue.config.devtools = false;
        Vue.config.productionTip = false;
        const store = new Vuex.Store({
            state: {
                ShowDiv: false
            },
            mutations: {
                Change(state, value) {
                    debugger;
                    state.ShowDiv = value
                }
            }
        });
        const app = new Vue({

            el: '#appprofile',
            store,
            data() {
                return {
                    ServiceList: [],
                    AccountList: [],
                    KYCList: []
                }
            },
            methods: {
                GetList() {
                    var el = this;
                    const article = {
                    }
                    axios.post("Profile.aspx/GetServiceList", article)
                        .then(response => {
                            el.ServiceList = JSON.parse(response.data.d);
                        });
                },
                GetAccountList() {
                    var el = this;
                    const article = {
                    }
                    axios.post("Profile.aspx/GetBankAccountList", article)
                        .then(response => {
                            el.AccountList = JSON.parse(response.data.d);
                        });
                },
                GetKYCList() {
                    var el = this;
                    const article = {
                    }
                    axios.post("Profile.aspx/GetKYCList", article)
                        .then(response => {
                            el.KYCList = JSON.parse(response.data.d);
                        });
                }

            },
            mounted() {
                var el = this;
                el.GetList();
                el.GetAccountList();
                el.GetKYCList();
            }
        });
        function onpicchange() {
            const [file] = pic.files
            if (file) {
                imgpic.src = URL.createObjectURL(file)
            }
        }

        var _URL = window.URL || window.webkitURL;
        function dddchange(objData) {
            document.getElementById('load').style.visibility = "visible";
            var file, img;
            if ((file = objData.files[0])) {
                const [files] = pic.files

                $(".userpic").each(function (index) {
                    $(this).attr("src", URL.createObjectURL(files));
                });
                // imgpic.src = URL.createObjectURL(files);

                UplaodFile(file);


            }
        };

        function UplaodFile(file) {

            var formData = new FormData();
            formData.append('file', $('#pic')[0].files[0]);
            $.ajax({
                type: 'post',
                url: './Handlers/fileUploader.ashx',
                data: formData,
                success: function (status) {
                    if (status != 'error') {
                        document.getElementById('load').style.visibility = "hidden";
                    }
                },
                processData: false,
                contentType: false,
                error: function () {
                    alert("Whoops something went wrong!");
                }
            });
        }

        function progressHandlingFunction(e) {
            if (e.lengthComputable) {
                var percentage = Math.floor((e.loaded / e.total) * 100);
                //update progressbar percent complete
                statustxt.html(percentage + '%');
                console.log("Value = " + e.loaded + " :: Max =" + e.total);
            }
        }
    </script>
</asp:Content>

