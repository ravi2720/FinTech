function Action(ObjData) {
    try {
        $('#' + ObjData.ID).DataTable({

            "scrollX": true,
            dom: 'Bfrtip',
            stateSave: true,
            "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
            buttons: [
                'pageLength',
                {
                    extend: 'excelHtml5',
                    text: '<i class="fa fa" > <img style="height:25px;" src="data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pg0KPCEtLSBHZW5lcmF0b3I6IEFkb2JlIElsbHVzdHJhdG9yIDE5LjAuMCwgU1ZHIEV4cG9ydCBQbHVnLUluIC4gU1ZHIFZlcnNpb246IDYuMDAgQnVpbGQgMCkgIC0tPg0KPHN2ZyB2ZXJzaW9uPSIxLjEiIGlkPSJDYXBhXzEiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgeG1sbnM6eGxpbms9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkveGxpbmsiIHg9IjBweCIgeT0iMHB4Ig0KCSB2aWV3Qm94PSIwIDAgNTEyIDUxMiIgc3R5bGU9ImVuYWJsZS1iYWNrZ3JvdW5kOm5ldyAwIDAgNTEyIDUxMjsiIHhtbDpzcGFjZT0icHJlc2VydmUiPg0KPHBhdGggc3R5bGU9ImZpbGw6I0VDRUZGMTsiIGQ9Ik00OTYsNDMyLjAxMUgyNzJjLTguODMyLDAtMTYtNy4xNjgtMTYtMTZzMC0zMTEuMTY4LDAtMzIwczcuMTY4LTE2LDE2LTE2aDIyNA0KCWM4LjgzMiwwLDE2LDcuMTY4LDE2LDE2djMyMEM1MTIsNDI0Ljg0Myw1MDQuODMyLDQzMi4wMTEsNDk2LDQzMi4wMTF6Ii8+DQo8Zz4NCgk8cGF0aCBzdHlsZT0iZmlsbDojMzg4RTNDOyIgZD0iTTMzNiwxNzYuMDExaC02NGMtOC44MzIsMC0xNi03LjE2OC0xNi0xNnM3LjE2OC0xNiwxNi0xNmg2NGM4LjgzMiwwLDE2LDcuMTY4LDE2LDE2DQoJCVMzNDQuODMyLDE3Ni4wMTEsMzM2LDE3Ni4wMTF6Ii8+DQoJPHBhdGggc3R5bGU9ImZpbGw6IzM4OEUzQzsiIGQ9Ik0zMzYsMjQwLjAxMWgtNjRjLTguODMyLDAtMTYtNy4xNjgtMTYtMTZzNy4xNjgtMTYsMTYtMTZoNjRjOC44MzIsMCwxNiw3LjE2OCwxNiwxNg0KCQlTMzQ0LjgzMiwyNDAuMDExLDMzNiwyNDAuMDExeiIvPg0KCTxwYXRoIHN0eWxlPSJmaWxsOiMzODhFM0M7IiBkPSJNMzM2LDMwNC4wMTFoLTY0Yy04LjgzMiwwLTE2LTcuMTY4LTE2LTE2czcuMTY4LTE2LDE2LTE2aDY0YzguODMyLDAsMTYsNy4xNjgsMTYsMTYNCgkJUzM0NC44MzIsMzA0LjAxMSwzMzYsMzA0LjAxMXoiLz4NCgk8cGF0aCBzdHlsZT0iZmlsbDojMzg4RTNDOyIgZD0iTTMzNiwzNjguMDExaC02NGMtOC44MzIsMC0xNi03LjE2OC0xNi0xNnM3LjE2OC0xNiwxNi0xNmg2NGM4LjgzMiwwLDE2LDcuMTY4LDE2LDE2DQoJCVMzNDQuODMyLDM2OC4wMTEsMzM2LDM2OC4wMTF6Ii8+DQoJPHBhdGggc3R5bGU9ImZpbGw6IzM4OEUzQzsiIGQ9Ik00MzIsMTc2LjAxMWgtMzJjLTguODMyLDAtMTYtNy4xNjgtMTYtMTZzNy4xNjgtMTYsMTYtMTZoMzJjOC44MzIsMCwxNiw3LjE2OCwxNiwxNg0KCQlTNDQwLjgzMiwxNzYuMDExLDQzMiwxNzYuMDExeiIvPg0KCTxwYXRoIHN0eWxlPSJmaWxsOiMzODhFM0M7IiBkPSJNNDMyLDI0MC4wMTFoLTMyYy04LjgzMiwwLTE2LTcuMTY4LTE2LTE2czcuMTY4LTE2LDE2LTE2aDMyYzguODMyLDAsMTYsNy4xNjgsMTYsMTYNCgkJUzQ0MC44MzIsMjQwLjAxMSw0MzIsMjQwLjAxMXoiLz4NCgk8cGF0aCBzdHlsZT0iZmlsbDojMzg4RTNDOyIgZD0iTTQzMiwzMDQuMDExaC0zMmMtOC44MzIsMC0xNi03LjE2OC0xNi0xNnM3LjE2OC0xNiwxNi0xNmgzMmM4LjgzMiwwLDE2LDcuMTY4LDE2LDE2DQoJCVM0NDAuODMyLDMwNC4wMTEsNDMyLDMwNC4wMTF6Ii8+DQoJPHBhdGggc3R5bGU9ImZpbGw6IzM4OEUzQzsiIGQ9Ik00MzIsMzY4LjAxMWgtMzJjLTguODMyLDAtMTYtNy4xNjgtMTYtMTZzNy4xNjgtMTYsMTYtMTZoMzJjOC44MzIsMCwxNiw3LjE2OCwxNiwxNg0KCQlTNDQwLjgzMiwzNjguMDExLDQzMiwzNjguMDExeiIvPg0KPC9nPg0KPHBhdGggc3R5bGU9ImZpbGw6IzJFN0QzMjsiIGQ9Ik0yODIuMjA4LDE5LjY5MWMtMy42NDgtMy4wNC04LjU0NC00LjM1Mi0xMy4xNTItMy4zOTJsLTI1Niw0OEM1LjQ3Miw2NS43MDcsMCw3Mi4yOTksMCw4MC4wMTF2MzUyDQoJYzAsNy42OCw1LjQ3MiwxNC4zMDQsMTMuMDU2LDE1LjcxMmwyNTYsNDhjMC45NiwwLjE5MiwxLjk1MiwwLjI4OCwyLjk0NCwwLjI4OGMzLjcxMiwwLDcuMzI4LTEuMjgsMTAuMjA4LTMuNjgNCgljMy42OC0zLjA0LDUuNzkyLTcuNTg0LDUuNzkyLTEyLjMydi00NDhDMjg4LDI3LjI0MywyODUuODg4LDIyLjczMSwyODIuMjA4LDE5LjY5MXoiLz4NCjxwYXRoIHN0eWxlPSJmaWxsOiNGQUZBRkE7IiBkPSJNMjIwLjAzMiwzMDkuNDgzbC01MC41OTItNTcuODI0bDUxLjE2OC02NS43OTJjNS40NC02Ljk3Niw0LjE2LTE3LjAyNC0yLjc4NC0yMi40NjQNCgljLTYuOTQ0LTUuNDQtMTYuOTkyLTQuMTYtMjIuNDY0LDIuNzg0bC00Ny4zOTIsNjAuOTI4bC0zOS45MzYtNDUuNjMyYy01Ljg1Ni02LjcyLTE1Ljk2OC03LjMyOC0yMi41Ni0xLjUwNA0KCWMtNi42NTYsNS44MjQtNy4zMjgsMTUuOTM2LTEuNTA0LDIyLjU2bDQ0LDUwLjMwNEw4My4zNiwzMTAuMTg3Yy01LjQ0LDYuOTc2LTQuMTYsMTcuMDI0LDIuNzg0LDIyLjQ2NA0KCWMyLjk0NCwyLjI3Miw2LjQzMiwzLjM2LDkuODU2LDMuMzZjNC43NjgsMCw5LjQ3Mi0yLjExMiwxMi42NC02LjE3Nmw0MC44LTUyLjQ4bDQ2LjUyOCw1My4xNTINCgljMy4xNjgsMy42NDgsNy41ODQsNS41MDQsMTIuMDMyLDUuNTA0YzMuNzQ0LDAsNy40ODgtMS4zMTIsMTAuNTI4LTMuOTY4QzIyNS4xODQsMzI2LjIxOSwyMjUuODU2LDMxNi4xMDcsMjIwLjAzMiwzMDkuNDgzeiIvPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPC9zdmc+DQo=" type="image/svg+xml" /></i>',
                    titleAttr: 'EXCEL',
                    enabled: ObjData.EnpageLength,
                    visibility: false
                }
                ,
                {
                    extend: 'csvHtml5',
                    text: '<i class="fa fa"> <img style="height:25px;" src="data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pg0KPCEtLSBHZW5lcmF0b3I6IEFkb2JlIElsbHVzdHJhdG9yIDE2LjAuMCwgU1ZHIEV4cG9ydCBQbHVnLUluIC4gU1ZHIFZlcnNpb246IDYuMDAgQnVpbGQgMCkgIC0tPg0KPCFET0NUWVBFIHN2ZyBQVUJMSUMgIi0vL1czQy8vRFREIFNWRyAxLjEvL0VOIiAiaHR0cDovL3d3dy53My5vcmcvR3JhcGhpY3MvU1ZHLzEuMS9EVEQvc3ZnMTEuZHRkIj4NCjxzdmcgdmVyc2lvbj0iMS4xIiBpZD0iQ2FwYV8xIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIiB4PSIwcHgiIHk9IjBweCINCgkgd2lkdGg9IjU1MC44MDFweCIgaGVpZ2h0PSI1NTAuODAxcHgiIHZpZXdCb3g9IjAgMCA1NTAuODAxIDU1MC44MDEiIHN0eWxlPSJlbmFibGUtYmFja2dyb3VuZDpuZXcgMCAwIDU1MC44MDEgNTUwLjgwMTsiDQoJIHhtbDpzcGFjZT0icHJlc2VydmUiPg0KPGc+DQoJPGc+DQoJCTxwYXRoIGQ9Ik00NzUuMDg0LDEzMS45OTJjLTAuMDIxLTIuNTIxLTAuODI4LTUuMDIxLTIuNTYyLTYuOTkzTDM2Ni4zMjQsMy42ODljLTAuMDMxLTAuMDM1LTAuMDYyLTAuMDQ1LTAuMDg0LTAuMDc3DQoJCQljLTAuNjMzLTAuNzA2LTEuMzcxLTEuMjg0LTIuMTUxLTEuODAzYy0wLjIzMi0wLjE0NS0wLjQ2NC0wLjI4NS0wLjcwNy0wLjQyMmMtMC42NzUtMC4zNjctMS4zOTItMC42NjUtMi4xMy0wLjg4Ng0KCQkJYy0wLjIwMS0wLjA2My0wLjM4LTAuMTQ1LTAuNTgtMC4xOTdDMzU5Ljg3LDAuMTE0LDM1OS4wMzcsMCwzNTguMTkzLDBIOTcuMkM4NS4yODIsMCw3NS42LDkuNjkzLDc1LjYsMjEuNjAxdjUwNy42DQoJCQljMCwxMS45MDcsOS42ODIsMjEuNjAxLDIxLjYsMjEuNjAxSDQ1My42YzExLjkwOCwwLDIxLjYwMS05LjY5MywyMS42MDEtMjEuNjAxVjEzMy4yMDINCgkJCUM0NzUuMiwxMzIuNzkxLDQ3NS4xMzcsMTMyLjM5Myw0NzUuMDg0LDEzMS45OTJ6IE0xODEuMTAxLDQ5NS42NzJjOC45MzEsMCwxOC44NDUtMS45NCwyNC42NzctNC4yNzFsNC40NzIsMjMuMTI5DQoJCQljLTUuNDQ0LDIuNzIyLTE3LjY4NCw1LjY0My0zMy42MjYsNS42NDNjLTQ1LjI5MSwwLTY4LjYxOC0yOC4xOTEtNjguNjE4LTY1LjUwNmMwLTQ0LjcwOSwzMS44OC02OS41ODksNzEuNTI5LTY5LjU4OQ0KCQkJYzE1LjM1NiwwLDI3LjAyMSwzLjEwNiwzMi4yNzYsNS44MzNsLTYuMDMzLDIzLjUxNGMtNi4wMi0yLjUyNS0xNC4zODYtNC44NDYtMjQuODc3LTQuODQ2Yy0yMy41MTcsMC00MS43ODQsMTQuMTg2LTQxLjc4NCw0My4zMzENCgkJCUMxMzkuMTE2LDQ3OS4xNTUsMTU0LjY2Miw0OTUuNjcyLDE4MS4xMDEsNDk1LjY3MnogTTI2MS43NzYsNTIwLjE3MmMtMTQuOTY0LDAtMjkuNzQyLTMuODkyLTM3LjEyNS03Ljk3M2w2LjAyLTI0LjQ5DQoJCQljNy45NzYsNC4wODIsMjAuMjE4LDguMTYzLDMyLjg1NCw4LjE2M2MxMy42MDUsMCwyMC43OTgtNS42NDMsMjAuNzk4LTE0LjE5NmMwLTguMTYzLTYuMjE3LTEyLjgyNC0yMS45NjMtMTguNDU3DQoJCQljLTIxLjc2Ni03LjU4My0zNS45NTUtMTkuNjI3LTM1Ljk1NS0zOC42NzVjMC0yMi4zNTksMTguNjU4LTM5LjQ2Nyw0OS41NjUtMzkuNDY3YzE0Ljc3MSwwLDI1LjY2LDMuMTA2LDMzLjQyOCw2LjYxMw0KCQkJbC02LjYwMiwyMy45MDljLTUuMjU4LTIuNTMxLTE0LjU3Ni02LjIyMy0yNy40MTItNi4yMjNjLTEyLjgyNSwwLTE5LjA0NSw1LjgzMy0xOS4wNDUsMTIuNjM2YzAsOC4zNjksNy4zOCwxMi4wNTUsMjQuMzAzLDE4LjQ2OA0KCQkJYzIzLjEyNCw4LjU1OSwzNC4wMTQsMjAuNTk4LDM0LjAxNCwzOS4wNjVDMzE0LjY0NSw1MDEuNTA0LDI5Ny43MzgsNTIwLjE3MiwyNjEuNzc2LDUyMC4xNzJ6IE00MDAuMTgsNTE4LjIxNmgtMzQuNTk0DQoJCQlMMzIzLjYsMzg3LjIxM2gzMi40NjNsMTUuOTM3LDU1LjQwM2M0LjQ3MiwxNS41NDYsOC41NTQsMzAuNTEyLDExLjY2NSw0Ni44NDRoMC41OGMzLjMwMS0xNS43NDcsNy4zODMtMzEuMjk4LDExLjg1NC00Ni4yNTkNCgkJCWwxNi43MTctNTUuOTgyaDMxLjQ5M0w0MDAuMTgsNTE4LjIxNnogTTk3LjIsMzY2Ljc1MlYyMS42MDFoMjUwLjE5MnYxMTAuNTIxYzAsNS45NjIsNC44NDIsMTAuOCwxMC44MDEsMTAuOEg0NTMuNnYyMjMuODM3SDk3LjINCgkJCVYzNjYuNzUyeiIvPg0KCQk8cGF0aCBkPSJNMzc3LjE4OCwxNzAuMDU4aC02Ni4xNDloLTIuMzFoLTY2LjE1bDAsMGgtNjV2MjkuMjIzdjIuMTkzdjI3LjAzMnYyLjMxMnYyNi45MjR2Mi4zMDJ2MjkuMjI2aDY4LjQ2MmwwLDBoMTMxLjI3di05Mi42ODgNCgkJCWgtMC4xMjJWMTcwLjA1OHogTTI0My43MjgsMjg2Ljk3MUgxNzkuODl2LTI2LjkyN2g2My44MzdWMjg2Ljk3MXogTTI0My43MjgsMjU3Ljc0MkgxNzkuODl2LTI2LjkyNGg2My44MzdWMjU3Ljc0MnoNCgkJCSBNMjQzLjcyOCwyMjguNTA2SDE3OS44OXYtMjYuOTEzaDYyLjY4OGgxLjE0OVYyMjguNTA2eiBNMjQ0Ljg5MSwxOTkuMjgxdi0yNi45MTNoNjMuODM4djI2LjkxM0gyNDYuMDRIMjQ0Ljg5MXoNCgkJCSBNMzc0LjYwNCwyODYuNTY5aC0yNS4yMDd2LTc3LjA5OEgzMzIuMDl2NzcuMDk4aC0xNC40MTh2LTYxLjEzaC0xNy4zMDh2NjEuMTNoLTEyLjY5OHYtNDUuMzYyaC0xNy4zMXY0NS4zNjJoLTI0LjMyMXYtMjYuNTI1DQoJCQl2LTIuMzAydi0yNi45MjR2LTIuMzEydi0yNi45MTNoNjIuNjg4aDIuMzE1aDYzLjU2NVYyODYuNTY5eiBNMzc0Ljg3OCwxOTYuNTgxaC0wLjI3NHYyLjdoLTYzLjU2NXYtMjYuOTEzaDYzLjg0VjE5Ni41ODF6Ii8+DQoJPC9nPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPC9zdmc+DQo=" type="image/svg+xml" /></i>',
                    titleAttr: 'CSV',
                    enabled: ObjData.EncsvHtml5

                },
                {
                    extend: 'pdfHtml5',
                    text: '<i class="fa fa" > <img style="height:25px;" src="data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pg0KPCEtLSBHZW5lcmF0b3I6IEFkb2JlIElsbHVzdHJhdG9yIDE4LjAuMCwgU1ZHIEV4cG9ydCBQbHVnLUluIC4gU1ZHIFZlcnNpb246IDYuMDAgQnVpbGQgMCkgIC0tPg0KPCFET0NUWVBFIHN2ZyBQVUJMSUMgIi0vL1czQy8vRFREIFNWRyAxLjEvL0VOIiAiaHR0cDovL3d3dy53My5vcmcvR3JhcGhpY3MvU1ZHLzEuMS9EVEQvc3ZnMTEuZHRkIj4NCjxzdmcgdmVyc2lvbj0iMS4xIiBpZD0iQ2FwYV8xIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIiB4PSIwcHgiIHk9IjBweCINCgkgdmlld0JveD0iMCAwIDU2IDU2IiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1NiA1NjsiIHhtbDpzcGFjZT0icHJlc2VydmUiPg0KPGc+DQoJPHBhdGggc3R5bGU9ImZpbGw6I0U5RTlFMDsiIGQ9Ik0zNi45ODUsMEg3Ljk2M0M3LjE1NSwwLDYuNSwwLjY1NSw2LjUsMS45MjZWNTVjMCwwLjM0NSwwLjY1NSwxLDEuNDYzLDFoNDAuMDc0DQoJCWMwLjgwOCwwLDEuNDYzLTAuNjU1LDEuNDYzLTFWMTIuOTc4YzAtMC42OTYtMC4wOTMtMC45Mi0wLjI1Ny0xLjA4NUwzNy42MDcsMC4yNTdDMzcuNDQyLDAuMDkzLDM3LjIxOCwwLDM2Ljk4NSwweiIvPg0KCTxwb2x5Z29uIHN0eWxlPSJmaWxsOiNEOUQ3Q0E7IiBwb2ludHM9IjM3LjUsMC4xNTEgMzcuNSwxMiA0OS4zNDksMTIgCSIvPg0KCTxwYXRoIHN0eWxlPSJmaWxsOiNDQzRCNEM7IiBkPSJNMTkuNTE0LDMzLjMyNEwxOS41MTQsMzMuMzI0Yy0wLjM0OCwwLTAuNjgyLTAuMTEzLTAuOTY3LTAuMzI2DQoJCWMtMS4wNDEtMC43ODEtMS4xODEtMS42NS0xLjExNS0yLjI0MmMwLjE4Mi0xLjYyOCwyLjE5NS0zLjMzMiw1Ljk4NS01LjA2OGMxLjUwNC0zLjI5NiwyLjkzNS03LjM1NywzLjc4OC0xMC43NQ0KCQljLTAuOTk4LTIuMTcyLTEuOTY4LTQuOTktMS4yNjEtNi42NDNjMC4yNDgtMC41NzksMC41NTctMS4wMjMsMS4xMzQtMS4yMTVjMC4yMjgtMC4wNzYsMC44MDQtMC4xNzIsMS4wMTYtMC4xNzINCgkJYzAuNTA0LDAsMC45NDcsMC42NDksMS4yNjEsMS4wNDljMC4yOTUsMC4zNzYsMC45NjQsMS4xNzMtMC4zNzMsNi44MDJjMS4zNDgsMi43ODQsMy4yNTgsNS42Miw1LjA4OCw3LjU2Mg0KCQljMS4zMTEtMC4yMzcsMi40MzktMC4zNTgsMy4zNTgtMC4zNThjMS41NjYsMCwyLjUxNSwwLjM2NSwyLjkwMiwxLjExN2MwLjMyLDAuNjIyLDAuMTg5LDEuMzQ5LTAuMzksMi4xNg0KCQljLTAuNTU3LDAuNzc5LTEuMzI1LDEuMTkxLTIuMjIsMS4xOTFjLTEuMjE2LDAtMi42MzItMC43NjgtNC4yMTEtMi4yODVjLTIuODM3LDAuNTkzLTYuMTUsMS42NTEtOC44MjgsMi44MjINCgkJYy0wLjgzNiwxLjc3NC0xLjYzNywzLjIwMy0yLjM4Myw0LjI1MUMyMS4yNzMsMzIuNjU0LDIwLjM4OSwzMy4zMjQsMTkuNTE0LDMzLjMyNHogTTIyLjE3NiwyOC4xOTgNCgkJYy0yLjEzNywxLjIwMS0zLjAwOCwyLjE4OC0zLjA3MSwyLjc0NGMtMC4wMSwwLjA5Mi0wLjAzNywwLjMzNCwwLjQzMSwwLjY5MkMxOS42ODUsMzEuNTg3LDIwLjU1NSwzMS4xOSwyMi4xNzYsMjguMTk4eg0KCQkgTTM1LjgxMywyMy43NTZjMC44MTUsMC42MjcsMS4wMTQsMC45NDQsMS41NDcsMC45NDRjMC4yMzQsMCwwLjkwMS0wLjAxLDEuMjEtMC40NDFjMC4xNDktMC4yMDksMC4yMDctMC4zNDMsMC4yMy0wLjQxNQ0KCQljLTAuMTIzLTAuMDY1LTAuMjg2LTAuMTk3LTEuMTc1LTAuMTk3QzM3LjEyLDIzLjY0OCwzNi40ODUsMjMuNjcsMzUuODEzLDIzLjc1NnogTTI4LjM0MywxNy4xNzQNCgkJYy0wLjcxNSwyLjQ3NC0xLjY1OSw1LjE0NS0yLjY3NCw3LjU2NGMyLjA5LTAuODExLDQuMzYyLTEuNTE5LDYuNDk2LTIuMDJDMzAuODE1LDIxLjE1LDI5LjQ2NiwxOS4xOTIsMjguMzQzLDE3LjE3NHoNCgkJIE0yNy43MzYsOC43MTJjLTAuMDk4LDAuMDMzLTEuMzMsMS43NTcsMC4wOTYsMy4yMTZDMjguNzgxLDkuODEzLDI3Ljc3OSw4LjY5OCwyNy43MzYsOC43MTJ6Ii8+DQoJPHBhdGggc3R5bGU9ImZpbGw6I0NDNEI0QzsiIGQ9Ik00OC4wMzcsNTZINy45NjNDNy4xNTUsNTYsNi41LDU1LjM0NSw2LjUsNTQuNTM3VjM5aDQzdjE1LjUzN0M0OS41LDU1LjM0NSw0OC44NDUsNTYsNDguMDM3LDU2eiIvPg0KCTxnPg0KCQk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTE3LjM4NSw1M2gtMS42NDFWNDIuOTI0aDIuODk4YzAuNDI4LDAsMC44NTIsMC4wNjgsMS4yNzEsMC4yMDUNCgkJCWMwLjQxOSwwLjEzNywwLjc5NSwwLjM0MiwxLjEyOCwwLjYxNWMwLjMzMywwLjI3MywwLjYwMiwwLjYwNCwwLjgwNywwLjk5MXMwLjMwOCwwLjgyMiwwLjMwOCwxLjMwNg0KCQkJYzAsMC41MTEtMC4wODcsMC45NzMtMC4yNiwxLjM4OGMtMC4xNzMsMC40MTUtMC40MTUsMC43NjQtMC43MjUsMS4wNDZjLTAuMzEsMC4yODItMC42ODQsMC41MDEtMS4xMjEsMC42NTYNCgkJCXMtMC45MjEsMC4yMzItMS40NDksMC4yMzJoLTEuMjE3VjUzeiBNMTcuMzg1LDQ0LjE2OHYzLjk5MmgxLjUwNGMwLjIsMCwwLjM5OC0wLjAzNCwwLjU5NS0wLjEwMw0KCQkJYzAuMTk2LTAuMDY4LDAuMzc2LTAuMTgsMC41NC0wLjMzNWMwLjE2NC0wLjE1NSwwLjI5Ni0wLjM3MSwwLjM5Ni0wLjY0OWMwLjEtMC4yNzgsMC4xNS0wLjYyMiwwLjE1LTEuMDMyDQoJCQljMC0wLjE2NC0wLjAyMy0wLjM1NC0wLjA2OC0wLjU2N2MtMC4wNDYtMC4yMTQtMC4xMzktMC40MTktMC4yOC0wLjYxNWMtMC4xNDItMC4xOTYtMC4zNC0wLjM2LTAuNTk1LTAuNDkyDQoJCQljLTAuMjU1LTAuMTMyLTAuNTkzLTAuMTk4LTEuMDEyLTAuMTk4SDE3LjM4NXoiLz4NCgkJPHBhdGggc3R5bGU9ImZpbGw6I0ZGRkZGRjsiIGQ9Ik0zMi4yMTksNDcuNjgyYzAsMC44MjktMC4wODksMS41MzgtMC4yNjcsMi4xMjZzLTAuNDAzLDEuMDgtMC42NzcsMS40NzdzLTAuNTgxLDAuNzA5LTAuOTIzLDAuOTM3DQoJCQlzLTAuNjcyLDAuMzk4LTAuOTkxLDAuNTEzYy0wLjMxOSwwLjExNC0wLjYxMSwwLjE4Ny0wLjg3NSwwLjIxOUMyOC4yMjIsNTIuOTg0LDI4LjAyNiw1MywyNy44OTgsNTNoLTMuODE0VjQyLjkyNGgzLjAzNQ0KCQkJYzAuODQ4LDAsMS41OTMsMC4xMzUsMi4yMzUsMC40MDNzMS4xNzYsMC42MjcsMS42LDEuMDczczAuNzQsMC45NTUsMC45NSwxLjUyNEMzMi4xMTQsNDYuNDk0LDMyLjIxOSw0Ny4wOCwzMi4yMTksNDcuNjgyeg0KCQkJIE0yNy4zNTIsNTEuNzk3YzEuMTEyLDAsMS45MTQtMC4zNTUsMi40MDYtMS4wNjZzMC43MzgtMS43NDEsMC43MzgtMy4wOWMwLTAuNDE5LTAuMDUtMC44MzQtMC4xNS0xLjI0NA0KCQkJYy0wLjEwMS0wLjQxLTAuMjk0LTAuNzgxLTAuNTgxLTEuMTE0cy0wLjY3Ny0wLjYwMi0xLjE2OS0wLjgwN3MtMS4xMy0wLjMwOC0xLjkxNC0wLjMwOGgtMC45NTd2Ny42MjlIMjcuMzUyeiIvPg0KCQk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTM2LjI2Niw0NC4xNjh2My4xNzJoNC4yMTF2MS4xMjFoLTQuMjExVjUzaC0xLjY2OFY0Mi45MjRINDAuOXYxLjI0NEgzNi4yNjZ6Ii8+DQoJPC9nPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPC9zdmc+DQo=" type="image/svg+xml" /></i>',
                    titleAttr: 'PDF',
                    enabled: ObjData.EnpdfHtml5,
                    //key: {
                    //    key: 'c',
                    //    CtrlKey: true
                    //},
                    className: 'ExcelPDFCPCSVShow'


                },

                {
                    extend: 'copyHtml5',
                    text: '<i class="fa fa" > <img style="height:20px;" src="data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pg0KPCEtLSBHZW5lcmF0b3I6IEFkb2JlIElsbHVzdHJhdG9yIDE5LjAuMCwgU1ZHIEV4cG9ydCBQbHVnLUluIC4gU1ZHIFZlcnNpb246IDYuMDAgQnVpbGQgMCkgIC0tPg0KPHN2ZyB2ZXJzaW9uPSIxLjEiIGlkPSJDYXBhXzEiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgeG1sbnM6eGxpbms9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkveGxpbmsiIHg9IjBweCIgeT0iMHB4Ig0KCSB2aWV3Qm94PSIwIDAgMzQuNTU1IDM0LjU1NSIgc3R5bGU9ImVuYWJsZS1iYWNrZ3JvdW5kOm5ldyAwIDAgMzQuNTU1IDM0LjU1NTsiIHhtbDpzcGFjZT0icHJlc2VydmUiPg0KPGc+DQoJPGc+DQoJCTxnPg0KCQkJPHBhdGggZD0iTTI0LjA2NSwzNC41NTVINS40ODljLTEuMzc5LDAtMi41LTEuMTIyLTIuNS0yLjVWNy44NjRjMC0xLjM3OCwxLjEyMS0yLjUsMi41LTIuNWgyLjM2NGMwLjI3NiwwLDAuNSwwLjIyNCwwLjUsMC41DQoJCQkJcy0wLjIyNCwwLjUtMC41LDAuNUg1LjQ4OWMtMC44MjcsMC0xLjUsMC42NzMtMS41LDEuNXYyNC4xOWMwLDAuODI3LDAuNjczLDEuNSwxLjUsMS41aDE4LjU3NmMwLjgyNywwLDEuNS0wLjY3MywxLjUtMS41di0yLjM2NQ0KCQkJCWMwLTAuMjc2LDAuMjI0LTAuNSwwLjUtMC41czAuNSwwLjIyNCwwLjUsMC41djIuMzY1QzI2LjU2NSwzMy40MzMsMjUuNDQ0LDM0LjU1NSwyNC4wNjUsMzQuNTU1eiIvPg0KCQk8L2c+DQoJPC9nPg0KCTxnPg0KCQk8Zz4NCgkJCTxwYXRoIGQ9Ik0yOS4wNjUsMjkuMTlIMTAuNDg5Yy0xLjM3OSwwLTIuNS0xLjEyMi0yLjUtMi41VjIuNWMwLTEuMzc4LDEuMTIxLTIuNSwyLjUtMi41aDEzLjYwNGMwLjI3NiwwLDAuNSwwLjIyNCwwLjUsMC41DQoJCQkJUzI0LjM3LDEsMjQuMDk0LDFIMTAuNDg5Yy0wLjgyNywwLTEuNSwwLjY3My0xLjUsMS41djI0LjE5YzAsMC44MjcsMC42NzMsMS41LDEuNSwxLjVoMTguNTc2YzAuODI3LDAsMS41LTAuNjczLDEuNS0xLjVWNy42NjENCgkJCQljMC0wLjI3NiwwLjIyNC0wLjUsMC41LTAuNXMwLjUsMC4yMjQsMC41LDAuNVYyNi42OUMzMS41NjUsMjguMDY5LDMwLjQ0NCwyOS4xOSwyOS4wNjUsMjkuMTl6Ii8+DQoJCQk8cGF0aCBkPSJNMzEuMDY1LDguMTYxaC02Ljk3MmMtMC4yNzYsMC0wLjUtMC4yMjQtMC41LTAuNVYwLjY4OGMwLTAuMjc2LDAuMjI0LTAuNSwwLjUtMC41czAuNSwwLjIyNCwwLjUsMC41djYuNDczaDYuNDcyDQoJCQkJYzAuMjc2LDAsMC41LDAuMjI0LDAuNSwwLjVTMzEuMzQyLDguMTYxLDMxLjA2NSw4LjE2MXoiLz4NCgkJCTxwYXRoIGQ9Ik0zMS4wNjUsOC4xNjFjLTAuMTMsMC0wLjI2LTAuMDUxLTAuMzU4LTAuMTUxbC02Ljk3Mi03LjE2MWMtMC4xOTItMC4xOTgtMC4xODgtMC41MTQsMC4wMS0wLjcwNw0KCQkJCWMwLjE5Ny0wLjE5MSwwLjUxNi0wLjE4NywwLjcwNywwLjAxbDYuOTcyLDcuMTYxYzAuMTkyLDAuMTk4LDAuMTg4LDAuNTE0LTAuMDEsMC43MDdDMzEuMzE3LDguMTE0LDMxLjE5MSw4LjE2MSwzMS4wNjUsOC4xNjF6Ig0KCQkJCS8+DQoJCTwvZz4NCgk8L2c+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8L3N2Zz4NCg==" type="image/svg+xml" /></i>',
                    titleAttr: 'COPY',
                    enabled: ObjData.EncopyHtml5
                },

                {
                    extend: 'print',
                    text: '<i class="fa fa" > <img style="height:20px;" src="data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pg0KPCEtLSBHZW5lcmF0b3I6IEFkb2JlIElsbHVzdHJhdG9yIDE5LjAuMCwgU1ZHIEV4cG9ydCBQbHVnLUluIC4gU1ZHIFZlcnNpb246IDYuMDAgQnVpbGQgMCkgIC0tPg0KPHN2ZyB2ZXJzaW9uPSIxLjEiIGlkPSJMYXllcl8xIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIiB4PSIwcHgiIHk9IjBweCINCgkgdmlld0JveD0iMCAwIDQ5MCA0OTAiIHN0eWxlPSJlbmFibGUtYmFja2dyb3VuZDpuZXcgMCAwIDQ5MCA0OTA7IiB4bWw6c3BhY2U9InByZXNlcnZlIj4NCjxnPg0KCTxnPg0KCQk8cGF0aCBzdHlsZT0iZmlsbDojMkMyRjMzOyIgZD0iTTQ4MS44NSwzNDIuOVYxNjIuMmMwLTIyLjctMTguNS00MS4yLTQxLjItNDEuMmgtNDkuNlYyNi41YzAtMTQuNi0xMS45LTI2LjUtMjYuNS0yNi41aC0yMzkuMQ0KCQkJYy0xNC42LDAtMjYuNSwxMS45LTI2LjUsMjYuNVYxMjFoLTQ5LjZjLTIyLjcsMC00MS4yLDE4LjUtNDEuMiw0MS4ydjE4MC43YzAsMjIuNywxOC41LDQxLjIsNDEuMiw0MS4yaDQ5LjZ2NzkuNA0KCQkJYzAsMTQuNiwxMS45LDI2LjUsMjYuNSwyNi41aDIzOS4xYzE0LjYsMCwyNi41LTExLjksMjYuNS0yNi41VjM4NGg0OS42QzQ2My40NSwzODQsNDgxLjg1LDM2NS42LDQ4MS44NSwzNDIuOXogTTExOC43NSwyNi41DQoJCQljMC0zLjcsMy02LjcsNi43LTYuN2gyMzkuMWMzLjcsMCw2LjcsMyw2LjcsNi43VjEyMWgtMjUyLjVWMjYuNXogTTM3MS4zNSw0NjMuNWMwLDMuNy0zLDYuNy02LjcsNi43aC0yMzkuMmMtMy43LDAtNi43LTMtNi43LTYuNw0KCQkJdi0yMDFoMjUyLjV2MjAxSDM3MS4zNXogTTQ2Mi4wNSwzNDIuOWMwLDExLjgtOS42LDIxLjQtMjEuNCwyMS40aC00OS42VjI1Mi42YzAtNS41LTQuNC05LjktOS45LTkuOWgtMjcyLjMNCgkJCWMtNS41LDAtOS45LDQuNC05LjksOS45djExMS43aC00OS42Yy0xMS44LDAtMjEuNC05LjYtMjEuNC0yMS40VjE2Mi4yYzAtMTEuOCw5LjYtMjEuNCwyMS40LTIxLjRoMzkxLjMNCgkJCWMxMS44LDAsMjEuNCw5LjYsMjEuNCwyMS40VjM0Mi45TDQ2Mi4wNSwzNDIuOXoiLz4NCgkJPHBhdGggc3R5bGU9ImZpbGw6IzJDMkYzMzsiIGQ9Ik03OC41NSwxODEuNmMtMi42LDAtNS4xLDEuMS03LDIuOWMtMS44LDEuOS0yLjksNC40LTIuOSw3czEuMSw1LjEsMi45LDdjMS45LDEuOCw0LjQsMi45LDcsMi45DQoJCQlzNS4xLTEuMSw3LTIuOWMxLjgtMS44LDIuOS00LjQsMi45LTdzLTEuMS01LjEtMi45LTdDODMuNzUsMTgyLjcsODEuMTUsMTgxLjYsNzguNTUsMTgxLjZ6Ii8+DQoJCTxwYXRoIHN0eWxlPSJmaWxsOiMzQzkyQ0E7IiBkPSJNMTc0Ljk1LDM3Ni4yaDE0MC4zYzUuNSwwLDkuOS00LjQsOS45LTkuOXMtNC40LTkuOS05LjktOS45aC0xNDAuM2MtNS41LDAtOS45LDQuNC05LjksOS45DQoJCQlTMTY5LjQ1LDM3Ni4yLDE3NC45NSwzNzYuMnoiLz4NCgkJPHBhdGggc3R5bGU9ImZpbGw6IzNDOTJDQTsiIGQ9Ik0xNzQuOTUsNDMyLjNoMTQwLjNjNS41LDAsOS45LTQuNCw5LjktOS45cy00LjQtOS45LTkuOS05LjloLTE0MC4zYy01LjUsMC05LjksNC40LTkuOSw5LjkNCgkJCVMxNjkuNDUsNDMyLjMsMTc0Ljk1LDQzMi4zeiIvPg0KCQk8cGF0aCBzdHlsZT0iZmlsbDojM0M5MkNBOyIgZD0iTTE3NC45NSwzMjAuMWgxNDAuM2M1LjUsMCw5LjktNC40LDkuOS05LjlzLTQuNC05LjktOS45LTkuOWgtMTQwLjNjLTUuNSwwLTkuOSw0LjQtOS45LDkuOQ0KCQkJUzE2OS40NSwzMjAuMSwxNzQuOTUsMzIwLjF6Ii8+DQoJPC9nPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPGc+DQo8L2c+DQo8Zz4NCjwvZz4NCjxnPg0KPC9nPg0KPC9zdmc+DQo=" type="image/svg+xml" /></i>',
                    titleAttr: 'PRINT',
                    enabled: ObjData.Enprint
                }
            ],
        });
    } catch (err) {
    }
}
function SearchTable(ObjData, tid) {
    try {
        var texto = $(ObjData).val();
        //filtro(texto, tid);
        ////$("#search").on("keyup", function () {
        //    var value = $(ObjData).val().toLowerCase();
        //    $("#" + tid + " li").filter(function () {
        //        $(this).toggle($(this).va.toLowerCase().indexOf(value) > -1)
        //    });
        ////});
        var value = $(ObjData).val().toLowerCase();
        $("#" + tid + " li").filter(function () {
            debugger;
            if ($(this).text().toLowerCase().trim().indexOf(texto.toLowerCase()) >= 0) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    } catch (err) {
    }
}
function Validation(cls) {
    var flag = true;
    try {
        $('.' + cls).each(function () {
            var Datas = "";
            if (this.selectedIndex != undefined) {
                Datas = this.selectedOptions[0].innerText;
            } else {
                Datas = this.value.trim();
            }
            if (this.value.trim() != "" && Datas != 'Select Your Operator' && Datas != 'Select Your Circle') {
                var TAgName = this.getAttribute('TagName');
                if (TAgName != null) {
                    if (TAgName == "Pan") {
                        if (this.value.length < 10) {
                            flag = false;
                            this.style.border = "1px solid red";
                            alertify.error('Pan Number Not Correct.')
                        } else {
                            flag = PanValidate(this);
                        }
                    }
                    if (TAgName == "Aadhaar") {
                        if (this.value.trim().length < 12) {
                            flag = false;
                            this.style.border = "1px solid red";
                            alertify.error('Addhar Number Not Correct.')
                        } else {
                            this.style.border = "1px solid lightgrey";
                        }
                    }
                } else {
                    this.style.border = "1px solid lightgrey";
                }
            } else {
                flag = false;
                this.style.border = "1px solid red";
            }
        });
    } catch (err) {
    }
    return flag;
}


function PanValidate(ObjData) {
    var flag = true;
    try {
        var regpan = /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/;
        if (regpan.test(ObjData.value.trim())) {
            //  alertify.success('valid pan card number');
            ObjData.style.border = "1px solid lightgrey";
        } else {
            flag = false;
            alertify.error('invalid pan card number');
            ObjData.style.border = "1px solid red";

        }
    } catch (err) {
    }
    return flag;
}

function TextMatchData(cls, cls1, idcls) {
    try {
        var ObjData1 = $('.' + cls);//.val().trim();
        var ObjData2 = $('.' + cls1);//.val().trim();
        var Val1 = ObjData1.val().trim();
        var Val2 = ObjData2.val().trim();
        if (Val1 != "" && Val2 != "") {
            if (Val1 == Val2) {
                $('.' + idcls).removeAttr("disabled");
            } else {
                alertify.error('Enter right password');
                $('.' + idcls).attr("disabled", true);
            }
        }
    } catch (err) {
    }
}

function xmlToJson(xml) {

    // Create the return object
    var obj = {};

    if (xml.nodeType == 1) { // element
        // do attributes
        if (xml.attributes.length > 0) {
            obj["@attributes"] = {};
            for (var j = 0; j < xml.attributes.length; j++) {
                var attribute = xml.attributes.item(j);
                obj["@attributes"][attribute.nodeName] = attribute.nodeValue;
            }
        }
    } else if (xml.nodeType == 3) { // text
        obj = xml.nodeValue;
    }

    // do children
    if (xml.hasChildNodes()) {
        for (var i = 0; i < xml.childNodes.length; i++) {
            var item = xml.childNodes.item(i);
            var nodeName = item.nodeName;
            if (typeof (obj[nodeName]) == "undefined") {
                obj[nodeName] = xmlToJson(item);
            } else {
                if (typeof (obj[nodeName].push) == "undefined") {
                    var old = obj[nodeName];
                    obj[nodeName] = [];
                    obj[nodeName].push(old);
                }
                obj[nodeName].push(xmlToJson(item));
            }
        }
    }
    return obj;
};

