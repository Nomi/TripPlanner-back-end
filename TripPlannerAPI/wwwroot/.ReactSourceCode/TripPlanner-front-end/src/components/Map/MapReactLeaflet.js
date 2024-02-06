import L from "leaflet";
import "leaflet-routing-machine";
import "leaflet-control-geocoder";
import "lrm-graphhopper";
import { useEffect } from "react";

import styles from "./Map.module.css";

let greenIcon = new L.Icon({
  iconUrl:
    "https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png",
  shadowUrl:
    "https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png",
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  shadowSize: [41, 41],
});

const Map = (props) => {
  let waypointsHandler = props.onWaypointsHandler;
  let userWaypointsInput = props.userWaypointsInput;
  let calculatedTripDataHandler = props.onCalculatedTripDataHandler;
  let isMapStatic = props.staticMap;
  let typeOfTransport = props.typeOfTransport ? props.typeOfTransport : "car";
  if (userWaypointsInput.length === 0) {
    userWaypointsInput = false;
  }

  useEffect(() => {
    var container = L.DomUtil.get("map");
    if (container != null) {
      container._leaflet_id = null;
    }

    let map = L.map("map", { drawControl: true }).setView(
      [52.2297, 21.0122],
      6
    );
    L.tileLayer("https://tile.openstreetmap.org/{z}/{x}/{y}.png", {
      maxZoom: 19,
      attribution:
        '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
    }).addTo(map);

    let userWaypointsInputTransformed = [];
    if (userWaypointsInput) {
      userWaypointsInput.forEach((element) => {
        let waypoint = L.Routing.waypoint(
          L.latLng(element.lat, element.lng),
          element.name
        );
        userWaypointsInputTransformed.push(waypoint);
      });
    }

    L.Routing.control({
      waypoints: userWaypointsInputTransformed,
      routeWhileDragging: true,
      router: L.Routing.graphHopper("44c78c7e-16d3-4fd5-80a9-fa1b12807da7", {
        urlParameters: {
          vehicle: typeOfTransport,
        },
      }),
      geocoder: L.Control.Geocoder.nominatim(),
    })
      .on("routeselected", function (e) {
        if (!userWaypointsInput && !isMapStatic) {
          let route = e.route;
          let userWaypointsReturn = [];
          route.inputWaypoints.forEach((element) => {
            console.log(element.name);
            let waypoint = {
              lat: element.latLng.lat,
              lng: element.latLng.lng,
              name: element.name,
            };
            userWaypointsReturn.push(waypoint);
          });
          if (waypointsHandler) {
            waypointsHandler(userWaypointsReturn);
          }
          let summary = route.summary;
          let hours = Math.floor(summary.totalTime / 3600);
          let minutes = Math.round((summary.totalTime - hours * 3600) / 60);
          let tripData = {
            distance: Math.round(summary.totalDistance / 1000),
            totalTime: summary.totalTime,
            timeHours: hours,
            timeMinutes: minutes,
          };
          calculatedTripDataHandler(tripData);
        }
      })
      .addTo(map);

    map.on("click", function (e) {
      let container = L.DomUtil.create("div");
      let inputField = createInput(container);
      let saveBtn = createButton("Save", container);

      L.popup().setContent(container).setLatLng(e.latlng).openOn(map);

      L.DomEvent.on(saveBtn, "click", function () {
        console.log(e.latlng);
        L.marker(e.latlng, { icon: greenIcon })
          .bindPopup(
            inputField.value.startsWith("http")
              ? `<img src=${inputField.value} alt='Image' height='200' width='200' />`
              : inputField.value
          )
          .addTo(map);
        map.closePopup();
      });
    });

    if (isMapStatic) {
      let waypoints = document.getElementsByClassName(
        "leaflet-routing-geocoder"
      );
      for (let waypoint of waypoints) {
        waypoint.style.pointerEvents = "none";
      }
      let buttonAdd = document.getElementsByClassName(
        "leaflet-routing-add-waypoint"
      );
      for (let button of buttonAdd) {
        button.style.visibility = "hidden";
      }
    }
  }, [
    waypointsHandler,
    userWaypointsInput,
    isMapStatic,
    calculatedTripDataHandler,
    typeOfTransport,
  ]);

  return <div id="map" className={styles.map} />;
};

function createButton(label, container) {
  let btn = L.DomUtil.create("button", "button-save", container);
  btn.setAttribute("type", "button");
  btn.innerHTML = label;
  return btn;
}

function createInput(container) {
  let btn = L.DomUtil.create("input", "", container);
  return btn;
}

export default Map;