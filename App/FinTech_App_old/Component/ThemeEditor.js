'use strict'
class ThemeEditor {
    constructor() {

    }
    static loadingBar() {
        document.getElementById('load').style.visibility = "visible";
    }

    static CloseloadingBar() {
        document.getElementById('load').style.visibility = "hidden";
    }
}