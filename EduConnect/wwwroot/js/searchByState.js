const btn = document.getElementById("btn-tag");
const inputTag = document.getElementById("input-tag");
console.log(btn);

btn.addEventListener("click", function () {
    insert(inputTag.value);
    console.log("fdghjkl")
}
)
function insert(res) {
    window.location.href = `https://localhost:44354/Tutor/GetTutorByState?name=${res}`
}