btn.addEventListener("click",()=>{
    let url="https://localhost:7028/qr";
    url+="?text=" + txtQR.value;
    fetch(url).then(res=>res.text())
    .then(text=>QR.src= "data:imgae/png;base64,"+text)
})