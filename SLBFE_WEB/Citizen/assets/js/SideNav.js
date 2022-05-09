const menu = document.querySelector(".side-nav");
const hamburger= document.querySelector(".hamburger");

function openMenu(){
    if (menu.classList.contains("openMenu")) {
        menu.classList.remove("openMenu");
        hamburger.classList.remove("hamburger-active");
      } 
    else {
        menu.classList.add("openMenu");
        hamburger.classList.add("hamburger-active");
      }
}