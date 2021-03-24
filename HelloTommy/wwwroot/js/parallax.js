const parallax = document.querySelectorAll("#parallax");
window.addEventListener("scroll", function () {
    let offset = window.pageYOffset;
    parallax.forEach(function (prllx) {
        prllx.style.backgroundPositionY = (offset - prllx.offsetTop) * 0.7 + "px";
    })
})