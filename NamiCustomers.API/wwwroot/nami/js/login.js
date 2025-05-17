$(function () {

    $("#btnLogin").on("click", function () {
        Login();
    });

    function toEnDigit(s) {
        return s.replace(/[\u0660-\u0669\u06f0-\u06f9]/g,    // Detect all Persian/Arabic Digit in range of their Unicode with a global RegEx character set
            function (a) { return a.charCodeAt(0) & 0xf }     // Remove the Unicode base(2) range that not match
        )
    }
    function Login() {
        var natinalcode = toEnDigit($("#natinalcode").val());
        var mobile = toEnDigit($("#mobile").val());
        var postData = {
            'natinalcode': natinalcode,
            'mobile': mobile,
        };
        $.ajax({
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json',
            type: "POST",
            url: "/Home/login",
            data: postData,
            success: function (data) {
                if (data.isSuccess == true) {
                    swal.fire(
                        'موفق!',
                        data.message,
                        'success'
                    ).then(function (isConfirm) {
                        window.location.replace("/Home/authconfirmmobile");
                    });


                }
                else {


                    toastr.options = {
                        "closeButton": true,
                        "debug": false,
                        "newestOnTop": true,
                        "progressBar": true,
                        "positionClass": "toast-bottom-right",
                        "preventDuplicates": false,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "slideDown",
                        "hideMethod": "slideUp",
                        "closeMethod": "slideUp"
                    }; toastr.options.rtl = true;
                    toastr["warning"](data.message);



                }
            },
            error: function (request, status, error) {

                toastr.options = {
                    "closeButton": true,
                    "debug": false,
                    "newestOnTop": true,
                    "progressBar": true,
                    "positionClass": "toast-bottom-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "300",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "slideDown",
                    "hideMethod": "slideUp",
                    "closeMethod": "slideUp"
                }; toastr.options.rtl = true;
                toastr["error"](request.responseText);
            }
        });
    }

    //function Membership() {
       

    //    //function toEnDigit(s) {
    //    //    return s.replace(/[\u0660-\u0669\u06f0-\u06f9]/g,    // Detect all Persian/Arabic Digit in range of their Unicode with a global RegEx character set
    //    //        function (a) { return a.charCodeAt(0) & 0xf }     // Remove the Unicode base(2) range that not match
    //    //    )
    //    //}
    //    var mobile = toEnDigit($("#mobilemembership").val());

    //   // var sample = $("#mobilemembership").val();
    //    // English: 0123456789 - Persian: 0123456789 - Arabic: 0123456789



    //    var name = $("#name").val();
    //    var family = $("#family").val();

    //    var mobile = toEnDigit($("#mobilemembership").val());
    //    var postData = {
    //        'name': name,
    //        'family': family,
    //        'mobile': mobile,
    //    };
    //    $.ajax({
    //        contentType: 'application/x-www-form-urlencoded',
    //        dataType: 'json',
    //        type: "POST",
    //        url: "/Home/Membership",
    //        data: postData,
    //        success: function (data) {
    //            if (data.isSuccess == true) {
    //                swal.fire(
    //                    'موفق!',
    //                    data.message,
    //                    'success'
    //                ).then(function (isConfirm) {
    //                    window.location.replace("/Home/authconfirmmobileMember");
    //                });


    //            }
    //            else {


    //                toastr.options = {
    //                    "closeButton": true,
    //                    "debug": false,
    //                    "newestOnTop": true,
    //                    "progressBar": true,
    //                    "positionClass": "toast-bottom-right",
    //                    "preventDuplicates": false,
    //                    "onclick": null,
    //                    "showDuration": "300",
    //                    "hideDuration": "1000",
    //                    "timeOut": "5000",
    //                    "extendedTimeOut": "1000",
    //                    "showEasing": "swing",
    //                    "hideEasing": "linear",
    //                    "showMethod": "slideDown",
    //                    "hideMethod": "slideUp",
    //                    "closeMethod": "slideUp"
    //                }; toastr.options.rtl = true;
    //                toastr["warning"](data.message);



    //            }
    //        },
    //        error: function (request, status, error) {

    //            toastr.options = {
    //                "closeButton": true,
    //                "debug": false,
    //                "newestOnTop": true,
    //                "progressBar": true,
    //                "positionClass": "toast-bottom-right",
    //                "preventDuplicates": false,
    //                "onclick": null,
    //                "showDuration": "300",
    //                "hideDuration": "1000",
    //                "timeOut": "5000",
    //                "extendedTimeOut": "1000",
    //                "showEasing": "swing",
    //                "hideEasing": "linear",
    //                "showMethod": "slideDown",
    //                "hideMethod": "slideUp",
    //                "closeMethod": "slideUp"
    //            }; toastr.options.rtl = true;
    //            toastr["error"](request.responseText);
    //        }
    //    });
    //}

});