var map = L.map('map', {
    minZoom: 4,
    maxZoom: 7,
    zoomControl: false
}).setView([20, -60], 4);

var goes = L.TileLayer.extend({
    getTileUrl: function (coords) {
        const tileBounds = this._tileCoordsToBounds(coords);
        const sw = L.CRS.EPSG3857.project(tileBounds.getSouthWest());
        const ne = L.CRS.EPSG3857.project(tileBounds.getNorthEast());

        const params = new URLSearchParams({
            minx: Math.min(sw.x, ne.x).toFixed(2),
            miny: Math.min(sw.y, ne.y).toFixed(2),
            maxx: Math.max(sw.x, ne.x).toFixed(2),
            maxy: Math.max(sw.y, ne.y).toFixed(2),
            width: 256,
            height: 256,
            layers: 'global_longwave_imagery_mosaic'
        });

        return `${window.location.origin}/api/goesproxy/tile?${params}`;
    }
});

var goesLayer = new goes(null, {
    opacity: 0.85,
    attribution: 'NOAA nowCOAST',
    minZoom: 2,
    maxZoom: 7,
    bounds: [[-72, -180], [72, 180]]
}).addTo(map);

var labels = L.tileLayer(
    'https://services.arcgisonline.com/ArcGIS/rest/services/Reference/World_Boundaries_and_Places/MapServer/tile/{z}/{y}/{x}',
    { opacity: 1, pane: 'overlayPane' }
).addTo(map);

let rainLayer = null;
async function loadRainLayer() {
    try {
        const res = await fetch("https://api.rainviewer.com/public/weather-maps.json");
        const data = await res.json();
        const frames = data.radar.past || data.radar.nowcast;
        if (!frames || frames.length === 0) return;
        const latest = frames[frames.length - 1];
        rainLayer = L.tileLayer(
            `https://tilecache.rainviewer.com${latest.path}/256/{z}/{x}/{y}/2/1_1.png`,
            { opacity: 0.7, attribution: 'RainViewer' }
        );
        rainLayer.addTo(map);
        labels.bringToFront();
    } catch (err) {
        console.error("Rain failed:", err);
    }
}
loadRainLayer();

setInterval(() => goesLayer.redraw(), 5 * 60 * 1000);

document.getElementById("toggleLabels").addEventListener("change", (e) => {
    if (e.target.checked) map.addLayer(labels);
    else map.removeLayer(labels);
});

document.getElementById("toggleRadar").addEventListener("change", (e) => {
    if (!rainLayer) return;
    if (e.target.checked) map.addLayer(rainLayer);
    else map.removeLayer(rainLayer);
});

// Observaties van observaties
obsMarkers = []
fetch('/Observatie/GetObservaties')
    .then(response => response.json())
    .then(observations => {
        observations.forEach(function (o) {
            var marker = L.marker([o.lat, o.lng])
                .addTo(map)
                .bindPopup(
                    "<b>Observatie:</b><br>" +
                    o.description +
                    "<br><small>" + o.tijdstip + "</small>"
                );
            obsMarkers.push(marker);
        });
        labels.bringToFront(); 
    })
    .catch(err => console.error(err));

// Storm colors
function stormColor(windKts) {
    const w = parseInt(windKts);
    if (w < 34) return '#5eead4'; // Tropische Depressie
    if (w < 64) return '#facc15'; // Tropische Storm
    if (w < 83) return '#fb923c'; // Categorie 1
    if (w < 96) return '#f87171'; // Categorie 2
    if (w < 113) return '#c084fc'; // Categorie 3
    if (w < 137) return '#e879f9'; // Categorie 4
    return '#ff0000'; // Categorie 5 
}

function stormLabel(windKts) {
    const w = parseInt(windKts);
    if (w < 34) return 'TD'; // Tropische Depressie
    if (w < 64) return 'TS'; // Tropische Storm
    if (w < 83) return 'Cat 1'; // Categorie 1
    if (w < 96) return 'Cat 2'; // Categorie 2
    if (w < 113) return 'Cat 3'; // Categorie 3
    if (w < 137) return 'Cat 4'; // Categorie 4
    return 'Cat 5'; // Categorie 5 
}

function parseKmlCoords(coordString) {
    return coordString.trim().split(/\s+/).map(c => {
        const parts = c.split(',');
        return [parseFloat(parts[1]), parseFloat(parts[0])];
    });
}

var stormLayers = [];

function clearStormLayers() {
    stormLayers.forEach(l => map.removeLayer(l));
    stormLayers = [];
}

