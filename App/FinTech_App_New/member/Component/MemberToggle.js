Vue.component('togglebtn', {
    //props: ['title'],
    props: {
        title: {
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
        Auth: {
            type: String
        },
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
    template: '<div><label class="switch"><input type="checkbox" :disabled="Disable == 1"  v-bind:id="ColN" v-bind:checked="Check" @click="toggleCheckbox"><div class="slider round"></div></label><p><b>{{ title }}</b></p></div>',
    methods: {
        toggleCheckbox() {
            var el = this;
            this.checkbox = !this.checkbox
            this.$emit('setCheckboxVal', this.checkbox)
            const article = {
                "MethodName": "PermissionSetting",
                "ColN": $(this)[0].$attrs.coln,
                "Auth": el.$parent.MainData[0].Msrno
            }
            axios.post("Settings.aspx/UpdatePermission", article)
                .then(response => {
                    SuccessMess(el.title + " update Successfully");
                });
        }
    }
})