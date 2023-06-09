let  mybutton = document.querySelector(".btn-menu");
let  quant = document.querySelector(".quant");
   function add (e){
    // hide Add to cart
    e.style.display = "none";
    // display block for quant
    quant.style.display = "block";
  }
function handleFileSelection(event) {
    var file = event.target.files[0];

    // Perform your desired action here
    // This code will be executed immediately after selecting the file
    // You can access the selected file using the 'file' variable
}
function deletephoto()
{
    var img = document.getElementById("old");
    old.style = "display:none;";
    console.log("i do it")
}
