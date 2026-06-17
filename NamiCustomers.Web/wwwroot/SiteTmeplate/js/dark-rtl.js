"use strict";

function applyThemeSettings(themeKey, attributeName, switchElement, containerClass) {
  var currentTheme = localStorage.getItem(themeKey);

  // اگر تمی در localStorage نبود، از تم پیش‌فرض استفاده کن
  if (!currentTheme) {
    currentTheme = (themeKey === "theme") ? "dark" : "rtl"; // تم تاریک پیش‌فرض
    localStorage.setItem(themeKey, currentTheme);
  }

  document.documentElement.setAttribute(attributeName, currentTheme);
  
  if (switchElement && (currentTheme === "dark" || currentTheme === "rtl")) {
    switchElement.checked = true;
  }

  if (switchElement) {
    switchElement.addEventListener("change", function(e) {
      var newTheme = e.target.checked ? (themeKey === "theme" ? "dark" : "rtl") : (themeKey === "theme" ? "light" : "rtl");
      document.documentElement.setAttribute(attributeName, newTheme);
      localStorage.setItem(themeKey, newTheme);
    }, false);

    switchElement.addEventListener("click", function() {
      var container = document.querySelector(containerClass);
      container.style.display = "block";
      container.style.opacity = 1;

      setTimeout(function() {
        var fadeOut = setInterval(function() {
          if (!container.style.opacity) {
            container.style.opacity = 1;
          }
          if (container.style.opacity > 0) {
            container.style.opacity -= 0.1;
          } else {
            clearInterval(fadeOut);
            container.style.display = "none";
          }
        }, 20);
      }, 1000);
    });
  }
}

document.addEventListener("DOMContentLoaded", function() {
  var toggleSwitch = document.getElementById("darkSwitch");
  var rtltoggleSwitch = document.getElementById("rtlSwitch");

  applyThemeSettings("theme", "data-theme", toggleSwitch, ".dark-mode-switching");
  applyThemeSettings("rtl", "view-mode", rtltoggleSwitch, ".rtl-mode-switching");
});