Vue.component('reset-pin', {
    props: {
        Auth: {
            type: String,
        },
        Title: {
            type: String,
        }
    },
    data() {
        return {
            MainData: "ffff",
            disabledPin: true,
            oldPin: "",
            NewPin: ""
        }
    },
    methods: {
        CheckPin() {
            var el = this;
            const article = {
                "MethodName": "CheckPin",
                "Data": el.oldPin
            }
            axios.post("Profile.aspx/CheckPin", article)
                .then(response => {
                    var Data = JSON.parse(response.data.d);
                    if (Data.statuscode == "TXN") {
                        el.disabledPin = false;
                        SuccessMess(Data.status);
                    } else {
                        el.disabledPin = true;
                        ErrorMess(Data.status);
                    }
                });
        },
        ChangePin() {
            document.getElementById('load').style.visibility = "visible";
            var el = this;
            const article = {
                "MethodName": "ChangePin",
                "OLD": el.oldPin,
                "New": el.NewPin
            }
            axios.post("Profile.aspx/ChangePin", article)
                .then(response => {
                    var Data = JSON.parse(response.data.d);
                    if (Data.statuscode == "TXN") {
                        el.disabledPin = false;
                        SuccessMess(Data.status);
                    } else {
                        ErrorMess(Data.status);
                    }
                    document.getElementById('load').style.visibility = "hidden";
                });
        }
    },
    mounted() {

    },
})