
Vue.component('logindetails', {
    //props: ['title'],
    props: {
        Auth: {
            type: String,
        }
    },
    data() {
        return {
            MainData: []
        }
    },
    template: '<div v-bind:id="Auth"><table class="table table-responsive" id="firstTable"><thead><tr><th>Last login</th><th>IP address</th><th>OS</th><th>Browser</th> </tr></thead><tbody><tr v-for="row in MainData"><td>{{ row.ADDDATE }}</td><td>{{ row.IP }}</td><td>{{ row.Browser }}</td><td>{{ row.OS }}</td></tr></tbody></table></div>',
    methods: {
        GetBankDetails() {
            var el = this;
            const article = {
                "MethodName": "PermissionSetting",
                "Auth": this.Auth
            }
            axios.post("Profile.aspx/GetBankData", article)
                .then(response => {
                    el.MainData = JSON.parse(response.data.d);
                });
        }
    },
    mounted() {
        this.GetBankDetails();
    },

})
  