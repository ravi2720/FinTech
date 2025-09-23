<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="products.aspx.cs" Inherits="Member_products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
   
    <script src="../Component/Reuse.js"></script>
    <script src="../Component/ThemeEditor.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
  <div class="main-container container-fluid">
        <div class="main-content-body">
			<!-- container -->
			<div class="main-container container-fluid">


				<!-- row -->
				<div class="row row-sm" id="appHome">
					<div class="col-md-3 mb-3 mb-md-0">
						<div class="card">
							<h5 class="m-0 p-3 card-title bg-light border-bottom">Category</h5>

							<div class="py-2 px-3">
								<label class="p-1 mt-2 d-flex align-items-center"  v-for="itemC in AllCategory">
									<span class="checkbox">
										<span class="check-box mb-0">
											<span class="ckbox"><input  type="checkbox"><span></span></span>
										</span>
									</span>
									<span class="ms-2">
										{{itemC.Name}}
									</span>
								</label>								
							</div>

							<h5 class="m-0 p-3 card-title bg-light border-bottom border-top">Price</h5>
							<div class="p-3 d-flex align-items-center">
								<div class="w-50">
									<input placeholder="From" class="form-control rounded-0" />
								</div>
								<span class="h4 m-0 font-weight-normal px-2">-</span>
								<div class="w-50">
									<input placeholder="Up to" class="form-control rounded-0" />
								</div>
							</div>
							<h5 class="m-0 p-3 card-title bg-light border-bottom border-top">Ratings</h5>
							<div class="py-2 px-3">
								<label class="p-1 mt-2 d-flex align-items-center">
									<span class="check-box mb-0">
										<span class="ckbox"><input checked="" type="checkbox"><span></span></span>
									</span>
									<span class="ms-3 tx-16 my-auto">
										<i class="ion ion-md-star  text-warning"></i>
										<i class="ion ion-md-star  text-warning"></i>
										<i class="ion ion-md-star  text-warning"></i>
										<i class="ion ion-md-star  text-warning"></i>
										<i class="ion ion-md-star  text-warning"></i>
									</span>
								</label>
								<label class="p-1 mt-2 d-flex align-items-center">
									<span class="check-box mb-0">
										<span class="ckbox"><input checked="" type="checkbox"><span></span></span>
									</span>
									<span class="ms-3 tx-16 my-auto">
										<i class="ion ion-md-star  text-warning"></i>
										<i class="ion ion-md-star  text-warning"></i>
										<i class="ion ion-md-star  text-warning"></i>
										<i class="ion ion-md-star  text-warning"></i>
									</span>
								</label>
								<label class="p-1 mt-2 d-flex align-items-center">
									<span class="check-box mb-0">
										<span class="ckbox"><input checked="" type="checkbox"><span></span></span>
									</span>
									<span class="ms-3 tx-16 my-auto">
										<i class="ion ion-md-star  text-warning"></i>
										<i class="ion ion-md-star  text-warning"></i>
										<i class="ion ion-md-star  text-warning"></i>
									</span>
								</label>
								<label class="p-1 mt-2 d-flex align-items-center">
									<span class="checkbox mb-0">
										<span class="check-box">
											<span class="ckbox"><input type="checkbox"><span></span></span>
										</span>
									</span>
									<span class="ms-3 tx-16 my-auto">
										<i class="ion ion-md-star  text-warning"></i>
										<i class="ion ion-md-star  text-warning"></i>
									</span>
								</label>
								<label class="p-1 mt-2 d-flex align-items-center">
									<span class="checkbox mb-0">
										<span class="check-box">
											<span class="ckbox"><input type="checkbox"><span></span></span>
										</span>
									</span>
									<span class="ms-3 tx-16 my-auto">
										<i class="ion ion-md-star  text-warning"></i>
									</span>
								</label>
							</div>
							<div class="px-3 py-2 border-top">
								<button class="btn btn-danger btn-block">FILTER</button>
							</div>
						</div>
					</div>
					<div class="col-md-9">
						<div class="card">
							<div class="card-body p-2">
								<div class="input-group">
									<input type="text" class="form-control" placeholder="Search ...">
									<span class="input-group-append">
										<button class="btn btn-primary" type="button">Search</button>
									</span>
								</div>
							</div>
						</div>
						<div class="row row-sm">

							<div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-4"  v-for="item in Features" >
								<div class="product-card card">
									<div class="card-body h-100">
										<div class="d-flex">
											<span class="text-secondary small text-uppercase">{{item.Labels}}</span>
											<span class="ms-auto"><i class="fa fa-heart text-danger"></i></span>
										</div>
										<h3 class="h6 mb-2 font-weight-bold text-uppercase">{{item.ProductName}}</h3>
										<div class="d-flex">
											<h4 class="h5 w-50 font-weight-bold text-danger">₹{{item.NetAmount}} <span class="text-secondary font-weight-normal tx-13 ms-1 prev-price">₹{{item.Price}}</span></h4>
											<span class="tx-15 ms-auto">
												<i class="ion ion-md-star text-warning"></i>
												<i class="ion ion-md-star text-warning"></i>
												<i class="ion ion-md-star text-warning"></i>
												<i class="ion ion-md-star-half text-warning"></i>
												<i class="ion ion-md-star-outline text-warning"></i>
                                             
											</span>
										</div>
                                         <a v-bind:href="'product-details.aspx?ID='+item.ID">
										<img class="w-100 mt-2 mb-3" v-bind:src="'./images/'+item.ImageType" alt="product-image"/>
                                             </a>


											<a class="add-to-cart btn btn-success  btn-block my-1" type="button"  v-bind:href="'product-details.aspx?ID='+item.ID">ADD TO CART</a>
									</div>
								</div>
							</div>
                            

						</div>
					</div>
				</div>
				<!-- row closed -->
			</div>
			<!-- Container closed -->
		</div>
		<!-- main-content closed -->
           </div>
    
    <script type="text/javascript">

        new Vue({
            el: '#appHome',
            mixins: [toggle],
            data() {
                return {
                    AllCategory: [],
                    CategoryResult: [],
                    Features: [],

                }
            },

            methods: {
                GetAllCategory: function () {
                    ThemeEditor.loadingBar();
                    var el = this;
                    const article = {
                    }
                    axios.post("products.aspx/GetAllCategory", article)
                        .then(response => {
                            el.AllCategory = JSON.parse(response.data.d);
                            ThemeEditor.CloseloadingBar();
                        });
                },
                FeaturesData: function () {
                    ThemeEditor.loadingBar();
                    var el = this;
                    const article = {
                        "MethodName": "Product"
                    }
                    axios.post("products.aspx/GetFeaturesData", article)
                        .then(response => {                            
                            el.Features = JSON.parse(response.data.d);
                            ThemeEditor.CloseloadingBar();
                        });
                },
                CategoryData: function () {
                    ThemeEditor.loadingBar();
                    var el = this;
                    const article = {
                        "MethodName": "Product"
                    }
                    axios.post("products.aspx/GetProductData", article)
                        .then(response => {
                            el.CategoryResult = JSON.parse(response.data.d);
                            ThemeEditor.CloseloadingBar();
                        });
                },


                GetVal: function () {
                    var vars = [], hash;
                    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                    for (var i = 0; i < hashes.length; i++) {
                        hash = hashes[i].split('=');
                        vars.push(hash[0]);
                        vars[hash[0]] = hash[1];
                    }
                    return vars;
                },

            },

            mounted() {
                this.GetAllCategory();
                this.FeaturesData();
                this.CategoryData();

                $(".categories_title").on("click", function () {
                    $(this).toggleClass('active');
                    $('.categories_menu_toggle').slideToggle('medium');
                });

               
            }
        });
    </script>

</asp:Content>

