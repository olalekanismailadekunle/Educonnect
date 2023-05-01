// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var url = window.location.href.split('=')[1];
console.log(url)
var studentId = parseInt(url.split(',')[1]);
var tutorId = parseInt(url.split(',')[0]);
console.log(studentId);
console.log(tutorId);
var inputTag1 = document.getElementById("tutor-id");
inputTag1.value = tutorId;

var inputTag2 = document.getElementById("student-id");
inputTag2.value = studentId;

