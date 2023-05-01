// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//const stateChoosen = document.querySelector("#state");
//const result = document.querySelector("#state-choosen");
//const localGovernment = document.querySelector("#lga");
//localGovernment.innerHTML = '<option value=""> Choose LocalGovernment</option> ';
//state.innerHTML = '<option value="">Choose State</option> ';
//console.log(stateChoosen)
//stateChoosen.addEventListener("change", () => { 
//    /*    console.log("changing...")*/
//    console.log(stateChoosen.value)
//   GetLocalGovernnment(stateChoosen.value);
//});
//function GetState() {
//    fetch("https://locationsng-api.herokuapp.com/api/v1/states")
//        .then((response) => response.json())
//        .then((res) =>
           
//            res.forEach((item) =>
//            state.innerHTML += ` <option value=${item.name}>${item.name}</option>`)
            
//        )
//}
//function GetLocalGovernnment(state)
//{
   

//    console.log(state)
//    console.log(localGovernment)
//    fetch(`https://locationsng-api.herokuapp.com/api/v1/states/${state}/lgas`)
//        .then((response) => response.json())
//        .then((res) => res.forEach((item) =>
//            localGovernment.innerHTML += ` <option value=${item}>${item}</option>`))
//}
//GetState();



function Get() {
    fetch('https://www.universal-tutorial.com/api/getaccesstoken',
        {
            headers: {
                "Accept": "application/json",
                "api-token": "3hu95rushMhJYpkh4gC2etU-QakZzWf8zqYa6XqZMgN56fJ7WvCfyHtiG3eG8-0Gzfg",
                "user-email": "yusuffahmad2005@gmail.com"
            }
        }).then(function (res) {
            return res.json()
        }).then((result) => {
            console.log(result)
        })
}

Get();
