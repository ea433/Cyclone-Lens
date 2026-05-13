document.addEventListener("DOMContentLoaded", function () {

    const sources = {
        GeoColor:
            "https://cdn.star.nesdis.noaa.gov/GOES16/ABI/SECTOR/taw/GEOCOLOR/latest.jpg",

        Luchtmassa:
            "https://cdn.star.nesdis.noaa.gov/GOES19/ABI/SECTOR/taw/AirMass/latest.jpg",

        SWIR:
            "https://cdn.star.nesdis.noaa.gov/GOES16/ABI/SECTOR/taw/07/latest.jpg",
        
        Bliksemflitsomvang:
            "https://cdn.star.nesdis.noaa.gov/GOES19/GLM/SECTOR/taw/EXTENT3/latest.jpg",

        Droog_Luchtmassa:
            "https://cdn.star.nesdis.noaa.gov/GOES19/ABI/SECTOR/taw/Dust/latest.jpg"
    };

    let currentProduct = "GEOCOLOR";

    function updateTime() {

        const now = new Date();

        const timeEl = document.getElementById("time");

        if (timeEl) {
            timeEl.textContent =
                "Last updated: " + now.toLocaleString();
        }
    }

    function updateGOES() {

        const img = document.getElementById("goesImage");

        if (!img) return;

        const timestamp = Date.now();

        img.src =
            sources[currentProduct] + "?t=" + timestamp;
        updateTime();
    }

    // Make available to HTML buttons
    window.setSat = function (product) {

        currentProduct = product;

        updateGOES();
    };

    // Initial load
    updateGOES();

    // Refresh every 10 minutes
    setInterval(updateGOES, 600000);

});