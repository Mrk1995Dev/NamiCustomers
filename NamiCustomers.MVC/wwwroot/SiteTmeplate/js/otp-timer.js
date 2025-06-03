"use strict";

let resendOTP = document.getElementById("resendOTP");

if(resendOTP) {
    let optcount = 120; // 2 دقیقه = 120 ثانیه
    let optcounter = setInterval(otptimer, 1000);
    
    function otptimer() {
        optcount = optcount - 1;
        
        // تبدیل ثانیه به دقیقه و ثانیه
        let minutes = Math.floor(optcount / 60);
        let seconds = optcount % 60;
        
        // فرمت‌دهی به صورت دو رقمی برای ثانیه (05 به جای 5)
        let formattedTime = minutes + ":" + (seconds < 10 ? "0" + seconds : seconds);
        
        if (optcount <= 0) {
            clearInterval(optcounter);
            resendOTP.innerHTML = '<a class="resendOTP" href="">دریافت مجدد کد</a>';
        } else {
            resendOTP.innerHTML = formattedTime + ' تا دریافت مجدد کد';
        }
    
        if (optcount <= 10) {
            resendOTP.style.color = "red";
            resendOTP.style.fontWeight = "bold";
        }
    }
    
    // اجرای اولیه تایمر
    otptimer();
}