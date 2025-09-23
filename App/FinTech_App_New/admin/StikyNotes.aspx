<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="StikyNotes.aspx.cs" Inherits="Admin_StikyNotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        @import url(https://fonts.googleapis.com/css?family=Gloria+Hallelujah);

        * {
            box-sizing: border-box;
        }

        /*body { background:url(https://subtlepatterns.com/patterns/little_pluses.png) #cacaca; margin:30px; }*/

        #create, textarea {
            float: left;
            padding: 25px 25px 40px;
            margin: 0 20px 20px 0;
            width: 200px;
            height: 250px;
        }

        #create {
            user-select: none;
            padding: 20px;
            border-radius: 20px;
            text-align: center;
            border: 15px solid rgba(0,0,0,0.1);
            cursor: pointer;
            color: rgba(0,0,0,0.1);
            font: 220px "Helvetica", sans-serif;
            line-height: 185px;
        }

            #create:hover {
                border-color: rgba(0,0,0,0.2);
                color: rgba(0,0,0,0.2);
            }

        textarea {
            font: 20px 'Gloria Hallelujah', cursive;
            line-height: 1.5;
            border: 0;
            border-radius: 3px;
            background: linear-gradient(#F9EFAF, #F7E98D);
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
            overflow: hidden;
            transition: box-shadow 0.5s ease;
            font-smoothing: subpixel-antialiased;
            max-width: 520px;
            max-height: 250px;
        }

            textarea:hover {
                box-shadow: 0 5px 8px rgba(0,0,0,0.15);
            }

            textarea:focus {
                box-shadow: 0 5px 12px rgba(0,0,0,0.2);
                outline: none;
            }

        .closeNotes {
            display: inline-block;
            position: absolute;
            float: right;
            display: none;
            /* right: 5px; */
            /*left: 200px;
    top: 2px;*/
        }
    </style>
    <script type="text/javascript">

        function Add(dd) {
            var i = 0;

            var count = $('.temp').length+1;

            var id = 'txtArea_' + count;
            $(dd).before("<div id=" + id + " class='temp'><a href='#' class='closeNotes' onclick='Remove(" + id + ");'><i class='fa fa-close' style='font-size:15px;color:red;'></i></a><textarea  onchange=\"find('add',this);\"></textarea></div>");
        }
        var item = "";
        function find(type,obj) {
            var i = 1;

            var ss = $(obj)[0].parentElement.id;

            var obj = { notes: $(obj).val(), ID: ss.split('_')[1] };
            $.ajax({
                type: 'POST',
                url: 'StikyNotes.aspx/SaveNotes',
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(obj),
                success: function (data) {

                    alertify.success("Add Successfully");



                },
                error: function (data) {
                    alertify.error("Error!!");
                },
            });
        }
        function Remove(id) {

            $(id).remove();


            find();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">

                <div runat="server" clientside="static" id="bindNotes">
                </div>



            </div>
        </section>
    </div>

</asp:Content>

