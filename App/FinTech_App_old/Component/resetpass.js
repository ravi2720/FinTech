Vue.component('reset-password', {
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
            disabled: true,
            oldPass: "",
            NewPass: ""
        }
    },
    methods: {
        keyup() {
            var el = this;
            let pass = document.getElementById("password").value;
            el.checkStrength(pass);
        },
        checkStrength(password) {
            var el = this;
            let strength = 0;
            let state = false;

            let passwordStrength = document.getElementById("password-strength");
            let lowUpperCase = document.querySelector(".low-upper-case i");
            let number = document.querySelector(".one-number i");
            let specialChar = document.querySelector(".one-special-char i");
            let eightChar = document.querySelector(".eight-character i");
            //If password contains both lower and uppercase characters
            if (password.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) {
                strength += 1;
                lowUpperCase.classList.remove('fa-circle');
                lowUpperCase.classList.add('fa-check');
            } else {
                lowUpperCase.classList.add('fa-circle');
                lowUpperCase.classList.remove('fa-check');
            }
            //If it has numbers and characters
            if (password.match(/([0-9])/)) {
                strength += 1;
                number.classList.remove('fa-circle');
                number.classList.add('fa-check');
            } else {
                number.classList.add('fa-circle');
                number.classList.remove('fa-check');
            }
            //If it has one special character
            if (password.match(/([!,%,&,@,#,$,^,*,?,_,~])/)) {
                strength += 1;
                specialChar.classList.remove('fa-circle');
                specialChar.classList.add('fa-check');
            } else {
                specialChar.classList.add('fa-circle');
                specialChar.classList.remove('fa-check');
            }
            //If password is greater than 7
            if (password.length > 7) {
                strength += 1;
                eightChar.classList.remove('fa-circle');
                eightChar.classList.add('fa-check');
            } else {
                eightChar.classList.add('fa-circle');
                eightChar.classList.remove('fa-check');
            }

            // If value is less than 2
            if (strength < 2) {
                passwordStrength.classList.remove('progress-bar-warning');
                passwordStrength.classList.remove('progress-bar-success');
                passwordStrength.classList.add('progress-bar-danger');
                passwordStrength.style = 'width: 10%';
            } else if (strength == 3) {
                passwordStrength.classList.remove('progress-bar-success');
                passwordStrength.classList.remove('progress-bar-danger');
                passwordStrength.classList.add('progress-bar-warning');
                passwordStrength.style = 'width: 60%';
            } else if (strength == 4) {
                passwordStrength.classList.remove('progress-bar-warning');
                passwordStrength.classList.remove('progress-bar-danger');
                passwordStrength.classList.add('progress-bar-success');
                passwordStrength.style = 'width: 100%';
                el.disabled = false;
            }
        },
        CheckPass() {
            var el = this;
            const article = {
                "MethodName": "CheckPass",
                "Data": el.oldPass
            }
            axios.post("Profile.aspx/CheckPass", article)
                .then(response => {
                    var Data = JSON.parse(response.data.d);
                    if (Data.statuscode == "TXN") {
                        el.disabled = false;
                        SuccessMess(Data.status);
                    } else {
                        el.disabled = true;
                        ErrorMess(Data.status);
                    }
                });
        },
        ChangePass() {
            document.getElementById('load').style.visibility = "visible";
            var el = this;
            const article = {
                "MethodName": "ChangePass",
                "OLD": el.oldPass,
                "New": el.NewPass
            }
            axios.post("Profile.aspx/ChangePass", article)
                .then(response => {
                    var Data = JSON.parse(response.data.d);
                    if (Data.statuscode == "TXN") {
                        el.disabled = false;
                        SuccessMess(Data.status);
                    } else {
                        el.disabled = true;
                        ErrorMess(Data.status);
                    }
                    document.getElementById('load').style.visibility = "hidden";
                });
        }
    },
    mounted() {

    },
})