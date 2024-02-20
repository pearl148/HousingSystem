// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let paratext = document.getElementById('paratext');
let background = document.getElementById('background');
let car1 = document.getElementById('car1');
let car2 = document.getElementById('car2');
let building2 = document.getElementById('building2');
let building1 = document.getElementById('building1');
let righthouse = document.getElementById('righthouse');
let lastbase = document.getElementById('lastbase');
let cloud1 = document.getElementById('cloud1');
let cloud2 = document.getElementById('cloud2');
let cloud3 = document.getElementById('cloud3');
let cloud5 = document.getElementById('cloud5');
let leftchristmastree1 = document.getElementById('leftchristmastree1');
let rightbuildings = document.getElementById('rightbuildings');

window.addEventListener('scroll',()=>{
let value = window.scrollY;
paratext.style.marginLeft = value * 2.5 + 'px';

car1.style.left=value * 1.5 + 'px';
car2.style.left= value*1.5+'px';
building1.style.left=value * -1.5 + 'px';
building2.style.left=value * -1.5 + 'px';
rightbuildings.style.left=value *1.5+'px';
cloud3.style.left=value *1.5+'px';
cloud5.style.top=value *-1.5+'px';
cloud2.style.left=value *-3.5+'px';
cloud1.style.left=value *-3.5+'px';
righthouse.style.left=value*1.5+'px';

});