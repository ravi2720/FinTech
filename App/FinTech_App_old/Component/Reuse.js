const toggle = {

    data() {
        return {
            SingleDataProduct: [],
            AddToCartData: [],
            AddToCartOrder: [],
            AddToCardDataPropert: {
                ID: 0,
                Msrno: 2,
                Img: "",
                Quantity: 1,
                Amount: 1,
                Name: ""
            },




            authenticated: true,
        }
    },
    methods: {
        GetProductData: function (ID) {
            var el = this;
            el.SingleDataProduct = [];
            //const article = {
            //    "MethodName": "GetBranch",
            //    "ID": ID
            //}
            //axios.post("products.aspx/GetProductData", article)
            //    .then(response => {
            //        el.SingleDataProduct = JSON.parse(response.data.d);
            //    });
            el.SingleDataProduct.push(ID);
        },
        AddToCart: function (ObjectP) {
            debugger;
            ThemeEditor.loadingBar();
            this.AddToCardDataPropert.Img = ObjectP.ImageType;
            this.AddToCardDataPropert.ID = ObjectP.ID;
            this.AddToCardDataPropert.Msrno = 2;
            this.AddToCardDataPropert.Quantity = 1;
            this.AddToCardDataPropert.Name = ObjectP.ProductName;
            this.AddToCardDataPropert.Amount = ObjectP.NetAmount;

            this.AddToCartData.push(this.AddToCardDataPropert);
            if (this.CheckAuth()) {
                this.AddToCartServer(this.AddToCardDataPropert);
            }
            alertify.success('Product Add to Cart Successfully');
            this.AddToCardDataPropert = {};

            localStorage.setItem("CartData", JSON.stringify(this.AddToCartData));


            ThemeEditor.CloseloadingBar();

        },
        AddToCartServer: function (ObjectP) {
            var el = this;
            const article = {
                "addToCart": ObjectP,

            }
            axios.post("products.aspx/AddToCart", article)
                .then(response => {

                });
        },
        DeleteCartServer: function (ObjectP) {
            var el = this;
            el.AddToCartData.splice(el.AddToCartData.findIndex(a => a.ID === ObjectP.ID && a.Msrno === ObjectP.Msrno), 1);
            localStorage.setItem("CartData", JSON.stringify(this.AddToCartData));
            if (el.CheckAuth()) {
                const article = {
                    "addToCart": ObjectP,
                }
                axios.post("products.aspx/DeleteAddToCart", article)
                    .then(response => {

                    });
            }
            alertify.success('Product Add to Cart Delete Successfully');
        },
        setAuthenticated(status) {
            this.authenticated = status;
        },
        CheckAuth: function () {
            return this.authenticated;
        },
        logout() {
            this.authenticated = false;
        },
        OrderItem: function () {
            //ThemeEditor.loadingBar();
            var AddToCartDataOrder = JSON.parse(localStorage.CartData);
            debugger;
            for (var i = 0; i < AddToCartDataOrder.length; i++) {

                var OrderItemDataProperty = {
                    ID: 0,
                    //Msrno: 0,
                    PID: 0,
                    order_id: 0,
                    SKU: 0,
                    Price: 0,
                    Discount: 0,
                    Quantity: 0,
                    //Address: "0",
                    Status: "0",
                    //NetAmount: 0,
                    Content: "",
                    AddressID: 0
                }
                
                //OrderItemDataProperty.Msrno = $("#hdnmsrno").val();              
                OrderItemDataProperty.PID = AddToCartDataOrder[i].ID;
                OrderItemDataProperty.order_id = 6666;
                OrderItemDataProperty.SKU = 22;
                OrderItemDataProperty.Price =AddToCartDataOrder[i].Amount;
                OrderItemDataProperty.Discount = 0;
                OrderItemDataProperty.Quantity = AddToCartDataOrder[i].Quantity;
                //OrderItemDataProperty.Address = '';
                OrderItemDataProperty.Status = '';
                //OrderItemDataProperty.NetAmount = 0;
                OrderItemDataProperty.Content = '';
                OrderItemDataProperty.AddressID = $("#hdnid").val()

                {


                    var el = this;
                    const article = {
                        "OrderItem": OrderItemDataProperty
,

                    }
                    axios.post("checkout.aspx/OrderItem", article)
                        .then(response => {

                        });
                }
              
                //ThemeEditor.CloseloadingBar();
            }

        },



        No2Word: function (amount) {
            var words = new Array();
            words[0] = '';
            words[1] = 'One';
            words[2] = 'Two';
            words[3] = 'Three';
            words[4] = 'Four';
            words[5] = 'Five';
            words[6] = 'Six';
            words[7] = 'Seven';
            words[8] = 'Eight';
            words[9] = 'Nine';
            words[10] = 'Ten';
            words[11] = 'Eleven';
            words[12] = 'Twelve';
            words[13] = 'Thirteen';
            words[14] = 'Fourteen';
            words[15] = 'Fifteen';
            words[16] = 'Sixteen';
            words[17] = 'Seventeen';
            words[18] = 'Eighteen';
            words[19] = 'Nineteen';
            words[20] = 'Twenty';
            words[30] = 'Thirty';
            words[40] = 'Forty';
            words[50] = 'Fifty';
            words[60] = 'Sixty';
            words[70] = 'Seventy';
            words[80] = 'Eighty';
            words[90] = 'Ninety';
            amount = amount.toString();
            var atemp = amount.split(".");
            var number = atemp[0].split(",").join("");
            var n_length = number.length;
            var words_string = "";
            if (n_length <= 9) {
                var n_array = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0);
                var received_n_array = new Array();
                for (var i = 0; i < n_length; i++) {
                    received_n_array[i] = number.substr(i, 1);
                }
                for (var i = 9 - n_length, j = 0; i < 9; i++ , j++) {
                    n_array[i] = received_n_array[j];
                }
                for (var i = 0, j = 1; i < 9; i++ , j++) {
                    if (i == 0 || i == 2 || i == 4 || i == 7) {
                        if (n_array[i] == 1) {
                            n_array[j] = 10 + parseInt(n_array[j]);
                            n_array[i] = 0;
                        }
                    }
                }
                value = "";
                for (var i = 0; i < 9; i++) {
                    if (i == 0 || i == 2 || i == 4 || i == 7) {
                        value = n_array[i] * 10;
                    } else {
                        value = n_array[i];
                    }
                    if (value != 0) {
                        words_string += words[value] + " ";
                    }
                    if ((i == 1 && value != 0) || (i == 0 && value != 0 && n_array[i + 1] == 0)) {
                        words_string += "Crores ";
                    }
                    if ((i == 3 && value != 0) || (i == 2 && value != 0 && n_array[i + 1] == 0)) {
                        words_string += "Lakhs ";
                    }
                    if ((i == 5 && value != 0) || (i == 4 && value != 0 && n_array[i + 1] == 0)) {
                        words_string += "Thousand ";
                    }
                    if (i == 6 && value != 0 && (n_array[i + 1] != 0 && n_array[i + 2] != 0)) {
                        words_string += "Hundred and ";
                    } else if (i == 6 && value != 0) {
                        words_string += "Hundred ";
                    }
                }
                words_string = words_string.split("  ").join(" ");
            }
            return words_string;
        },

    },
    created: function () {
        if (localStorage.CartData != null)
            this.AddToCartData = JSON.parse(localStorage.CartData);
    },
    mounted() {
        if (!this.authenticated) {
            //this.$router.replace({ name: "login" });
        }
    }
}