// =============================== Get District List ========================== //
function get_district_list(val)
{
  if (val == '') 
  {
    $('#district').html('<option value="">Select State First</option>');
  }
  else
  {
    $.ajax({
      url : site_url + 'Registration/getdistrictlist',
      type : 'post',
      data : {state_name:val},
      dataType : 'json',
      success:function(result)
      {
        $('#district').html('<option value="">Select District</option>');
        $.each(result,function(index,value){
          $('#district').append('<option>'+value.district_name+'</option>');
        });
      },
    });
  }
}


function checkUserReferral(val)
{
  if (val.length == 8) {
    $.ajax({
      url : site_url + 'Registration/getReferralDetails',
      type : 'post',
      data : {referral_code:val},
      dataType : 'json',
      success:function(result)
      {
        if (result.status == 0) {
          $('#srcbtn').removeAttr('onclick');
          $('#srcbtn').attr("onclick","msgerror()");

            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Oops...',
                text: result.message,
                showConfirmButton: true})
            $('input[name=parent_referral_code').val('');

        } else {
            $('#mfull_name').html(result.full_name);
            $('#mcompany_name').html(result.company_name);
            $('#mmobile_no').html(result.mobile_no);
            $('#exampleModal').modal('show');
            $('#srcbtn').attr("onclick","openRefInfoModal()");
            

        }
      },
    });
  }
}

function msgerror()
{
  var val = $('#parent_referral_code').val();
  checkUserReferral(val);
}

function openRefInfoModal()
{
  $('#exampleModal').modal('show');
}
// =============================== end of get district list ======================= //

$("#signup_form").on("submit",function(){

  $('#preloader').show();

  $.ajax({
    url: site_url + 'Registration/signUp',
    type:'post',
    data:  new FormData(this),
    contentType: false,
    cache: false,
    processData:false,
    dataType:'json',

    success : function (result) {
      $('#preloader').hide();
        if (result.status == 0) {
              Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Oops...',
                text: result.message,
                showConfirmButton: false,
                timer: 2100})
          }
          else if (result.status == 2) {
              Swal.fire({
                position: 'center',
                icon: 'warning',
                title: 'Oops...',
                text: result.message,
                showConfirmButton: true})
          }
          else {
              Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Looks Good',
                text: result.message,
                showConfirmButton: true}).then(function() {
                 window.location = "/Login";});
          }
    }

  });
});