// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const stateChoosen = document.querySelector("#state");
const result = document.querySelector("#state-choosen");
const localGovernment = document.querySelector("#lga");
localGovernment.innerHTML = '<option value=""> Choose LocalGovernment</option> ';
state.innerHTML = '<option value="">Choose State</option> ';
console.log(stateChoosen)
stateChoosen.addEventListener("change", () => { 
    /*    console.log("changing...")*/
    console.log(stateChoosen.value)
   GetLocalGovernnment(stateChoosen.value);
});
function GetState() {
    fetch('https://locationsng-api.herokuapp.com/api/v1/states')
        .then((response) => response.json())
        .then((res) => {
            console.log(res)
            res.forEach((item) => {
                console.log(item);
                state.innerHTML += ` <option value=${item.name}>${item.name}</option>`;
            })
               
        })
           
            
        
}
function GetLocalGovernnment(state)
{
   

    console.log(state)
    console.log(localGovernment)
    fetch(`https://locationsng-api.herokuapp.com/api/v1/states/${state}/lgas`)
        .then((response) => response.json())
        .then((res) => {
            console.log(res);
            localGovernment.innerHTML = '<option value=""> Choose LocalGovernment</option>';
            res.forEach((item) =>
                localGovernment.innerHTML += ` <option value=${item}>${item}</option>`)
        })
}
GetState();


