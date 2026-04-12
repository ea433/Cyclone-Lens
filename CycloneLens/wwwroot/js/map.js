/* real high-quality sat data 
|| without atlantic + labels

var map = L.map('map', {
            minZoom: 2,
            maxZoom: 7,
        }).setView([20, -60], 4);

        var labels = L.tileLayer(
            'https://services.arcgisonline.com/ArcGIS/rest/services/Reference/World_Boundaries_and_Places/MapServer/tile/{z}/{y}/{x}',
            { opacity: 1 }
        );
        labels.addTo(map);

        var nasa = L.tileLayer(
          'https://gibs.earthdata.nasa.gov/wmts/epsg3857/best/VIIRS_SNPP_CorrectedReflectance_TrueColor/default/GoogleMapsCompatible_Level9/{z}/{y}/{x}.jpg',
          {
            opacity: 0.95,
            attribution: 'NASA VIIRS'
          }
        ).addTo(map);
*/

var map = L.map('map', {
    minZoom: 2,
    maxZoom: 7,
}).setView([20, -60], 4);

var nasa = L.tileLayer(
    'https://gibs.earthdata.nasa.gov/wmts/epsg3857/best/MODIS_Aqua_CorrectedReflectance_TrueColor/default/GoogleMapsCompatible_Level9/{z}/{y}/{x}.jpg',
    {
        maxZoom: 9,
        opacity: 0.8,
        attribution: 'NASA GIBS'
    }
).addTo(map);

var clouds = L.tileLayer(
    'https://tile.openweathermap.org/map/clouds_new/{z}/{x}/{y}.png?appid=28d9aec8fcaefad710f8ad00443b1830',
    { opacity: 1 }
).addTo(map);

var labels = L.tileLayer(
    'https://services.arcgisonline.com/ArcGIS/rest/services/Reference/World_Boundaries_and_Places/MapServer/tile/{z}/{y}/{x}',
    { opacity: 0.7 }
);

satellite.addTo(map);
labels.addTo(map);

async function loadRainLayer() {
    try {
        const res = await fetch("https://api.rainviewer.com/public/weather-maps.json");
        const data = await res.json();

        const frames = data.radar.past;
        const latest = frames[frames.length - 1];

        const rainLayer = L.tileLayer(
            `https://tilecache.rainviewer.com${latest.path}/256/{z}/{x}/{y}/2/1_1.png`,
            {
                opacity: 0.7,
                attribution: 'RainViewer'
            }
        );

        rainLayer.addTo(map);

    } catch (e) {
        console.error("Rain failed:", e);
    }
}

loadRainLayer();

    var trackMaria = [
    {lat: 14, lng: -23, category: 0 },
    {lat: 14.5, lng: -25, category: 0 },
    {lat: 15, lng: -28, category: 0 },
    {lat: 15.5, lng: -31, category: 1 },
    {lat: 16, lng: -35, category: 1 },
    {lat: 16.5, lng: -38, category: 2 },
    {lat: 17, lng: -42, category: 2 },
    {lat: 17.5, lng: -46, category: 3 },
    {lat: 18, lng: -50, category: 3 },
    {lat: 18.5, lng: -54, category: 4 },
    {lat: 19, lng: -58, category: 4 },
    {lat: 19.5, lng: -61, category: 4 },
    {lat: 20, lng: -65, category: 4 },
    {lat: 20.5, lng: -68, category: 3 },
    {lat: 21, lng: -72, category: 3 },
    {lat: 21.5, lng: -75, category: 3 },
    {lat: 22, lng: -78, category: 3 },
    {lat: 21.5, lng: -82, category: 2 },
    {lat: 21, lng: -85, category: 2 },
    {lat: 21.5, lng: -86, category: 2 },
    {lat: 22, lng: -87, category: 2 },
    {lat: 23.5, lng: -85.5, category: 3 },
    {lat: 25, lng: -84, category: 3 },
    {lat: 26.5, lng: -82, category: 2 },
    {lat: 28, lng: -80, category: 2 },
    {lat: 30, lng: -77, category: 1 },
    {lat: 32, lng: -75, category: 1 },
    {lat: 35, lng: -72, category: 0 },
    {lat: 38, lng: -68, category: 0 },
    {lat: 42, lng: -64, category: 0 },
    {lat: 45, lng: -60, category: 0 }
    ];

    var trackAlberto = [
    {lat: 12, lng: -30, category: 0 },
    {lat: 12.5, lng: -33, category: 0 },
    {lat: 13, lng: -36, category: 1 },
    {lat: 13.5, lng: -39, category: 1 },
    {lat: 14, lng: -42, category: 1 },
    {lat: 14.5, lng: -45, category: 2 },
    { lat: 15, lng: -48, category: 2 }, 
    // nog actief
    ];

    var latlngsAlberto = trackAlberto.map(p => [p.lat, p.lng]);
    var latlngsMaria = trackMaria.map(q => [q.lat, q.lng]);

    L.polyline(latlngsAlberto, {
        color: 'red',
    weight: 3
        }).addTo(map);

    L.polyline(latlngsMaria, {
        color: 'red',
    weight: 3
        }).addTo(map);

    function getColor(cat) {
    if (cat == 0) return 'white';
    if (cat == 1) return 'yellow';
    if (cat == 2) return 'orange';
    if (cat == 3) return 'red';
            if (cat >= 4) return 'purple';
        }

        trackMaria.forEach(p => {
        L.circleMarker([p.lat, p.lng], {
            radius: 6,
            color: getColor(p.category),
            fillOpacity: 0.8
        })
            .addTo(map)
            .bindPopup("Category " + p.category);
        });

        trackAlberto.forEach(q => {
        L.circleMarker([q.lat, q.lng], {
            radius: 6,
            color: getColor(q.category),
            fillOpacity: 0.8
        })
            .addTo(map)
            .bindPopup("Category " + q.category);
        });


    
    var observations = [
    // Miami
    {
    lat: 25.8,
    lng: -80.2,
    wind: 140,
    pressure: 950,
    description: "Gebruikersrapport: Ernstige schade, sterke winden"
            },

    // Tampa (FIXED)
    {
    lat: 27.95,
    lng: -82.46,
    wind: 110,
    pressure: 970,
    description: "Gebruikersrapport: Hevige regen en overstromingen"
            },

    // Mexico (Yucatán / Cancun area)
    {
    lat: 21.16,
    lng: -86.85,
    wind: 120,
    pressure: 965,
    description: "Gebruikersrapport: Stormvloed en sterke winden"
            },

    // Jamaica
    {
    lat: 18.1,
    lng: -77.3,
    wind: 100,
    pressure: 980,
    description: "Gebruikersrapport: Overstromingen en aardverschuivingen"
            },

    // Puerto Rico
    {
    lat: 18.4,
    lng: -66.1,
    wind: 130,
    pressure: 955,
    description: "Gebruikersrapport: Stroomverstoringen"
            },

    // Cape Verde (storm origin region)
    {
    lat: 15.1,
    lng: -23.6,
    wind: 60,
    pressure: 1005,
    description: "Gebruikersrapport: Ontwikkelende tropische golf."
            }
    ];

    var obsMarkers = [];

    observations.forEach(function(o) {
            var marker = L.marker([o.lat, o.lng], {
        radius: 10, // initial
    color: 'cyan',
    fillOpacity: 0.7
            })
    .addTo(map)
    .bindPopup(
    "<b>Observatie:</b><br>" +
        "Wind: " + o.wind + " km/h<br>" +
            "Pressure: " + o.pressure + " mb<br>" +
                o.description
                );

                obsMarkers.push(marker);
    });