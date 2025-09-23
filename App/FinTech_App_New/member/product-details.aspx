<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="product-details.aspx.cs" Inherits="Member_product_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../Component/Reuse.js"></script>
    <script src="../Component/ThemeEditor.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-container container-fluid" id="appHome">
        <div class="main-content-body">





            <!-- row -->
            <div class="row row-sm" v-for="item in GetResult">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body h-100">
                            <div class="row row-sm ">
                                <div class=" col-xl-5 col-lg-12 col-md-12">



                                    <div class="preview-pic tab-content">
                                        <div class="tab-pane active" id="pic-1">
                                            <img src="../assets/img/ecommerce/5.jpg" alt="image" /></div>
                                        <div class="tab-pane" id="pic-2">
                                            <img src="../assets/img/ecommerce/4.jpg" alt="image" /></div>
                                        <div class="tab-pane" id="pic-3">
                                            <img src="../assets/img/ecommerce/6.jpg" alt="image" /></div>
                                        <div class="tab-pane" id="pic-4">
                                            <img src="../assets/img/ecommerce/7.jpg" alt="image" /></div>
                                        <div class="tab-pane" id="pic-5">
                                            <img src="../assets/img/ecommerce/3.jpg" alt="image" /></div>
                                    </div>



                                    <ul class="preview-thumbnail nav nav-tabs">
                                        <li class="active"><a data-bs-target="#pic-1" data-bs-toggle="tab">
                                            <img src="../assets/img/ecommerce/5.jpg" alt="image" /></a></li>
                                        <li><a data-bs-target="#pic-2" data-bs-toggle="tab">
                                            <img src="../assets/img/ecommerce/4.jpg" alt="image" /></a></li>
                                        <li><a data-bs-target="#pic-3" data-bs-toggle="tab">
                                            <img src="../assets/img/ecommerce/6.jpg" alt="image" /></a></li>
                                        <li><a data-bs-target="#pic-4" data-bs-toggle="tab">
                                            <img src="../assets/img/ecommerce/7.jpg" alt="image" /></a></li>
                                        <li><a data-bs-target="#pic-5" data-bs-toggle="tab">
                                            <img src="../assets/img/ecommerce/3.jpg" alt="image" /></a></li>
                                    </ul>
                                </div>
                                <div class="details col-xl-7 col-lg-12 col-md-12 mt-4 mt-xl-0">
                                    <h4 class="product-title">{{item.ProductName}}</h4>
                                    <div class="rating">
                                        <div class="stars">
                                            <span class="fa fa-star checked"></span>
                                            <span class="fa fa-star checked"></span>
                                            <span class="fa fa-star checked"></span>
                                            <span class="fa fa-star text-muted"></span>
                                            <span class="fa fa-star text-muted"></span>
                                        </div>
                                        <span class="review-no">41 reviews</span>
                                    </div>
                                    <p class="product-description" v-html="item.ShortDescription"></p>
                                    <h6 class="price">current price: <span class="h3 ms-2">₹{{item.NetAmount}}</span></h6>
                                    <p class="vote"><strong>91%</strong> of buyers enjoyed this product! <strong>(87 votes)</strong></p>
                                    <div class="sizes d-flex">
                                        sizes:
											<span class="size d-flex" data-bs-toggle="tooltip" title="small">
                                                <label class="rdiobox mb-0">
                                                    <input checked="" name="rdio" type="radio">
                                                    <span class="font-weight-bold">s</span></label></span>
                                        <span class="size d-flex" data-bs-toggle="tooltip" title="medium">
                                            <label class="rdiobox mb-0">
                                                <input name="rdio" type="radio">
                                                <span>m</span></label></span>
                                        <span class="size d-flex" data-bs-toggle="tooltip" title="large">
                                            <label class="rdiobox mb-0">
                                                <input name="rdio" type="radio">
                                                <span>l</span></label></span>
                                        <span class="size d-flex" data-bs-toggle="tooltip" title="extra-large">
                                            <label class="rdiobox mb-0">
                                                <input name="rdio" type="radio">
                                                <span>xl</span></label></span>
                                    </div>
                                    <div class="colors d-flex me-3 mt-2">
                                        <span class="mt-2">colors:</span>
                                        <div class="row gutters-xs ms-4">
                                            <div class="me-2">
                                                <label class="colorinput">
                                                    <input name="color" type="radio" value="azure" class="colorinput-input" checked="">
                                                    <span class="colorinput-color bg-danger"></span>
                                                </label>
                                            </div>
                                            <div class="me-2">
                                                <label class="colorinput">
                                                    <input name="color" type="radio" value="indigo" class="colorinput-input">
                                                    <span class="colorinput-color bg-secondary"></span>
                                                </label>
                                            </div>
                                            <div class="me-2">
                                                <label class="colorinput">
                                                    <input name="color" type="radio" value="purple" class="colorinput-input">
                                                    <span class="colorinput-color bg-dark"></span>
                                                </label>
                                            </div>
                                            <div class="me-2">
                                                <label class="colorinput">
                                                    <input name="color" type="radio" value="pink" class="colorinput-input">
                                                    <span class="colorinput-color bg-pink"></span>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="d-flex  mt-2">
                                        <div class="mt-2 product-title">Quantity:</div>
                                        <div class="d-flex ms-2">
                                            <ul class=" mb-0 qunatity-list">
                                                <li>
                                                    <div class="form-group">
                                                       <%-- <select name="quantity" id="select-countries17" class="form-control nice-select wd-100">
                                                            <option value="1" selected="">1</option>
                                                            <option value="2">2</option>
                                                            <option value="3">3</option>
                                                            <option value="4">4</option>
                                                        </select>--%>
                                                        {{item.Quantity}}
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="action">
                                        <a class="add-to-cart btn btn-danger my-1" type="button" href="wish-list.html">ADD TO WISHLIST</a>
                                        <a class="add-to-cart btn btn-success  my-1" type="button" v-bind:class='(item.Quantity==0?"btn btn-danger":"btn btn-primary")' v-on:click="AddToCart(item)" :disabled="(item.Quantity==0?true:false)"  v-bind:value='(item.Quantity==0?"Sold Out":"Add to cart")' href="product-cart.aspx">ADD TO CART</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /row -->




            <!-- row -->
            <div class="row">
                <div class="col-lg-3" v-for="item in RealtedCategoryProductResult">
                    <div class="card item-card">
                        <div class="card-body pb-0 h-100">
                            <div class="text-center">
                                <img v-bind:src="'images/'+item.Imagetype" alt="img" class="img-fluid">
                            </div>
                            <div class="card-body cardbody relative">
                                <div class="cardtitle">
                                    <span>{{item.Labels}}</span>
                                    <a>{{item.ProductName}}</a>
                                </div>
                                <div class="cardprice">
                                    <span class="type--strikethrough">₹{{item.Price}}</span>
                                    <span>₹{{item.NetAmount}}</span>
                                </div>
                            </div>
                        </div>
                        <div class="text-center border-top p-3">
                            <a href="shop-des.html" class="btn btn-primary my-1 btn-sm">View More</a>
                            <a class="add-to-cart btn btn-success  btn-sm my-1" type="button" v-bind:href="'product-details.aspx?ID='+item.ID">ADD TO CART</a>
                        </div>
                    </div>
                </div>


            </div>
            <!-- /row -->

            <!-- row -->
            <div class="row row-sm">
                <div class="col-md-12 col-xl-4 col-xs-12 col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="feature2">
                                <i class="mdi mdi-airplane-takeoff bg-purple ht-50 wd-50 text-center brround text-white"></i>
                            </div>
                            <h5 class="mb-2 tx-16">Free Shipping</h5>
                            <span class="fs-14 text-muted">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua domenus orioneu.</span>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 col-xl-4 col-xs-12 col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="feature2">
                                <i class="mdi mdi-headset bg-pink  ht-50 wd-50 text-center brround text-white"></i>
                            </div>
                            <h5 class="mb-2 tx-16">Customer Support</h5>
                            <span class="fs-14 text-muted">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua domenus orioneu.</span>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 col-xl-4 col-xs-12 col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="feature2">
                                <i class="mdi mdi-refresh bg-teal ht-50 wd-50 text-center brround text-white"></i>
                            </div>
                            <div class="icon-return"></div>
                            <h5 class="mb-2  tx-16">30 days money back</h5>
                            <span class="fs-14 text-muted">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua domenus orioneu.</span>
                        </div>
                    </div>
                </div>
            </div>
            <!-- row closed -->

            <!-- main-content closed -->
        </div>
    </div>

    <script type="text/javascript">

        new Vue({
            el: '#appHome',
            mixins: [toggle],
            data() {
                return {
                    CategoryProductResult: [],
                    GetResult: [],
                    RealtedCategoryProductResult: [],
                    AllCategory: [],
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



                GetData: function () {
                    ThemeEditor.loadingBar();
                    var el = this;
                    const article = {
                        "MethodName": "Product",
                        "ID": this.GetVal()["ID"]
                    }
                    axios.post("product-details.aspx/GetCategoryData", article)
                        .then(response => {

                            el.GetResult = JSON.parse(response.data.d);
                            el.RealtedCategoryProductData(el.GetResult[0].CategoryID);
                            ThemeEditor.CloseloadingBar();
                        });
                },




                RealtedCategoryProductData: function (ID) {
                    ThemeEditor.loadingBar();
                    var el = this;
                    const article = {
                        "MethodName": "Product",
                        "ID": ID

                    }
                    axios.post("product-details.aspx/GetRealtedCategoryProductData", article)
                        .then(response => {

                            el.RealtedCategoryProductResult = JSON.parse(response.data.d);
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

                this.RealtedCategoryProductData();
                this.GetAllCategory();
                if (this.GetVal()["ID"] != null) {
                    this.GetData();
                }

            }
        });
    </script>
</asp:Content>

