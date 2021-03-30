const modal = document.querySelector("#modalContainer");
const link = document.querySelector("#size-chart");
const closeButton = document.querySelector(".close"); 
const main = document.querySelector("main");

link.addEventListener('click', () => {
    modal.style.display = "block";
});

closeButton.addEventListener('click', () => {
    modal.style.display = "none";
});

modal.addEventListener('click', () => {
        modal.style.display = "none";
});