async function loadKmzLayer(kmzUrl, type) {
    try {
        const res = await fetch(`/api/tropical/kmz?url=${encodeURIComponent(kmzUrl)}`);
        if (!res.ok) return;
        const kmlText = await res.text();

        const parser = new DOMParser();
        const kml = parser.parseFromString(kmlText, 'application/xml');
        const placemarks = kml.querySelectorAll('Placemark');

        const trackCoords = [];

        placemarks.forEach(pm => {
            // Track line
            const line = pm.querySelector('LineString coordinates');
            if (line && type === 'track') {
                const coords = parseKmlCoords(line.textContent);
                // Only draw the 120hr line (longest one)
                const fcstpd = pm.querySelector('Data[name="fcstpd"] value');
                if (!fcstpd || fcstpd.textContent === '120') {
                    const layer = L.polyline(coords, {
                        color: '#ffffff',
                        weight: 2,
                        dashArray: '6 3',
                        opacity: 0.9
                    }).addTo(map);
                    stormLayers.push(layer);
                }
            }

            // Cone polygon
            const polygon = pm.querySelector('Polygon outerBoundaryIs LinearRing coordinates');
            if (polygon && type === 'cone') {
                const layer = L.polygon(parseKmlCoords(polygon.textContent), {
                    color: '#ffffff',
                    fillColor: '#94a3b8',
                    fillOpacity: 0.2,
                    weight: 1,
                    dashArray: '4 4'
                }).addTo(map);
                stormLayers.push(layer);
            }

            // Forecast points
            const point = pm.querySelector('Point coordinates');
            if (point && type === 'track') {
                const parts = point.textContent.trim().split(',');
                const lon = parseFloat(parts[0]);
                const lat = parseFloat(parts[1]);
                if (isNaN(lat) || isNaN(lon)) return;

                // Extract wind from description HTML
                const rawDesc = pm.querySelector('description')?.textContent || '';
                const windMatch = rawDesc.match(/Maximum Wind:\s*(\d+)\s*knots/);
                const wind = windMatch ? parseInt(windMatch[1]) : 0;
                const color = stormColor(wind);

                const styleUrl = pm.querySelector('styleUrl')?.textContent || '';
                const isInitial = styleUrl.includes('initial');

                const parser2 = new DOMParser();
                const descDoc = parser2.parseFromString(rawDesc, 'text/html');
                const rows = descDoc.querySelectorAll('td');
                const lines = Array.from(rows)
                    .map(td => td.textContent.trim())
                    .filter(t => t.length > 0 && t !== '—' && t !== '-' && t !== '');

                const tooltipHtml = `<div style="line-height:1.6">${lines.join('<br>')}</div>`;

                const layer = L.circleMarker([lat, lon], {
                    radius: isInitial ? 9 : 6,
                    fillColor: color,
                    color: '#ffffff',
                    weight: isInitial ? 2.5 : 1.5,
                    fillOpacity: 0.9
                }).bindTooltip(tooltipHtml, {
                    className: 'storm-tooltip',
                    sticky: true,
                    opacity: 0.95,
                    direction: 'top',
                    offset: [0, -10]
                }).addTo(map);
                stormLayers.push(layer);
            }
        });
        labels.bringToFront();
    } catch (err) {
        console.error(`KMZ load failed (${type}):`, err);
    }
}

async function loadActiveStorms() {
    try {
        clearStormLayers();

        const res = await fetch('/api/tropical/nhc-active');
        const data = await res.json();

        if (!data.activeStorms || data.activeStorms.length === 0) return;

        data.activeStorms.forEach(storm => {
            const color = stormColor(storm.intensity);
            const label = stormLabel(storm.intensity);

            const marker = L.circleMarker(
                [storm.latitudeNumeric, storm.longitudeNumeric],
                {
                    radius: 10,
                    fillColor: color,
                    color: '#ffffff',
                    weight: 2,
                    fillOpacity: 1
                }
            ).bindTooltip(
                `<b>${storm.name}</b><br>
                 ${label} — ${storm.intensity} kt<br>
                 Pressure: ${storm.pressure} mb<br>
                 Updated: ${new Date(storm.lastUpdate).toUTCString()}`,
                { className: 'storm-tooltip', permanent: false }
            ).addTo(map);
            stormLayers.push(marker);

            if (storm.forecastTrack?.kmzFile)
                loadKmzLayer(storm.forecastTrack.kmzFile, 'track');

            if (storm.trackCone?.kmzFile)
                loadKmzLayer(storm.trackCone.kmzFile, 'cone');
        });

    } catch (err) {
        console.error('Active storms failed:', err);
    }
}

loadActiveStorms();
setInterval(loadActiveStorms, 30 * 60 * 1000);