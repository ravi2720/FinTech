Vue.component('togglebtnpage', {
    //props: ['title'],
    props: {
        title: {
            type: String,
        },
        PageID: {
            type: String,
        },
        RoleID: {
            type: String,
        },
        Check: {
            type: String
        }
        ,
        ColN: {
            type: String
        }
        ,
        Disable: {
            type: Number,
            default: 0
        }
    },
    data() {
        return {
            MainData: []
        }
    },
    template: '<div><label class="switch"><input type="checkbox" :disabled="Disable == 1" v-bind:id="ColN" v-bind:checked="Check" @click="toggleCheckbox"><div class="slider round"></div></label><p><b>{{ title }}</b></p></div>',
    methods: {
        toggleCheckbox() {
            debugger;
            var el = this;
            this.checkbox = !this.checkbox
            this.$emit('setCheckboxVal', this.checkbox)
            const article = {
                "MethodName": "PermissionSetting",
                "ColN": $(this)[0].$attrs.coln,
                "PageID": $(this)[0].$attrs.pageid,
                "RoleID": $(this)[0].$attrs.roleid
            }
            axios.post("PagePermissionSetting.aspx/UpdatePermission", article)
                .then(response => {
                    alertify.success(el.title + " update Successfully");
                });
        }
    }
})