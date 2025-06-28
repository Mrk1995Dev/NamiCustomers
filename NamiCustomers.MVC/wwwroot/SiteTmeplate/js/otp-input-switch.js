"use strict";

let otpInput = document.querySelectorAll(".otp-input-group")[0];

// Add an event listener for the 'keyup' event
otpInput.addEventListener("keyup", function (e) {
    let optcode;
   

    let t = e.target,
        n = parseInt(t.attributes.maxlength.value, 10),
        a = t.value.length;
    optcode = document.getElementById("otp1").value + document.getElementById("otp2").value + document.getElementById("otp3").value + document.getElementById("otp4").value + document.getElementById("otp5").value;
    document.getElementById("otp").value = optcode;

    if (optcode.length == 5) {

    }
    if (a >= n) {
        for (let r = t; (r = r.previousElementSibling) && r != null;) {
            if (r.tagName.toLowerCase() === "input") {
                r.focus();
                
                break;
            }
        }
    } else if (a === 0) {
        for (let u = t; (u = u.nextElementSibling) && u != null;) {
            if (u.tagName.toLowerCase() === "input") {
                u.focus();
                
                break;
            }
        }
    }
});
