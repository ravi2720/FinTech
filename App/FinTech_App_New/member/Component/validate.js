const Validate = Vue.component('validate', {
    template: '#admin--validate',
    props: {
        Visible: {
            type: Boolean
        }
    },
    data() {
        return {
            matrix: [
                //0: 
                [5, 5],
                //1: 
                [15, 5],
                //2: 
                [25, 5],
                //3: 
                [5, 15],
                //4: 
                [15, 15],
                //5: 
                [25, 15],
                //6: 
                [5, 25],
                //7: 
                [15, 25],
                //8: 
                [25, 25]
            ],
            path: [],
            trackMouseMove: false,
            screenX: 0,
            screenY: 0,
            success: false,
            error: false,
            password: '',
            isLoading: false,
        };
    },
    created() {
        this.$nextTick(function () {
            window.addEventListener('touchmove', this.mousemoveANDtouchmove)
            window.addEventListener('mousemove', this.mousemoveANDtouchmove)
            window.addEventListener('mouseup', this.touchendANDmouseup);
            window.addEventListener('touchend', this.touchendANDmouseup);
            window.addEventListener('mousedown', this.touchstartANDmousedown);
            window.addEventListener('touchstart', this.touchstartANDmousedown);
        })
    },
    destroyed() {
        if (this.$refs && this.$refs.touchArea) {
            window.removeEventListener('mousedown', this.touchstartANDmousedown);
            window.removeEventListener('touchdown', this.touchstartANDmousedown);
            window.removeEventListener('mouseup', this.touchendANDmouseup);
            window.removeEventListener('touchend', this.touchendANDmouseup);
            window.removeEventListener('touchmove', this.mousemoveANDtouchmove)
            window.removeEventListener('mousemove', this.mousemoveANDtouchmove)
        }
    },
    computed: {
        pathToString() {
            return this.path.join('')
        },
        glowMatrix() {
            return _.map(this.matrix, (function (n, i) {
                return this.path.includes(i)
            }).bind(this))
        },
        pathToSvg() {

            if (!this.path.length) return ''

            if (this.screenX && this.screenY && this.trackMouseMove)
                var svgB = this.$refs.svg.getBoundingClientRect()

            return _.map(this.path, (function (n, i) {
                return (i ? ' L ' : 'M ') + this.matrix[n][0] + ' ' + this.matrix[n][1];
            }).bind(this)).join('') + (this.screenX && this.screenY && this.trackMouseMove ? ' L ' + ((this.screenX - svgB.left) * 30 / svgB.width) + ' ' + ((this.screenY - svgB.top) * 30 / svgB.width) : '')
        }
    },
    methods: {
        touchstartANDmousedown(ev) {
            this.error = false;
            this.success = false;

            this.trackMouseMove = true;
            this.path = [];
            // avoid scrolling when using touch
            document.body.style.overflow = 'hidden';
        },
        touchendANDmouseup(ev) {

            this.trackMouseMove = false;
            document.body.style.overflow = 'auto';

            this.login();


        },
        mousemoveANDtouchmove(ev) {
            if (!this.trackMouseMove) return false;
            window['times_tried'] = 0;
            if (ev.type == 'mousemove') {

                var target = document.elementFromPoint(ev.clientX, ev.clientY);
                this.screenX = ev.clientX;
                this.screenY = ev.clientY;
            } else {
                var myLocation = ev.changedTouches[0];
                var target = document.elementFromPoint(myLocation.clientX, myLocation.clientY);
                this.screenX = myLocation.clientX;
                this.screenY = myLocation.clientY;
            }

            let keyAsString = target.getAttribute('data-key');
            let key = undefined;

            if (keyAsString) key = Number(keyAsString);
            else return false;

            /*
               This part of the code is a little bit nasty.
               For performance reasons, when moving the finger or mouse fast enough,
               there was a chance that one would "skip" one of the touchable dots.
               So this code looks for the currently touched dot and the previus one
               and fill in the blank with the dot that may have left inbetween
            */

            let last_key = this.path[this.path.length - 1];

            if (last_key == 0 && key == 2)
                this.path.push(1);
            if (last_key == 0 && key == 6)
                this.path.push(3);
            if (last_key == 0 && key == 8)
                this.path.push(4);

            if (last_key == 1 && key == 7)
                this.path.push(4);

            if (last_key == 2 && key == 1)
                this.path.push(1);
            if (last_key == 2 && key == 6)
                this.path.push(4);
            if (last_key == 2 && key == 8)
                this.path.push(5);

            if (last_key == 3 && key == 5)
                this.path.push(4);

            //4

            if (last_key == 5 && key == 3)
                this.path.push(4);


            if (last_key == 6 && key == 0)
                this.path.push(3);
            if (last_key == 6 && key == 8)
                this.path.push(7);
            if (last_key == 6 && key == 4)
                this.path.push(4);

            if (last_key == 7 && key == 1)
                this.path.push(4);

            if (last_key == 8 && key == 0)
                this.path.push(4);
            if (last_key == 8 && key == 2)
                this.path.push(5);
            if (last_key == 8 && key == 6)
                this.path.push(7);

            if (!this.path.includes(key))
                this.path.push(key);



        },
        login() {

            var vm = this;

            if (!this.path.length)
                return false;

            vm.success = false;
            vm.error = false;

            var data = { pattern: vm.pathToString };

            /* fake request. Will allways fail every n of times, and succeed every n+1 times */
            window['times_tried'] = window['times_tried'] + 1 || 0;
            this.isLoading = true;
            var vm = this;
            //window.setTimeout(function () {
                vm.isLoading = false;
                //if (window['times_tried'] % 2) {
                if (window['times_tried']==1) {
                    // success
                    vm.success = true;

                    // DEMO WARNING
                    if (window['times_tried'] == 1) {
                        // first success
                        vm.ChangePattern(data.pattern);
                        //vm.$parent.ShowDiv = true;
                        //alert('This is just a demo to emulate success state. This demo is hardcoded to throw success every two tries. (Real AJAX validation followed by a redirect should go here in a real app). \nRemember to also include a username/password form that works as an alternative, for accessibility!');
                    }


                    // RESET THE DEMO TO ALLOW USERS TO CONTINUE DEMO-ING
                    window.setTimeout(function () {
                        vm.success = false;
                    }, 2500);
                } else {
                    //failure
                    window['times_tried'] = 0;
                    vm.error = true;
                }
            //}, 400)
            
        },
        ChangePattern(Pattern) {
            var el = this;

            const article = {
                "Pattern": Pattern
            }
            axios.post("../Member/profile.aspx/CheckAuthentication", article)
                .then(response => {

                    var Data = JSON.parse(response.data.d);
                    if (Data.statuscode == "TXN") {
                        el.$store.state.ShowDiv = true;
                        el.$store.commit('Change', true)
                    } else {
                        window['times_tried'] = 0;
                        el.error = true;
                        el.success = false;
                        alert(Data.status);
                    }
                });
        }
    },
});

