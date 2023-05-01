const btn = document.getElementById("input-tag");
const inputTag = document.getElementsByClassName("li-tag");
const searchBtn = document.getElementById("btn-tag");
console.log(btn);
console.log(inputTag)


function GetElement() {
    Array.from(inputTag).forEach(box => {
        box.addEventListener('click', (event) => {
            btn.value = box.innerText;
        });
    });
}


searchBtn.addEventListener("click", function () {
    insert(btn.value);
    console.log("fdghjkl")
}
)
function insert(res) {
    window.location.href = `https://localhost:44354/Tutor/GetTutorByStatus?text=${res}`
}

GetElement();