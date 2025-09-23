<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="product-cart.aspx.cs" Inherits="Member_product_cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script src="../Component/Reuse.js"></script>
    <script src="../Component/ThemeEditor.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="main-container container-fluid">
        <div class="main-content-body" id="appHome">
            
				<!-- row opened -->
				<div class="row" >
					<div class="col-md-12 col-xl-8" >
						<div class="card">
							<div class="card-body">
								<div class="product-details table-responsive text-nowrap">
									<table class="table table-bordered table-hover mb-0 text-nowrap">
										<thead>
											<tr>
												<th>Product</th>
												<th class="w-150">Quantity</th>
												<th >Price</th>
												<th >Action</th>
											</tr>
										</thead>
										<tbody>
											<tr v-for="itemAC in AddToCartData">
												<td>
													<div class="media">
														<div class="card-aside-img">
															<img v-bind:src="'./images/'+itemAC.Img" alt="img" class="h-60 w-60">
														</div>
														<div class="media-body">
															<div class="card-item-desc mt-0">
																<h6 class="font-weight-semibold mt-0 text-uppercase">{{itemAC.Name}}</h6>
																<dl class="card-item-desc-1">
																  <dt>Size: </dt>
																  <dd>XXL</dd>
																</dl>
																<dl class="card-item-desc-1">
																  <dt>Color: </dt>
																  <dd>Green and Black color</dd>
																</dl>
															</div>
														</div>
													</div>
												</td>
												<td>
													<div class="form-group">
														<select name="quantity" id="select-countries" class="form-control form-select select2">
															<option value="1" selected>1</option>
															<option value="2">2</option>
															<option value="3" >3</option>
															<option value="4">4</option>
														</select>
													</div>
												</td>
												<td>₹{{AddToCartData.reduce((a, b) => a + b.Amount, 0)}}</td>
												<td>
													<a class="btn btn-danger btn-sm text-white" data-bs-toggle="tooltip" data-bs-original-title="Delete"  v-on:click="DeleteCartServer(itemAC)"><i class="fe fe-trash"></i></a>
													<a class="btn btn-info btn-sm text-white" data-bs-toggle="tooltip" data-bs-original-title="Save to Wishlist"><i class="fe fe-heart"></i></a>
												</td>
											</tr>
											<%--<tr>
												<td>
													<div class="media">
														<div class="card-aside-img">
															<img src="../assets/img/ecommerce/06.jpg" alt="img" class="h-60 w-60">
														</div>
														<div class="media-body">
															<div class="card-item-desc mt-0">
																<h6 class="font-weight-semibold mt-0 text-uppercase">Digital Watch</h6>
																<dl class="card-item-desc-1">
																  <dt>Size: </dt>
																  <dd>XL</dd>
																</dl>
																<dl class="card-item-desc-1">
																  <dt>Color: </dt>
																  <dd>Black color</dd>
																</dl>
															</div>
														</div>
													</div>
												</td>
												<td>
													<div class="form-group">
														<select name="quantity" id="select-countries1" class="form-control form-select select2">
															<option value="1" selected>1</option>
															<option value="2">2</option>
															<option value="3" >3</option>
															<option value="4">4</option>
														</select>
													</div>
												</td>
												<td>USD 15</td>
												<td>
													<a class="btn btn-danger btn-sm text-white" data-bs-toggle="tooltip" data-bs-original-title="Delete"><i class="fe fe-trash"></i></a>
													<a class="btn btn-info btn-sm text-white" data-bs-toggle="tooltip" data-bs-original-title="Save to Wishlist"><i class="fe fe-heart"></i></a>
												</td>
											</tr>
											<tr>
												<td>
													<div class="media">
														<div class="card-aside-img">
															<img src="../assets/img/ecommerce/08.jpg" alt="img" class="h-60 w-60">
														</div>
														<div class="media-body">
															<div class="card-item-desc mt-0">
																<h6 class="font-weight-semibold mt-0 text-uppercase">Western Wear</h6>
																<dl class="card-item-desc-1">
																  <dt>Size: </dt>
																  <dd>XL</dd>
																</dl>
																<dl class="card-item-desc-1">
																  <dt>Color: </dt>
																  <dd>LightPink color</dd>
																</dl>
															</div>
														</div>
													</div>
												</td>
												<td>
													<div class="form-group">
														<select name="quantity" id="select-countries2" class="form-control form-select select2">
															<option value="1" selected>1</option>
															<option value="2">2</option>
															<option value="3" >3</option>
															<option value="4">4</option>
														</select>
													</div>
												</td>
												<td>USD 136</td>
												<td>
													<a class="btn btn-danger btn-sm text-white" data-bs-toggle="tooltip" data-bs-original-title="Delete"><i class="fe fe-trash"></i></a>
													<a class="btn btn-info btn-sm text-white" data-bs-toggle="tooltip" data-bs-original-title="Save to Wishlist"><i class="fe fe-heart"></i></a>
												</td>
											</tr>
											<tr>
												<td>
													<div class="media">
														<div class="card-aside-img">
															<img src="../assets/img/ecommerce/02.jpg" alt="img" class="h-60 w-60">
														</div>
														<div class="media-body">
															<div class="card-item-desc mt-0">
																<h6 class="font-weight-semibold mt-0 text-uppercase">Womens Handbag</h6>
																<dl class="card-item-desc-1">
																  <dt>Size: </dt>
																  <dd>11-12 inches</dd>
																</dl>
																<dl class="card-item-desc-1">
																  <dt>Color: </dt>
																  <dd>LightGray color</dd>
																</dl>
															</div>
														</div>
													</div>
												</td>
												<td>
													<div class="form-group">
														<select name="quantity" id="select-countries3" class="form-control form-select select2">
															<option value="1" selected>1</option>
															<option value="2">2</option>
															<option value="3" >3</option>
															<option value="4">4</option>
														</select>
													</div>
												</td>
												<td>USD 274</td>
												<td>
													<a class="btn btn-danger btn-sm text-white" data-bs-toggle="tooltip" data-bs-original-title="Delete"><i class="fe fe-trash"></i></a>
													<a class="btn btn-info btn-sm text-white" data-bs-toggle="tooltip" data-bs-original-title="Save to Wishlist"><i class="fe fe-heart"></i></a>
												</td>
											</tr>--%>
										</tbody>
									</table>
								</div>
							</div>
						</div>
					</div>
					<div class="col-md-12 col-xl-4">
						<div class="card">
							<div class="card-header pb-0">
								<h3 class="card-title mb-0">Estimate Shipping and tax</h3>
							</div>
							<div class="card-body">
								<div class="form-group mb-4">
									<div class=" gutters-xs">
											<p class="mg-b-10">Country</p>
											<select class="form-control select2-no-search">
												<option label="Choose one">
												</option>
											<option label="Choose one"></option>
											<option value="UM">United States of America</option>
											<option value="AF">Afghanistan</option>
											<option value="AL">Albania</option>
											<option value="AD">Andorra</option>
											<option value="AG">Antigua and Barbuda</option>
											<option value="AU">Australia</option>
											<option value="AM">Armenia</option>
											<option value="AO">Angola</option>
											<option value="AR">Argentina</option>
											<option value="AT">Austria</option>
											<option value="AZ">Azerbaijan</option>
											<option value="BA">Bosnia and Herzegovina</option>
											<option value="BB">Barbados</option>
											<option value="BD">Bangladesh</option>
											<option value="BE">Belgium</option>
											<option value="BF">Burkina Faso</option>
											<option value="BG">Bulgaria</option>
											<option value="BH">Bahrain</option>
											<option value="BJ">Benin</option>
											<option value="BN">Brunei</option>
											<option value="BO">Bolivia</option>
											<option value="BT">Bhutan</option>
											<option value="BY">Belarus</option>
											<option value="CD">Congo</option>
											<option value="CA">Canada</option>
											<option value="CF">Central African Republic</option>
											<option value="CI">Cote d'Ivoire</option>
											<option value="CL">Chile</option>
											<option value="CM">Cameroon</option>
											<option value="CN">China</option>
											<option value="CO">Colombia</option>
											<option value="CU">Cuba</option>
											<option value="CV">Cabo Verde</option>
											<option value="CY">Cyprus</option>
											<option value="DJ">Djibouti</option>
											<option value="DK">Denmark</option>
											<option value="DM">Dominica</option>
											<option value="DO">Dominican Republic</option>
											<option value="EC">Ecuador</option>
											<option value="EE">Estonia</option>
											<option value="ER">Eritrea</option>
											<option value="ET">Ethiopia</option>
											<option value="FI">Finland</option>
											<option value="FJ">Fiji</option>
											<option value="FR">France</option>
											<option value="GA">Gabon</option>
											<option value="GD">Grenada</option>
											<option value="GE">Georgia</option>
											<option value="GH">Ghana</option>
											<option value="GH">Ghana</option>
											<option value="HN">Honduras</option>
											<option value="HT">Haiti</option>
											<option value="HU">Hungary</option>
											<option value="ID">Indonesia</option>
											<option value="IE">Ireland</option>
											<option value="IL">Israel</option>
											<option value="IN">India</option>
											<option value="IQ">Iraq</option>
											<option value="IR">Iran</option>
											<option value="IS">Iceland</option>
											<option value="IT">Italy</option>
											<option value="JM">Jamaica</option>
											<option value="JO">Jordan</option>
											<option value="JP">Japan</option>
											<option value="KE">Kenya</option>
											<option value="KG">Kyrgyzstan</option>
											<option value="KI">Kiribati</option>
											<option value="KW">Kuwait</option>
											<option value="KZ">Kazakhstan</option>
											<option value="LA">Laos</option>
											<option value="LB">Lebanons</option>
											<option value="LI">Liechtenstein</option>
											<option value="LR">Liberia</option>
											<option value="LS">Lesotho</option>
											<option value="LT">Lithuania</option>
											<option value="LU">Luxembourg</option>
											<option value="LV">Latvia</option>
											<option value="LY">Libya</option>
											<option value="MA">Morocco</option>
											<option value="MC">Monaco</option>
											<option value="MD">Moldova</option>
											<option value="ME">Montenegro</option>
											<option value="MG">Madagascar</option>
											<option value="MH">Marshall Islands</option>
											<option value="MK">Macedonia (FYROM)</option>
											<option value="ML">Mali</option>
											<option value="MM">Myanmar (formerly Burma)</option>
											<option value="MN">Mongolia</option>
											<option value="MR">Mauritania</option>
											<option value="MT">Malta</option>
											<option value="MV">Maldives</option>
											<option value="MW">Malawi</option>
											<option value="MX">Mexico</option>
											<option value="MZ">Mozambique</option>
											<option value="NA">Namibia</option>
											<option value="NG">Nigeria</option>
											<option value="NO">Norway</option>
											<option value="NP">Nepal</option>
											<option value="NR">Nauru</option>
											<option value="NZ">New Zealand</option>
											<option value="OM">Oman</option>
											<option value="PA">Panama</option>
											<option value="PF">Paraguay</option>
											<option value="PG">Papua New Guinea</option>
											<option value="PH">Philippines</option>
											<option value="PK">Pakistan</option>
											<option value="PL">Poland</option>
											<option value="QA">Qatar</option>
											<option value="RO">Romania</option>
											<option value="RU">Russia</option>
											<option value="RW">Rwanda</option>
											<option value="SA">Saudi Arabia</option>
											<option value="SB">Solomon Islands</option>
											<option value="SC">Seychelles</option>
											<option value="SD">Sudan</option>
											<option value="SE">Sweden</option>
											<option value="SG">Singapore</option>
											<option value="TG">Togo</option>
											<option value="TH">Thailand</option>
											<option value="TJ">Tajikistan</option>
											<option value="TL">Timor-Leste</option>
											<option value="TM">Turkmenistan</option>
											<option value="TN">Tunisia</option>
											<option value="TO">Tonga</option>
											<option value="TR">Turkey</option>
											<option value="TT">Trinidad and Tobago</option>
											<option value="TW">Taiwan</option>
											<option value="UA">Ukraine</option>
											<option value="UG">Uganda</option>
											<option value="UY">Uruguay</option>
											<option value="UZ">Uzbekistan</option>
											<option value="VA">Vatican City (Holy See)</option>
											<option value="VE">Venezuela</option>
											<option value="VN">Vietnam</option>
											<option value="VU">Vanuatu</option>
											<option value="YE">Yemen</option>
											<option value="ZM">Zambia</option>
											<option value="ZW">Zimbabwe</option>
										</select>
									</div>
									<label class="form-label mt-4 fs-15">Expiration Date</label>
									<div class="gutters-xs">
										<select name="alabama" class="form-control select2-no-search">
											<option label="Choose one"></option>
											<option value="">Alabama</option>
											<option value="Alabama">Alabama</option>
											<option value="Alaska">Alaska</option>
											<option value="American Samoa">American Samoa</option>
											<option value="Arkansas">Arkansas</option>
											<option value="California">California</option>
											<option value="Colorado">Colorado</option>
											<option value="Connecticut">Connecticut</option>
											<option value="Delaware">Delaware</option>
											<option value="Florida">Florida</option>
											<option value="Georgia">Georgia</option>
											<option value="Idaho">Idaho</option>
										</select>
									</div>
									<label class="form-label mt-4 fs-15">Zip/Postal Code</label>
									<div class="form-group mb-0">
										<div class="form-group">
											<input type="number" class="form-control" id="postal" placeholder="Zip/Postal">
										</div>
									</div>
								</div>
								<div class="form-footer mt-2">
									<a   href="javascript:void(0);" class="btn btn-primary">GET A QUOTE</a>
								</div>
							</div>
						</div>

					<div class="col-lg-12">
						<div class="card">
							<div class="card-header pb-0">
								<div class="card-title mb-0">Order Summery</div>

							</div>
							<div class="card-body">
								<div class="row mb-4">
									<div class="col-6"><input class="productcart form-control" type="text" placeholder="Coupon Code"></div>
									<div class="col-6"><a   href="javascript:void(0);" class="btn btn-primary btn-md">Apply</a></div>
								</div>
								<div class="table-responsive">
									<table class="table table-bordered">
										<tbody>
											<tr>
												<td>Cart Subtotal</td>
												<td class="text-end">₹{{AddToCartData.reduce((a, b) => a + b.Amount, 0)}}</td>
											</tr>
											<tr>
												<td><span>Totals</span></td>
												<td class="text-end text-muted"><span>₹{{AddToCartData.reduce((a, b) => a + b.Amount, 0)}}</span></td>
											</tr>
											<tr>
												<td><span>Order Total</span></td>
												<td><h2 class="price text-end mb-0">₹{{AddToCartData.reduce((a, b) => a + b.Amount, 0)}}</h2></td>
											</tr>
										</tbody>
									</table>
								</div>
								<form class="text-center">
									<button class="btn btn-secondary float-start mt-2 m-b-20 " type="submit" value="Proceed To Checkout">Proceed To Checkout</button>
									<button class="btn btn-success mt-2 float-end" type="submit" value="Continue Shopping">Continue Shopping</button>
								</form>
							</div>
						</div>
					</div>
					</div>
				</div>
				<!-- row closed -->
            </div>
         </div>
    
     <script type="text/javascript">

        new Vue({
            el: '#appHome',
            mixins: [toggle],
            data() {
                return {
                    AllCategory: [],
                    CategoryProductResult: [],
                }
            },

            methods: {

                CategoryProductData: function () {
                    ThemeEditor.loadingBar();
                    var el = this;
                    const article = {
                        "MethodName": "Product",
                        "ID": this.GetVal()["CateID"]
                    }
                    axios.post("products.aspx/GetCategoryProductData", article)
                        .then(response => {
                            el.CategoryProductResult = JSON.parse(response.data.d);

                            ThemeEditor.CloseloadingBar();
                        });
                },

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

                if (this.GetVal()["CateID"] != null) {
                    this.CategoryProductData();
                }

                this.GetAllCategory();


            }
        });
    </script>
</asp:Content>

