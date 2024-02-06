import L from "leaflet";
import "leaflet-routing-machine";
import "leaflet-control-geocoder";
import "lrm-graphhopper";
import React, { memo, useState } from "react";
import { MapContainer } from "react-leaflet/MapContainer";
import { TileLayer } from "react-leaflet/TileLayer";
import { useMap } from "react-leaflet/hooks";

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

let routingControl;

const RoutingControlMemo = memo(RoutingControl, routingPropsEqual);

function Map(props) {
  let userWaypointsInput = props.userWaypointsInput;
  let typeOfTransport = props.typeOfTransport ? props.typeOfTransport : "car";
  if (userWaypointsInput.length === 0) {
    userWaypointsInput = false;
  }

  console.log("map-render");

  return (
    <MapContainer
      className={styles.map}
      center={[52.2297, 21.0122]}
      zoom={6}
      scrollWheelZoom={false}
    >
      <TileLayer
        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />
      <RoutingControlMemo
        onWaypointsHandler={props.onWaypointsHandler}
        userWaypointsInput={props.userWaypointsInput}
        onCalculatedTripDataHandler={props.onCalculatedTripDataHandler}
        typeOfTransport={typeOfTransport}
        isMapStatic={props.staticMap}
      />
      <PinsControl
        isMapStatic={props.staticMap}
        userPinsInput={props.userPinsInput}
        onPinsHandler={props.onPinsHandler}
        isCreatedByCurrentUser={props.isCreatedByCurrentUser}
        isJoinedByCurrentUser={props.isJoinedByCurrentUser}
        isTripCreated={props.isTripCreated}
      ></PinsControl>
    </MapContainer>
  );
}

function RoutingControl(props) {
  const map = useMap();
  console.log("routing-render");
  if (routingControl) {
    routingControl.remove();
    routingControl = undefined;
  }

  let userWaypointsInputTransformed = [];
  if (props.userWaypointsInput) {
    props.userWaypointsInput.forEach((element) => {
      let waypoint = L.Routing.waypoint(
        L.latLng(element.lat, element.lng),
        element.name
      );
      userWaypointsInputTransformed.push(waypoint);
    });
  }

  routingControl = L.Routing.control({
    waypoints: userWaypointsInputTransformed,
    routeWhileDragging: true,
    router: L.Routing.graphHopper("44c78c7e-16d3-4fd5-80a9-fa1b12807da7", {
      urlParameters: {
        vehicle: props.typeOfTransport,
      },
    }),
    geocoder: L.Control.Geocoder.nominatim(),
  }).on("routeselected", function (e) {
    if (!props.isMapStatic) {
      let route = e.route;
      let userWaypointsReturn = [];
      route.inputWaypoints.forEach((element) => {
        let waypoint = {
          lat: element.latLng.lat,
          lng: element.latLng.lng,
          name: element.name,
        };
        userWaypointsReturn.push(waypoint);
      });
      let summary = route.summary;
      let hours = Math.floor(summary.totalTime / 3600);
      let minutes = Math.round((summary.totalTime - hours * 3600) / 60);
      let tripData = {
        distance: Math.round(summary.totalDistance / 1000),
        totalTime: summary.totalTime,
        timeHours: hours,
        timeMinutes: minutes,
      };
      props.onWaypointsHandler(userWaypointsReturn);
      props.onCalculatedTripDataHandler(tripData);
    }
  });
  routingControl.addTo(map);

  if (props.isMapStatic) {
    let waypoints = document.getElementsByClassName("leaflet-routing-geocoder");
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
}

function PinsControl(props) {
  const [pins, setPins] = useState([]);
  const map = useMap();
  console.log(pins);

  if (props.isMapStatic) {
    for (const pin of props.userPinsInput) {
      L.marker([pin.lat, pin.lng], { icon: greenIcon })
        .bindPopup(
          pin.name.startsWith("http")
            ? `<img src=${pin.name} alt='Image' height='200' width='200' />`
            : pin.name
        )
        .addTo(map);
    }
  }

  if (props.isCreatedByCurrentUser || props.isJoinedByCurrentUser || props.isTripCreated) {
    map.on("click", function (e) {
      let container = L.DomUtil.create("div");
      let inputField = createInput(container);
      let saveBtn = createButton("Save", container);

      L.popup().setContent(container).setLatLng(e.latlng).openOn(map);

      L.DomEvent.on(saveBtn, "click", function () {
        L.marker(e.latlng, { icon: greenIcon })
          .bindPopup(
            inputField.value.startsWith("http")
              ? `<img src=${inputField.value} alt='Image' height='200' width='200' />`
              : inputField.value
          )
          .addTo(map);
        map.closePopup();
        if (!props.isMapStatic) {
          props.onPinsHandler([
            ...pins,
            {
              lat: e.latlng.lat,
              lng: e.latlng.lng,
              name: inputField.value,
            },
          ]);
          setPins([
            ...pins,
            {
              lat: e.latlng.lat,
              lng: e.latlng.lng,
              name: inputField.value,
            },
          ]);
        }
        if (props.isMapStatic) {
          props.onPinsHandler({
            lat: e.latlng.lat,
            lng: e.latlng.lng,
            name: inputField.value,
          });
        }
      });
    });
  }
}

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

function mapPropsEqual(prevProps, nextProps) {
  console.log("comparison");
  return prevProps.typeOfTransport === nextProps.typeOfTransport;
}

function routingPropsEqual(prevProps, nextProps) {
  console.log("comparison-routing");
  return prevProps.typeOfTransport === nextProps.typeOfTransport;
}

export const MapMemo = memo(Map, mapPropsEqual);
export default MapMemo;
