// all this JS is from yesterday testing - doesn't currently work

document.addEventListener("DOMContentLoaded", function () {

    function updateTime() {
        const now = new Date();

        const formatted = now.toLocaleString();

        const timeEl = document.getElementById("time");
        if (timeEl) timeEl.textContent = formatted;
    }

    function updateGOES() {
        const img = document.getElementById("goesImage");

        if (!img) return;

        const timestamp = new Date().getTime();

        img.src = `https://cdn.star.nesdis.noaa.gov/GOES19/ABI/SECTOR/taw/GEOCOLOR/latest.jpg?t=${timestamp}`;

        updateTime();
    }

    // set location safely
    const loc = document.getElementById("location");
    if (loc) loc.textContent = "North Atlantic Basin";

    // run immediately
    updateGOES();

    // refresh every 10 minutes
    setInterval(updateGOES, 600000);

});