// all this JS is from yesterday testing - doesn't currently work

document.addEventListener("DOMContentLoaded", function () {

    function updateTime() {
        const now = new Date();

        const formatted = now.toLocaleString();

        const timeEl = document.getElementById("time");
        if (timeEl) timeEl.textContent = formatted;
    }

    function updateGOES() {
        const timestamp = new Date().getTime();

        const GeoColor = document.getElementById("geoColor");
        const AirMass = document.getElementById("airMass");
        const SWIR = document.getElementById("swir");

        if (GeoColor) {
            GeoColor.src = `https://cdn.star.nesdis.noaa.gov/GOES19/ABI/SECTOR/taw/GEOCOLOR/latest.jpg?t=${timestamp}`;
        }

        if (AirMass) {
            AirMass.src = `https://cdn.star.nesdis.noaa.gov/GOES19/ABI/SECTOR/taw/AIRMASS/latest.jpg?t=${timestamp}`;
        }

        if (SWIR) {
            SWIR.src = `https://cdn.star.nesdis.noaa.gov/GOES19/ABI/SECTOR/taw/SWIR/latest.jpg?t=${timestamp}`;
        }

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