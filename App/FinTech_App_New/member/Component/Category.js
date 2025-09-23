Vue.component('logindetails', {
    props: {
        title: {
            type: String,
        },
    },
    data() {
        return {
            CategoryList: []
        }
    },
    template: ' <div class="row"><div class="col-md-2 text-center" v-for="item in CategoryList"><img  v-bind:src="item.Class" class="img-circle" style="border: 0px solid red !important" height="80" /><br /><strong>{{item.Name}}</strong></div></div>',
    methods: {
        GetCategory() {
            var el = this;
            const article = {
                "MethodName": "ParentCategoryJson"
            }
            axios.post("SearchProduct.aspx/GetCategory", article)
                .then(response => {
                    el.CategoryList = JSON.parse(response.data.d).CategoryList;
                });
        }
    },
    mounted() {
        this.GetCategory();
    },

})
