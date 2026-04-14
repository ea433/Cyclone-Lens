function updateGOES() {
    const img = document.getElementById("goesImage");
    const timestamp = new Date().getTime();
    img.src = `https://cdn.star.nesdis.noaa.gov/GOES16/ABI/SECTOR/tatl/GEOCOLOR/latest.jpg?t=${timestamp}`;
}

updateGOES();
setInterval(updateGOES, 600000);