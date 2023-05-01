// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const btn = document.getElementsByClassName("btn-choose");
let id;
const divTag = document.getElementById("div-container")
divTag.style.display = "none";

console.log(id);
var addEvent = () => {
    Array.from(btn).forEach(item => {
        item.addEventListener('click', (e) => {

            
            console.log("proddcd")
            if (item.innerText == "Choose") {
                item.innerText = "Choosed";
                id = item.id;
                divTag.style.display = "block";

                Array.from(btn).forEach(item1 => {

                    if (item1 != e.target) {
                        item1.style.visibility = "hidden"
                    }
                })

            }
            else if (item.innerText == "Choosed")
            {
                item.innerText = "Choose";

                divTag.style.display = "none";

                Array.from(btn).forEach(item1 => {

                    if (item1 != e.target) {
                        item1.style.visibility = "visible"
                    }
                })
            }

            
              

    
        })
    })

}
var printArray = () => {
    arr.forEach(item => console.log(item));
}
var changeUrl = () => {
    divTag.addEventListener('click', () => {
        id += ',' + window.location.href.split('/')[5].toString();
        window.location.href = `https://localhost:44354/Booking/Create?id=${id}`

    })
}
addEvent();
changeUrl();
