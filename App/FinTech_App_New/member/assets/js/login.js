// =============================== Start of Login Check =============================== //
  $('#login_check').on('submit',function(e){

    e.preventDefault();

    $('#preloader').show();

    $.ajax({
      url : site_url + 'Home/login_check',
      type: 'POST',
      data:  new FormData(this),
      contentType: false,
      cache: false,
      processData:false,
      dataType:'json',
      beforeSend: function(){
      $("#login_btn").attr({ "value":"Please Wait !!!", "disabled":"disabled" });
      },
      complete: function(){
      $("#login_btn").attr({ "value":"Login in", "disabled":false });
      },
      success : function (result) {
        $('#preloader').hide(0);
        if (result.status == 0) {
          Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Oops...',
            text: result.message,
            showConfirmButton: true})

        } else if (result.status == 2) {
          
          $('#login_div').hide();
          $('#otp_div').show();
          $('#loginMobile3').val(result.mobileno);
          $('#loginPassword3').val(result.password);
          $('#loginMobile4').val(result.mobileno);
          $('#loginPassword4').val(result.password);

        } else if (result.status == 3) {

          $('#login_div').hide();
          $('#login_otp_div').show();
          $('#loginMobile').val(result.mobileno);
          $('#loginPassword').val(result.password);
          $('#loginMobile2').val(result.mobileno);
          $('#loginPassword2').val(result.password);

        } else if(result.status == 1) {
          Swal.fire({
            position: 'center',
            icon: 'success',
            title: 'Looks Good',
            text: result.message,
            showConfirmButton: false})
            window.location.href = result.redirect_url;

        }
      }
    });
  });
// =============================== End of Login Check ================================= //


// ============================== Start of Verify Login OTP =========================== //
  $('#login_otp_check').on('submit',function(e){

    e.preventDefault();

    $('#preloader').show();

    $.ajax({
      url : site_url + 'Home/verifyLoginOtp',
      type: 'POST',
      data:  new FormData(this),
      contentType: false,
      cache: false,
      processData:false,
      dataType:'json',
      success : function (result) {
        $('#preloader').hide(0);
        if (result.status == 0) {
          Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Oops...',
            text: result.message,
            showConfirmButton: true})

        } else if(result.status == 1) {
          Swal.fire({
            position: 'center',
            icon: 'success',
            title: 'Looks Good',
            text: result.message,
            showConfirmButton: false})
            window.location.href = result.redirect_url;
        }
      }
    });
  });
// ============================== End of Verify Login OTP ============================= //


// ============================== Start of Resend Login OTP =========================== //
  $('#resendLoginOtp').on('submit',function(e){

    e.preventDefault();

    $('#preloader').show();

    $.ajax({
      url : site_url + 'Home/resendOTP',
      type: 'POST',
      data:  new FormData(this),
      contentType: false,
      cache: false,
      processData:false,
      dataType:'json',
      beforeSend: function(){
      $("#login_resend_btn").attr({ "value":"Please Wait !!!", "disabled":"disabled" });
      },
      complete: function(){
      $("#login_resend_btn").attr({ "value":"Resend OTP", "disabled":false });
      },
      success : function (result) {
        $('#preloader').hide(0);
        if (result.status == 0) {
          Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Oops...',
            text: result.message,
            showConfirmButton: true})

        } else if(result.status == 1) {
          Swal.fire({
            position: 'center',
            icon: 'success',
            title: 'Looks Good',
            text: result.message,
            showConfirmButton: false,
            timer: 2100})
        }
      }
    });
  });
// ============================== End of Resend Login OTP ============================= //


// ============================== Start of Reset Password =========================== //
  $('#resetPassword').on('submit',function(e){

    e.preventDefault();

    $('#preloader').show();

    $.ajax({
      url : site_url + 'Home/resetPassword',
      type: 'POST',
      data:  new FormData(this),
      contentType: false,
      cache: false,
      processData:false,
      dataType:'json',
      beforeSend: function(){
      $("#password_btn").attr({ "value":"Please Wait !!!", "disabled":"disabled" });
      },
      complete: function(){
      $("#password_btn").attr({ "value":"Reset Password", "disabled":false });
      },
      success : function (result) {
        $('#preloader').hide(0);
        if (result.status == 0) {
          Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Oops...',
            text: result.message,
            showConfirmButton: true})

        } else if(result.status == 1) {
          Swal.fire({
            position: 'center',
            icon: 'success',
            title: 'Looks Good',
            text: result.message,
            showConfirmButton: false,
            timer: 2100})
        }
      }
    });
  });
// ============================== End of Reset Password ============================= //

// ============================== Start of Verify Mobile Number =========================== //
  $('#verifyMobileOtp').on('submit',function(e){

    e.preventDefault();

    $('#preloader').show();

    $.ajax({
      url : site_url + 'Home/verifyMobile',
      type: 'POST',
      data:  new FormData(this),
      contentType: false,
      cache: false,
      processData:false,
      dataType:'json',
      success : function (result) {
        $('#preloader').hide(0);
        if (result.status == 0) {
          Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Oops...',
            text: result.message,
            showConfirmButton: true})

        } else if(result.status == 1) {
          Swal.fire({
            position: 'center',
            icon: 'success',
            title: 'Looks Good',
            text: result.message,
            showConfirmButton: false})
            window.location.href = result.redirect_url;
        }
      }
    });
  });
// ============================== End of Verify Mobile Number ============================= //


// ============================== Start of Resend Login OTP =========================== //
  $('#resendVerifyOtp').on('submit',function(e){

    e.preventDefault();

    $('#preloader').show();

    $.ajax({
      url : site_url + 'Home/resendOTP',
      type: 'POST',
      data:  new FormData(this),
      contentType: false,
      cache: false,
      processData:false,
      dataType:'json',
      beforeSend: function(){
      $("#verify_resend_btn").attr({ "value":"Please Wait !!!", "disabled":"disabled" });
      },
      complete: function(){
      $("#verify_resend_btn").attr({ "value":"Resend OTP", "disabled":false });
      },
      success : function (result) {
        $('#preloader').hide(0);
        if (result.status == 0) {
          Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Oops...',
            text: result.message,
            showConfirmButton: true})

        } else if(result.status == 1) {
          Swal.fire({
            position: 'center',
            icon: 'success',
            title: 'Looks Good',
            text: result.message,
            showConfirmButton: false,
            timer: 2100})
        }
      }
    });
  });
// ============================== End of Resend Login OTP ============================= //